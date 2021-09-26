namespace Core
{
    public class Transform 
    {    
        private Point location;
        private Size size;

        public Transform(Snapshot snapshot)
        {
            this.location = snapshot.Location;
            this.size = snapshot.Size;
        }

        public Snapshot CreateSnapshot() 
        {
            return new Snapshot(location, size);
        }

        public void Move(float deltaX, float deltaY) 
        {
            location.X += deltaX;
            location.Y += deltaY;
        }

        public void Resize(float deltaWigth, float deltaHeight) 
        {
            size.Width += deltaWigth;
            size.Height += deltaHeight;
        }
    }
}
