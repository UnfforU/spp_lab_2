using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.CollectionTypesGenerators
{
    class CollectionGenerator : ICollectionTypeGenerator
    {
        public Type[] CollectionTypes => new Type[] { typeof(List<>), typeof(IEnumerable<>), typeof(IList<>), typeof(ICollection<>) };

        public object Generate<T>(Faker faker)
        {
            List<T> result = new List<T>();
            int count = new Random().Next(1, 10);
            for(int i = 0; i < count; i++)
            {
                result.Add(faker.Create<T>());
            }

            return result;
        }
    }
}
