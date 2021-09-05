namespace Core
{
    public class Transform 
    {    
        private Point location;
        private Size size;

        public Transform(Point location, Size size)
        {
            this.location = location;
            this.size = size;
        }

        public Snapshot CreateSnapshot() 
        {
            return new Snapshot
            {
                Size = size,
                Location = location
            };
        }

        public void Relocate(float deltaX, float deltaY) 
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
