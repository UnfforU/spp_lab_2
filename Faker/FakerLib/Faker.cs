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
            return (T)new object();
        }
    }
}
