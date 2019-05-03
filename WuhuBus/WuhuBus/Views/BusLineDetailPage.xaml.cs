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

        protected override void OnAppearing()
        {
            base.OnAppearing();

#pragma warning disable 4014
            switch (_viewModel.LastLineType)
            {
                case GetArriveInfoInput.LineType.UP:
                    tabbedPage.SelectedItem = upLinePage;
                    upLineStations.SelectedItem = _viewModel.UpLineStationList.FirstOrDefault(s => s.Id == _viewModel.UpLineLastFocusStation);
                    QueryArriveInfo(_viewModel.LastLineType, _viewModel.UpLineLastFocusStation, upLineTip);
                    break;
                case GetArriveInfoInput.LineType.DOWN:
                    tabbedPage.SelectedItem = downLinePage;
                    downLineStations.SelectedItem = _viewModel.DownLineStationList.FirstOrDefault(s => s.Id == _viewModel.DownLineLastFocusStation);
                    QueryArriveInfo(_viewModel.LastLineType, _viewModel.DownLineLastFocusStation, downLineTip);
                    break;
            }
#pragma warning restore 4014
        }

        private async void UpLineStations_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItem is GetLineDetailOutput.Station station)
                await QueryArriveInfo(GetArriveInfoInput.LineType.UP, station.Id, upLineTip);
        }

        private async void DownLineStations_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItem is GetLineDetailOutput.Station station)
                await QueryArriveInfo(GetArriveInfoInput.LineType.DOWN, station.Id, downLineTip);
        }

        private async Task QueryArriveInfo(GetArriveInfoInput.LineType type, string stationId, Label tipLabel)
        {
            //记录这次点击的站台
            if (_viewModel.CollectionTip == "取消收藏")
            {
#pragma warning disable 4014
                switch (type)
                {
                    case GetArriveInfoInput.LineType.UP:
                        if (_viewModel.UpLineLastFocusStation != stationId)
                        {
                            _viewModel.UpLineLastFocusStation = stationId;
                            _viewModel.LastLineType = GetArriveInfoInput.LineType.UP;
                            App.Db.UpdateAsync(_viewModel.ToBusLine());
                        }
                        break;
                    case GetArriveInfoInput.LineType.DOWN:
                        if (_viewModel.DownLineLastFocusStation != stationId)
                        {
                            _viewModel.DownLineLastFocusStation = stationId;
                            _viewModel.LastLineType = GetArriveInfoInput.LineType.DOWN;
                            App.Db.UpdateAsync(_viewModel.ToBusLine());
                        }
                        break;
                }
#pragma warning restore 4014
            }

            var input = new GetArriveInfoInput
            {
                LineName = _viewModel.LineName,
                Type = type,
                StationId = stationId
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

            tipLabel.Text = $"{response.Result.WillArriveTime}({response.Result.Distance}) 快到 [{nextStation?.Name}] 了 {response.Result.Plate}";
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