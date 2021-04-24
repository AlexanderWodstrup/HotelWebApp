using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWepApp2.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public List<Guest> Guests { get; set; }
    }
}
