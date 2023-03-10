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
    public class UserService : ServiceAsync<User, UserDto>, IUserService
    {

        private readonly IRepositoryAsync<User> _repoUser;
        private readonly IServiceAsync<User, UserDto> _usrService;
        private readonly IMapper _mapper;

        public UserService(IRepositoryAsync<User> repoUser, 
            IServiceAsync<User, UserDto> usrService, 
            IMapper mapper) : base(repoUser, mapper)
        {
            _repoUser = repoUser;
            _usrService = usrService;
            _mapper = mapper;
        }


        /// <summary>
        /// Ajout d'un utilisateur
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> AddUser(UserDto User)
        {
            await _usrService.Add(User);
            return true;

        }

        /// <summary>
        /// Suppression d'un utilisateur
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<bool> DelUser(int UserId)
        {
            await _usrService.Delete(UserId);
            return true;
        }


        /// <summary>
        /// Recherche d'un utilisateur par Id
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUser(int UserId)
        {
             return await _usrService.GetById(UserId);
        }

        /// <summary>
        /// Revoie tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserDto> GetUsers()
        {
            return _usrService.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<bool> UpdUser(UserDto User)
        {
            await _usrService.Update(User);
            return true;
        }
    }
}
