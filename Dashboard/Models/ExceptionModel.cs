namespace Dashboard.Models
{
    public class ExceptionModel
    {
        public List<ExceptionsDataModel> ExceptionsDataModel { get; set; }
    }

    public class ExceptionsDataModel
    {
        public string? moduleName { get; set; }
        public string? controlName { get; set; }
        public string? actionName { get; set; }
        public DateTime requestedon { get; set; }
        public DateTime responseon { get; set; }
        public int? TotalExceptions { get; set; }
    }
}
