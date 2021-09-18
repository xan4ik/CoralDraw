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
        private EventArgumentsConverter eventConvrter;
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

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeEventFor, 
                eventConvrter.ConvertFrom(e, ClickType.Down)
            );
        }

        private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeEventFor, 
                eventConvrter.ConvertFrom(e, ClickType.Hold)
            );
            OnRefesh();
        }

        private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeEventFor, 
                eventConvrter.ConvertFrom(e, ClickType.Up)
            );
        }

        private void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) 
        {
            InvokeMethod(
                redactor.InvokeEventFor, 
                eventConvrter.ConvertFrom(e, ClickType.Up)
            );
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            InvokeMethod(
                redactor.InvokeEventFor, 
                eventConvrter.ConvertFrom(e, ClickType.Down)
            );
        }

        private void OnRefesh() 
        {
            Refresh();
            InvokeMethod(
                    redactor.InvokeEventFor, 
                    adapter
            );
        }

        private void OnChangeFigureFactory(object sender, EventArgs e)
        {
            InvokeMethod(
                redactor.InvokeEventFor,
                eventConvrter.CreateArgsForFigureFactory(
                    comboBox1.SelectedItem.ToString())
            );
        }

        private void OnChangeDrawerFactory(object sender, EventArgs e)
        {
            InvokeMethod(
                redactor.InvokeEventFor,
                eventConvrter.CreateArgsForDrawerFactory(
                    comboBox2.SelectedItem.ToString())
            );
        }

        private void OnChangeState(object sender, EventArgs e)
        {
            redactor.SwapState();
        }

        private void OnChangeColor(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK) 
            {
                InvokeMethod(
                    redactor.InvokeEventFor,
                    converter.ConvertFrom(colorDialog1.Color)
                );    
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

    class EventArgumentsConverter
    {
        private SystemDrawingToCoreConverter converter;
        public EventArgumentsConverter()
        {
            converter = new SystemDrawingToCoreConverter();
        }

        public ApiShell.MouseEventArgs ConvertFrom(System.Windows.Forms.MouseEventArgs args, ClickType type)
        {
            var eventData = new ApiShell.MouseEventArgs()
            {
                Touch = converter.ConvertFrom(args.Location),
                Mouse = ConvertFrom(args.Button),
                Type = type
            };
            return eventData;
        }

        private MouseType ConvertFrom(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return MouseType.Left;
                case MouseButtons.Right:
                    return MouseType.Right;
                case MouseButtons.Middle:
                    return MouseType.Midle;
                default:
                    throw new ArgumentException("Unsupported mouse button!");
            }
        }

        public ApiShell.KeyEventArgs ConvertFrom(System.Windows.Forms.KeyEventArgs args, ClickType type) 
        {
            var eventData = new ApiShell.KeyEventArgs()
            {
                Key = ConvertFrom(args),
                Type = type
            };
            return eventData;
        }

        private string ConvertFrom(System.Windows.Forms.KeyEventArgs args)
        {
            if (args.Shift)
            {
                return "shift";
            }
            else if (args.Control) 
            {
                return "ctrl";
            }
            return args.KeyData.ToString();
        }

        public ApiShell.ChangeCreatorEventArgs CreateArgsForFigureFactory(string creatorKey) 
        {
            var eventArgs = new ChangeCreatorEventArgs()
            {
                IsFigureFactory = true,
                Key = creatorKey
            };
            return eventArgs;
        }

        public ApiShell.ChangeCreatorEventArgs CreateArgsForDrawerFactory(string creatorKey)
        {
            var eventArgs = new ChangeCreatorEventArgs()
            {
                IsFigureFactory = false,
                Key = creatorKey
            };
            return eventArgs;
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