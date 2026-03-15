using A2Template.Models;
using System;
using System.Text;

namespace A2Template.Helper
{
    public static class CalendarOutputFormatter
    {
        public static string FormatEventAsCalendar(Event ev)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:yzhu484");
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine($"UID:{ev.Id}");
            sb.AppendLine($"DTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"DTSTART:{ev.Start}");
            sb.AppendLine($"DTEND:{ev.End}");
            sb.AppendLine($"SUMMARY:{ev.Summary}");
            sb.AppendLine($"DESCRIPTION:{ev.Description}");
            sb.AppendLine($"LOCATION:{ev.Location}");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");
            return sb.ToString();
        }
    }
}