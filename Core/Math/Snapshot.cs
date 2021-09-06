namespace Core
{
    public struct Snapshot
    {
        public Point Location;
        public Size Size;

        public Snapshot(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        public bool ContainsPoint(Point point) 
        {
            return BetweenXRange(point.X) &&
                   BetweenYRange(point.Y);
        }

        private bool BetweenXRange(float x) 
        {
            return x >= Location.X && x <= Location.X + Size.Width;
        }

        private bool BetweenYRange(float y) 
        {
            return y >= Location.Y && y <= Location.Y + Size.Height;
        }
    }
}