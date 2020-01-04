using System;
using System.Collections.Generic;
using Betting_Aggregator.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Betting_Aggregator.API.Dtos
{
    public class BadRequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            context.Result = new BadRequestObjectResult(
                BadRequestDtoHelper.GetResponseDto(context.ModelState));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }

    public static class BadRequestDtoHelper
    {
        public static BadResponseDto GetResponseDto(ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return null;

            var badRequestResponseDetail = new List<ResponseDetail>();

            foreach (var errorState in modelState)
            {
                var detail = GetDetail(errorState);

                if (detail.Messages.Count > 0) badRequestResponseDetail.Add(detail);
            }

            return new BadResponseDto
            {
                Type = ExceptionHandler.FIELD_VALIDATION,
                Details = badRequestResponseDetail
            };
        }

        private static ResponseDetail GetDetail(KeyValuePair<string, ModelStateEntry> errorState)
        {
            var detail = new ResponseDetail
            {
                Name = errorState.Key.ToCamelCase(),
                Messages = new List<string>()
            };

            if (errorState.Value?.Errors == null) return detail;

            foreach (var error in errorState.Value.Errors) detail.Messages.Add(GetErrorFrom(error));

            return detail;
        }

        private static string GetErrorFrom(ModelError error)
        {
            if (error == null) return string.Empty;

            return string.IsNullOrWhiteSpace(error.ErrorMessage) ? ExceptionHandler.INVALID_DATATYPE : error.ErrorMessage;
        }
    }
}
