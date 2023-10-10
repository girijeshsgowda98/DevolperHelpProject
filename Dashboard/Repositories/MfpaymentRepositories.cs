using Dashboard.DbService;
using Dashboard.Models;

namespace Dashboard.Repositories
{
    public class MfpaymentRepositories
    {
        List<MfpaymentDataModel> mfpayment = new List<MfpaymentDataModel>();
        public List<MfpaymentDataModel> MfpaymentImp(NpgsqlDbService dbService)
        {
            var sql = "SELECT id, clientid, ucid,orderid,apiname, inserteddate, status, remark, request, response, dionrequest, dionresponse FROM usermaster.mf_payment_logs limit 1000;";
            var reader = dbService.ExecuteQuery(sql);

            while (reader.Read())
            {
                mfpayment.Add(new MfpaymentDataModel
                {
                    Id = reader.GetInt32(0),
                    Clientid = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                    Ucid = reader.IsDBNull(2) ? "Unknown" : reader.GetString(2),
                    Orderid = reader.IsDBNull(3) ? "Unknown" : reader.GetString(3),
                    Apiname = reader.IsDBNull(4) ? "Unknown" : reader.GetString(4),
                    Inserteddate = reader.GetDateTime(5),
                    Status = reader.IsDBNull(6) ? "Unknown" : reader.GetString(6),
                    Remark = reader.IsDBNull(7) ? "Unknown" : reader.GetString(7),
                    Request = reader.IsDBNull(8) ? "Unknown" : reader.GetString(8),
                   Response = reader.IsDBNull(9) ? "Unknown" : reader.GetString(9),
                    Dionrequest = reader.IsDBNull(10) ? "Unknown" : reader.GetString(10),
                    Dionresponse = reader.IsDBNull(11) ? "Unknown" : reader.GetString(11),
                });
            }
            /*var result = mfpayment
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
            return result;*/
            return mfpayment;
        }
    }
}
