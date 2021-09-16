using Core;
using System;

namespace ApiShell
{
    public enum Key
    {
        Shift,
        Ctrl,
        Empty
    };


    public partial class Redactor
    {
        private abstract class FacadeState 
        {
            protected const string NotImplementedMessage = "This option is not awailable!";
            protected readonly Redactor parent;

            protected FacadeState(Redactor parent)
            {
                this.parent = parent;
            }

            public abstract void MouseDown(Point point);
            public abstract void MouseMove(Point point);
            public abstract void MouseUp(Point point);
            public abstract void KeyDown(Key key);
            public abstract void KeyUp(Key key);
            public abstract void Draw(IDrawerAdapter adapter);
            public abstract void UpdateFigureCreator(string factoryKey);
            public abstract void UpdateDrawerCreator(string factoryKey);
            public abstract void ChangeColorTo(Color color);
        }
    }
}
