using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;

namespace GigaBike {
    static class DateCalculator {
        private static readonly CultureInfo cultureInfo = new CultureInfo("en-US");
        private static readonly Calendar calendar = cultureInfo.Calendar;

        static public DateTime GetNextDay(DateTime today) {
            today = today.AddDays(1);

            if (IsWorkWeekDay(today) == false) today = GoToStartOfNextWeek(today);
            return today;
        }

        static public int GetWeekOfYear(DateTime day) {
            CalendarWeekRule calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            return calendar.GetWeekOfYear(day, calendarWeekRule, firstDayOfWeek);
        }

        static private bool IsWorkWeekDay(DateTime day) {
            int weekDay = (int)day.DayOfWeek;

            return (weekDay != (int)DayOfWeek.Saturday) && (weekDay != (int)DayOfWeek.Sunday);
        }

        /*
         * Go to the start of the next week
         * Ex : (7 - Friday + 1= % 7 = Monday
         */
        static private DateTime GoToStartOfNextWeek(DateTime day) {
            return day.AddDays((7 - (int)day.DayOfWeek + 1) % 7);
        }
    }
}
