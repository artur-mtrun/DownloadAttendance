using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadAttendance
{
    public class Event
    {
        public int event_id { get; set; }
        public string enrollnumber { get; set; }
        public DateTime event_date { get; set; }
        public string event_time { get; set; }
        public int Verifymode { get; set; }
        public int in_out { get; set; }
        public int Workcode { get; set; }
        public int machinenumber { get; set; }


        public Event()
        {

        }
        public Event(int deviceNumber, string enrollNumber, int verifyMode, int inOutMode,
    string year, string month, string day, string hour, string minute, string second, int workCode)
        {
            this.machinenumber = deviceNumber;
            this.enrollnumber = enrollNumber;
            this.Verifymode = verifyMode;
            this.in_out = inOutMode;
            this.event_date = LZToDateTime(year, month, day,
             hour, minute, second);
            this.Workcode = workCode;
            this.event_time = hour+":"+minute+":"+second;
        }
        private DateTime LZToDateTime(string year, string month, string day,
            string hour, string minute, string second)
        {
            var sDatetime = year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;

            return DateTime.ParseExact(sDatetime, "yyyy-MM-dd HH:mm:ss", null);
        }
        public bool AddEventToDB()
        {
            var cr_date= DateTime.Now;
           
            var query = "INSERT INTO public.events" +
            "( \"enrollnumber\", \"event_date\", \"in_out\", \"event_time\", \"machinenumber\", \"createdAt\", \"updatedAt\") " +
                "VALUES( '" + enrollnumber + "', '" + event_date + "', "  + in_out + ", '"
                + event_time + "', " + machinenumber + ",'"+cr_date+"','"+cr_date+ "');";

            if(DBConnection.RunDBQuery(query))
                return true;

            Log.UpdateLog("Błąd w wykonaniu zapytania: " + query);
            return false;
        }
        public static void SavSaveEventsToDB(List<Event> EventsList)
        {
            foreach (var E in EventsList)
            {
                E.AddEventToDB();
            }
            Log.UpdateLog("Zakończono blok zapisu zdarzeń do bazy");
        }
    }
}
