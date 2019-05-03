using System;
using System.Linq;
using System.Threading.Tasks;
using WuhuBus.ApiSdk;
using WuhuBus.ApiSdk.Input;
using WuhuBus.ApiSdk.OutPut;
using WuhuBus.ApiSdk.Request;
using WuhuBus.Models;
using WuhuBus.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WuhuBus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusLineDetailPage : TabbedPage
    {
        public BusLineDetailPage(BusLineDetailPageViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        private readonly BusLineDetailPageViewModel _viewModel;

        private async void UpLineStations_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItem is GetLineDetailOutput.Station station)
                await QueryArriveInfo(GetArriveInfoInput.LineType.UP, station, upLineTip);
        }

        private async void DownLineStations_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItem is GetLineDetailOutput.Station station)
                await QueryArriveInfo(GetArriveInfoInput.LineType.DOWN, station, downLineTip);
        }

        private async Task QueryArriveInfo(GetArriveInfoInput.LineType type, GetLineDetailOutput.Station station, Label tipLabel)
        {
            var input = new GetArriveInfoInput
            {
                LineName = _viewModel.LineName,
                Type = type,
                StationId = station.Id
            };
            var response = await ApiClient.Execute(new GetArriveInfoRequest { Params = input });
            if (response.IsError)
            {
                tipLabel.Text = response.ErrMsg;
                return;
            }

            var nextStation =
                (type == GetArriveInfoInput.LineType.UP ? _viewModel.UpLineStationList : _viewModel.DownLineStationList)
                .FirstOrDefault(s => s.Location > response.Result.Index);

            tipLabel.Text = $"{response.Result.WillArriveTime}({response.Result.Distance}) 快到[{nextStation?.Name}]了 {response.Result.Plate}";
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            switch (_viewModel.CollectionTip)
            {
                case "收藏":
                    await App.Db.InsertOrReplaceAsync(_viewModel.ToBusLine());
                    _viewModel.CollectionTip = "取消收藏";
                    break;
                case "取消收藏":
                    await App.Db.DeleteAsync<BusLine>(_viewModel.LineName);
                    _viewModel.CollectionTip = "收藏";
                    break;
            }
        }

    }
}