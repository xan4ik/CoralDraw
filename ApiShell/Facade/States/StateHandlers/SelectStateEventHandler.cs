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


        public void Handle(MouseEventArgs args, Redactor redactor)
        {
            if (args.Mouse == MouseType.Right)
            {
                HandleRightClick(args, redactor);
            }
            else if (args.Mouse == MouseType.Left) 
            {
                HandleLeftClick(args, redactor);
            }
        }

        private void HandleLeftClick(MouseEventArgs args, Redactor redactor)
        {
            switch (args.Type)
            {
                case (ClickType.Down):
                    {
                        OnLeftMouseDown(args, redactor);
                        break;
                    }
                case (ClickType.Up):
                    {
                        OnLeftMouseUp(args, redactor);
                        break;
                    }
                case (ClickType.Hold):
                    {
                        OnLeftMouseMove(args, redactor);
                        break;
                    }
            }
        }

        private void HandleRightClick(MouseEventArgs args, Redactor redactor) 
        {
            if (args.Type == ClickType.Down)
            {
                var figure = selectOption.GetFigureByTouch(redactor.Figures, args.Touch);
                manipulator.AttachTo(figure);
            }
        }

        private void OnLeftMouseDown(MouseEventArgs args, Redactor redactor) 
        {
            manipulator.HandleTouch(args.Touch);

            lastTouch = args.Touch;
            isMouseDown = true;
        }

        private void OnLeftMouseUp(MouseEventArgs args, Redactor redactor)
        {
            isMouseDown = false;
        }

        private void OnLeftMouseMove(MouseEventArgs args, Redactor redactor)
        {
            if (isMouseDown) 
            {
                float dx = args.Touch.X - lastTouch.X;
                float dy = args.Touch.Y - lastTouch.Y;

                lastTouch = args.Touch;

                manipulator.Drag(dx, dy);
            }
        }


        public void Handle(KeyEventArgs args, Redactor redactor)
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

        public void Handle(IDrawerAdapter args, Redactor redactor)
        {
            defaultDraw.Handle(args, redactor);
            manipulator.DrawWith(args);
        }
        
        public void Handle(CompositeEventArgs args, Redactor redactor)
        {
            if (manipulator.AttachedFigure() is IComposite<IFigure> composite)
            {
                HandleCompositeEvent(composite, args, redactor);
            }
            else throw new InvalidCastException("You are truing to group not complex figure!");
        }

        private void HandleCompositeEvent(IComposite<IFigure> composite, CompositeEventArgs args, Redactor redactor)
        {
            switch (args.Type)
            {
                case (CompositeEventArgs.EventType.Group):
                    {
                        redactor.History.ExecuteCommand(
                                new GroupCommand(redactor.Figures, composite));
                        break;
                    }
                case (CompositeEventArgs.EventType.Ungroup):
                    {
                        redactor.History.ExecuteCommand(
                                new UngroupCommand(redactor.Figures, composite));
                        break;
                    }
            }
        }

        void IStateHandler.Handle(object args, Redactor redactor)
        {
            throw new NotImplementedException();
        }

    }
}
