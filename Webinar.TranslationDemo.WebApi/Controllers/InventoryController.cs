using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Webinar.TranslationDemo.Application.Services;
using Webinar.TranslationDemo.Models;

namespace Webinar.TranslationDemo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/inventory/")]
    [EnableCors("Test")]
    public class InventoryController : Controller
    {
        IInventoryApplicationService InventoryApplication;

        public InventoryController(IConfiguration configuration, IInventoryApplicationService inventoryApplication)
        {
            InventoryApplication = inventoryApplication;
        }

        [HttpPost]
        [Route("UpdateStock")]
        public IActionResult UpdateStock(List<InventoryUpdateViewModel> inventoriesUpdates)
        {
            try
            {
                ResponseViewModel response = InventoryApplication.UpdateStock(inventoriesUpdates);
                return Ok(response);
            }
            catch 
            {
                return StatusCode(500);
            }
        }
    }
}
