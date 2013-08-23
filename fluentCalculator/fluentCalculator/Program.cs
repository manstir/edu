using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace fluentCalculator
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void test()
        {
                Assert.AreEqual(10, (int)new Calculator().Calc(10));
        }
    }

    public class Calculator
    {
        private Calculator _instance;
        private int _startInt;

        public Calculator Calc(int n)
        {
            _startInt = n;
            return this;
        }

        // change explicit to implicit depending on what you need
        public static explicit operator int(Calculator c)
        {
            return c._startInt;
        }
    }


}
