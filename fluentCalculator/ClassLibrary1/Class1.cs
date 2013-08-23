using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace fluentCalculator
{
    [TestFixture]
    public class Test
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

    public class Calculator
    {
        private Stack<Func<int, int>> _actions; 
        private Stack<Func<int, int>> _undone; 

        public Calculator Calc(int n)
        {
            Init(n);
            return this;
        }

        private void Init(int n)
        {
            ResetActionList();
            Plus(n);
            ResetUndoList();
        }

        public Calculator Plus(int n)
        {
            _actions.Push((a) => a + n);
            return this;
        }

        public Calculator Minus(int n)
        {
            _actions.Push((a) => a - n);
            return this;
        }

        public Calculator Undo()
        {
            if (_actions.Count > 1) // cannot undo first init action!
            {
                _undone.Push(_actions.Pop());

            }
            return this;
        }

        public Calculator Redo()
        {
            if (_undone.Any())
            {
                _actions.Push(_undone.Pop());
            }
            return this;
        }

        public Calculator Save()
        {
            Init(Result());
            return this;
        }

        private int Result()
        {
            return _actions.Aggregate(0, (current, f) => f(current));
        }

        public static explicit operator int(Calculator c)
        {
            return c.Result();
        }

        private void ResetActionList()
        {
            _actions = new Stack<Func<int, int>>();
        }

        private void ResetUndoList()
        {
            _undone = new Stack<Func<int, int>>();
        }
    }
}
