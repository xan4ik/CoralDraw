using UseCases;

namespace ApiShell
{
    class CreatePrototypeFactory : ICommand
    {
        private IFactory<string, IFigureCreator> figureFactory;
        private IFigureCreator creator;
        private string key;

        public CreatePrototypeFactory(IFactory<string, IFigureCreator> figureFactory, string key, IFigureCreator creator)
        {
            this.figureFactory = figureFactory;
            this.creator = creator;
            this.key = key;
        }

        public void Execute()
        {
            figureFactory.AddCreator(key, creator);
        }

        public void Undo()
        {
            figureFactory.RemoveCreator(key);
        }
    }
}
