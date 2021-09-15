namespace UseCases
{
    public class DrawerVisitorFactory : BaseFactory<string, IDrawerCreator>
    {
        public DrawerVisitorFactory() : base(new StaticInitDrawerVisitorFactory())
        { }
    }
}
