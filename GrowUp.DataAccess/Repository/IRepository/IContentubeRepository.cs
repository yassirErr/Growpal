using GrowUp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository.IRepository
{
    public interface IContentubeRepository : IRepository<Contentube>
    {
        void Update(Contentube obj);
    }

}
