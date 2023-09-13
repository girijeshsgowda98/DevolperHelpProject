namespace Dashboard.Models
{
    public class FilterModel
    {
       public string type { get; set; }
       public string moduleName { get; set; }
       public string input { get; set; }

        }
    public class FilterListModel
    {
        public List<RequestDataModel> RequestDataModel { get; set; }
        public List<ExceptionsDataModel> ExceptionsDataModel { get; set; }

    }

}

