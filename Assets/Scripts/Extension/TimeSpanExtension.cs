using System;

public static class TimeSpanExtension
{
    public static bool IsNegative(this TimeSpan timeSpan) => (timeSpan.TotalDays < 0 || timeSpan.TotalHours < 0 || 
         timeSpan.TotalMinutes < 0 || timeSpan.TotalSeconds< 0);
}
