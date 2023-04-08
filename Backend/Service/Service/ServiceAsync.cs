using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Core.Entities;
//using Core.Specifications;
using DAL;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore.Query;
using Serilog;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ServiceAsync<TEntity, TDto> : IServiceAsync<TEntity, TDto> 
        where TDto : class where TEntity : class
    {
        private readonly IRepositoryAsync<TEntity> _repository;
        private readonly IMapper _mapper;
        
        

        public ServiceAsync(IRepositoryAsync<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
            
        }

        #region Add
        public async Task Add(TDto tDto)
        {
            var entity = _mapper.Map<TEntity>(tDto);
            await _repository.Add(entity);
        }

        public async Task Add(IEnumerable<TDto> tDto)
        {
            var entity = _mapper.Map<IEnumerable<TEntity>>(tDto);
            var e = entity.AsEnumerable();
            await _repository.Add(e);
        }

        #endregion
       


        public async Task<int> Count(Expression<Func<TDto, bool>> predicate = null)
        {
            var predicates = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            return await _repository.Count(predicates);
        }

        public async Task Delete(object id)
        {
            await _repository.Delete(id);
        }

        public async Task Delete(TDto entityToDelete)
        {
            var entity = _mapper.Map<TEntity>(entityToDelete);
            await _repository.Delete(entity);
        }

        public async Task Delete(IEnumerable<TDto> entities)
        {
            var entity = _mapper.Map<IEnumerable<TEntity>>(entities);
            var e = entity.AsEnumerable();
            await _repository.Delete(e);
        }

        
        public async Task<bool> Exists(Expression<Func<TDto, bool>> predicate)
        {
            var predicates = _mapper.Map<Expression<Func<TEntity, bool>>>(predicate);
            return await _repository.Exists(predicates);
        }


        public int ExecuteQuery(string Sql)
        {
            return _repository.ExecuteQuery(Sql);
        }

        public IQueryable<TDto> FromSql(string sql, params object[] parameters)
        {
            
            var result= _repository.FromSql(sql, parameters);
            var ret=_mapper.Map<List<TDto>>(result);
            return ret.AsQueryable();
        }


        public IEnumerable<dynamic> DynamicListFromSql(string Sql, Dictionary<string, object> Params)
        {
            var result=_repository.DynamicListFromSql(Sql,Params);
            return result.AsEnumerable();
        }

        public IQueryable<TDto> GetAll()
        {
            var result= _repository.GetAll();
            var ret=_mapper.Map<List<TDto>>(result);
            return ret.AsQueryable();
        }

        

        public async Task<TDto> GetById(params object[] keyValues)
        {
            var result= await _repository.GetById(keyValues);
            return _mapper.Map<TDto>(result);
        }

        
        
        public async Task<TDto> GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true)
        {
            
            var result=await _repository.GetFirstOrDefault(predicate,orderBy,include,disableTracking);
            return _mapper.Map<TDto>(result);
        }




        
        public async Task<IEnumerable<TDto>> GetMuliple(Expression<Func<TEntity, bool>> predicate = null,
                                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                  bool disableTracking = true)
        {
            
            var result= await _repository.GetMuliple(predicate,orderBy,include,disableTracking);
            var e= _mapper.Map<List<TDto>>(result);
            
            return e.AsEnumerable();
        }


        public async Task Save()
        {
            await _repository.Save();
        }

        public async Task Update(TDto entityTDto)
        {
            var ret=_mapper.Map<TEntity>(entityTDto);
            await _repository.Update(ret);
        }

        public async Task Update(IEnumerable<TDto> entities)
        {
            var ret = _mapper.Map<List<TEntity>>(entities);
            var e = ret.AsEnumerable();
            await _repository.Update(e);
        }

        
    }
}