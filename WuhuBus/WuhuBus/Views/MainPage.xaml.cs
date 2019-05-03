using System;
using System.ComponentModel;
using System.Threading.Tasks;
using WuhuBus.ApiSdk;
using WuhuBus.ApiSdk.Request;
using WuhuBus.Models;
using WuhuBus.ViewModels;
using Xamarin.Forms;

namespace WuhuBus.Views
{
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MainPageViewModel();
        }

        private readonly MainPageViewModel _viewModel;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            searchResult.IsVisible = false;
            _viewModel.LoadLines();
        }

        private async void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            var searchBar = (SearchBar)sender;

            var request = new SearchLineRequest(searchBar.Text);
            var response = await ApiClient.Execute(request);
            searchResult.ItemsSource = response.IsError ? null : response.Result.List;
            if (response.IsError)
            {
                searchResult.IsVisible = false;
                await DisplayAlert("错误", response.ErrMsg, "菜鸡！！");
            }
            else
            {
                searchResult.ItemsSource = response.Result.List;
                searchResult.IsVisible = true;
            }
        }

        private async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;

            if (listView.SelectedItem is BusLine busLine)
            {
                await LoadDetail(busLine.Name);
            }
            listView.SelectedItem = null;
            searchResult.IsVisible = false;
        }

        private async void SearchResult_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItem is string lineName)
            {
                await LoadDetail(lineName);
            }
        }

        private async Task LoadDetail(string lineName)
        {
            var request = new GetLineDetailRequest(lineName);
            var response = await ApiClient.Execute(request);
            if (response.IsError)
            {
                await DisplayAlert("错误", response.ErrMsg, "菜鸡！！");
                return;
            }

            var existInfo = await App.Db.Table<BusLine>().FirstOrDefaultAsync(b => b.Name == lineName);
            var viewModel = new BusLineDetailPageViewModel(response.Result, existInfo != null);

            //如果已经收藏过则进行更新(异步)
            if (existInfo != null)
            {
                existInfo = viewModel.ToBusLine();

#pragma warning disable 4014
                App.Db.UpdateAsync(existInfo);
#pragma warning restore 4014
            }

            var page = new BusLineDetailPage(viewModel);
            await Navigation.PushAsync(page);
        }
    }
}
