using System;

namespace ApiShell
{
    internal interface ICallbackHandler<T, Res> : IStateHandler
    {
        void Handle(T args, Action<Res> callback);
    }
}
