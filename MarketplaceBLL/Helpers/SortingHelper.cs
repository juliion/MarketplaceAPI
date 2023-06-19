using MarketplaceBLL.Interfaces;
using MarketplaceBLL.Services;
using MarketplaceDAL.Entities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace MarketplaceBLL.Helpers
{
    public class SortingHelper<TModel> : ISortingHelper<TModel>
    {
        private readonly ILogger<SortingHelper<TModel>> _logger;

        public SortingHelper(ILogger<SortingHelper<TModel>> logger)
        {
            _logger = logger;
        }
        public IQueryable<TModel> ApplySorting(IQueryable<TModel> query, string sortColumn, string order)
        {
            var type = typeof(Sale);
            var param = Expression.Parameter(type, "p");
            sortColumn = sortColumn.ToLower();

            var sortProp = type.GetProperties()
                                .Where(p => p.Name.ToLower() == sortColumn)
                                .FirstOrDefault();

            if (sortProp == null)
            {
                _logger.LogWarning($"Could not find a property named '{sortColumn}' on type {type.FullName}");

                return query;
            }

            var propertyAccess = Expression.MakeMemberAccess(param, sortProp);
            var orderByExpression = Expression.Lambda(propertyAccess, param);

            string method = order.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExpression = Expression.Call(typeof(Queryable), method,
                new Type[] { type, sortProp.PropertyType }, query.Expression,
                Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<TModel>(resultExpression);
        }
    }
}
