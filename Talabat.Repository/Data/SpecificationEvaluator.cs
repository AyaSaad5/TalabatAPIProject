using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Enitites;
using Talabat.Core.Specifications;

namespace Talabat.Repository.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public  static IQueryable<T> GetQuery(IQueryable<T> Inputquery, ISpecifications<T> spec)
        {
            var query = Inputquery; // _context.Set<Product>()

            if (spec.Criteria != null) // P => P.id == 10
                query = query.Where(spec.Criteria);

            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            if(spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if(spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            // _context.Set<Product>().where(P => P .id == 10)
            query = spec.Includes.Aggregate(query,(currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            //_context.Set<Product>().where(P => P .id == 10).Include(P => P.ProductBrand)
            //_context.Set<Product>().where(P => P .id == 10).Include(P => P.ProductBrand).Include(P => P.ProductType)

            return query;
        }
    }
}
