using System;
using System.Collections.Generic;

namespace Betting_Aggregator.Utils
{
    public class InputDataValidationException : Exception
    {
        public InputDataValidationException(IDictionary<string, string> details)
            : base(string.Empty)
        {
            Details = details;
        }

        public InputDataValidationException(IDictionary<string, string> details, string message)
            : base(message)
        {
            Details = details;
        }

        public InputDataValidationException(IDictionary<string, string> details, string message,
            Exception innerException) :
            base(message, innerException)
        {
            Details = details;
        }

        public IDictionary<string, string> Details { get; }
    }
}