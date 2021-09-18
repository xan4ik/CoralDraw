using System.Drawing;
using Core;

namespace CoralDraw_WinForms
{
    class GraphicsAdapter : IDrawerAdapter
    {
        private CoreToSystemDrawingConverter converter;
        private Graphics graphics;
        private SolidBrush brush;
        private Pen pen;

        public GraphicsAdapter(Graphics graphics)
        {
            this.converter = new CoreToSystemDrawingConverter();
            this.brush = new SolidBrush(System.Drawing.Color.Black);
            this.pen = new Pen(System.Drawing.Color.Black);
            this.graphics = graphics;
        }

        public void DrawBoundEllipse(Core.Point location, Core.Size size)
        {
            var convertedPoint = converter.ConvertFrom(location);
            var convertedSize = converter.ConvertFrom(size);
            graphics.DrawEllipse(pen, new RectangleF(convertedPoint, convertedSize));
        }

        public void DrawBoundRectngle(Core.Point location, Core.Size size)
        {
            var convertedPoint = converter.ConvertFrom(location);
            var convertedSize = converter.ConvertFrom(size);
            graphics.DrawRectangle(pen, convertedPoint.X, convertedPoint.Y, convertedSize.Width, convertedSize.Height);
        }

        public void DrawSolidEllipse(Core.Point location, Core.Size size)
        {
            var convertedPoint = converter.ConvertFrom(location);
            var convertedSize = converter.ConvertFrom(size);
            graphics.FillEllipse(brush, new RectangleF(convertedPoint, convertedSize));
        }

        public void DrawSolidRectngle(Core.Point location, Core.Size size)
        {
            var convertedPoint = converter.ConvertFrom(location);
            var convertedSize = converter.ConvertFrom(size);
            graphics.FillRectangle(brush, convertedPoint.X, convertedPoint.Y, convertedSize.Width, convertedSize.Height);
        }

        public void SetColor(Core.Color color)
        {
            var convertedColor = converter.ConvertFrom(color);
            brush.Color = convertedColor;
            pen.Color = convertedColor;
        }
    }
}