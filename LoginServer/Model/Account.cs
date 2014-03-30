using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Model
{
    public class Account
    {
        public int Uid;
        public string Name;
        public string Password;
        public string Token;
        public int Cash;

        public string LastIpAddress;
    }
}
