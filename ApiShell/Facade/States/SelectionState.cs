using Core;
using System;
using UseCases;

namespace ApiShell
{

    public partial class Redactor
    {
        private class SelectionState : FacadeState
        {
            private FigureManipulator manipulator;
            private ISelectOption selectOption;
            private Point pointDown;
            private bool isMouseDown;

            public SelectionState(Redactor parent) : base(parent)
            {
                manipulator = new FigureManipulator();
            }

            public override void KeyDown(string key)
            {
                if (key == "Shift")
                {
                    selectOption = new ShiftPressed();
                }
                else if (key == "Ctrl") 
                {
                    selectOption = new CtrlPressed();
                }
            }

            public override void KeyUp(string key)
            {
                selectOption = new DefaultOption();
            }

            //TODO: handle touch
            public override void MouseDown(Point touch)
            {
                var selectedFigure = selectOption.GetFigureByTouch(parent.figures, touch);

                if (selectedFigure.IsNotDummy())
                {
                    manipulator.HandleTouch(touch);
                    manipulator.AttachTo(selectedFigure);
                }

                if (!manipulator.IsTouched(touch))
                {
                    manipulator.Detach();
                }
                pointDown = touch;
                isMouseDown = true;
            }

            public override void MouseHold(Point point)
            {
                if (isMouseDown) 
                {
                    float dx = point.X - pointDown.X;
                    float dy = point.Y - pointDown.Y;
                    manipulator.Drag(dx, dy);
                    pointDown = point;
                }
            }

            public override void MouseUp(Point point)
            {
                manipulator.Detach();
                isMouseDown = false;
            }


            public override void Draw(IDrawerAdapter adapter)
            {
                manipulator.Draw(adapter);
                foreach (var figure in parent.figures) 
                {
                    figure.Draw(adapter);
                }
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
#endregion
        }
    }
}
