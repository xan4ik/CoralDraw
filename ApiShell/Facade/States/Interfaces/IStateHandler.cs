namespace ApiShell
{
    internal interface IStateHandler 
    {
        void Init(RedactorCore core); // Use it for initialization
        void LateInit(RedactorCore core); // Use it for initialization
    }
}
