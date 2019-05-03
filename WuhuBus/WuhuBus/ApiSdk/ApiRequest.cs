using Newtonsoft.Json;

namespace WuhuBus.ApiSdk
{
    public abstract class ApiRequest<T, TR>
    {
        [JsonProperty("cmd")]
        public abstract string Cmd { get; }

        [JsonProperty("params")]
        public T Params { get; set; }
    }
}
