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
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private AppDbContext _db;

        public ContactRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Contact obj)
        {
            _db.Contacts.Update(obj);
        }
    }

}
