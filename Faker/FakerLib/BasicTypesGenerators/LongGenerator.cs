using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.BasicTypesGenerators
{
    class LongGenerator : IBasicTypeGenerator
    {
        public Type ResType => typeof(long);

        public object Generate()
        {
            var rnd = new Random();
            long result = rnd.Next() * rnd.Next();
            return result;
        }
    }
}
