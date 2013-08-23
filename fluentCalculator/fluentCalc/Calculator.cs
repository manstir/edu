namespace fluentCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
