using Core.Entities;
using Core.Utilitaires;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAL.Repository
{

    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly IDbContextFactory _dbContextFactory;
        protected IAMDbContext _dbContext => _dbContextFactory?.DbContext;
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the GenericRepository<TEntity>.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public RepositoryAsync(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public IAMDbContext DbContextIAM()
        {
            return _dbContext;
        }

        #region CREATE
        public virtual async Task Add(TEntity entity)
        {
            var entry = await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Add(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region READ
        public virtual async Task<TEntity> GetById(params object[] keyValues) => await _dbSet.FindAsync(keyValues);

        public virtual async Task<TEntity> GetFirstOrDefault(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true
        )
        {
            IQueryable<TEntity> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return  _dbSet;
        }

        public virtual async Task<IEnumerable<TEntity>> GetMuliple(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true
        )
        {
            IQueryable<TEntity> query = _dbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }



        public virtual IQueryable<TEntity> FromSql(
            string sql,
            params object[] parameters
        ) => _dbSet.FromSqlRaw(sql, parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public int ExecuteQuery(string Sql)
        {
            using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                return cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Sql"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
        public IEnumerable<dynamic> DynamicListFromSql(string Sql, Dictionary<string, object> Params)
        {
            using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open) { cmd.Connection.Open(); }

                foreach (KeyValuePair<string, object> p in Params)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = p.Key;
                    dbParameter.Value = p.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var row = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                        }
                        yield return row;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable DynamicSqlQuery(string sql, params object[] parameters)
        {
            
            TypeBuilder builder = BDExtensions.createTypeBuilder(
                    "MyDynamicAssembly", "MyDynamicModule", "MyDynamicType");
            //DbCommand command= this._dbContext.Database.GetDbConnection().CreateCommand();

            using (DbCommand command = this._dbContext.Database.GetDbConnection().CreateCommand())
            {
                try
                {
                    command.CommandTimeout = 30;
                    command.CommandText = sql;
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    using (System.Data.IDataReader reader = command.ExecuteReader())
                    {
                        var schema = reader.GetSchemaTable();

                        foreach (System.Data.DataRow row in schema.Rows)
                        {
                            string name = (string)row["ColumnName"];
                            //var a=row.ItemArray.Select(d=>d.)
                            Type type = (Type)row["DataType"];
                            if (type != typeof(string) && (bool)row.ItemArray[schema.Columns.IndexOf("AllowDbNull")])
                            {
                                type = typeof(Nullable<>).MakeGenericType(type);
                            }
                            BDExtensions.createAutoImplementedProperty(builder, name, type);
                        }
                    }
                }
                finally
                {
                    command.Parameters.Clear();
                }
            }

            Type resultType = builder.CreateType();

            var ent=this._dbContext.Database.ExecuteSqlRaw(sql, parameters);
            yield return ent;
        }
        #endregion

        #region UPDATE
        public virtual async Task Update(TEntity entity)
        {
             _dbSet.Update(entity);
             await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region DELETE
        public virtual async Task Delete(object id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (entityToDelete != null)
            {
                _dbSet.Remove(entityToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region OTHER
        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await _dbSet.CountAsync();
            }
            else
            {
                return await _dbSet.CountAsync(predicate);
            }
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
        #endregion
    }
}
