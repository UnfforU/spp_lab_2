using System;

namespace FakerLib.BasicTypesGenerators
{
    class IntGenerator : IBasicTypeGenerator
    {
        public Type ResType => typeof(int);
        
        public object Generate()
        {
            return new Random().Next();
        }
    }
}
