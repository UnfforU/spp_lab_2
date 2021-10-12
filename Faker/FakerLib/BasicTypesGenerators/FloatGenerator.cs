using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.BasicTypesGenerators 
{
    class FloatGenerator : IBasicTypeGenerator
    {
        public Type ResType => typeof(float);

        public object Generate()
        {
            var rnd = new Random();
            float result = (float)(rnd.NextDouble() * rnd.Next());
            return result;
        }
    }
}
