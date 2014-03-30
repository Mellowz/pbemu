using Common.Utilities;
using LoginServer.Crypt;
using LoginServer.Model;
using LoginServer.Network.Send;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LoginServer.Network
{
    public class Client
    {
        public EndPoint _address;
        public TcpClient _client;
        public NetworkStream _stream;
        private byte[] _buffer;

        public Account _account;
        public LoginCrypt _Crypt;

        public int CRYPT_KEY { get; set; }

        public Client(TcpClient tcpClient)
        {
            _client = tcpClient;
            _stream = tcpClient.GetStream();
            _address = tcpClient.Client.RemoteEndPoint;
            _account = new Account();
            _account.LastIpAddress = tcpClient.Client.RemoteEndPoint.ToString().Split(':')[0];
            _Crypt = new LoginCrypt();

            new Thread(new ThreadStart(init)).Start();
            new Thread(new ThreadStart(read)).Start();
        }

        private void init()
        {
            SendPacket(new SpInit());
        }

        private void read()
        {
            try
            {
                if (this._stream == null || !this._stream.CanRead)
                    return;

                this._buffer = new byte[2];
                this._stream.BeginRead(_buffer, 0, 2, new AsyncCallback(OnReceiveCallbackStatic), (object)null);
            }
            catch (Exception ex)
            {
                Log.ErrorException("[Login]: read() Exception", ex);
                close();
            }
        }

        private void OnReceiveCallbackStatic(IAsyncResult ar)
        {
            try
            {
                if (this._stream.EndRead(ar) <= 0)
                    return;

                byte num = this._buffer[0];
                if (this._stream.DataAvailable)
                {
                    this._buffer = new byte[(int)num + 2];
                    this._stream.BeginRead(this._buffer, 0, (int)num + 2, new AsyncCallback(OnReceiveCallback), ar.AsyncState);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException(string.Format("[Login]: {0} was closed by force", this._address), ex);
                close();
            }
        }

        private void OnReceiveCallback(IAsyncResult ar)
        {
            this._stream.EndRead(ar);
            byte[] data = new byte[this._buffer.Length];
            this._buffer.CopyTo((Array)data, 0);

            if (data.Length >= 2)
                handlePacket(_Crypt.Decrypt(data));

            new Thread(new ThreadStart(this.read)).Start();
        }

        private void handlePacket(byte[] Data)
        {
            short opcode = BitConverter.ToInt16(new byte[2] { Data[0], Data[1] }, 0);

            if(Opcode.Recv.ContainsKey(opcode))
            {
                ((ARecvPacket)Activator.CreateInstance(Opcode.Recv[opcode])).execute(this, Data);
            }
            else
            {
                string opCodeLittleEndianHex = BitConverter.GetBytes(opcode).ToHex();
                Log.Debug("Unknown Opcode: 0x{0}{1} [{2}]",
                                 opCodeLittleEndianHex.Substring(2),
                                 opCodeLittleEndianHex.Substring(0, 2),
                                 Data.Length);

                Log.Debug("Data:\n{0}", Data.FormatHex());
            }
        }

        private void close()
        {
            ClientManager.GetInstance().RemoveClient(this);
            this._stream.Dispose();
        }

        public void SendPacket(ASendPacket packet)
        {
            packet._Client = this;

            if (!Opcode.Send.ContainsKey(packet.GetType()))
            {
                Log.Warn("UNKNOWN packet opcode: {0}", packet.GetType().Name);
                return;
            }

            try
            {
                packet.WriteH(0); // packet len
                packet.WriteH(Opcode.Send[packet.GetType()]); // opcode
                packet.Write();

                byte[] Data = packet.ToByteArray();
                BitConverter.GetBytes((short)(Data.Length - 4)).CopyTo(Data, 0);

                Log.Debug("Send: {0}", Data.FormatHex());
                this._stream.BeginWrite(Data, 0, Data.Length, new AsyncCallback(EndSendCallBackStatic), (object)null);
            }
            catch (Exception ex)
            {
                Log.Warn("Can't send packet: {0}", GetType().Name);
                Log.WarnException("ASendPacket", ex);
                return;
            } 
        }

        public void Send(byte[] buff)
        {
            try
            {
                Log.Debug("Send: {0}", buff.FormatHex());
                this._stream.BeginWrite(buff, 0, buff.Length, new AsyncCallback(EndSendCallBackStatic), (object)null);
            }
            catch (Exception ex)
            {
                Log.Warn("Can't send packet: {0}", GetType().Name);
                Log.WarnException("ASendPacket", ex);
                return;
            } 
        }

        private void EndSendCallBackStatic(IAsyncResult ar)
        {
            try
            {
                this._stream.EndWrite(ar);
                this._stream.Flush();
            }
            catch (Exception ex)
            {
                Log.WarnException("EndSendCallBackStatic", ex);
            }
        }
    }
}
