using Core;
using System;
using UseCases;

namespace ApiShell
{

    public partial class Facade
    {
        private class SelectionState : FacadeState
        {
            private FigureManipulator manipulator;
            private ISelectOption selectOption;
            private Point pointDown;
            private bool isMouseDown;

            public SelectionState(Facade parent) : base(parent)
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
            public override void MouseDown(Point point)
            {
                var selectedFigure = selectOption.GetFigureByTouch(parent.figures, point);

                if (selectedFigure.IsNotDummy())
                {
                    manipulator.AttachTo(selectedFigure);
                }

                if (!manipulator.IsTouched(point))
                {
                    manipulator.Detach();
                }
                pointDown = point;
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
#endregion
        }
    }
}
