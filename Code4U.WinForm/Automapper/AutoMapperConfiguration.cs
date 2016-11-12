using AutoMapper;
using Code4U.Models;
using Code4U.WinForm.Forms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void SetUp()
        {
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Property, PropertyViewModel>();
                cfg.CreateMap<Entity, EntityViewModel>();
                cfg.CreateMap<Project, ProjectViewModel>();

                cfg.CreateMap<PropertyViewModel, Property>();
                cfg.CreateMap<EntityViewModel, Entity>();
                cfg.CreateMap<ProjectViewModel, Project>();
            });
        } 
    }
}
