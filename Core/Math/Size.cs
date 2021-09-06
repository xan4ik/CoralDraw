using System;

namespace Core
{
    //TODO : value check
    public struct Size
    {
        private const string WidthString = "Width";

        private float width;
        private float height;

        public Size(float width, float height)
        {
            LessThenZeroCheck()
            Width = width;
            Height = height;
        }


        public float Width 
        {
            get => width;
            set 
            {
                if (value < 0)
                {
                    width = value;
                }
                else throw new ArgumentLessThanZero("Height can't be less zero",value);
            }
        }

        public float Height
        {
            get => height;
            set
            {
                
            }
        }

        private void LessThenZeroCheck(string dimension, float value) 
        {
            if (value < 0)
                throw new ArgumentLessThanZero(dimension + "can't be less then zero", value);
        }

    }


    public class ArgumentLessThanZero : Exception 
    {
        public ArgumentLessThanZero(string message, float value) : base(message)
        {
            ExceptionValue = value;
        }

        public readonly float ExceptionValue;
    }
}
