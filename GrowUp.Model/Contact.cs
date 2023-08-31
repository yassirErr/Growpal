using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }

        public string? Subject { get; set; }
        [Required]
        [StringLength(100)]
        public string Message { get; set; }

    }
}
