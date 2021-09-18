namespace CoralDraw_WinForms
{
    class SystemDrawingToCoreConverter
    {
        public Core.Color ConvertFrom(System.Drawing.Color color) 
        {
            return new Core.Color()
            {
                Red = color.R,
                Green = color.G,
                Blue = color.B
            };
        }

        public Core.Point ConvertFrom(System.Drawing.PointF point) 
        {
            return new Core.Point(point.X, point.Y);
        }

        public Core.Size ConvertFrom(System.Drawing.SizeF size) 
        {
            return new Core.Size(size.Width, size.Height);
        }
    }
}