using System.Collections.Generic;
using Newtonsoft.Json;

namespace WuhuBus.ApiSdk.OutPut
{
    public class SearchLineOutput
    {
        [JsonProperty("list")]
        public List<string> List { get; set; }
    }
}
