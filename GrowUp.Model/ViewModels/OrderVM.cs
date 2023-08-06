
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.Model.ViewModels
{
    public class OrderVM
    {
      
        public IEnumerable <PayMonthlyPlan> PayMonthlyPlan { get; set; }
        public IEnumerable <OrderHeader> OrderList { get; set; }
        public IEnumerable <OrderDetail> OrderListDetail { get; set; }

    }
}
