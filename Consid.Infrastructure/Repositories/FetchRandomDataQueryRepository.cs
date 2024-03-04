using Azure.Storage.Blobs;
using Consid.Domain.Entities;
using Consid.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Infrastructure.Repositories
{
    public class FetchRandomDataQueryRepository : IFetchRandomDataQueryRepository
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FetchRandomDataQueryRepository> _logger;
        private readonly BlobContainerClient _blobContainerClient;

        public FetchRandomDataQueryRepository(ILogger<FetchRandomDataQueryRepository> logger, BlobServiceClient blobServiceClient, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;            
            _blobServiceClient = blobServiceClient;
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureRandomDataBlobContainerName"]);
        }

        public async Task<RandomJsonData> GetRandomDataByName(string name)
        {
            try
            {
                var data = await _blobContainerClient.GetBlobClient(name).OpenReadAsync();
                data.Position = 0;
                string text;
                using (StreamReader reader = new StreamReader(data, Encoding.UTF8))
                {
                    text = await reader.ReadToEndAsync();
                }
                return new RandomJsonData(text, name);
                data.Dispose();
            }
            catch (Exception exc)
            {
                throw;
            }
        }
    }
}
