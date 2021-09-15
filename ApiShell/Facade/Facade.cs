using Core;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public partial class Redactor
    {
        private List<IFigure> figures;
        private CommandHistory history;
        private FacadeState state;
        private StateMemento memento;

        public Redactor()
        {
            state= new CreationState(new FigureFactory(), new DrawerVisitorFactory(), this);
            memento = new StateMemento( new SelectionState(this));
            history = new CommandHistory();
            figures = new List<IFigure>();
        }

        public void SwitchState() 
        {
            var newMemento = new StateMemento(state);
            memento.RestoreStateFor(this);
            memento = newMemento;
        }

        public void MouseDown(Point point) 
        {
            state.MouseDown(point);
        }
        
        public  void MouseMove(Point point)
        {
            state.MouseDown(point);
        }
        
        public  void MouseUp(Point point)
        {
            state.MouseUp(point);
        }
        
        public void KeyDown(string key)
        {
            state.KeyDown(key);
        }
        
        public void KeyUp(string key)
        {
            state.KeyUp(key);
        }
        
        public void Draw(IDrawerAdapter adapter)
        {
            state.Draw(adapter);
        }

        public void UpdateFigureCreator(string factoryKey)
        {
            state.UpdateFigureCreator(factoryKey);
        }

        public void UpdateDrawerCreator(string factoryKey)
        {
            state.UpdateDrawerCreator(factoryKey);
        }

        public void ChangeColorTo(Color color) 
        {
            state.ChangeColorTo(color);
        }
    }
}
