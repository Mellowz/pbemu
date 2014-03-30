using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Crypt
{
    public class BitRotate
    {
        public static byte[] Decrypt(byte[] data, int shift)
        {
            byte lastElement = data[data.Length - 1];
            for (int i = data.Length - 1; i > 0; i--)
            {
                data[i] = (byte)(((data[i - 1] & 0xFF) << (8 - shift)) | ((data[i] & 0xFF) >> shift));
            }
            data[0] = (byte)((lastElement << (8 - shift)) | ((data[0] & 0xFF) >> shift));
            return data;
        }
    }
}
