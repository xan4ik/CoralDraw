using Core;
using System;

namespace ApiShell
{

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
            public abstract void MouseHold(Point point);
            public abstract void MouseUp(Point point);
            public abstract void KeyDown(string key);
            public abstract void KeyUp(string key);
            public abstract void Draw(IDrawerAdapter adapter);
            public abstract void UpdateFigureCreator(string factoryKey);
            public abstract void UpdateDrawerCreator(string factoryKey);
            public abstract void ChangeColorTo(Color color);
        }
    }
}
