using Android.Database;
using Java.IO;

namespace CrossProcessPreferences
{
    // NOTE: This class is focussed on InputStream, OutputStream, Reader and
    // Writer. Each method should take at least one of these as a parameter,
    // or return one of them.
    public class IOUtils
    {
        public static void CloseQuietly(InputStream @is)
        {
            if (@is != null)
            {
                try
                {
                    @is.Close();
                }
                catch (IOException)
                {
                    // ignore
                }
            }
        }

        public static void CloseQuietly(OutputStream os)
        {
            if (os != null)
            {
                try
                {
                    os.Close();
                }
                catch (IOException)
                {
                    // ignore
                }
            }
        }

        public static void CloseQuietly(Reader r)
        {
            if (r != null)
            {
                try
                {
                    r.Close();
                }
                catch (IOException)
                {
                    // ignore
                }
            }
        }

        public static void CloseQuietly(ICursor cursor)
        {
            if (cursor != null && !cursor.IsClosed)
            {
                cursor.Close();
            }
        }
    }
}
