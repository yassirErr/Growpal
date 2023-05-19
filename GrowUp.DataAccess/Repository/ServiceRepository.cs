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
    public class ServiceRepository : Repository<Service> , IServiceRepository
    {
        private AppDbContext _db;

        public ServiceRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Service obj)
        {
            _db.Services.Update(obj);
        }
    }
}
