using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FakerLib.BasicTypesGenerators;
using FakerLib.CollectionTypesGenerators;

namespace FakerLib
{
    public class Faker
    {

        private Dictionary<Type, IBasicTypeGenerator> BasicGenerators = new Dictionary<Type, IBasicTypeGenerator>();
        private Dictionary<Type, ICollectionTypeGenerator> CollectionGenerators = new Dictionary<Type, ICollectionTypeGenerator>();

        private Stack<Type> nestedTypes = new Stack<Type>();


        public Faker()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type[] types = assembly.GetTypes();

                Type basicInterface = typeof(IBasicTypeGenerator),
                     collectionInterface = typeof(ICollectionTypeGenerator);

                for(int i = 0; i < types.Length; i++)
                {
                    if(basicInterface.IsAssignableFrom(types[i]) && basicInterface != types[i])
                    {
                        IBasicTypeGenerator gen = (IBasicTypeGenerator)Activator.CreateInstance(types[i]);
                        BasicGenerators.Add(gen.ResType, gen);
                        continue;
                    }

                    if(collectionInterface.IsAssignableFrom(types[i]) && collectionInterface != types[i])
                    {
                        ICollectionTypeGenerator gen = (ICollectionTypeGenerator)Activator.CreateInstance(types[i]);
                        foreach(Type type in gen.CollectionTypes)
                        {
                            CollectionGenerators.Add(type, gen);
                        }
                        continue;
                    }
                }
            }
            catch
            {
                throw new Exception("Can't create Facker");
            }
        }

        public T Create<T>()
        {

            Type currType = typeof(T);
            nestedTypes.Push(currType);

            object nestedRes = GetGenerateValueIfExict(currType);
            if (nestedRes != null)
            {
                nestedTypes.Pop();
                return (T)nestedRes;
            }

            T result = default(T);
            try
            {
                result = Activator.CreateInstance<T>();
            }
            catch(MissingMethodException)
            {
                var typeConstructors = currType.GetConstructors();
                if(typeConstructors.Length == 0)
                {
                    nestedTypes.Pop();
                    return result;
                }
                foreach(ConstructorInfo constructor in typeConstructors)
                {
                    List<object> values = new List<object>();
                    foreach(ParameterInfo param in constructor.GetParameters())
                    {
                        values.Add(GetGenerateValue(param.ParameterType));
                    }

                    result = (T)constructor.Invoke(values.ToArray());
                }
            }
            catch(Exception ex)
            {
                nestedTypes.Pop();
                throw ex;
            }

            if (currType.IsValueType)
            {
                nestedTypes.Pop();
                return result;
            }

            foreach (PropertyInfo property in currType.GetProperties())
            {
                if (property.CanWrite) { property.SetValue(result, GetGenerateValue(property.PropertyType)); }
            }

            foreach (FieldInfo f in currType.GetFields())
            {
                f.SetValue(result, GetGenerateValue(f.FieldType));
            }

            nestedTypes.Pop();
            return result;
        }

        private object GetGenerateValue(Type desType)
        {
            object value = default(object);
            if (BasicGenerators.ContainsKey(desType))
            {
                value = BasicGenerators[desType].Generate();
            }
            else if (!nestedTypes.Contains(desType))
            {
                value = this.GetType().GetMethod("Create").MakeGenericMethod(desType).Invoke(this, null);
            }

            return value;
        }


        private object GetGenerateValueIfExict(Type desType)
        {
            foreach(Type t in BasicGenerators.Keys)
            {
                if (BasicGenerators[t].ResType == desType) {
                    return BasicGenerators[t].Generate();
                }
            }

            if (!desType.IsGenericType)
                return null;

            foreach (Type t in CollectionGenerators.Keys)
            {
                if (CollectionGenerators[t].CollectionTypes.Contains(desType.GetGenericTypeDefinition()))
                {
                    var type = CollectionGenerators[t].GetType();
                    var method = type.GetMethod("Generate");
                    var memberType = desType.GenericTypeArguments.First();
                    var res = (object)method.MakeGenericMethod(memberType).Invoke(CollectionGenerators[t], new object[] { this });
                    return res;
                }
            }
            return null;
        }
    }
}
