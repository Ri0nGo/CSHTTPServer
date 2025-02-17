using opcda_collector.server;

namespace opcda_collector
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of Engine
            Engine server = new Engine
            {
                Ip = "192.168.1.6",
                //Ip = "192.168.75.241",
                Port = 8000,
                ServerName = "OPCDA Collector Service"
            };

            // Register a route
            server.RegisterRoute("/data", HandleDataRequest);


            // Start the server
            server.Run();
        }

        static void HandleDataRequest(Context ctx)
        {
            
            ctx.BindJson(out model.RequestData jsonData);

            if (jsonData == null) {
                ctx.Json(model.RespCommon<model.RequestData?>.RespFailed(model.StatusCode.ErrParams));
                return;
            }

            Console.WriteLine($"request data: DeviceId: {jsonData.deviceId}, Value: {jsonData.value}");

            ctx.Json(model.RespCommon<model.RequestData>.RespSuccess(jsonData));
        }
        
    }
}
