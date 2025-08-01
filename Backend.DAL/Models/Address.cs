using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL.Models
{
    public class Address:BaseModel
    {
        [MaxLength(40)]
        [MinLength(5)]
        public string? FullName { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(85)]
        public string? City { get; set; }
        [MaxLength(200)]
        public string? Street { get; set; }
        [MaxLength(20)]
        public string? PostalCode { get; set; }
        [MaxLength(100)]
        public string? Country { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
