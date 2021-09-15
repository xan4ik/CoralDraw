namespace UseCases
{
    public class StaticInitFigureFactory : IFactoryInitializer<string, IFigureCreator>
    {
        public void Init(IFactory<string, IFigureCreator> factory)
        {
            factory.AddCreator("Ellipse", new EllipceCreator());
            factory.AddCreator("Rectangle", new RectangleCreator());
        }
    }
}
