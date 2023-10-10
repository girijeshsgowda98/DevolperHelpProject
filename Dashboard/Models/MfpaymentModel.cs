namespace Dashboard.Models
{
    public class MfpaymentModel
    {
        public List<MfpaymentDataModel> MfpaymentDataModel { get; set; }
    }
    public class MfpaymentDataModel
    {
        public int Id { get; set; }
        public string Clientid { get; set; }
        public string Ucid { get; set; }
        public string Orderid { get; set; }
        public string Apiname { get; set; }
        public DateTime Inserteddate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Dionrequest { get; set; }
        public string Dionresponse { get; set; }
       }
}
