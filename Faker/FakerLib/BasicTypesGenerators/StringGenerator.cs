using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib.BasicTypesGenerators
{
    class StringGenerator : IBasicTypeGenerator
    {
        public Type ResType => typeof(string);

        public object Generate()
        {
            var random = new Random(DateTime.Now.Millisecond);
            byte[] randomBytes = new byte[random.Next(30) + 1];
            random.NextBytes(randomBytes);
            return Convert.ToBase64String(randomBytes).Replace("=", "a");
        }
    }
}
