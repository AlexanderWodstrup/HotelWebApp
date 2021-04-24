using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebApp.Models
{
    public class Chef : ApplicationUser
    {
        [Required]
        public string Name { get; set; }
    }
}
