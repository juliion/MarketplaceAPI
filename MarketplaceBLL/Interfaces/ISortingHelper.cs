using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceBLL.Interfaces
{
    public interface ISortingHelper<TModel>
    {
        public IQueryable<TModel> ApplySorting(IQueryable<TModel> query, string sortColumn, string order);
    }
}
