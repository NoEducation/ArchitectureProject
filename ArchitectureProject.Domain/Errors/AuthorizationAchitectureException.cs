using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureProject.Domain.Errors
{
    public class AuthorizationArchitectureException : Exception
    {
        public ICollection<string> Errors { get; set; }

        public AuthorizationArchitectureException(string[] errors)
        {
            Errors = errors;
        }

        public void AddError(string source)
        {
            Errors.Add(source);
        }
    }
}
