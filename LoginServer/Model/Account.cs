using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Model
{
    public class Account
    {
        //public int Uid;
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int Cash { get; set; }

        public string LastIpAddress { get; set; }
    }
}
