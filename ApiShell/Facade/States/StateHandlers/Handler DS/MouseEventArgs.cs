using Core;

namespace ApiShell
{
    public enum ClickType
    {
        Down, Up, Hold
    }

    public enum MouseType
    {
        Right, Left, Midle, None
    }

    public struct MouseEventArgs
    {
        public MouseType Mouse;
        public ClickType Type;
        public Point Touch;
    }
}
