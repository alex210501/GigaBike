using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaBike {
    static class DateCalculator {
        enum DayWeekEnum {
            Monday = 1,
            Thursday,
            Wednesday,
            Thuesday,
            Friday,
            Saturday,
            Sunday
        }

        static public DateTime GetNextDay(DateTime today) {
            today.AddDays(1);

            if (IsWorkWeekDay(today) == false) today = GoToStartNextWeek(today);
            return today.AddDays(1);
        }

        static private int GetDayOfWeek(DateTime today) {
            return (today.DayOfWeek == 0) ? (int)DayWeekEnum.Sunday : (int)today.DayOfWeek;
        }

        static private bool IsWorkWeekDay(DateTime day) {
            int weekDay = GetDayOfWeek(day);

            return (weekDay != (int)DayWeekEnum.Saturday) && (weekDay != (int)DayWeekEnum.Sunday);
        }

        /*
         * Go to the start of the next week
         * Ex : Sunday - Friday + 1 = Monday
         */
        static private DateTime GoToStartNextWeek(DateTime day) {
            return day.AddDays((int)DayWeekEnum.Sunday - GetDayOfWeek(day) + 1);
        }
    }
}
