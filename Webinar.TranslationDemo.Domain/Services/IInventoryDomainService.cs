using System;
using System.Collections.Generic;
using System.Text;
using Webinar.TranslationDemo.Domain.Entities;

namespace Webinar.TranslationDemo.Domain.Services
{
    public interface IInventoryDomainService
    {
        Response UpdateStock(List<InventoryUpdate> inventoryUpdate);
    }
}
