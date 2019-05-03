using Newtonsoft.Json;

namespace WuhuBus.ApiSdk
{
    public class ApiResponse<T>
    {
        [JsonProperty("result")]
        public T Result { get; set; }

        [JsonProperty("error")]
        public ResultError Error { get; set; }


        [JsonIgnore] public bool IsError => Error != null;
        [JsonIgnore] public int ErrCode => Error.Code;
        [JsonIgnore] public string ErrMsg => Error.Message;

        public class ResultError
        {
            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }
        }
    }
}
