namespace UseCases
{
    public interface IFactory <Key, Factory>
    {
        Factory GetCreator(Key key);
        void AddCreator(Key key, Factory factory);
        void RemoveCreator(Key key);
    }
}
