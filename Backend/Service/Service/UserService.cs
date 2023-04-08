using AutoMapper;
using Core.Entities;
using DAL.IRepository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Service.DTO;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class UserService : ServiceAsync<User, UserDto>, IUserService
    {

        private readonly IRepositoryAsync<User> _repoUser;
        private readonly IServiceAsync<User, UserDto> _usrService;
        //private readonly IServiceAsync<Role, RoleDto> _roleService;
        private readonly IMapper _mapper;

        public UserService(IRepositoryAsync<User> repoUser, 
            IServiceAsync<User, UserDto> usrService,
            IMapper mapper) : base(repoUser, mapper)
        {
            _repoUser = repoUser;
            _usrService = usrService;
            //_roleService = roleService;
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
            Guid userGuid = System.Guid.NewGuid();
            
            HMACSHA1 hash = new HMACSHA1();
            string mchKeys = MachinesKeys("CleMachine.json");
            hash.Key = HexToByte(mchKeys);
            
            var encodedPassword = Convert.ToBase64String(
                hash.ComputeHash(Encoding.Unicode.GetBytes(User.UserPasswordhash + userGuid.ToString())));
            
            User.UserPasswordhash = encodedPassword;
            User.UserGuid = userGuid.ToString();

            await _usrService.Add(User);
            return true;

        }

        /// <summary>
        /// Permet de faire la verifaction d'authenfication de l'user
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="UserPwd"></param>
        /// <returns></returns>
        public async Task<UserDto> IsLogin(string Username,string UserPwd)
        {

            var Usr = await _usrService.GetFirstOrDefault(
                predicate: (i => i.UserUsername == Username),
                include: ( s => s.
                      Include(s1 => s1.Usersroles)
                          .ThenInclude(s2 => s2.UrRole)
                             .ThenInclude(s3 => s3.Rolesmenus)
                               .ThenInclude(s4 => s4.RmMenu)
                         ),

                disableTracking: true);
            if (Usr != null)
            {
                HMACSHA1 hash = new HMACSHA1();
                string mchKeys = MachinesKeys("CleMachine.json");
                hash.Key = HexToByte(mchKeys);

                var encodedPassword = Convert.ToBase64String(
                    hash.ComputeHash(Encoding.Unicode.GetBytes(UserPwd + Usr.UserGuid)));

                if (Usr.UserPasswordhash == encodedPassword)
                {
                    return Usr;
                }
            }
            return null;
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
            var Usr = await _usrService.GetFirstOrDefault(
                predicate: (i => i.UserId == UserId),
                include: (s => s.
                      Include(s1 => s1.Usersroles)
                          .ThenInclude(s2 => s2.UrRole)
                             .ThenInclude(s3 => s3.Rolesmenus)
                               .ThenInclude(s4 => s4.RmMenu)
                         ),

                disableTracking: true);
            return Usr;
        }

        /// <summary>
        /// Retourne tous les utilsateurs avec leurs menus
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserDto>> GetAllUsers()
        {
            var Usr = await _usrService.GetMuliple(
                predicate: (i => i.UserActive == true),
                include: (s => s.
                      Include(s1 => s1.Usersroles)
                          .ThenInclude(s2 => s2.UrRole)
                             .ThenInclude(s3 => s3.Rolesmenus)
                               .ThenInclude(s4 => s4.RmMenu)
                         ),

                disableTracking: true);
            return Usr.ToList();
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

        /// <summary>
        /// hexa to byte
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns> </returns>
        private byte[] HexToByte(string hexString)
        {
            long taille = (hexString.Length / 2) - 1;
            byte[] ReturnBytes = new byte[taille];
            for (int i = 0; i < ReturnBytes.Length - 1; i++)
            {
                ReturnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return ReturnBytes;
        }
        /// <summary>
        /// Machineses the keys.
        /// </summary>
        /// <param name="NJson">The n json.</param>
        /// <returns></returns>
        private string MachinesKeys(string NJson)
        {
            string json = string.Empty;

            using (StreamReader r = new StreamReader(NJson))

            {
                json = r.ReadToEnd();
                JObject jObject = JObject.Parse(json);
                string displayName = (string)jObject.SelectToken("ValidationKey");
                return (displayName);
            }
        }
    }
}
