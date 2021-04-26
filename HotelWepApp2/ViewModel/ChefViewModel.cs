using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWepApp2.ViewModel
{
    public class ChefViewModel
    {
        public DateTime Date { get; set; }
        public int GuestCount { get; set; }
        public int AdultCount { get; set; }
        public int KidCount { get; set; }
        public int AdultCheckInCount { get; set; }
        public int KidCheckInCount { get; set; }
        public int CheckInCount { get; set; }
        public int NotCheckInCount { get; set; }
    }
}
