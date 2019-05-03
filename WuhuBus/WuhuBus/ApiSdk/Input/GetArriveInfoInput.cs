using Newtonsoft.Json;

namespace WuhuBus.ApiSdk.Input
{
    public class GetArriveInfoInput
    {
        [JsonProperty("type")]
        public LineType Type { get; set; }

        [JsonProperty("stationId")]
        public string StationId { get; set; }

        [JsonProperty("lineName")]
        public string LineName { get; set; }

        public enum LineType
        {
            UN_KNOW = 0,
            UP = 1,
            DOWN = 2
        }
    }
}
