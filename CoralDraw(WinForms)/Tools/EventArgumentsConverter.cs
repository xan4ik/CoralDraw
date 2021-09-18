using System;
using System.Windows.Forms;
using ApiShell;

namespace CoralDraw_WinForms
{
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
}