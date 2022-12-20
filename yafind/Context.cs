
namespace yafind
{
    public class Context: IDisposable
    {
        public string Plain = "";
        public string Salt = "";
        private bool disposedValue;

        //Static constructor
        static Context()
        {
            
        }

        public static string md5(string plain, string salt)
        {
            return "ABCDEF";
        }

        public static string md5(string value)
        {
            return $"0{value}0";
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
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
