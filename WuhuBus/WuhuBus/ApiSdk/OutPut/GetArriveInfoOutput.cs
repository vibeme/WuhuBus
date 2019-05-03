using Newtonsoft.Json;

namespace WuhuBus.ApiSdk.OutPut
{
    public class GetArriveInfoOutput
    {
        [JsonProperty("willArriveTime")]
        public string WillArriveTime { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("plate")]
        public string Plate { get; set; }
    }
}
