using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelWepApp2.Models;

namespace HotelWepApp2.ViewModel
{
    public class WaiterViewModel
    {
        public Guest Guest { get; set; }
        public Buffet Buffet { get; set; }
        public DateTime Date { get; set; }
    }
}
