using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Webinar.TranslationDemo.Lambda;
using Xunit;

namespace Webinar.TranslationDemo.Debug.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void UpdateStockTest()
        {
            var responseAPI = InvokeAPIGatewayRequest();
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(responseAPI));
        }

        /// <summary>
        /// Method used to call lambda function using a json file.
        /// </summary>
        /// <returns></returns>
        private APIGatewayProxyResponse InvokeAPIGatewayRequest()
        {
            StackTrace stackTrace = new StackTrace(); //Para tener la traza de la pila.

            string testCase = stackTrace.GetFrame(1).GetMethod().Name; /*Obtiene el segundo StackFrame en la 
                                                                        * pila de llamadas del hilo actual para 
                                                                        * obtener el nombre del método que se quiere probar*/

            TestLambdaContext context = new TestLambdaContext(); //Usado para escribir test locales de funciones lambda.
            var lambdaFunction = new Function();

            //Se obtiene la ubicación del ensamblado para localizar la carpeta JsonTest y el archivo TestData.json
            var filePath = Path.Combine(Path.GetDirectoryName(this.GetType().GetTypeInfo().Assembly.Location), "JsonTest/TestData.json");

            var requestStr = File.ReadAllText(filePath);

            var request = JsonConvert.DeserializeObject<Dictionary<string, APIGatewayProxyRequest>>(requestStr);
            APIGatewayProxyRequest reques = request.FirstOrDefault(x => x.Key == testCase).Value;

            return lambdaFunction.FunctionHandler(reques, context);
        }
    }
}
