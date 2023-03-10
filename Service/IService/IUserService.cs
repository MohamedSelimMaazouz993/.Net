using Core.Entities;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IUserService : IServiceAsync<User, UserDto>
    {
        IQueryable<UserDto> GetUsers();
        Task<UserDto> GetUser(int UserId);


        /// Operation de MAJ        
        Task<bool> AddUser(UserDto User);
        Task<bool> UpdUser(UserDto User);
        Task<bool> DelUser(int UserId);
    }
}
