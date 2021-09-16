namespace UseCases
{
    public class FigureFactory : BaseFactory<string, IFigureCreator>
    {
        public FigureFactory() : base(new StaticInitFigureFactory())
        { }
    }
}
