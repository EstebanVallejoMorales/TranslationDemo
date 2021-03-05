using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Webinar.TranslationDemo.Application.Services;
using Webinar.TranslationDemo.Domain.Entities;
using Webinar.TranslationDemo.Infrastructure.Common.Enumerators;
using Webinar.TranslationDemo.Models;
using static Webinar.TranslationDemo.Infrastructure.Common.Enumerators.Enumerators;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Webinar.TranslationDemo.Lambda
{
    public class Function
    {

        private readonly ServiceProvider Provider;
        private readonly IConfiguration Configuration;
        const string ID_QUERY_COMMAND = "cmd";

        public Function()
        {
            Provider = new Startup().ServiceProvider;
            Configuration = Provider.GetService<IConfiguration>();
        }

        //Se debe recibir un APIGatewayProxyRequest request para que pueda ser invocada desde AWS ApiGateway
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            ResponseViewModel responseViewModel = new ResponseViewModel();
            APIGatewayProxyResponse responseFunction = new APIGatewayProxyResponse();
            DefineCulture(request);

            try
            {
                string command = "";

                if (request.PathParameters != null && request.PathParameters.Keys.Contains(ID_QUERY_COMMAND))
                {
                    command = request.PathParameters[ID_QUERY_COMMAND]?.ToString();
                }

                if (EnumHelper.ExistsValue<Command>(command))
                {
                    switch (EnumHelper.Parse<Command>(command))
                    {
                        case Command.UpdateStock:
                            responseViewModel = UpdateStock(request);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"Command no exist from Path { request.Path }");
                    Console.WriteLine($"Command no exist from Resource { request.Resource }");
                    Console.WriteLine($"Command no exist from RequestContext { request.RequestContext }");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Module:TranslationDemo, Class:Function.cs, Method:FunctionHandler, Error: {ex}");
                throw;
            }
            responseFunction.Body = System.Text.Json.JsonSerializer.Serialize(responseViewModel);
            responseFunction.StatusCode = 200;
            return responseFunction;
        }

        private void DefineCulture(APIGatewayProxyRequest request)
        {
            string culture = request.QueryStringParameters != null && request.QueryStringParameters.Keys.Contains("culture")
                ? request.QueryStringParameters["culture"]
                : "es-CO";

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
        }

        private ResponseViewModel UpdateStock(APIGatewayProxyRequest request)
        {
            IInventoryApplicationService inventoryApplication = Provider.GetService<IInventoryApplicationService>();
            List<InventoryUpdateViewModel> inventoriesUpdates = System.Text.Json.JsonSerializer.Deserialize<List<InventoryUpdateViewModel>>(request.Body);
            return inventoryApplication.UpdateStock(inventoriesUpdates);
        }
    }
}
