using Service.Common.Mappings;

using Core.Entities;
using AutoMapper;

namespace Service.DTO
{
    public class UserDto: IMapFrom<User>
    {
        public int UserId { get; set; }
        public string UserNom { get; set; }
        public string UserPrenom { get; set; }
        public string UserUsername { get; set; }
        public string UserEmail { get; set; }
        public string UserPasswordhash { get; set; }
        public string UserGuid { get; set; }
        public string UserTel { get; set; }
        public string UserAdresse { get; set; }
        public byte[] UserPhoto { get; set; }
        public bool UserActive { get; set; }
        public long? UserCreatedbyuser { get; set; }
        public DateTime? UserCreatedondate { get; set; }
        public long? UserUpdatedbyuser { get; set; }
        public DateTime? UserUpdatedondate { get; set; }

        public virtual ICollection<Core.Entities.Usersrole> Usersroles { get; set; }
        public void Mapping (Profile profile)
        {
            profile.CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
