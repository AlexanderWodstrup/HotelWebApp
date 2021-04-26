using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWepApp2.Models
{
    public class Guest
    {
        public int GuestId { get; set; }
        public string Type { get; set; }
        public Room Room { get; set; }
        public bool BuffetCheckIn { get; set; }
        public List<Buffet> Buffet { get; set; }

    }
}
