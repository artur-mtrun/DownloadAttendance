using System.Collections.Generic;

namespace DownloadAttendance
{
    public class TerminalLZ
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
        public string Descript { get; set; }
        public bool Restart()
        {
            LibZkem MyZkem = new LibZkem();
            bool isConnected = MyZkem.Connect_Net(Ip, Port);
            if (!isConnected)
                return false;
            //restart terminal
            MyZkem.RestartDevice(Number);
            return true;
        }
        internal List<Event> GetNewEventsFromTerminal()
        {
            List<Event> Events = new List<Event>();
            #region variables
            LibZkem MyZkem = new LibZkem();
            int machineNumber = Number;
            string enrollNumber = "";
            int verifyMode = 0;
            int inOutMode = 0;
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            int second = 0;
            int workCode = 0;
            int errorCode = 0;

            #endregion
            bool isConnected = MyZkem.Connect_Net(Ip, Port);
            if (isConnected)
            {
                Log.UpdateLog("Połączono z terminalem: " + Number + " " + Descript);
                MyZkem.EnableDevice(machineNumber, false);//disable the device


                while (MyZkem.SSR_GetGeneralLogData(machineNumber, out enrollNumber, out verifyMode,
                       out inOutMode, out year, out month, out day, out hour, out minute, out second, ref workCode))//get records from the memory
                {
                    Events.Add(new Event(machineNumber, enrollNumber, verifyMode, inOutMode,
                        ConvertTimeToString(year), ConvertTimeToString(month), ConvertTimeToString(day),
                        ConvertTimeToString(hour), ConvertTimeToString(minute), ConvertTimeToString(second), workCode));
                }
            }
            else
            {
                Log.UpdateLog("Nieudana próba łączenia z terminalem: " + Number + " " + Descript);
                return null;
            }
            MyZkem.EnableDevice(machineNumber, true);
            MyZkem.Disconnect();
            Log.UpdateLog("Rozłączono z terminalem: " + Number + " " + Descript);
            this.Restart();
            Log.UpdateLog("Restart terminala: " + Number + " " + Descript);
            return Events;
        }
        private static string ConvertTimeToString(int time)
        {
            string converted = time.ToString();
            if (converted.Length == 1)
                return "0" + converted;
            return converted;
        }
    }
}
