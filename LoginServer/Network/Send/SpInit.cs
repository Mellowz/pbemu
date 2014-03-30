using System.Net;

namespace LoginServer.Network.Send
{
    public class SpInit : ASendPacket
    {
        protected internal override void Write()
        {
            WriteD(_Client._Crypt.GetId());
            WriteB(IPAddress.Parse(_Client._account.LastIpAddress).GetAddressBytes());
            WriteH(29890);
            WriteH(31146);

            for (int index = 0; index < 10; ++index)
                WriteC(1);

            WriteC(1);
            WriteD(1); // server count

            for (int index = 0; index < 1; ++index)
            {
                WriteD(1);
                WriteB(IPAddress.Parse("127.0.0.1").GetAddressBytes());
                WriteH(unchecked((short)39191)); // Port
                WriteC(1); // Unk
                WriteH(300); // Max user
                WriteD(0); // Current User
            }

            /*WriteC(1);
            for (int index = 0; index < 12; ++index)
                WriteC(0);*/
        }
    }
}
