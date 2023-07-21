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
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private AppDbContext _db;

        public CountryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Country obj)
        {
            _db.Countries.Update(obj);
        }
    }

}
