using SQLite;
using WuhuBus.ApiSdk.Input;

namespace WuhuBus.Models
{
    public class BusLine
    {
        /// <summary>
        /// 线路名称
        /// </summary>
        [PrimaryKey]
        public string Name { get; set; }

        /// <summary>
        /// 运行时间
        /// </summary>
        public string RunTime { get; set; }

        /// <summary>
        /// 上行
        /// </summary>
        public string UpLine { get; set; }

        /// <summary>
        /// [上行] 上次使用的站台
        /// </summary>
        public string UpLineLastFocusStation { get; set; } 

        /// <summary>
        /// 下行
        /// </summary>
        public string DownLine { get; set; }

        /// <summary>
        /// [下行] 上次使用的站台
        /// </summary>
        public string DownLineLastFocusStation { get; set; }

        /// <summary>
        /// 上次使用的线路
        /// </summary>
        public GetArriveInfoInput.LineType LastLineType { get; set; }

        public string FormatName => $"{Name} ({UpLine} <-> {DownLine})";
    }
}
