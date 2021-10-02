using Core;
using System.Collections.Generic;
using System.Diagnostics;
using UseCases;

namespace ApiShell
{
    internal class SelectEventHandler :
        IStateHandler<MouseEventArgs>,
        IStateHandler<KeyEventArgs>,
        IStateHandler<IDrawerAdapter>
    {
        private Dictionary<string, IPrototype<ISelectOption>> options;
        private DefaultDrawEventHadler defaultDraw;
        private FigureManipulator manipulator;
        private ISelectOption selectOption;
        private Point lastTouch;
        private bool isMouseDown;

        public SelectEventHandler()
        {
            options = new Dictionary<string, IPrototype<ISelectOption>>()
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
                core.EventBus.Publish<IFigure>(figure);
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
                selectOption = options[args.Key].CreateClone();
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
        
        void IStateHandler.Init(RedactorCore core)
        {
            core.EventBus.CreateEventOf<IFigure>();
        }

        void IStateHandler.LateInit(RedactorCore core)
        {
            return;
        }
    }
}
