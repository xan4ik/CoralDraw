namespace ApiShell
{
    internal interface IStateHandler 
    {
        void Handle(object args, Redactor redactor);
    }
}
