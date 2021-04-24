using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebApp.Models
{
    public class Receptionist : ApplicationUser
    {
        public string ReceptionistId { get; set; }
        [Required]
        public string Name { get; set; }    
    }
}
