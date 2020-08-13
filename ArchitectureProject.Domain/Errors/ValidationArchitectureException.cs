using System;
using System.Collections.Generic;

namespace ArchitectureProject.Domain.Errors
{
    public class ValidationArchitectureException : Exception
    {
        public Dictionary<string, string> Errors { get; set; }

        public ValidationArchitectureException(Dictionary<string,string> errors)
        {
            Errors = errors;
        }

        public void AddError(string name, string message)
        {
            Errors.Add(name,message);
        }
    }
}
