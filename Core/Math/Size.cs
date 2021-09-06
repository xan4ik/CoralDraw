namespace Core
{
    public struct Size
    {
        private const string WidthString = "Width";
        private const string HeightString = "Height";

        private float width;
        private float height;

        public Size(float width, float height)
        {
            if (width < 0)
                throw new ArgumentLessThanZero("Width can't be less zero", width);
            if (height < 0)
                throw new ArgumentLessThanZero("Height can't be less zero", height);

            this.width = width;
            this.height = height;
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
                else throw new ArgumentLessThanZero("Width can't be less zero",value);
            }
        }

        public float Height
        {
            get => height;
            set
            {
                if (value < 0)
                {
                    height = value;
                }
                else throw new ArgumentLessThanZero("Height can't be less zero", value);
            }
        }
    }
}
