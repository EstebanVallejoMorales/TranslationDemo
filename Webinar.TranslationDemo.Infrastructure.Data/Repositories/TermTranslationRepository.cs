using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Webinar.TranslationDemo.Domain.Entities;
using Webinar.TranslationDemo.Domain.Entities.Utilities;
using Webinar.TranslationDemo.Domain.Repositories;

namespace Webinar.TranslationDemo.Infrastructure.Data.Repositories
{
    public class TermTranslationRepository: ITermTranslationRepository
    {
        public TermTranslationRepository()
        {

        }

        public List<TermTranslation> GetTranslations()
        {
            if (InstanceTermsTranslation.Instance == null)
            {
                InstanceTermsTranslation.Instance = GetAllTermsByModule();
            }
            return InstanceTermsTranslation.Instance;
        }

        private DynamoDBContext GetContext()
        {
            string prefix = "Dev-";
            AmazonDynamoDBClient Client = new AmazonDynamoDBClient();
            AWSConfigsDynamoDB.Context.TableNamePrefix = prefix;
            return new DynamoDBContext(Client);
        }

        public List<TermTranslation> GetAllTermsByModule()
        {
            try
            {
                List<TermTranslation> termTranslations = new
                List<TermTranslation>();

                List<string> modules = new List<string> { "Inventory", "Transversal" };
                QueryFilter queryFilter = new QueryFilter();

                foreach (var module in modules)
                {
                    //Se añaden los filtros.
                    queryFilter.AddCondition("Module", QueryOperator.Equal, module);
                    
                    List<TermTranslation> termsTranslationResult = GetTermTranslationsByFilters(queryFilter);

                    if (termsTranslationResult != null && termsTranslationResult.Any())
                    {
                        termTranslations.AddRange(termsTranslationResult);
                    }
                }

                if (termTranslations.Any())
                {
                    return termTranslations;
                }

                return new List<TermTranslation>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method allows to get a list of TermTranlation according to the specified filters
        /// </summary>
        /// <param name="queryFilter">Object with the requerid filters.</param>
        /// <returns>A list of TermTranslation</returns>
        public List<TermTranslation> GetTermTranslationsByFilters(QueryFilter queryFilter)
        {
            List<TermTranslation> partialResultDynamo = new
            List<TermTranslation>();
            List<TermTranslation> totalResultDynamo = new List<TermTranslation>();

            using (DynamoDBContext context = GetContext())
            {
                var table = context.GetTargetTable<TermTranslation>();
                Search search = table.Query(new QueryOperationConfig
                {
                    Filter = queryFilter
                });

                do
                {
                    partialResultDynamo.Clear();
                    List<Document> items = search.GetNextSetAsync().GetAwaiter().GetResult();
                    partialResultDynamo = context.FromDocuments<TermTranslation>(items).ToList();
                    if (partialResultDynamo.Count > 0)
                    {
                        totalResultDynamo.AddRange(partialResultDynamo);
                    }
                } while (!search.IsDone);
                return totalResultDynamo;
            }
        }

        /// <summary>
        /// Use this method in order to update the instance o term tranlation when there was a change in TermTranslation table
        /// </summary>
        /// <returns>The result of the refresh execution</returns>
        public string RefreshTranslations()
        {
            try
            {
                InstanceTermsTranslation.Instance = GetAllTermsByModule();
                return "OK";
            }
            catch (Exception)
            {
                return "FAIL";
            }
        }
    }
}
