using BlazorSozluk.Api.Application.Interfaces.Repositories.@base;
using BlazorSozluk.Infastructure.Persistance.Context;
using BlazorSozlukApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbcontext;
        protected DbSet<TEntity> entity => dbcontext.Set<TEntity>();
        public GenericRepository(DbContext dbcontext)
        {
            this.dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }
        #region Insert Methods
        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return dbcontext.SaveChanges();
        }

        public virtual int Add(IEnumerable<TEntity> entities)
        {
            this.entity.Add((TEntity)entities);
            return dbcontext.SaveChanges();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbcontext.SaveChangesAsync();
        }

        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Update Methods

        public virtual int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            dbcontext.Entry(entity).State = EntityState.Modified;
            return dbcontext.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            dbcontext.Entry(entity).State = EntityState.Modified;
            return await dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Delete Methods
        public virtual int Delete(TEntity entity)
        {
            if (dbcontext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);

            return dbcontext.SaveChanges();
        }

        public virtual int Delete(Guid id)
        {
            var entity = this.entity.Find(id);
            return Delete(entity);
        }

        public virtual async Task<int> DeleteAsync(Guid Id)
        {
            var entity = this.entity.Find(Id);
            return await DeleteAsync(entity);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if (dbcontext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }
            this.entity.Remove(entity);

            return await dbcontext.SaveChangesAsync();
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            dbcontext.RemoveRange(entity.Where(predicate));
            return dbcontext.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            dbcontext.RemoveRange(predicate);
            return await dbcontext.SaveChangesAsync() > 0;
        }
        #endregion

        #region AddOrUpdateMethods
        public virtual int AddorUpdate(TEntity entity)
        {

            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            {
                dbcontext.Update(entity);
            }
            return dbcontext.SaveChanges();
        }

        public virtual async Task<int> AddorUpdateAsync(TEntity entity)
        {
            if (!this.entity.Local.Any(i => EqualityComparer<Guid>.Default.Equals(i.Id, entity.Id)))
            {
                dbcontext.Update(entity);
            }
            return await dbcontext.SaveChangesAsync();
        }
        #endregion

        #region Get Methods
        public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();

        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            if (noTracking)
                query = query.AsNoTracking();

            return query;
        }

        public Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await entity.FindAsync(id);
            if (found == null)
                return null;
            if (noTracking)
                dbcontext.Entry(found).State = EntityState.Detached;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                dbcontext.Entry(found).Reference(include).Load();
            }
            return found;
        }

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        //TODO
        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }


        #endregion

        #region SaveChanges
        public Task<int> SaveChangesAsync()
        {
            return dbcontext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return dbcontext.SaveChanges();
        }
        #endregion

        #region Bulk Methods
        public virtual async Task Bulkadd(IEnumerable<TEntity> entities)
        {
            if (entities != null && !entities.Any())
                await Task.CompletedTask;
            await entity.AddRangeAsync(entities);

            await dbcontext.SaveChangesAsync();

        }

        public Task BulkDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if (ids != null && !ids.Any())
                return Task.CompletedTask;

            dbcontext.RemoveRange(entity.Where(i => ids.Contains(i.Id)));
            return dbcontext.SaveChangesAsync();
        }

        public Task BulkDeleteById(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
        #endregion

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }

            return query;
        }



    }
}
