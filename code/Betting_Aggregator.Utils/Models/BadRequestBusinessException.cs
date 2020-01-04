using System;
using System.Collections.Generic;

namespace Betting_Aggregator.Utils
{
    public class BadRequestBusinessException : Exception
    {
        public BadRequestBusinessException(IDictionary<string, string> details)
            : base(string.Empty)
        {
            Details = details;
        }

        public BadRequestBusinessException(IDictionary<string, string> details, string message)
            : base(message)
        {
            Details = details;
        }

        public BadRequestBusinessException(IDictionary<string, string> details, string message, Exception innerException) :
            base(message, innerException)
        {
            Details = details;
        }

        public IDictionary<string, string> Details { get; }
    }
}