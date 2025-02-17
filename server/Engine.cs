using System.Net;

namespace opcda_collector.server
{
    public class Engine
    {
        public string ServerName { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }

        public Dictionary<string, Action<Context>> Routes { get; set; }

        public Action<Context> NotFoundMethod { get; set; }

        public Action<Context> InternalServerError { get; set; }


        public Engine()
        {
            Routes = new Dictionary<string, Action<Context>>();
        }

        public void RegisterRoute(string path, Action<Context> handler)
        {
            Routes.Add(path, handler);
        }

        public void printRoutes()
        {
            Console.WriteLine($"[Server Name]\t{ServerName}");

            foreach (var route in Routes)
            {
                Console.WriteLine($"[ROUTE]\t\t{route.Key}");
            }
        }

        public void Run()
        {
            printRoutes();

            HttpListener listener = new HttpListener();
            listener.Prefixes.Add($"http://{Ip}:{Port}/");
            listener.Start();
            Console.WriteLine($"Listening on {Ip}:{Port}");


            serve(listener);

        }

        public void serve(HttpListener listener)
        {
            while (true)
            {
                try
                {
                    HttpListenerContext ctx = listener.GetContext();
                    HttpListenerRequest req = ctx.Request;  // 获取请求对象
                    HttpListenerResponse resp = ctx.Response;  // 

                    string rawUrl = req.RawUrl;
                    string path = rawUrl.Split('?')[0];

                    var result = FindRouteByPath(path);
                    if (result.Item2)
                    {
                        Action<Context> handler = result.Item1;

                        Context context = new Context(req, resp);
                        handler(context);  // 调用路由处理函数
                    }
                    else
                    {
                        // 返回 404 错误
                        resp.StatusCode = 404;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("404 Not Found");
                        resp.ContentLength64 = buffer.Length;
                        resp.OutputStream.Write(buffer, 0, buffer.Length);
                    }
                    resp.OutputStream.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Http Internal Error: {ex.Message}");
                }

            }
        }

        public Tuple<Action<Context>, bool> FindRouteByPath(string path)
        {
            if (Routes.ContainsKey(path))
            {
                return new Tuple<Action<Context>, bool>(Routes[path], true);
            }
            else
            {
                return new Tuple<Action<Context>, bool>(null, false);
            }

        }
    }
}
