using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases
{
    public class PrototypeCreator : IFigureCreator
    {
        private IPrototype<IFigure> prototype;

        public PrototypeCreator(IPrototype<IFigure> prototype)
        {
            this.prototype = prototype;
        }

        public IFigure CreateFigure(IDrawerFigureVisitor visitor, Snapshot snapshot)
        {
            var clone = prototype.CreateClone();
            ApplySnapshotToClone(clone, snapshot);
            return clone;
        }

        private void ApplySnapshotToClone(IFigure clone, Snapshot snapshot) 
        {
            var offset = Snapshot.OffsetFromTo(clone.GetFigureSnapshot(), snapshot);
            clone.Move(offset.LocationOffset.X, offset.LocationOffset.Y);
            clone.Resize(offset.SizeOffset.X, offset.SizeOffset.Y);
        }
    }
}
