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
    public class ModuleService : ServiceAsync<Module, ModuleDto>, IModuleService
    {

        private readonly IRepositoryAsync<Module> _repoModule;
        private readonly IServiceAsync<Module, ModuleDto> _moduleService;
        private readonly IMapper _mapper;

        public ModuleService(IRepositoryAsync<Module> repoModule,
            IServiceAsync<Module, ModuleDto> moduleService,
            IMapper mapper) : base(repoModule, mapper)
        {
            _repoModule = repoModule;
            _moduleService = moduleService;
            _mapper = mapper;
        }


        /// <summary>
        /// Ajout d'un utilisateur
        /// </summary>
        /// <param name="Module"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> AddModule(ModuleDto Module)
        {
            await _moduleService.Add(Module);
            return true;

        }

        /// <summary>
        /// Suppression d'un utilisateur
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<bool> DelModule(int ModuleId)
        {
            await _moduleService.Delete(ModuleId);
            return true;
        }


        /// <summary>
        /// Recherche d'un utilisateur par Id
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<ModuleDto> GetModule(int ModuleId)
        {
            return await _moduleService.GetById(ModuleId);
        }

        /// <summary>
        /// Revoie tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public IQueryable<ModuleDto> GetModules()
        {
            return _moduleService.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Module"></param>
        /// <returns></returns>
        public async Task<bool> UpdModule(ModuleDto Module)
        {
            await _moduleService.Update(Module);
            return true;
        }
    }
}
