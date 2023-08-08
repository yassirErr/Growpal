using GrowUp.DataAccess.Data;
using GrowUp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository.IRepository
{
    public interface IContentubeRepository : IRepository<Contentube>
    {
        //int Count(Expression<Func<Contentube, bool>> filter = null);
        void Update(Contentube obj);
    }

}
