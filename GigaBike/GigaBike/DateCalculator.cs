using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;

namespace GigaBike {
    static class DateCalculator {
        private static readonly CultureInfo cultureInfo = new CultureInfo("es-ES", false);
        private static readonly Calendar calendar = new GregorianCalendar(); // cultureInfo.Calendar;

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


        /*
         * Go to the start of the next week
         * Ex : (7 - Friday + 1= % 7 = Monday
         */
        static public DateTime GoToStartOfNextWeek(DateTime day) {
            return GoToStartOfWeek(day.AddDays(7));
        }

        static public DateTime GoToStartOfWeek(DateTime day) {
            if (day.DayOfWeek == DayOfWeek.Sunday) return day.AddDays(-6);

            return day.AddDays(DayOfWeek.Monday - day.DayOfWeek);
        }

        static public DateTime GetDateFromWeekOfYear(int weekOfYear, int year) {
            DateTime firstDayOfYear = new DateTime(year, 1, 1);
            DateTime currentWeekDate = firstDayOfYear.AddDays(7 * weekOfYear);

            return GoToStartOfWeek(currentWeekDate);
        }

        static public DateTime GetNextWorkDayFromToday() {
            return GetNextDay(DateTime.Now.Date);
        }

        static public bool IsWorkWeekDay(DateTime day) {
            int weekDay = (int)day.DayOfWeek;

            return (weekDay != (int)DayOfWeek.Saturday) && (weekDay != (int)DayOfWeek.Sunday);
        }
    }
}
