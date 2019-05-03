using Newtonsoft.Json;

namespace WuhuBus.ApiSdk.Input
{
    public class SearchLineInput
    {
        [JsonProperty("lineName")]
        public string LineName { get; set; }
    }
}
