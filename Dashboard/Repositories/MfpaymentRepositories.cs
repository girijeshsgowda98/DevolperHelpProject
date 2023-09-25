using Dashboard.DbService;
using Dashboard.Models;

namespace Dashboard.Repositories
{
    public class MfpaymentRepositories
    {
        List<MfpaymentDataModel> mfpayment = new List<MfpaymentDataModel>();
        public List<MfpaymentDataModel> MfpaymentImp(NpgsqlDbService dbService)
        {
            var sql = "SELECT orderid, razorpayid, clientid, ucid, status, remark, inserteddate, request, response, dionrequest, dionresponse, apiname FROM usermaster.mf_payment_logs limit 10;";
            var reader = dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                mfpayment.Add(new MfpaymentDataModel
                {
                    Orderid = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                    
                    Razorpayid = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                    Clientid = reader.IsDBNull(2) ? "Unknown" : reader.GetString(2),
                    Ucid = reader.IsDBNull(2) ? "Unknown" : reader.GetString(3),
                    Inserteddate = reader.GetDateTime(6)
                });
            }
            var result = mfpayment
               .GroupBy(r => r.Orderid)
               .Select(group => new MfpaymentDataModel
               {
                   Orderid = group.Key,
                   Razorpayid = group.Select(r => r.Razorpayid).First(),
                   Clientid = group.Select(r => r.Clientid).First(),
                   Ucid = group.Select(r => r.Ucid).First(),
                   Inserteddate = group.Select(r => r.Inserteddate).First()
               }).OrderByDescending(r => r.Inserteddate)
               .ToList();
            return result;
        }
    }
}
