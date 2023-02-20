using JWTAuthAPI.Application.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthAPI.Application.Infrastructure.Specifications
{
    public class SpecificationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> _query, ISpecification<T> specification)
        {
            var query = _query;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query, (current, include) =>
                    current.Include(include));

            query = specification.IncludeStrings.Aggregate(query, (current, include) =>
                    current.Include(include));

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            return query;
        }
    }
}
