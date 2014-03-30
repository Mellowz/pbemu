using Common.Utilities;

namespace LoginServer.Crypt
{
    public class LoginCrypt
    {
        /// <summary>
        /// 
        /// </summary>
        protected int _Key = 29890;

        /// <summary>
        /// 
        /// </summary>
        protected int CRYPT_KEY = -1;

        public int GetId()
        {
            return GetHashCode() / 4096;
        }

        public int GetKey()
        {
            return _Key;
        }

        public void SetKey(int key)
        {
            _Key = key;
        }

        public int GetShift()
        {
            return CRYPT_KEY;
        }

        public void SetShift(int shift)
        {
            CRYPT_KEY = shift;
        }

        public byte[] Decrypt(byte[] data)
        {
            int id = GetId();
            int cryptKey = GetKey();
            int shift = GetShift();

            if (shift <= 0)
            {
                shift = ((id + cryptKey) % 7) + 1;
                SetShift(shift);
                Log.Debug("NEW key: {0}", shift);
            }

            Log.Debug("id: " + id + "; cryptKey: " + cryptKey + "; shift: " + shift);
            data = BitRotate.Decrypt(data, shift);

            return data;
        }
    }
}
