using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadAttendance
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.UpdateLog("********** START skryptu pobierającego **********");
            Utils.DownloadEvents();
            Log.UpdateLog("**** ZAKOŃCZENIE skryptu pobierającego ****");
        }
    }
}
