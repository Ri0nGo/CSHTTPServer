using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opcda_collector.model
{
    public class RequestData
    {
        public string deviceId { get; set; }
        public string value { get; set; }
    }

    //public class RespSuccess
    //{
    //    public int code { get; set; }
    //    public string msg { get; set; }
    //}

    public class RespCommon<TVal>
    {
        public StatusCode code { get; set; }
        public string msg { get; set; }
        public TVal data { get; set; }

        public static RespCommon<TVal> RespSuccess(TVal data = default)
        {
            return new RespCommon<TVal>
            { 
                code = StatusCode.OK,
                msg = StatusCodeMapper.GetMsg(StatusCode.OK),
                data = data
            };
        }
        public static RespCommon<TVal> RespFailed(StatusCode code, TVal data = default)
        {
            return new RespCommon<TVal>
            {
                code = code,
                msg = StatusCodeMapper.GetMsg(code),
                data = data
            };
        }
    }

    
}
