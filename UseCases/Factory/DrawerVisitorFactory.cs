namespace UseCases
{
    public class DrawerVisitorFactory : BaseFactory<string, IDrawerCreator>
    {
        public DrawerVisitorFactory(IFactoryInitializer<string, IDrawerCreator> initializer) : base(initializer)
        { }
    }
}
