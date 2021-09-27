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
                throw new ArgumentLessThanZero($"Width can't be less zero. value:{width}", width);
            if (height < 0)
                throw new ArgumentLessThanZero($"Height can't be less zero. value:{height}", height);

            this.width = width;
            this.height = height;
        }

        public float Width 
        {
            get => width;
            set 
            {
                if (value >= 0)
                {
                    width = value;
                }
                else throw new ArgumentLessThanZero($"Width can't be less zero. value:{value}",value);
            }
        }

        public float Height
        {
            get => height;
            set
            {
                if (value >= 0)
                {
                    height = value;
                }
                else throw new ArgumentLessThanZero($"Height can't be less zero. value:{value}", value);
            }
        }

        public static Point OffsetFromTo(Size from, Size to) 
        {
            return new Point()
            {
                X = to.Width - from.width,
                Y = to.Height - from.Height
            };
        }
    }
}
