namespace ApiShell
{
    internal interface IStateHandler<T> : IStateHandler
    {
        void Handle(T args, RedactorCore core);
    }
}
