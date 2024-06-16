using System;

namespace HrDataManager.Application.Common.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string message) : base(message)
        {
        }

        public BusinessRuleValidationException(string message, Exception innerException = null) : base(message, innerException)
        {
        }

        public BusinessRuleValidationException(string message, string details) : base(message)
        {
            this.Details = details;
        }

        public BusinessRuleValidationException(string message, string details, Exception innerException = null) : base(message, innerException)
        {
            this.Details = details;
        }

        public string Details { get; }
    }
}
