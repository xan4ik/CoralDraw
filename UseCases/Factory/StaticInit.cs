namespace UseCases
{
    public class StaticInit : IFactoryInitializer<string, IFigureCreator>
    {
        public void Init(IFactory<string, IFigureCreator> factory)
        {
            factory.AddCreator("Ellipse", new EllipceCreator());
            factory.AddCreator("Rectangle", new RectangleCreator());
        }
    }
}
