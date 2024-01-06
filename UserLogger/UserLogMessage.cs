using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserLogger
{
    public class UserLogMessage
    {
        public string ClientIP { get; set; }
        public string ClientName { get; set; }
        public string Host { get; set; }
        public string MethodName { get; set; }
        public string Parameters { get; set; }
        public string Message { get; set; }



        public UserLogMessage()
        {

        }

        public UserLogMessage(string clientIP, string clientName, string clientHost, string methodName, string parameters, string message)
        {
            ClientIP = clientIP;
            ClientName = clientName;
            Host = clientHost;
            MethodName = methodName;
            Parameters = parameters;
            Message = message;
        }

        public override string ToString()
        {
            return $"ClientIP: {ClientIP} ; ClientHost: {Host} ; ClientName: {ClientName} ; from Method: {MethodName} ; with Params: {Parameters} ; Message: {Message}";
        }
    }
}
