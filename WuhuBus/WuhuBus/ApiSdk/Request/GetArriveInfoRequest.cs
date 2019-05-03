using WuhuBus.ApiSdk.Input;
using WuhuBus.ApiSdk.OutPut;

namespace WuhuBus.ApiSdk.Request
{
    public class GetArriveInfoRequest : ApiRequest<GetArriveInfoInput, GetArriveInfoOutput>
    {
        public override string Cmd => "getArriveInfo";
    }
}
