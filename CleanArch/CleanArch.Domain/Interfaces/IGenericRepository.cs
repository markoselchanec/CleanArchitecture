using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
    public interface IGenericRepository<TClass, TId> where TClass : class where TId : IEquatable<TId>
    {
        IEnumerable<TClass> GetAll(string includeProperties = null);
        TClass Add(TClass cl);
        TClass GetFirstOrDefault(Expression<Func<TClass, bool>> expression, string includeProperties = null);
    }
}
