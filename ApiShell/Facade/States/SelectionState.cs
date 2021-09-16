using Core;
using System;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public partial class Redactor
    {
        private class SelectionState : FacadeState
        {
            private Dictionary<Key, ISelectOption> options;
            private FigureManipulator manipulator;
            private ISelectOption selectOption;
            private Point lastTouch;
            private bool isMouseDown;

            public SelectionState(Redactor parent) : base(parent)
            {
                options = new Dictionary<Key, ISelectOption>();
                manipulator = new FigureManipulator();
                selectOption = new DefaultOption();

                options.Add(Key.Shift, new ShiftPressed());
                options.Add(Key.Ctrl, new CtrlPressed());
            }

            public override void KeyDown(Key key)
            {
                selectOption = options[key];
            }

            public override void KeyUp(Key key)
            {
                selectOption = new DefaultOption();
            }

            //TODO: Fix it
            public override void MouseDown(Point touch)
            {
                var selectedFigure = selectOption.GetFigureByTouch(parent.figures, touch);
                
                manipulator.AttachTo(selectedFigure);
                manipulator.HandleTouch(touch);                

               

                lastTouch = touch;
                isMouseDown = true;
            }

            public override void MouseMove(Point touch)
            {
                if (isMouseDown) 
                {
                    float dx = touch.X - lastTouch.X;
                    float dy = touch.Y - lastTouch.Y;

                    manipulator.Drag(dx, dy);
                    lastTouch = touch;
                }
            }

            public override void MouseUp(Point touch)
            {
                isMouseDown = false;
            }


            public override void Draw(IDrawerAdapter adapter)
            {
                foreach (var figure in parent.figures) 
                {
                    figure.Draw(adapter);
                }
                manipulator.Draw(adapter);
            }
            
#region OtherStateMethods
            public override void UpdateFigureCreator(string factoryKey)
            {
                throw new NotImplementedException(NotImplementedMessage);
            }

            public override void UpdateDrawerCreator(string factoryKey)
            {
                throw new NotImplementedException(NotImplementedMessage);
            }

            public override void ChangeColorTo(Color color)
            {
                throw new NotImplementedException(NotImplementedMessage);
            }
            #endregion
        }
    }
}
