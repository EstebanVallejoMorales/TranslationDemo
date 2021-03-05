using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Webinar.TranslationDemo.Domain.Entities;
using Webinar.TranslationDemo.Domain.Repositories;

namespace Webinar.TranslationDemo.Domain.Services
{
    public class InventoryDomainService : IInventoryDomainService
    {
        private readonly ITranslationDomainService TranlationDomainService;
        private readonly IConfiguration Configuration;

        public InventoryDomainService(ITranslationDomainService translationDomainService, IConfiguration configuration)
        {
            TranlationDomainService = translationDomainService;
            Configuration = configuration;
        }

        public Response UpdateStock(List<InventoryUpdate> inventoryUpdate)
        {
            var response = new Response()
            {
                Data = true,
                Errors = new List<Error>(),
                Message = null
            };

            try
            {
                //If model is not valid
                foreach (var inventUpdate in inventoryUpdate)
                {
                    if (inventUpdate.Stock < 0)
                    {
                        response.Errors.Add(new Error 
                        {
                            Message = TranlationDomainService.GetTranslationTerm("Msg_NoNegativeStock") //"El stock no puede ser negativo"
                        });
                    }
                    else
                    {
                        //Update stock
                    }
                }

                if (response.Errors.Any())
                {
                    response.Message = TranlationDomainService.GetTranslationTerm("Msg_UpdateInventoryError"); //"Se presentaron errores en el proceso de actualización de inventario";
                }
                else
                {
                    response.Message = TranlationDomainService.GetTranslationTerm("Msg_UpdateInventorySuccess"); //"Actualización de inventario exitosa.";
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
                response.Message = TranlationDomainService.GetTranslationTerm("Msg_UpdateInventoryError"); //"Se presentaron errores en el proceso de actualización de inventario";
                return response;
            }
        }
    }
}
