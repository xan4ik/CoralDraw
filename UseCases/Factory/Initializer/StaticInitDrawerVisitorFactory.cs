namespace UseCases
{
    public class StaticInitDrawerVisitorFactory : IFactoryInitializer<string, IDrawerCreator>
    {
        public void Init(IFactory<string, IDrawerCreator> factory)
        {
            factory.AddCreator("Solid", new SolidDrawerCreator());
            factory.AddCreator("Bound", new BoundDrawerCreator());
        }
    }
}
