using Core.Entities;
using Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IModuleService : IServiceAsync<Module, ModuleDto>
    {
        IQueryable<ModuleDto> GetModules();
        Task<ModuleDto> GetModule(int ModuleId);


        /// Operation de MAJ        
        Task<bool> AddModule(ModuleDto Module);
        Task<bool> UpdModule(ModuleDto Module);
        Task<bool> DelModule(int ModuleId);
    }
}
