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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
            Service = new ServiceRepository(db);
            Content = new ContentubeRepository(db);
            ApplicationUser = new ApplicationUserRepository(db);
            Reactube = new ReactubeRepository(db);
        }

        public ICategoryRepository Category { get; private set;}
        public IServiceRepository Service { get; private set; }


        public IContentubeRepository Content { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IReactubeRepository Reactube { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
