namespace Dashboard.Models
{
    public class MfpaymentModel
    {
        public List<MfpaymentDataModel> MfpaymentDataModel { get; set; }
    }
    public class MfpaymentDataModel
    {
        public string Orderid { get; set; }
        public string Razorpayid { get; set; }
        public string Clientid { get; set; }
        public string Ucid { get; set; }
        public DateTime Inserteddate { get; set; }
    }
}
