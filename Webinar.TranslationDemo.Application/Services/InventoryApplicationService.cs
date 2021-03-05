using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Webinar.TranslationDemo.Domain.Entities;
using Webinar.TranslationDemo.Domain.Services;
using Webinar.TranslationDemo.Models;

namespace Webinar.TranslationDemo.Application.Services
{
    public class InventoryApplicationService : IInventoryApplicationService
    {
        private readonly IInventoryDomainService InventoryDomainService;
        private readonly IMapper Mapper;

        public InventoryApplicationService(IInventoryDomainService inventoryDomainService, IMapper mapper)
        {
            InventoryDomainService = inventoryDomainService;
            Mapper = mapper;
        }

        public ResponseViewModel UpdateStock(List<InventoryUpdateViewModel> invetoriesView)
        {
            List<InventoryUpdate> inventoriesToUpdate = Mapper.Map<List<InventoryUpdate>>(invetoriesView);
            return Mapper.Map<ResponseViewModel>(InventoryDomainService.UpdateStock(inventoriesToUpdate));
        }
    }
}
