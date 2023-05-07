using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string>? ValidationErrors { get; set; }

        public BadRequestException(string name) : base(name)
        {
        }

        public BadRequestException(string name, ValidationResult validationResult) : base(name)
        {
            ValidationErrors = new();

            foreach (var error in validationResult.Errors)
            {
                ValidationErrors.Add(error.ErrorMessage);
                //name += ":" + error.ErrorMessage;
            }
        }
    }
}