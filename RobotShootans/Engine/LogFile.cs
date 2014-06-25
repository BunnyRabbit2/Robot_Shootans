using System;
using System.IO;

namespace RobotShootans.Engine
{
    /// <summary>
    /// The type of Log to be entered into the LogFile
    /// </summary>
    public enum LogType
    {
        /// <summary>Error</summary>
        ERROR,
        /// <summary>Warning</summary>
        WARNING,
        /// <summary>Info</summary>
        INFO
    }

    /// <summary>
    /// A static class for writing to a LogFile
    /// </summary>
    public static class LogFile
    {
        /// <summary>
        /// Deletes the old log file.
        /// </summary>
        public static void ClearLogFile()
        {
            File.Delete("logFile.txt");
        }

        /// <summary>
        /// Logs a string without a line end in the format [DD/MM/YYYY][HH:MM::SS]: LOGTYPE - STRING
        /// </summary>
        /// <param name="stringToLog">The string to log in the file</param>
        /// <param name="logtype">The type of Log</param>
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

        /// <summary>
        /// Logs a string with a line end in the format [DD/MM/YYYY][HH:MM::SS]: LOGTYPE - STRING
        /// </summary>
        /// <param name="stringToLog">The string to log in the file</param>
        /// <param name="logtype">The type of Log</param>
        public static void LogStringLine(string stringToLog, LogType logtype = LogType.INFO)
        {
            LogString(stringToLog + Environment.NewLine);
        }

        /// <summary>
        /// Logs an int without a line end in the format [DD/MM/YYYY][HH:MM::SS]: LOGTYPE - STRING
        /// </summary>
        /// <param name="intToLog">The string to log in the file</param>
        /// <param name="logtype">The type of Log</param>
        public static void LogInt(int intToLog, LogType logtype = LogType.INFO)
        {
            LogString(intToLog.ToString());
        }

        /// <summary>
        /// Logs an int with a line end in the format [DD/MM/YYYY][HH:MM::SS]: LOGTYPE - STRING
        /// </summary>
        /// <param name="intToLog">The string to log in the file</param>
        /// <param name="logtype">The type of Log</param>
        public static void LogIntLine(int intToLog, LogType logtype = LogType.INFO)
        {
            LogStringLine(intToLog.ToString());
        }

        /// <summary>
        /// Logs a float without a line end in the format [DD/MM/YYYY][HH:MM::SS]: LOGTYPE - STRING
        /// </summary>
        /// <param name="floatToLog">The string to log in the file</param>
        /// <param name="logtype">The type of Log</param>
        public static void LogFloat(float floatToLog, LogType logtype = LogType.INFO)
        {
            LogString(floatToLog.ToString());
        }

        /// <summary>
        /// Logs a float with a line end in the format [DD/MM/YYYY][HH:MM::SS]: LOGTYPE - STRING
        /// </summary>
        /// <param name="floatToLog">The string to log in the file</param>
        /// <param name="logtype">The type of Log</param>
        public static void LogFloatLine(float floatToLog, LogType logtype = LogType.INFO)
        {
            LogStringLine(floatToLog.ToString());
        }
    }
}
