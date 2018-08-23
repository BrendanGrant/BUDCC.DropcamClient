using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace BUDCC.DropcamClient
{
    internal class Logger
    {
        public static void Log(string s)
        {
            string outputLine = string.Format("{0} {1}: {2}", GetTimestamp(), GetThreadInfo(), s);
            Debug.WriteLine(outputLine);
        }

        private static string GetThreadInfo()
        {
#if WINDOWS_PHONE
            return System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
#else
            return Environment.CurrentManagedThreadId.ToString();
#endif
        }

        private static string GetTimestamp()
        {
            return "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]";
        }

        public static void Log(string format, params object[] args)
        {
            Log(string.Format(format, args));
        }

        public static void Log(Exception ex, [CallerMemberName] string memberName = "")
        {
            Log($"{memberName}() Exception - ", ex.ToString());
        }
    }
}
