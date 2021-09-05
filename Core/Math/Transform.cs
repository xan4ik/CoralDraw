namespace Core
{
    public struct Transform 
    {    
        public Point Location;
        public Size Size;

        public Transform(Point location, Size size)
        {
            Location = location;
            Size = size;
        }

        public void Relocate(float deltaX, float deltaY) 
        {
            Location.X += deltaX;
            Location.Y += deltaY;
        }

        public void Resize(float deltaWigth, float deltaHeight) 
        {
            Size.Width += deltaWigth;
            Size.Height += deltaHeight;
        }
    }



}
