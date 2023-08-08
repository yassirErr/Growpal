using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model
{
    public class PayMonthlyPlan
    {
        public int Id { get; set; }

        public double PriceMonthly { get; set; }
        public string? ContactMethode { get; set; }
        public string? Notes { get; set; }
        public string? SelectedOption { get; set; }
        public string? Period { get; set; }

    }
}
