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
    public class ReactubeRepository : Repository<Reactube>, IReactubeRepository
    {
        private AppDbContext _db;

        public ReactubeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Reactube obj)
        {
            _db.Reactubes.Update(obj);
        }
    }


}
