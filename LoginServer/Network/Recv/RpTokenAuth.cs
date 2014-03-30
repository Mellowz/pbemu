using Common.Utilities;
using LoginServer.Database;
using LoginServer.Network.Send;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network.Recv
{
    public class RpTokenAuth : ARecvPacket
    {
        protected string Token;

        protected internal override void Read()
        {
            Log.Debug("Unk1 = {0}", ReadH());
            Log.Debug("Unk2 = {0}", ReadC());
            Log.Debug("Unk3 = {0}", ReadH());
            Log.Debug("Unk4 = {0}", ReadH());
            Token = ReadS(ReadH());
            Log.Debug("Token = {0}", Token);
        }

        protected internal override void Run()
        {
            var account = AccountDatabase.GetInstance().GetAccountByToken(Token);

            if (account != null)
            {
                _Client.SendPacket(new SpAccountAuth(account));
                _Client.Send("0c002102000000000000000000000000".ToBytes());
                _Client.Send("02802850db30".ToBytes());
            }
            else
                Log.Warn("Account Doesn't exists...");
        }
    }
}
