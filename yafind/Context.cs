using System.Security.Cryptography;
using System.Text;

namespace yafind
{
    //Create a custom type so that we can use the + operator against byte arrays
    public readonly struct Bytes
    {
        private readonly byte[] _bytes;

        public Bytes(byte[] value)
        {
            _bytes = value;
        }

        public byte[] GetValue()
        {
            return _bytes;
        }

        public static Bytes operator +(Bytes a, Bytes b)
        {
            var first = a.GetValue();
            var second = b.GetValue();

            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return new Bytes(ret);
        }

        public static Bytes operator +(Bytes a, string b)
        {
            var first = a.GetValue();
            var second = Encoding.UTF8.GetBytes(b);

            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return new Bytes(ret);
        }

        public static Bytes operator +(string a, Bytes b)
        {
            var first = Encoding.UTF8.GetBytes(a); 
            var second = b.GetValue();

            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return new Bytes(ret);
        }
    }

    public class Context: IDisposable
    {
        public string Plain = "";
        public string Salt = "";

        private static readonly MD5 _md5;

        private bool disposedValue;

        //Static constructor
        static Context()
        {
            _md5 = MD5.Create();
        }

        //We use lowercase so that our provided function will compile correctly
        public static Bytes md5(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return new Bytes(_md5.ComputeHash(bytes));
        }

        public static Bytes md5(Bytes value)
        {
            return new Bytes(_md5.ComputeHash(value.GetValue()));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //dispose managed state (managed objects)
                    _md5.Dispose();
                }

                //free unmanaged resources (unmanaged objects) and override finalizer
                //set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
