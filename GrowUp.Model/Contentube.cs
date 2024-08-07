﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model
{
    public class Contentube
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Content name")]
        public string Content_name { get; set; }



        [StringLength(500)]
        public string? Description { get; set; }


        [Required(ErrorMessage = "Content URL is required")]
        [Display(Name = "Content link")]
    
        public string Content_Url { get; set; }

        [Required]
        [Display(Name = "Category type")]
        public int Category_typeId { get; set; }
        [ForeignKey("Category_typeId")]
        [ValidateNever]
        public Category Category { get; set; }

    
        [Required]
        [Display(Name = "Service type")]
        public int Service_typeId { get; set; }
        [ForeignKey("Service_typeId")]
        [ValidateNever]
        public Service Service { get; set; }


        [Display(Name = "Country name")]
        public int Country_nameId { get; set; }
        [ValidateNever]
        [ForeignKey("Country_nameId")]
        public Country Country { get; set; }


        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public bool StatusContent { get; set; }

    }
}
