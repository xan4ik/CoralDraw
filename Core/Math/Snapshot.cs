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

        public static bool SnapshotContainsPoint(Snapshot snapshot, Point point) 
        {
            return BetweenXRange(point.X, snapshot) &&
                   BetweenYRange(point.Y, snapshot);
        }

        private static bool BetweenXRange(float x, Snapshot snapshot)
        {
            return x >= snapshot.Location.X && x <= snapshot.Location.X + snapshot.Size.Width;
        }

        private static bool BetweenYRange(float y, Snapshot snapshot)
        {
            return y >= snapshot.Location.Y && y <= snapshot.Location.Y + snapshot.Size.Height;
        }

        public static SnapshotOffset OffsetFromTo(Snapshot from,Snapshot to) 
        {
            return new SnapshotOffset()
            {
                LocationOffset = Point.OffsetFromTo(from.Location, to.Location),
                SizeOffset = Size.OffsetFromTo(from.Size, to.Size)
            };
        }
    }

    public struct SnapshotOffset 
    {
        public Point LocationOffset;
        public Point SizeOffset;
    }
}