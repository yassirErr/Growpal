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
    public class ContentubeRepository : Repository<Contentube>, IContentubeRepository
    {
        private AppDbContext _db;

        public ContentubeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Contentube obj)
        {
            _db.Contentubes.Update(obj);


            //var objFormDb = _db.Contentubes.FirstOrDefault(u => u.Id == obj.Id);
            //if (objFormDb != null)
            //{
            //    objFormDb.Content_name = obj.Content_name;
            //    objFormDb.Description = obj.Description;
            //    objFormDb.Content_Url = obj.Content_Url;
            //    objFormDb.Service_typeId = obj.Service_typeId;
            //    objFormDb.Category_typeId = obj.Category_typeId;
            //    objFormDb.Country_nameId = obj.Country_nameId;
            //}
        }
    }

}
