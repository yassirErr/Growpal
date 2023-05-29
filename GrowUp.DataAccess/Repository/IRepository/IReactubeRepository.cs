using GrowUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository.IRepository
{
    public interface IReactubeRepository : IRepository<Reactube>
    {
        int Count(Expression<Func<Reactube, bool>> filter = null);
        void Update(Reactube obj);
    }


}
