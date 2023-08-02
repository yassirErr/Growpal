using GrowUp.DataAccess.Data;
using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository
{
    public class PricingPlanRepository : Repository<PayMonthlyPlan>, IPricingPlanRepository
    {
        private AppDbContext _db;

        public PricingPlanRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PayMonthlyPlan obj)
        {
            _db.PayMonthlyPlans.Update(obj);
        }
    }

}
