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

        public ValidationArchitectureException(string name , string message)
        {
            Errors = new Dictionary<string, string>(){[name] = message };
        }

        public void AddError(string name, string message)
        {
            Errors.Add(name,message);
        }
    }
}
