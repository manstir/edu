using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fluentCalcTest
{
    using System.Linq.Expressions;

    using NUnit.Framework;

    using fluentCalculator;

    [TestFixture]
    public class CalcTest
    {
        [Test]
        public void ExpT()
        {
            Expression firstArg = Expression.Constant(2);
            Expression secondArg = Expression.Constant(3);
            Expression add = Expression.Add(firstArg, secondArg);
            Func<int> compiled = Expression.Lambda<Func<int>>(add).Compile();
            Console.WriteLine(compiled());
            Console.WriteLine(add);

            Expression<Func<int>> return5 = () => 5;
            var cp = return5.Compile();
            Console.WriteLine(cp());

            Expression<Func<string, string, bool>> expression = (s, s1) => s.StartsWith(s1);
            var function = expression.Compile();
            Console.WriteLine(function("foo", "mee"));
            Console.WriteLine(function("foo mee", "foo"));
        }

        [Test]
        public void Sample1()
        {
            Assert.AreEqual(20, (int)new Calculator()
                .Calc(10)
                .Plus(5)
                .Minus(2)
                .Undo()
                .Redo()
                .Undo()
                .Plus(5));
        }

        [Test]
        public void Sample2()
        {
            Assert.AreEqual(13, (int)new Calculator()
                .Calc(10)
                .Plus(5)
                .Minus(2)
                .Undo()
                .Undo()
                .Redo()
                .Redo()
                .Redo());
        }

        [Test]
        public void Sample3()
        {
            Assert.AreEqual(18, (int)new Calculator()
                .Calc(10)
                .Plus(5)
                .Minus(2)
                .Save()
                .Undo()
                .Redo()
                .Undo()
                .Plus(5));
        }
    }
}
