namespace UseCases
{
    public interface IFactory <Key, Factory>
    {
        Factory GetFactory(Key key);
        void AddFactory(Key key, Factory factory);
        void RemoveFactory(Key key);
    }
}
