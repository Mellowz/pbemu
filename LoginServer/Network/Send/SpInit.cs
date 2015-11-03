using System.Net;
using Common.Utilities;

namespace LoginServer.Network.Send
{
    public class SpInit : ASendPacket
    {
        protected internal override void Write()
        {
            WriteH(unchecked((short)34152));
            WriteD(_Client._Crypt.GetId());
            WriteC(225);
            WriteC(2);
            WriteC(2);
            WriteC(12);
            WriteC(0);
            WriteH(16);
            WriteH(133);
            WriteH(128);
            // Unknown byte 128 byte
            WriteB("8D884CC363C7B11AA93DEC088A0AEDFE93797518580B108ABCA9E353F584C9920DA5755D98F9BBBCDCB16858CBD161B085696E83B6A4D8F072EDC3CEA3A362E15B4BBBC929E74AC4A16EB7784403125F1274976B27469FCBABEFD169DBF4767C5D6D74C11DAF6431CD02AD8E8FC5782F5B04462BF44FE4D4B3E0D283151E09B4".ToBytes());
            WriteH(1);
            WriteH(2577);
            WriteC(2);
            WriteC(1);
            WriteC(1);
            WriteC(1);
            WriteC(1);
            WriteC(1);
            WriteC(1);
            WriteC(1);
            WriteC(1);
            WriteC(4);
        }
    }
}
