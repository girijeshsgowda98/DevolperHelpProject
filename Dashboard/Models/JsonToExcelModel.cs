namespace Dashboard.Models
{
    public class JsonToExcelModel
    {
        public string message { get; set; }
        public bool status { get; set; }
        public string data { get; set; }
    }
    public class JsonToExcelTextModel
    {
        public string message { get; set; }
        public bool status { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public int totaluser { get; set; }
        public int iosusers { get; set; }
        public int androidusers { get; set; }
        public int otherusers { get; set; }
        public List<UserDetails> userdetails { get; set; }
    }

    public class UserDetails
    {
        public string _name { get; set; }
        public string _mobile { get; set; }
        public string ucc { get; set; }
        public string clienttype { get; set; }
        public int userlogincount { get; set; }
        public List<Userloggeddevicedetails> userloggeddevicedetails { get; set; }
    }

    public class Userloggeddevicedetails
    {
        public string eventtime { get; set; }
        public string userip { get; set; }
        public string deviceinfo { get; set; }
        public string model { get; set; }
        public string uuid { get; set; }
    }
    
}
public class ClientModel
{
    public string? ClientTypeSum { get; set; }
    public int? LoginCountSum { get; set; }
}

