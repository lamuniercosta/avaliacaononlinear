using Microsoft.EntityFrameworkCore;
using ShoppingCartCore.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShoppingCartCore.Repositories
{
    public class GenericRepository<T> : IUnitOfWork<T> where T : BaseEntity
    {
        private readonly BaseContext context;
        private DbSet<T> entities;

        public GenericRepository(BaseContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        public virtual void ChangeObjectState(object model, EntityState state)
        {
            this.context.ChangeTracker.TrackGraph(model, m => m.Entry.State = state);
        }

        public virtual int Save(T model)
        {
            this.entities.Add(model);
            return this.context.SaveChanges();
        }

        public virtual int Update(T model)
        {
            var entry = this.context.Entry(model);
            if (entry.State == EntityState.Detached)
                this.entities.Attach(model);

            this.ChangeObjectState(model, EntityState.Modified);
            return this.context.SaveChanges();
        }

        public virtual void Delete(T model)
        {
            var entry = this.context.Entry(model);
            if (entry.State == EntityState.Detached)
                this.entities.Attach(model);

            this.ChangeObjectState(model, EntityState.Deleted);
            this.context.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            return this.entities.ToList();
        }

        public virtual T GetById(object id)
        {
            return this.entities.Find(id);
        }

        public virtual List<T> Where(Expression<Func<T, bool>> expression)
        {
            return this.entities.Where(expression).ToList();
        }

        public List<T> OrderBy(Expression<Func<T, bool>> expression)
        {
            return this.entities.OrderBy(expression).ToList();
        }
    }
}
