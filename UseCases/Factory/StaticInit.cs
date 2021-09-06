namespace UseCases
{
    public class StaticInit : IFactoryInitializer<string, IFigureCreator>
    {
        public void Init(IFactory<string, IFigureCreator> factory)
        {
            factory.AddFactory("Ellipse", new EllipceCreator());
            factory.AddFactory("Rectangle", new RectangleCreator());
        }
    }
}
