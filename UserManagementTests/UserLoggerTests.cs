using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog;
using UserLogger;

namespace UserManagement.Tests
{
    [TestClass()]
    public class UserLoggerTests
    {
        private ILogger _logger;
        private UserLogMessage _message;

        private const string LOG_ADDRESS = @"D:\UsrMngLogs\";
        private const string LOG_FILE = $"log-.txt";

        public UserLoggerTests()
        {
            //Setup logger
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(GetLogAddress(), shared: true, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            //Setup shared:true,
            _message = new UserLogMessage();
            _message.ClientIP = "127.0.0.1";
            _message.ClientName = "test";
            _message.Host = "localhost";
            _message.Parameters = "UserId = test";
            _message.Message = "testMessage";
            _message.MethodName = "testMethod";
        }

        [TestMethod()]
        public void LogDebugMessageTest()
        {
            CleanFile();

            var _userLogger = new UserLogger(_logger);

            _userLogger.LogDebugMessage(_message);

            string logMsg = ReadLog();

            Assert.IsTrue(logMsg.Contains("DBG"));
        }


        [TestMethod()]
        public void LogErrorMessageTest()
        {
            CleanFile();

            var _userLogger = new UserLogger(_logger);

            _userLogger.LogErrorMessage(_message);

            string logMsg = ReadLog();

            Assert.IsTrue(logMsg.Contains("ERR"));
        }

        [TestMethod()]
        public void LogInfoMessageTest()
        {
            CleanFile();

            var _userLogger = new UserLogger(_logger);

            _userLogger.LogInfoMessage(_message);

            string logMsg = ReadLog();

            Assert.IsTrue(logMsg.Contains("INF"));
        }

        [TestMethod()]
        public void LogWarnMessageTest()
        {
            CleanFile();

            var _userLogger = new UserLogger(_logger);

            _userLogger.LogWarningMessage(_message);

            string logMsg = ReadLog();

            Assert.IsTrue(logMsg.Contains("WRN"));
        }

        /// <summary>
        /// Cleans the LOG file so it can be propperly valued in assertation
        /// </summary>
        private void CleanFile()
        {
            string datePattern = DateTime.Now.ToString("yyyyMMdd");
            string logAddress = GetLogAddress(datePattern);

            if (!File.Exists(GetLogAddress(datePattern)))
                return;

            using (var fileStream = File.Open(logAddress, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite))
            {

                fileStream.Flush();
            }
        }

        /// <summary>
        /// Gets the log address, the full file location, and the file name.
        /// If there is no date pattern it generates original serilog file
        /// </summary>
        /// <param name="datePattern">Date pattern yyyyMMdd that is created by Serilog</param>
        /// <returns>Full log file path,with the fileName</returns>
        private string GetLogAddress(string datePattern = null)
        {
            if (datePattern == null)
                return $"{LOG_ADDRESS}{LOG_FILE}";

            return $"{LOG_ADDRESS}log-{datePattern}.txt";
        }

        /// <summary>
        /// Reads the data from the LogFile
        /// </summary>
        /// <returns>String: The log data string</returns>
        private string ReadLog()
        {
            string datePattern = DateTime.Now.ToString("yyyyMMdd");

            string logAddress = GetLogAddress(datePattern);

            using (var fileStream = File.Open(logAddress, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}