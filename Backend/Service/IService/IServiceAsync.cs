//using Core.Specifications;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IServiceAsync<TEntity, TDto>
    {
        
        Task Add(TDto tDto);
        Task Add(IEnumerable<TDto> tDto);

        Task Delete(object id);
        Task Delete(TDto entityToDelete);
        Task Delete(IEnumerable<TDto> entities);

        Task Update(TDto entityTDto);
        Task Update(IEnumerable<TDto> entities);

        Task<TDto> GetById(params object[] keyValues);
        

        Task<TDto> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true);

        IQueryable<TDto> GetAll();

        
        Task<IEnumerable<TDto>> GetMuliple(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true);

        int ExecuteQuery(string Sql);

        IQueryable<TDto> FromSql(string sql, params object[] parameters);

        IEnumerable<dynamic> DynamicListFromSql(string Sql, Dictionary<string, object> Params);

        Task<int> Count(Expression<Func<TDto, bool>> predicate = null);
        Task<bool> Exists(Expression<Func<TDto, bool>> predicate);
        Task Save();


    }
}