using System;
using System.IO;

namespace DownloadAttendance
{
    class Log
    {
        internal static void UpdateLog(string message)
        {
            DateTime currentTime = DateTime.Now;

            if (!File.Exists("rcplog.txt"))
                File.Create("rcplog.txt").Dispose();
            string msg = currentTime + " -- " + message;
            StreamWriter s = File.AppendText("rcplog.txt");
            s.WriteLine(msg);
            s.Close();
        }
    }
}
