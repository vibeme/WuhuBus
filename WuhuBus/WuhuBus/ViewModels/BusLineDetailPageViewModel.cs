using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WuhuBus.Annotations;
using WuhuBus.ApiSdk.Input;
using WuhuBus.ApiSdk.OutPut;
using WuhuBus.Models;

namespace WuhuBus.ViewModels
{
    public class BusLineDetailPageViewModel : INotifyPropertyChanged
    {
        private string _collectionTip;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public BusLineDetailPageViewModel(GetLineDetailOutput lineDetail, bool collectioned)
        {
            LineName = lineDetail.LineName;
            UpLine = lineDetail.UpLine;
            DownLine = lineDetail.DownLine;

            var runtime = lineDetail.StartendTime.Split(',');
            UpLineRunTime = "运营时间：" + runtime[0];
            DownLineRunTime = "运营时间：" + runtime[1];

            UpLineStationList = lineDetail.UpLineStationList;
            DownLineStationList = lineDetail.DownLineStationList;

            CollectionTip = collectioned ? "取消收藏" : "收藏";
        }

        public string CollectionTip
        {
            get => _collectionTip;
            set
            {
                if (value == _collectionTip) return;
                _collectionTip = value;
                OnPropertyChanged(nameof(CollectionTip));
            }
        }

        public string LineName { get; set; }

        public string UpLine { get; set; }
        public string UpLineRunTime { get; set; }
        public List<GetLineDetailOutput.Station> UpLineStationList { get; set; }
        public string UpLineLastFocusStation { get; set; }

        public string DownLine { get; set; }
        public string DownLineRunTime { get; set; }
        public List<GetLineDetailOutput.Station> DownLineStationList { get; set; }
        public string DownLineLastFocusStation { get; set; }

        public GetArriveInfoInput.LineType LastLineType { get; set; }

        public BusLine ToBusLine()
        {
            return new BusLine
            {
                Name = LineName,
                RunTime = $"运营时间：{UpLineRunTime.Substring(5)},{DownLineRunTime.Substring(5)}",
                UpLine = UpLine.Substring(2),
                UpLineLastFocusStation = UpLineLastFocusStation,
                DownLine = DownLine.Substring(2),
                DownLineLastFocusStation = DownLineLastFocusStation,
                LastLineType = LastLineType
            };
        }

    }
}
