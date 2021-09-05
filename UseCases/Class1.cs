using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;

namespace UseCases
{
    public interface IDrawerFactory
    {
        public IDrawerFigureVisitor CreateDrawer();
    }


    public interface IFigureFactory
    {
        public IFigure CreateFigure(Snapshot snapshot);
        public IFigure CreateFigure();
    }


    public class FigureFactory
    {
        private static FigureFactory instance;
        private Dictionary<string, IFigureFactory> creators;

        static FigureFactory() 
        {
            instance = new FigureFactory();
        }

        private FigureFactory() 
        {
            var factories = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsAssignableFrom(typeof(IFigureFactory)) && type.IsInterface);
            foreach (var factory in factories)
            {
                var attribute = factory.GetCustomAttribute<FactoryKeyAttribute>(false);
                if (!(attribute is null)) 
                {
                    var construct = factory.GetConstructor(null);
                    creators.Add(attribute.Key, (IFigureFactory)construct.Invoke(null));
                }
            }
        }

        private bool IsFactoryNotAdded(string key) 
        {
            return !creators.ContainsKey(key);
        }
    }

    [FactoryKey("Ellipse")]
    class EllipceCreator : IFigureFactory
    {
        public IFigure CreateFigure(Snapshot snapshot)
        {
            return new Ellipse(snapshot);
        }

        public IFigure CreateFigure()
        {
            return new Ellipse();
        }
    }


    [FactoryKey("Rectangle")]
    class RectangleCreator : IFigureFactory
    {
        public IFigure CreateFigure(Snapshot snapshot)
        {
            return new Rectngle(snapshot);
        }

        public IFigure CreateFigure()
        {
            return new Rectngle();
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    class FactoryKeyAttribute : Attribute 
    {
        public readonly string Key;

        public FactoryKeyAttribute(string key)
        {
            this.Key = key;
        }
    
    }
}
