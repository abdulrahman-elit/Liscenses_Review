using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using Guna.UI2.WinForms;

namespace DataLayer.EventLoger
{
    public static class EventLogger
    {
        private const string SourceName = "Lisence";
        private const string LogName = "Application";

        static EventLogger()
        {
            if (!EventLog.SourceExists(SourceName))
            {
                try
                {
                    EventLog.CreateEventSource(SourceName, LogName);

                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to create event source '{SourceName}'. Ensure you have the necessary permissions.", ex);
                }
            }
        }

        public static void LogInfo(string message, int eventId = 1000)
        {
            WriteToEventLog(message, EventLogEntryType.Information, eventId);
        }

        public static void LogWarning(string message, int eventId = 2000)
        {
            WriteToEventLog(message, EventLogEntryType.Warning, eventId);
        }

        public static void LogError(string message, Exception ex = null, int eventId = 3000)
        {
            string fullMessage = message;
            if (ex != null)
            {
                fullMessage += Environment.NewLine + "Exception: " + ex.ToString();
            }
            WriteToEventLog(fullMessage, EventLogEntryType.Error, eventId);
        }

        private static void WriteToEventLog(string message, EventLogEntryType type, int eventId)
        {
            try
            {
                EventLog.WriteEntry(SourceName, message, type, eventId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to write to event log '{LogName}' with source '{SourceName}'. Ensure you have the necessary permissions.", ex);
            }
        }
    }
}
