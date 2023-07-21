using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }


        public string PostalCode{ get; set; }


        [Display(Name = "County Name")]
        public int CountyNameId { get; set; }
        [ForeignKey("CountyNameId")]
        [ValidateNever]
        public Country Country { get; set; }


    }
}
