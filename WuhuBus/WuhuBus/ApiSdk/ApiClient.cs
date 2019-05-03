using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WuhuBus.ApiSdk
{
    public class ApiClient
    {
        public static async Task<ApiResponse<TR>> Execute<T, TR>(ApiRequest<T, TR> request)
        {
            var json = JsonConvert.SerializeObject(request);

            var webRequest = WebRequest.CreateHttp("http://220.180.139.42:8980/SmartBusServer/Main");
            webRequest.Method = "POST";
            using (var stream = webRequest.GetRequestStream())
            {
                var data = Encoding.UTF8.GetBytes(json);
                stream.Write(data, 0, data.Length);
            }

            using (var webResponse = await webRequest.GetResponseAsync())
            {
                json = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                return JsonConvert.DeserializeObject<ApiResponse<TR>>(json);
            }
        }
    }
}
