
using Core.Entities;

namespace DAL
{
    public interface IDbContextFactory
    {
        CMCDbContext DbContext { get; }
    }
}
