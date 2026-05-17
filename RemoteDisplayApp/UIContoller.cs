namespace RemoteDisplayApp
{

    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public interface IUIController
    {
       
        public void WriteLog(string message, LogLevel level = LogLevel.Info, bool extended = false);
    }
}