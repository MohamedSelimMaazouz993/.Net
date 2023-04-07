using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Common.Mappings
{
    public  interface IMapFrom<T>
    {
        //void Mapping(Profile profile);
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
