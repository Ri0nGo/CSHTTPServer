using System.Net;
using System.Text.Json;

namespace opcda_collector.server
{
    public class Context
    {
        HttpListenerRequest request { get; set; }
        HttpListenerResponse response { get; set; }

        public Context(HttpListenerRequest req, HttpListenerResponse resp)
        {
            request = req;
            response = resp;
        }
        public void BindJson<T>(out T targetObject)
        {
            try
            {
                string jsonData = readRequestBody(request);
                targetObject = JsonSerializer.Deserialize<T>(jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"反序列化失败: {ex.Message}");
                targetObject = default;
            }
        }

        public void Json(object data)
        {
            try
            {
                string jsonResponse = JsonSerializer.Serialize(data);
                response.ContentType = "application/json;charset=UTF-8"; // 设置响应头
                response.StatusCode = (int)HttpStatusCode.OK;  // 设置为 200 OK

                // 将 JSON 数据写入响应流
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonResponse);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                // 如果序列化失败，返回 500 错误
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string errorMessage = JsonSerializer.Serialize(new { message = "Internal server error", error = ex.Message });
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(errorMessage);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                response.OutputStream.Close();
            }

        }

        static string readRequestBody(HttpListenerRequest req)
        {
            using (var reader = new StreamReader(req.InputStream, req.ContentEncoding))
            {
                return reader.ReadToEnd();
            }
        }

    }
}
