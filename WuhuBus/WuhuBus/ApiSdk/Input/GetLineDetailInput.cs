using Newtonsoft.Json;

namespace WuhuBus.ApiSdk.Input
{
    public class GetLineDetailInput
    {
        [JsonProperty("lineName")]
        public string LineName { get; set; }
    }
}
