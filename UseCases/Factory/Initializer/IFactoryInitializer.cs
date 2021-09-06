namespace UseCases
{
    public interface IFactoryInitializer<Key, Factory> 
    {
        void Init(IFactory<Key, Factory> factory);
    }
}
