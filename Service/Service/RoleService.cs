using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Service.DTO;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class RoleService : ServiceAsync<Role, RoleDto>, IRoleService
    {

        private readonly IRepositoryAsync<Role> _repoRole;
        private readonly IServiceAsync<Role, RoleDto> _roleService;
        private readonly IMapper _mapper;

        public RoleService(IRepositoryAsync<Role> repoRole,
            IServiceAsync<Role, RoleDto> roleService,
            IMapper mapper) : base(repoRole, mapper)
        {
            _repoRole = repoRole;
            _roleService = roleService;
            _mapper = mapper;
        }

        public Task<bool> AddRole(RoleDto Role)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Ajout d'un utilisateur
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> AddUser(RoleDto Role)
        {
            await _roleService.Add(Role);
            return true;

        }

        public Task<bool> DelRole(int RoleId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Suppression d'un utilisateur
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public async Task<bool> DelUser(int RoleId)
        {
            await _roleService.Delete(RoleId);
            return true;
        }


        /// <summary>
        /// Recherche d'un utilisateur par Id
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public async Task<RoleDto> GetRole(int RoleId)
        {
            return await _roleService.GetById(RoleId);
        }

        /// <summary>
        /// Revoie tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public IQueryable<RoleDto> GetRoles()
        {
            return _roleService.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Role"></param>
        /// <returns></returns>
        public async Task<bool> UpdRole(RoleDto Role)
        {
            await _roleService.Update(Role);
            return true;
        }
    }
}
