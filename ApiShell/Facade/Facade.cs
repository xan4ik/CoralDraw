using Core;
using System.Collections.Generic;
using UseCases;

namespace ApiShell
{
    public partial class Facade
    {
        private List<IFigure> figures;
        private CommandHistory history;
        private FacadeState state;
        private StateMemento memento;

        public Facade(IFactory<string, IFigureCreator> figureFactory, 
                      IFactory<string, IDrawerCreator> drawerFactory)
        {
            var creionState = new CreationState(figureFactory, drawerFactory, this);
            var selectionState = new SelectionState(this);
            memento = new StateMemento(selectionState);
            state = creionState;
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
        
        public  void MouseHold(Point point)
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
    }
}
