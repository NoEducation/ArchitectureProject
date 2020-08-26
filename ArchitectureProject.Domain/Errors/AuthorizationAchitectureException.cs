using System;
using System.Collections.Generic;

namespace ArchitectureProject.Domain.Errors
{
    public class AuthorizationArchitectureException : Exception
    {
        public ICollection<string> Errors { get; set; }

        #region Constructors

        public AuthorizationArchitectureException(string[] errors)
        {
            Errors = errors;
        }

        public AuthorizationArchitectureException(string error)
        {
            Errors = new List<string>() {error};
        }

        public AuthorizationArchitectureException()
        {
            Errors = new List<string>();
        }

        #endregion
        public void AddError(string source)
        {
            Errors.Add(source);
        }
    }
}
