using MongoDB.Bson;

namespace LoginServer.Model
{
    public class Account
    {
        //public int Uid;
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Cash { get; set; }

        public string LastIpAddress { get; set; }
    }
}
