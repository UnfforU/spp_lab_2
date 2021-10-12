using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.BasicTypesGenerators
{
    class ObjectGenerator : IBasicTypeGenerator
    {
        public Type ResType => typeof(object);

        public object Generate()
        {
            return new object();
        }
    }
}
