using Core.Entities;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IRoleService : IServiceAsync<Role, RoleDto>
    {
        IQueryable<RoleDto> GetRoles();
        Task<RoleDto> GetRole(int RoleId);


        /// Operation de MAJ        
        Task<bool> AddRole(RoleDto Role);
        Task<bool> UpdRole(RoleDto Role);
        Task<bool> DelRole(int RoleId);
    }
}
