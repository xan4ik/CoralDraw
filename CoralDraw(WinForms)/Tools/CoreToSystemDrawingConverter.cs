using System.Drawing;

namespace CoralDraw_WinForms
{
    class CoreToSystemDrawingConverter 
    {
        public System.Drawing.Color ConvertFrom(Core.Color color)
        {
            return System.Drawing.Color.FromArgb(color.Red, color.Green, color.Blue);
        }

        public PointF ConvertFrom(Core.Point point)
        {
            return new PointF(point.X, point.Y);
        }

        public SizeF ConvertFrom(Core.Size size)
        {
            return new SizeF(size.Width, size.Height);
        }

    }
}