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
        private readonly string _internalServerErrorMessage = "Please contact administrator and present correlation identifier for troubleshooting";
        private const string JSON = "application/json";
        private const string BusinessValidation = "business-validation";
        private const string FieldValidation = "field-validation";


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
            catch (BadRequestBusinessException businessException)
            {
                await HandleBusinessExceptionAsync(context, businessException);
            }
            catch (BadRequestInputDataException badRequestException)
            {
                await HandleBadRequestExceptionAsync(context, badRequestException);
            }
            catch (Exception ex)
            {
                await HandleUnhandledExceptionAsync(context, ex);
            }
        }

        private Task HandleBadRequestExceptionAsync(HttpContext context, BadRequestInputDataException exception)
        {
            var result = JsonConvert.SerializeObject(HandleBadRequestException(exception), Formatting.Indented, _jsonSerializerSettings);

            context.Response.ContentType = JSON;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(result);
        }

        private Task HandleBusinessExceptionAsync(HttpContext context, BadRequestBusinessException exception)
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
                Type = BusinessValidation,
                Details = responseDetails
            };

            var result = JsonConvert.SerializeObject(businessExceptionDto, Formatting.Indented, _jsonSerializerSettings);

            context.Response.ContentType = JSON;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(result);
        }

        private BadResponseDto HandleBadRequestException(BadRequestInputDataException badRequestException)
        {
            
        var badRequestResponseDetail = new List<ResponseDetail>();

            foreach (var error in badRequestException.Details)
            {
                var detail = new ResponseDetail
                {
                    Name = error.Key.ToCamelCase(),
                    Messages = new List<string>
                    {
                        error.Value
                    }
                };

                if (detail.Messages.Count > 0) badRequestResponseDetail.Add(detail);
            }

            return new BadResponseDto
            {
                Type = FieldValidation,
                Details = badRequestResponseDetail
            };
        }

        private Task HandleUnhandledExceptionAsync(HttpContext context, Exception exception)
        {
            const string UNEXPECTEDSERVERERROR = "unexpected-internal-server-error";

            var response = new InternalServerErrorResponseDto
            {
                CorrelationId = context.TraceIdentifier,
                Type = UNEXPECTEDSERVERERROR,
                Details = new List<ResponseDetail>
                {
                    new ResponseDetail
                    {
                        Name = UNEXPECTEDSERVERERROR,
                        Messages = new List<string>
                        {
                            _internalServerErrorMessage
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
