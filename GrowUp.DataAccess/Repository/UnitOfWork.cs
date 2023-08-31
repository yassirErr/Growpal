using GrowUp.DataAccess.Data;
using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            Service = new ServiceRepository(db);
            Content = new ContentubeRepository(db);
            Country = new CountryRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
            Reactube = new ReactubeRepository(db);
            Watchtube = new WatchtubeRepository(db);
            PayMonthlyPlan = new PricingPlanRepository(db);
            OrderHeader = new OrderHeaderRepository(db);
            OrderDetail = new OrderDetailRepository(db);
            Contact = new ContactRepository(db); 


        }

        public ICategoryRepository Category { get; private set;}
        public IServiceRepository Service { get; private set; }

        public IContentubeRepository Content { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IReactubeRepository Reactube { get; private set; }
        public IWatchtubeRepository Watchtube { get; private set; }
        public IPricingPlanRepository PayMonthlyPlan { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public IContactRepository Contact { get; private set; }



        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
