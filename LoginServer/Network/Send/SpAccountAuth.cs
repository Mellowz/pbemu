using LoginServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.Send
{
    public class SpAccountAuth : ASendPacket
    {
        protected Account Account; 
        public SpAccountAuth(Account account)
        {
            Account = account;
        }

        protected internal override void Write()
        {
            for (int i = 0; i < 5; i++)
                WriteC(0);

            WriteH(24001);
            WriteH(1114);

            WriteD(0);

            WriteC((byte)Account.Name.Length);
            WriteS(Account.Name);
        }
    }
}
