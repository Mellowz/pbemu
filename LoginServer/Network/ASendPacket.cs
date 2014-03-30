using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Network
{
    public abstract class ASendPacket
    {
        private MemoryStream mstream = new MemoryStream();

        public Client _Client;

        public long Length
        {
            get
            {
                return this.mstream.Length;
            }
        }

        protected internal void WriteB(byte[] value)
        {
            this.mstream.Write(value, 0, value.Length);
        }

        protected internal void WriteD(int value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteH(short val)
        {
            WriteB(BitConverter.GetBytes(val));
        }

        protected internal void WriteC(byte value)
        {
            this.mstream.WriteByte(value);
        }

        protected internal void WriteF(double value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteQ(long value)
        {
            WriteB(BitConverter.GetBytes(value));
        }

        protected internal void WriteS(string value)
        {
            if (value != null)
                WriteB(Encoding.Default.GetBytes(value));
            WriteH((short)0);
        }

        protected internal void WriteS(string name, int count)
        {
            if (name == null)
                return;
            WriteB(Encoding.Default.GetBytes(name));
            WriteB(new byte[count - name.Length]);
        }

        public byte[] ToByteArray()
        {
            return this.mstream.ToArray();
        }

        protected internal abstract void Write();
    }
}
