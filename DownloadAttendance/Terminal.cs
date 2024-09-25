using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadAttendance
{
    public class Terminal
    {
        public int machine_id { get; set; }
        public int machinenumber { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public string descript { get; set; }
        public int  area_id { get; set; }

        public static DataTable GetTerminals()
        {
            return DBConnection.GetTerminalsFromDB();
        }

    }
}
