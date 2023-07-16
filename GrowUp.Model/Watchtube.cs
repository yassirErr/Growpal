using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model
{
    public class Watchtube
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Content")]
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        [ValidateNever]
        public Contentube Content { get; set; }

        [Required]
        [Display(Name = "User")]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }

        public int ReactubeId { get; set; }
        [ForeignKey("ReactubeId")]
        [ValidateNever]
        public Reactube Reactube { get; set; }
    }

}
