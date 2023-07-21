using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model
{
    public class Reactube
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Content link")]
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        [ValidateNever]
        public Contentube Content { get; set; }

        [Required]
        [Display(Name = "Video Item")]
        public string ItemVideo { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public bool Status { get; set; }

    }
}
