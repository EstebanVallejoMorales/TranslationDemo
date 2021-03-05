using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Webinar.TranslationDemo.Domain.Entities;
using Webinar.TranslationDemo.Models;

namespace Webinar.TranslationDemo.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        private static readonly object ThisLock = new object();

        public static Mapper Mapper { get; set; }

        public static Mapper RegisterMappings()
        {
            lock (ThisLock)
            {
                if (Mapper == null)
                {
                    Mapper = new Mapper(new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<InventoryUpdateViewModel, InventoryUpdate>().ReverseMap();
                        cfg.CreateMap<Response, ResponseViewModel>().ReverseMap();
                        cfg.CreateMap<Error, ErrorViewModel>().ReverseMap();
                    }));
                }
            }
            return Mapper;
        }
    }
}
