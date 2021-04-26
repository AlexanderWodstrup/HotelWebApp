using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWepApp2.Models
{
    public class Buffet
    {
        public int BuffetId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string GuestType { get; set; }
        public List<Guest> Guest { get; set; }

    }
}
