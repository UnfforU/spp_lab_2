using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakerLib;
using System;
using System.Collections.Generic;

namespace FakerTests
{
    [TestClass]
    public class FakerTests
    {
        private Faker faker = new Faker();

        [TestMethod]
        public void TestNotSupportedValueType()
        {
            char testValue = faker.Create<char>();
            Assert.AreEqual(testValue, default(char));
        }

        [TestMethod]
        public void TestBasicValueType()
        {
            var testValue = faker.Create<int>();
            Assert.IsInstanceOfType(testValue, typeof(int));
        }

        [TestMethod]
        public void TestCollectionType()
        {
            var testValue = faker.Create<List<string>>();
            Assert.IsInstanceOfType(testValue, typeof(List<string>));
        }

        [TestMethod]
        public void TestClassType()
        {
            var testValue = faker.Create<Class1>();
            Assert.IsInstanceOfType(testValue, typeof(Class1));
        }

        [TestMethod]
        public void TestCycleDependeñe()
        {
            var testValue = faker.Create<A>();
            Assert.IsNull(testValue.b.c.a);
        }

       


        public class Class1
        {
            public int numb;
            public string str;
            public DateTime dateTime;

            public Class1(int _numb, string _str, DateTime _dateTime)
            {
                numb = _numb;
                str = _str;
                dateTime = new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, _dateTime.Hour, _dateTime.Minute, _dateTime.Second, _dateTime.Millisecond);

            }
        }

        public class A
        {
            public B b;
        }

        public class B
        {
            public C c;
        }
        public class C
        {
            public A a;
        }

    }
}
