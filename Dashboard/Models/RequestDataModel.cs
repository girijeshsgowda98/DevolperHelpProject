using System.Data;

namespace Dashboard.Models
{
    public class RequestModel
    {
        public List<RequestDataModel> RequestDataModel { get; set; }
        public List<RequestListModel> RequestListModel { get; set; }
    }
    public class RequestDataModel
    {
        public string? moduleName { get; set; }
        public string? controlName { get; set; }
        public string? actionName { get; set; }
        public DateTime requestedon { get; set; }
        public DateTime responseon { get; set; }
        public int? TotalRequest { get; set; }
        public double AverageTime { get; set; }
    }

    public class RequestListModel
    {
        public string? moduleName { get; set; }
        public string? controlName { get; set;}
        public string? actionName { get; set; }
        public DateTime requestedon { get; set; }
        public DateTime responseon { get; set; }
    }
}
