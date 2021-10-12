using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLib
{
    public class Faker
    {
        public T Create<T>()
        {
            return (T)new object();
        }
    }
}
