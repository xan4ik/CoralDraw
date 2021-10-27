using Core;

namespace UseCases
{
    public interface IDrawerCreator
    {
        public IVisitorDrawer CreateDrawer(Color color);
    }
}
