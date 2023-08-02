using GrowUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository.IRepository
{
    public interface IPricingPlanRepository : IRepository<PayMonthlyPlan>
    {
        void Update(PayMonthlyPlan obj);
    }
}
