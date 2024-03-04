using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using MediatR;
using Microsoft.OpenApi.Models;
using Consid.Domain.Entities;
using Consid.Application.Commands;
using Consid.Application.Queries;
using System.Threading;

namespace Consid.Functions
{
    public class WebApiFunctions
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        private const string GET_FETCH_STATUS_LIST__ROLE = "fetchlist.read";
        private const string GET_BLOB_DATA_BY_BLOB_NAME__ROLE = "fetchedblodb.read";
        private const string GET_BLOB_DATA_BY_BLOB_ID__ROLE = "fetchedblodb.read";

        public WebApiFunctions(ILogger<WebApiFunctions> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }

        [Function("GetFetchRandomDataStatusList")]
        [OpenApiOperation(operationId: "GetFetchRandomDataStatusList", tags: new[] { "GetFetchRandomDataStatusList" })]
        [OpenApiResponseWithBody(HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<FetchRandomDataLog>), Description = "The OK response")]
        [OpenApiParameter(name: "StartDate", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "Start Date")]
        [OpenApiParameter(name: "EndDate", In = ParameterLocation.Query, Required = true, Type = typeof(DateTime), Description = "End Date")]
        public async Task<HttpResponseData> GetFetchRandomDataStatusList([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            try
            {
                Authorize(req, GET_FETCH_STATUS_LIST__ROLE);



                var startDate = DateTime.Parse(req.Query["StartDate"]);
                var endDate = DateTime.Parse(req.Query["EndDate"]);

                var data = await _mediator.Send(new GetFetchRandomDataLogQuery(startDate, endDate));

                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(data);
                return response;



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetFetchStatusListRun");
                return GetErrorResponse(req, ex, HttpStatusCode.InternalServerError);
            }
        }

        [Function("GetBlobDataByBlobName")]
        [OpenApiOperation(operationId: "GetBlobDataByBlobName", tags: new[] { "GetBlobDataByBlobName" })]
        [OpenApiResponseWithBody(HttpStatusCode.OK, contentType: "application/octet-stream", bodyType: typeof(Stream), Description = "The OK response")]
        [OpenApiParameter(name: "BlobFileName", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Blob file name with extension")]
        public async Task<HttpResponseData> GetBlobDataByBlobName([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            try
            {
                Authorize(req, GET_BLOB_DATA_BY_BLOB_NAME__ROLE);

                var blobName = req.Query["BlobFileName"];

                var randomjsondata = await _mediator.Send(new GetFetchRandomDataQuery(blobName));

                var response = req.CreateResponse();
                response.Headers.Add("Content-Disposition", "attachment; filename=" + blobName);
                response.Headers.Add("Content-Type", "application/octet-stream");

                response.Body.Write(Encoding.UTF8.GetBytes(randomjsondata.Data));
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetBlobDataByBlobNameRun");
                return GetErrorResponse(req, ex, HttpStatusCode.NotFound);

            }
        }


        [Function("GetMaxWindSpeedForCountry")]
        [OpenApiOperation(operationId: "GetMaxWindSpeedForCountry", tags: new[] { "GetMaxWindSpeedForCountry" })]
        [OpenApiResponseWithBody(HttpStatusCode.OK, contentType: "application/octet-stream", bodyType: typeof(Stream), Description = "The OK response")]
        [OpenApiParameter(name: "CountryName", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "")]
        public async Task<HttpResponseData> GetMaxWindSpeedForCountry(
                [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
                CancellationToken cancelationToken)
        {
            try
            {
                Authorize(req, GET_BLOB_DATA_BY_BLOB_NAME__ROLE);

                var countryName = req.Query["CountryName"]??"";

                var result = await _mediator.Send(new GetMaxWindSpeedForCountryQuery(countryName),cancelationToken);

                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetBlobDataByBlobNameRun");
                return GetErrorResponse(req, ex, HttpStatusCode.NotFound);

            }
        }

        [Function("GetMinTempMaxWindSpeedForCountry")]
        [OpenApiOperation(operationId: "GetMinTempMaxWindSpeedForCountryQuery", tags: new[] { "GetMinTempMaxWindSpeedForCountryQuery" })]
        [OpenApiResponseWithBody(HttpStatusCode.OK, contentType: "application/octet-stream", bodyType: typeof(Stream), Description = "The OK response")]
        [OpenApiParameter(name: "MaxTempForCountry", In = ParameterLocation.Query, Required = true, Type = typeof(decimal), Description = "Get Only Countries Where min temp was less then param")]
        public async Task<HttpResponseData> GetMinTempMaxWindSpeedForCountry(
               [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
               CancellationToken cancelationToken)
        {
            try
            {
                Authorize(req, GET_BLOB_DATA_BY_BLOB_NAME__ROLE);

                var maxTempForCountry = Convert.ToDecimal(req.Query["MaxTempForCountry"]);

                var result = await _mediator.Send(new GetMinTempMaxWindSpeedForCountryQuery(maxTempForCountry), cancelationToken);

                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetBlobDataByBlobNameRun");
                return GetErrorResponse(req, ex, HttpStatusCode.NotFound);

            }
        }

        [Function("GetCities")]
        [OpenApiOperation(operationId: "GetCities", tags: new[] { "GetCities" })]
        [OpenApiResponseWithBody(HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<FetchRandomDataLog>), Description = "The OK response")]       
        public async Task<HttpResponseData> GetCities([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            try
            {
                Authorize(req, GET_FETCH_STATUS_LIST__ROLE);


                var data = await _mediator.Send(new GetCitiesQuery());

                var response = req.CreateResponse();
                response.StatusCode = HttpStatusCode.OK;
                await response.WriteAsJsonAsync(data);
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetCities");
                return GetErrorResponse(req, ex, HttpStatusCode.InternalServerError);
            }
        }
        private void Authorize(HttpRequestData req, string requiredClaim)
        {
            return;

            if (Environment.GetEnvironmentVariable("API_ENVIRONMENT").Equals("DEV", StringComparison.OrdinalIgnoreCase))
                return;

            var items = req.Identities.SelectMany(i => i.Claims).ToList();
            if (!items.Any(c => c.Value == requiredClaim))
            {
                throw new UnauthorizedAccessException("You are not authorized to access this resource");
            }
        }
        private static HttpResponseData GetErrorResponse(HttpRequestData req, Exception ex, HttpStatusCode code)
        {
            var response = req.CreateResponse(code);
            response.WriteString(ex.Message);
            response.Headers.Add("Content-Type", "text/plain");
            return response;
        }
    }
}

