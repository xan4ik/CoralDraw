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
    }
}