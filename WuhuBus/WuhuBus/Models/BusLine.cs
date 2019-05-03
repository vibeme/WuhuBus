using SQLite;

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
        /// 下行
        /// </summary>
        public string DownLine { get; set; }


        public string FormatName => $"{Name} ({UpLine} <-> {DownLine})";
    }
}
