using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Service Name")]
        public string ServiceName { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
