using Serilog;namespace UserLogger
{
    public class UserSeriLogLogger : IUserLogger
    {
        ILogger _logger;
        public UserSeriLogLogger(ILogger logger) 
        { 
           _logger = logger;
        }

        public void LogDebugMessage(UserLogMessage userLogMessage)
        {
            _logger.Write(Serilog.Events.LogEventLevel.Debug, userLogMessage.ToString());
        }

        public void LogErrorMessage(UserLogMessage userLogMessage)
        {
            _logger.Write(Serilog.Events.LogEventLevel.Error, userLogMessage.ToString());
        }

        public void LogInfoMessage(UserLogMessage userLogMessage)
        {
            _logger.Write(Serilog.Events.LogEventLevel.Information, userLogMessage.ToString());
        }

        public void LogWarningMessage(UserLogMessage userLogMessage)
        {
            _logger.Write(Serilog.Events.LogEventLevel.Warning, userLogMessage.ToString());
        }        

    }
}