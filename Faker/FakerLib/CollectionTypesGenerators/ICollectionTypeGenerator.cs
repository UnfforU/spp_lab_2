using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.CollectionTypesGenerators
{
    public interface ICollectionTypeGenerator
    {
        Type[] CollectionTypes { get; }

        object Generate<T>(Faker faker);
    }
}
