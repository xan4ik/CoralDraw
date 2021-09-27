namespace Core
{
    public struct Point
    {
        public float X;
        public float Y;

        public Point(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Point OffsetFromTo(Point from, Point to) 
        {
            return new Point()
            {
                X = to.X - from.X,
                Y = to.Y - from.Y
            };
        }
    }
}
