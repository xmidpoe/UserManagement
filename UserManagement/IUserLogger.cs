using UserLogger;

namespace UserManagement
{
    public interface IUserLogger
    {
        void LogInfoMessage(UserLogMessage userLogMessage);
        void LogDebugMessage(UserLogMessage userLogMessage);
        void LogWarningMessage(UserLogMessage userLogMessage);
        void LogErrorMessage(UserLogMessage userLogMessage);
    }
}
