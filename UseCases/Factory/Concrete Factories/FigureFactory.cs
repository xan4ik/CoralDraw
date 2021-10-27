namespace UseCases
{
    public class FigureFactory : BaseFactory<string, IFigureCreator>
    {
        public FigureFactory(IFactoryInitializer<string, IFigureCreator> initializer) : base(initializer)
        { }
    }
}
