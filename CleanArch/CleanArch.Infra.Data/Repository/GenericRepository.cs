using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infra.Data.Repository
{
    public class GenericRepository<TClass, TId> : IGenericRepository<TClass, TId> where TClass : class where TId : IEquatable<TId>
    {
        private UniversityDBContext _ctx;
        public GenericRepository(UniversityDBContext ctx)
        {
            _ctx = ctx;
        }
        public TClass Add(TClass cl)
        {
            _ctx.Set<TClass>().Add(cl);
            _ctx.SaveChanges();
            return cl;
        }

        public IEnumerable<TClass> GetAll(string includeProperties = null)
        {
            IQueryable<TClass> query = _ctx.Set<TClass>();
            if(includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public TClass GetFirstOrDefault(Expression<Func<TClass, bool>> expression, string includeProperties = null)
        {
            //return _ctx.Set<TClass>().FirstOrDefault(x => x.Id == id);
            IQueryable<TClass> query = _ctx.Set<TClass>();
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            
            return query.FirstOrDefault(expression);
        }
    }
}
