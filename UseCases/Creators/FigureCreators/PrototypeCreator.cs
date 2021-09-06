using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases
{
    public class PrototypeCreator : IFigureCreator
    {
        private IFigurePrototype prototype;

        public PrototypeCreator(IFigurePrototype prototype)
        {
            this.prototype = prototype;
        }

        public IFigure CreateFigure(Snapshot snapshot)
        {
            var clone = prototype.CreateClone();
                clone.Move(snapshot.Location.X, snapshot.Location.Y);
                clone.Resize(snapshot.Size.Width, snapshot.Size.Height);
            return clone;
        }

        public IFigure CreateFigure()
        {
            return prototype.CreateClone();
        }
    }
}
