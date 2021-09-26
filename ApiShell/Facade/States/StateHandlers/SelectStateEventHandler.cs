using Core;
using System;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    internal class SelectStateEventHandler :
        IStateHandler<MouseEventArgs>,
        IStateHandler<KeyEventArgs>,
        IStateHandler<IDrawerAdapter>,
        IStateHandler<CompositeEventArgs>
    {
        private Dictionary<string, ISelectOption> options;
        private DefaultDrawEventHadler defaultDraw;
        private FigureManipulator manipulator;
        private ISelectOption selectOption;
        private Point lastTouch;
        private bool isMouseDown;

        public SelectStateEventHandler()
        {
            options = new Dictionary<string, ISelectOption>()
            {
                {"shift", new ShiftPressed() },
                {"ctrl", new CtrlPressed() }
            };
            defaultDraw = new DefaultDrawEventHadler();
            manipulator = new FigureManipulator();
            selectOption = new DefaultOption();
        }


        public void Handle(MouseEventArgs args, RedactorCore core)
        {
            if (args.Mouse == MouseType.Right)
            {
                HandleRightClick(args, core);
            }
            else if (args.Mouse == MouseType.Left) 
            {
                HandleLeftClick(args, core);
            }
        }

        private void HandleLeftClick(MouseEventArgs args, RedactorCore core)
        {
            switch (args.Type)
            {
                case (ClickType.Down):
                    {
                        OnLeftMouseDown(args, core);
                        break;
                    }
                case (ClickType.Up):
                    {
                        OnLeftMouseUp(args, core);
                        break;
                    }
                case (ClickType.Hold):
                    {
                        OnLeftMouseMove(args, core);
                        break;
                    }
            }
        }

        private void HandleRightClick(MouseEventArgs args, RedactorCore core) 
        {
            if (args.Type == ClickType.Down)
            {
                var figure = selectOption.GetFigureByTouch(core.Figures, args.Touch);
                manipulator.AttachTo(figure);
            }
        }

        private void OnLeftMouseDown(MouseEventArgs args, RedactorCore core) 
        {
            manipulator.HandleTouch(args.Touch);

            lastTouch = args.Touch;
            isMouseDown = true;
        }

        private void OnLeftMouseUp(MouseEventArgs args, RedactorCore core)
        {
            isMouseDown = false;
        }

        private void OnLeftMouseMove(MouseEventArgs args, RedactorCore core)
        {
            if (isMouseDown) 
            {
                float dx = args.Touch.X - lastTouch.X;
                float dy = args.Touch.Y - lastTouch.Y;

                lastTouch = args.Touch;

                manipulator.Drag(dx, dy);
            }
        }


        public void Handle(KeyEventArgs args, RedactorCore core)
        {
            if (args.Type == ClickType.Down)
            {
                selectOption = options[args.Key];
            }
            else if (args.Type == ClickType.Up) 
            {
                selectOption = new DefaultOption();
            }
        }

        public void Handle(IDrawerAdapter args, RedactorCore core)
        {
            defaultDraw.Handle(args, core);
            manipulator.DrawWith(args);
        }
        
        public void Handle(CompositeEventArgs args, RedactorCore core)
        {
            if (manipulator.AttachedFigure() is IComposite<IFigure> composite)
            {
                HandleCompositeEvent(composite, args, core);
            }
            else throw new InvalidCastException("You are truing to group not complex figure!");
        }

        private void HandleCompositeEvent(IComposite<IFigure> composite, CompositeEventArgs args, RedactorCore core)
        {
            switch (args.Type)
            {
                case (CompositeEventArgs.EventType.Group):
                    {
                        core.History.ExecuteCommand(
                                new GroupCommand(core.Figures, composite));
                        break;
                    }
                case (CompositeEventArgs.EventType.Ungroup):
                    {
                        core.History.ExecuteCommand(
                                new UngroupCommand(core.Figures, composite));
                        break;
                    }
            }
        }

        void IStateHandler.Handle(object args, Redactor core)
        {
            throw new NotImplementedException();
        }
    }

    internal class CompositeSaver : IStateHandler<CompositeEventArgs> 
    {
    
    }
}
