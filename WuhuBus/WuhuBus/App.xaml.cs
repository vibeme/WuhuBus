using SQLite;
using System;
using System.IO;
using WuhuBus.Models;
using WuhuBus.Views;
using Xamarin.Forms;

namespace WuhuBus
{
    public partial class App : Application
    {
        public static readonly SQLiteAsyncConnection Db;

        static App()
        {
            Db = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WuhuBus.db3"));
        }

        public App()
        {
            InitializeComponent();

            Db.CreateTableAsync<BusLine>().Wait();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
