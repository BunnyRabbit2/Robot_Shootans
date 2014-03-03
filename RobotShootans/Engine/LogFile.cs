using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RobotShootans.Engine
{
    public enum LogType
    {
        ERROR, WARNING, INFO,
    }

    public static class LogFile
    {
        public static void ClearLogFile()
        {
            File.Delete("logFile.txt");
        }

        public static void LogString(string stringToLog, LogType logtype = LogType.INFO)
        {
            string stringOut = "[" + DateTime.Now.ToShortDateString() + "][" + DateTime.Now.ToLongTimeString() + "]: ";
            switch(logtype)
            {
                case LogType.INFO:
                    stringOut += "INFO - ";
                    break;
                case LogType.ERROR:
                    stringOut += "ERROR - ";
                    break;
                case LogType.WARNING:
                    stringOut += "WARNING - ";
                    break;
            }

            stringOut += stringToLog;

            File.AppendAllText("logFile.txt", stringOut);
        }

        public static void LogStringLine(string stringToLog, LogType logtype = LogType.INFO)
        {
            LogString(stringToLog + Environment.NewLine);
        }

        public static void LogInt(int intToLog, LogType logtype = LogType.INFO)
        {
            LogString(intToLog.ToString());
        }

        public static void LogIntLine(int intToLog, LogType logtype = LogType.INFO)
        {
            LogStringLine(intToLog.ToString());
        }

        public static void LogFloat(float floatToLog, LogType logtype = LogType.INFO)
        {
            LogString(floatToLog.ToString());
        }

        public static void LogFloatLine(float floatToLog, LogType logtype = LogType.INFO)
        {
            LogStringLine(floatToLog.ToString());
        }
    }
}
