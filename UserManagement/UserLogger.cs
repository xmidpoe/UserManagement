using UserLogger;
using Serilog;
using Serilog.Configuration;


namespace UserManagement
{
    public class UserLogger : IUserLogger
    {
        ILogger _logger;
        public UserLogger()
        {


            //_logger = new LoggerConfiguration()
            //    //.MinimumLevel.Debug()
            //    //.WriteTo.File(@"D:\log-.txt", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();


            ////var configuration = new ConfigurationBuilder()
            ////.AddJsonFile("appsettings.json")
            ////.Build();

            //_logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration()
            //    .CreateLogger();
        }

        public UserLogger(ILogger logger) 
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