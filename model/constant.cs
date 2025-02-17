using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opcda_collector.model
{
    public enum StatusCode
    {
        OK = 0,
        ErrParams = 1,
        ErrInternalServer = 500,
    }

    public static class StatusCodeMapper
    {
        private static readonly Dictionary<StatusCode, string> statusCodeMap = new Dictionary<StatusCode, string>
        {
            {StatusCode.OK, "success" },
            {StatusCode.ErrParams, "invalid params" },
            {StatusCode.ErrInternalServer, "internal server error" },
        };

        public static string GetMsg(StatusCode code)
        {
            return statusCodeMap[code];
        }
    }
}
