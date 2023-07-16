using GrowUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository.IRepository
{
    public interface IWatchtubeRepository : IRepository<Watchtube>
    {
        int Count(Expression<Func<Watchtube, bool>> filter = null);
        void Update(Watchtube obj);
    }


}
