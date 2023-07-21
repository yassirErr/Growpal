using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model.ViewModels
{
    public class ContentVM
    {

        public Contentube Content { get; set; }


        [ValidateNever]
        public IEnumerable<SelectListItem> CountryListItem { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ServiceListItem { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryListItem { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ApplicationUserListItem { get; set; }


    }
}
