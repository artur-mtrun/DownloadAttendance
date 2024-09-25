using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadAttendance
{
    public static class Utils
    {
        public static void DownloadEvents()
        {
            var EventsList = GetEventsFromTerminals();
            EventsList = (List<Event>)EventsList.Where(e => e.event_date > DateTime.Now.AddDays(-35));
            Event.SavSaveEventsToDB(EventsList);

        }
        private static List<Event> GetEventsFromTerminals()
        {
            var Terminals = Parses.DataTableToList<Terminal>(Terminal.GetTerminals());
            List<Event> EventsList = new List<Event>();
            foreach (var t in (Terminals))
            {
                    List<Event> ev = Parses.TerminalToTerminalLZ(t).GetNewEventsFromTerminal();
                    EventsList.AddRange(ev);

            }
            return EventsList;
        }
    }
}
