using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Repository.IRepository
{
        public interface IUnitOfWork
        {
            ICategoryRepository Category { get; }
            IServiceRepository Service { get; }
            IContentubeRepository Content { get; }
            IApplicationUserRepository ApplicationUser { get; }
            IReactubeRepository Reactube { get; }
        IWatchtubeRepository Watchtube { get; }

        void Save();
    }
}
