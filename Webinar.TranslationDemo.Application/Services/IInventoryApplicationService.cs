using System;
using System.Collections.Generic;
using System.Text;
using Webinar.TranslationDemo.Models;

namespace Webinar.TranslationDemo.Application.Services
{
    public interface IInventoryApplicationService
    {
        ResponseViewModel UpdateStock(List<InventoryUpdateViewModel> invetoriesView);
    }
}
