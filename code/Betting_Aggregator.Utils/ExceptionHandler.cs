using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Betting_Aggregator.Utils
{
    public class ExceptionHandler
    {
        public const string INTERNAL_SERVER_ERROR_MESSAGE = "Please contact administrator and present correlation identifier for troubleshooting";
        public const string JSON = "application/json";
        public const string BUSINESS_VALIDATION = "business-validation";
        public const string FIELD_VALIDATION = "field-validation";
        public const string UNEXPECTED_SERVERERROR = "unexpected-internal-server-error";
        public const string INVALID_DATATYPE = "Invalid data type.";

        private readonly JsonSerializerSettings _jsonSerializerSettings =
            new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException businessException)
            {
                await HandleBusinessExceptionAsync(context, businessException);
            }
            catch (InputDataValidationException badRequestException)
            {
                await HandleInputDataValidationExceptionAsync(context, badRequestException);
            }
            catch (Exception ex)
            {
                await HandleUnhandledExceptionAsync(context, ex);
            }
        }

        private Task HandleInputDataValidationExceptionAsync(HttpContext context, InputDataValidationException exception)
        {
            var badRequestResponseDetail = new List<ResponseDetail>();

            foreach (var error in exception.Details)
            {
                var detail = new ResponseDetail
                {
                    Name = error.Key.ToCamelCase(),
                    Messages = new List<string>
                    {
                        error.Value
                    }
                };

                badRequestResponseDetail.Add(detail);
            }

            var inputDataValidationResponseDto = new BadResponseDto
            {
                Type = FIELD_VALIDATION,
                Details = badRequestResponseDetail
            };

            var result = JsonConvert.SerializeObject(inputDataValidationResponseDto, Formatting.Indented, _jsonSerializerSettings);

            context.Response.ContentType = JSON;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(result);
        }

        private Task HandleBusinessExceptionAsync(HttpContext context, BusinessException exception)
        {
            var responseDetails = new List<ResponseDetail>();

            foreach (var error in exception.Details)
            {
                var detail = new ResponseDetail
                {
                    Name = error.Key.ToCamelCase(),
                    Messages = new List<string>
                    {
                        error.Value
                    }
                };

                if (detail.Messages.Count > 0) responseDetails.Add(detail);
            }

            var businessExceptionDto = new BadResponseDto
            {
                Type = BUSINESS_VALIDATION,
                Details = responseDetails
            };

            var result = JsonConvert.SerializeObject(businessExceptionDto, Formatting.Indented, _jsonSerializerSettings);

            context.Response.ContentType = JSON;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(result);
        }

        private Task HandleUnhandledExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new InternalServerErrorResponseDto
            {
                CorrelationId = context.TraceIdentifier,
                Type = UNEXPECTED_SERVERERROR,
                Details = new List<ResponseDetail>
                {
                    new ResponseDetail
                    {
                        Name = UNEXPECTED_SERVERERROR,
                        Messages = new List<string>
                        {
                            INTERNAL_SERVER_ERROR_MESSAGE
                        }
                    }
                }
            };

            var result = JsonConvert.SerializeObject(response, Formatting.Indented, _jsonSerializerSettings);

            context.Response.ContentType = JSON;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
