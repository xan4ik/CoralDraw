using System;

namespace Core
{
    public class ArgumentLessThanZero : Exception 
    {
        public ArgumentLessThanZero(string message, float value) : base(message)
        {
            ExceptionValue = value;
        }

        public readonly float ExceptionValue;
    }
}
