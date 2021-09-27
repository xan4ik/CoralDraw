namespace ApiShell
{
    internal interface IStateHandler 
    {
        void Init(RedactorCore core); 
        void LateInit(RedactorCore core); 
    }
}
