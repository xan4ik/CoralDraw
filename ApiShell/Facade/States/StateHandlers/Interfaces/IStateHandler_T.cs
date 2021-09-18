namespace ApiShell
{
    internal interface IStateHandler<T> : IStateHandler
    {
        void Handle(T args, Redactor redactor);
    }
}
