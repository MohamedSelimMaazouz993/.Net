
using Core.Entities;

namespace DAL
{
    public interface IDbContextFactory
    {
        IAMDbContext DbContext { get; }
    }
}
