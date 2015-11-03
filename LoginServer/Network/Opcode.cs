using LoginServer.Network.Recv;
using LoginServer.Network.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network
{
    public class Opcode
    {
        public static Dictionary<short, Type> Recv = new Dictionary<short, Type>();
        public static Dictionary<Type, short> Send = new Dictionary<Type, short>();

        public static void Init()
        {
            #region Recv

            Recv.Add(unchecked((short)0x0A72), typeof(RpTokenAuth));

            #endregion

            #region Send

            Send.Add(typeof(SpInit), unchecked((short)0x0202));
            Send.Add(typeof(SpAccountAuth), unchecked((short)0x0A04));

            #endregion
        }
    }
}
