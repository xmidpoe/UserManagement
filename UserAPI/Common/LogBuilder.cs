using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Net;
using UserLogger;

namespace UserAPI.Common
{
    public class LogBuilder
    {
        //private static LogBuilder _instance;

        //public static LogBuilder Instance
        //{
        //    get
        //    {
        //        if(_instance == null)
        //        {
        //            _instance = new LogBuilder();
        //        }
        //        return _instance;
        //    }
        //}

        private HttpContext _context;

        public LogBuilder(HttpContext context)
        {
            _context = context;
        }

        public UserLogMessage GetMessage(string callerMethod, string parameters)
        {
            return new UserLogMessage
            {
                ClientIP = GetClientIp(),
                ClientName = GetUserHost(),
                Host = Environment.MachineName,
                MethodName = callerMethod,
                Parameters = parameters
            };
        }

        private string GetClientIp()
        {
            string ip = _context.Connection.RemoteIpAddress.ToString();

            if (ip == null)
            {
                ip = _context.GetServerVariable("REMOTE_ADDR");
            }

            return ip;
        }

        private string GetUserHost()
        {
            return Dns.GetHostName();
        }
    }
}
