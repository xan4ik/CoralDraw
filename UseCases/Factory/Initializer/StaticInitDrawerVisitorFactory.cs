using Core;

namespace UseCases
{
    public class StaticInitDrawerVisitorFactory : IFactoryInitializer<string, IDrawerCreator>
    {
        public void Init(IFactory<string, IDrawerCreator> factory)
        {
            factory.AddCreator("Solid", new VisitorDrawerCreator<SolidVisitorFlyweight>());
            factory.AddCreator("Bound", new VisitorDrawerCreator<BoundVisitorFlyweight>());
        }
    }
}
