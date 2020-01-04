using System;
using System.Collections.Generic;

namespace Betting_Aggregator.Utils
{
    public class BadRequestInputDataException : Exception
    {
        public BadRequestInputDataException(IDictionary<string, string> details)
            : base(string.Empty)
        {
            Details = details;
        }

        public BadRequestInputDataException(IDictionary<string, string> details, string message)
            : base(message)
        {
            Details = details;
        }

        public BadRequestInputDataException(IDictionary<string, string> details, string message,
            Exception innerException) :
            base(message, innerException)
        {
            Details = details;
        }

        public IDictionary<string, string> Details { get; }
    }
}