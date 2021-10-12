using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.BasicTypesGenerators
{
    class BoolGenerator : IBasicTypeGenerator
    {
        public Type ResType => typeof(bool);

        public object Generate()
        {
            return (new Random().Next(0, 2) == 1) ? true : false;
        }
    }
}
