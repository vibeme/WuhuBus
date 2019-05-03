using WuhuBus.ApiSdk.Input;
using WuhuBus.ApiSdk.OutPut;

namespace WuhuBus.ApiSdk.Request
{
    public class GetLineDetailRequest : ApiRequest<GetLineDetailInput, GetLineDetailOutput>
    {
        public GetLineDetailRequest(string lineName)
        {
            Params = new GetLineDetailInput { LineName = lineName };
        }

        public override string Cmd => "lineDetail";
    }
}
