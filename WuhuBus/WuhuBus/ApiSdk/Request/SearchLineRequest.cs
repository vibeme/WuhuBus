using WuhuBus.ApiSdk.Input;
using WuhuBus.ApiSdk.OutPut;

namespace WuhuBus.ApiSdk.Request
{
    public class SearchLineRequest : ApiRequest<SearchLineInput, SearchLineOutput>
    {
        public SearchLineRequest(string lineName)
        {
            Params = new SearchLineInput { LineName = lineName };
        }

        public override string Cmd => "searchLine";
    }
}
