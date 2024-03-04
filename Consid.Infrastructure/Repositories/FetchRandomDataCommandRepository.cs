using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Consid.Domain.Entities;
using Consid.Domain.Interfaces;
using Consid.Infrastructure.AzureTableLogger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consid.Infrastructure.Repositories;

internal class FetchRandomDataCommandRepository : IFetchRandomDataCommandRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    private IConfiguration _configuration;
    private readonly ILogger<FetchRandomDataCommandRepository> _logger;
    private readonly BlobContainerClient _blobContainerClient;

    public FetchRandomDataCommandRepository(ILogger<FetchRandomDataCommandRepository> logger, BlobServiceClient blobServiceClient, IConfiguration configuration)
    {
        _blobServiceClient = blobServiceClient;
        _configuration = configuration;
        _logger = logger;
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_configuration["AzureRandomDataBlobContainerName"]);
    }
    

    public async Task SaveFatchedData(RandomJsonData data)
    {
        _blobContainerClient.CreateIfNotExists();
        var blob = await _blobContainerClient.GetBlobClient(data.Name).UploadAsync(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(data.Data)));        
        
    }
}
