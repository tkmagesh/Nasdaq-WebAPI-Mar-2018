using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtils
{
    public class Greeter
    {
        private IDateTimeService dateTimeService;

        public Greeter(IDateTimeService dateTimeService)
        {
            this.dateTimeService = dateTimeService;
        }
        public string Greet(string userName)
        {
            if (this.dateTimeService.GetCurrent().Hour < 12)
            {
                return string.Format("Hi {0}, Good Morning!", userName);
            }
            return string.Format("Hi {0}, Good Afternoon!", userName);
        }
    }
}
