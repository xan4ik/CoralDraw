namespace UseCases
{
    public interface IFactory <Key, Creator>
    {
        Creator GetCreator(Key key);
        void AddCreator(Key key, Creator factory);
        void RemoveCreator(Key key);
    }
}
