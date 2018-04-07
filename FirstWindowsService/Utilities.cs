using System;
using System.IO;
using System.Diagnostics;

namespace FirstWindowsService
{
    /// <summary>
    /// Utilities class for write logs file
    /// </summary>
    public class Utilities
    {
        /// <summary>
        /// Writes the error log to text file
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteLogErrorToFile(string message)
        {
            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                streamWriter.WriteLine(DateTime.Now.ToString("g") + ": " + message);
                streamWriter.Flush();                       //Push stream to file
                streamWriter.Close();                       //Close stream
            }
            catch
            {
                //If write log to text file error, service will write log on Windows event
                WriteLogErrorToEventLog(DateTime.Now.ToString("g") + ": Can not write log file", 2);
            }
        }

        /// <summary>
        /// Writes the log error to event log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// 
        public static void WriteLogErrorToEventLog(string message, int status)
        {
            EventLog eventLog = null;

            eventLog = new EventLog("Application");
            eventLog.BeginInit();
            eventLog.Source = "Application";
            if (status == 1)
            {
                eventLog.WriteEntry(message, EventLogEntryType.Information, 101, 1);
            }
            else
            {
                eventLog.WriteEntry(message, EventLogEntryType.Error, 101, 1);
            }
            eventLog.Close();
        }
    }
}
