using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.BasicTypesGenerators
{
    class DoubleGenerator : IBasicTypeGenerator
    {
        public Type ResType => typeof(double);

        public object Generate()
        {
            var rnd = new Random();
            double result = rnd.NextDouble() * rnd.Next();
            return result;
        }
    }
}
