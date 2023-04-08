using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;


namespace DAL
{
    public class DbContextFactory : IDbContextFactory, IDisposable
    {
        /// <summary>
        /// Create Db context with connection string
        /// </summary>
        /// <param name="settings"></param>
        public DbContextFactory(IOptions<DbContextSettings> settings) 
        {
            var options = new DbContextOptionsBuilder<CMCDbContext>().UseNpgsql(settings.Value.DbConnectionString,
                npgsqlOptionsAction: s =>
                {
                  s.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
                }).Options;
            DbContext = new CMCDbContext(options);
            DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// Call Dispose to release DbContext
        /// </summary>
        ~DbContextFactory()
        {
            Dispose();
        }

        public CMCDbContext DbContext { get; private set; }
        /// <summary>
        /// Release DB context
        /// </summary>
        public void Dispose()
        {
            DbContext?.Dispose();
        }
    }
}
