using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WuhuBus.ApiSdk.OutPut
{
    public class GetLineDetailOutput
    {
        [JsonProperty("lineName")]
        public string LineName { get; set; }

        [JsonProperty("startendTime")]
        public string StartendTime { get; set; }

        [JsonProperty("upLine")]
        public string UpLine { get; set; }

        [JsonProperty("upLineStationList")]
        public List<Station> UpLineStationList { get; set; }

        [JsonProperty("downLine")]
        public string DownLine { get; set; }

        [JsonProperty("downLineStationList")]
        public List<Station> DownLineStationList { get; set; }


        public class Station : List<string>
        {
            [JsonIgnore]
            public string Id => this[0];
            [JsonIgnore]
            public string Name => this[1];

            private int _location = -1;
            [JsonIgnore]
            public int Location
            {
                get
                {
                    if (_location < 0)
                        _location = Convert.ToInt32(this[2]);
                    return _location;
                }
            }
        }
    }
}
