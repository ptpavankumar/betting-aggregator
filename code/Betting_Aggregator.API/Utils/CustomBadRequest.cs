using System.Collections.Generic;
using Betting_Aggregator.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Betting_Aggregator.API.Utils
{
    public class CustomBadRequest : ResponseDto
    {
        public CustomBadRequest(ActionContext context)
        {
            Type = ExceptionHandler.FIELD_VALIDATION;
            Details = new List<ResponseDetail>();
            ConstructErrorMessages(context);
        }

        private void ConstructErrorMessages(ActionContext context)
        {
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    var errorMessages = new List<string>();
                    for (var i = 0; i < errors.Count; i++)
                    {
                        errorMessages.Add(string.IsNullOrEmpty(errors[i].ErrorMessage) ? "The input was not valid." : errors[i].ErrorMessage);
                    }

                    var detail = new ResponseDetail
                    {
                        Name = key,
                        Messages = errorMessages
                    };
                    Details.Add(detail);
                }
            }
        }
    }
}
