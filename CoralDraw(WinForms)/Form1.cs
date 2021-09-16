using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ApiShell;
using Core;

namespace CoralDraw_WinForms
{
    public partial class Form1 : Form
    {
        private SystemDrawingToCoreConverter converter;
        private GraphicsAdapter adapter;
        private Redactor redactor;

        public Form1()
        {
            InitializeComponent();
            InitContent();
        }

        private void InitContent()
        {
            adapter = new GraphicsAdapter(this.CreateGraphics());
            converter = new SystemDrawingToCoreConverter();
            redactor = new Redactor();
            AllocConsole();
        }

        private void OnMouseDown(object sender, MouseEventArgs e) 
        {
            InvokeMethod(redactor.MouseDown, converter.ConvertFrom(e.Location));
        }

        private void OnMouseMove(object sender, MouseEventArgs e) 
        {
            InvokeMethod(redactor.MouseMove, converter.ConvertFrom(e.Location));
            OnRefesh();
        }

        private void OnMouseUp(object sender, MouseEventArgs e) 
        {
            InvokeMethod(redactor.MouseUp, converter.ConvertFrom(e.Location));
        }

        private void OnKeyUp(object sender, KeyEventArgs e) 
        {
            InvokeMethod(redactor.KeyUp, Key.Empty);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                InvokeMethod(redactor.KeyDown, Key.Ctrl);
            else if (e.Shift)
                InvokeMethod(redactor.KeyDown, Key.Shift);
        }

        private void OnRefesh() 
        {
            Refresh();
            InvokeMethod(redactor.Draw, adapter);
        }

        private void OnChangeFigureFactory(object sender, EventArgs e)
        {
            InvokeMethod(redactor.UpdateFigureCreator, comboBox1.SelectedItem.ToString());
        }

        private void OnChangeDrawerFactory(object sender, EventArgs e)
        {
            InvokeMethod(redactor.UpdateDrawerCreator, comboBox2.SelectedItem.ToString());
        }

        private void OnChangeState(object sender, EventArgs e)
        {
            redactor.SwitchState();
        }

        private void OnChangeColor(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) 
            {
                var convertedColor = converter.ConvertFrom(colorDialog1.Color);
                InvokeMethod(redactor.ChangeColorTo, convertedColor);    
            }
        }

        private void InvokeMethod<T>(Action<T> action, T param) 
        {
            try
            {
                action.Invoke(param);
            }
            catch (Exception exc) 
            {
                MessageBox.Show(exc.Message);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }


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
