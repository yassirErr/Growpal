using GrowUp.DataAccess.Data;
using GrowUp.DataAccess.Repository.IRepository;
using GrowUp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository
{


    public class WatchtubeRepository : Repository<Watchtube>, IWatchtubeRepository
    {
        private AppDbContext _db;

        public WatchtubeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public int Count(Expression<Func<Watchtube, bool>> filter = null)
        {
            IQueryable<Watchtube> query = _db.Set<Watchtube>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }


        public void Update(Watchtube obj)
        {
            _db.Watchtubes.Update(obj);
        }
    }


}
