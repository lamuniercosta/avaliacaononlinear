using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShoppingCartCore.Repositories
{
    public interface IUnitOfWork<T> where T : class
    {
        int Save(T model);
        int Update(T model);
        void Delete(T model);
        IQueryable<T> GetAll();
        T GetById(object id);
        List<T> Where(Expression<Func<T, bool>> expression);
        List<T> OrderBy(Expression<System.Func<T, bool>> expression);
    }
}
