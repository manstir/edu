namespace fluentCalculator
{
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
