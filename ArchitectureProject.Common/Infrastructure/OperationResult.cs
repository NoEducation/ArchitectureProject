using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureProject.Common.Infrastructure
{
    public class OperationResult
    {
        private readonly List<string> _errors;

        public bool IsSuccessful => !EnumerableExtensions.Any(_errors);
        public bool IsFailure => EnumerableExtensions.Any(_errors);


        protected OperationResult(IEnumerable<string> errors = null)
        {
            _errors = (errors ?? Enumerable.Empty<string>()) as List<string>;
        }

        public OperationResult AddError(params string[] errors)
        {
            if (errors == null || !errors.Any())
                throw new ArgumentException("No error for failed operation was provided");

            this._errors.AddRange(errors);

            return this;
        }

        public static OperationResult Fail(params string[] errors)
        {
            if (errors == null || !errors.Any())
                throw new ArgumentException("No error for failed operation was provided");

            return new OperationResult(errors);
        }

        public static OperationResult Fail<T>(T value, params string[] errors)
        {
            if (errors == null || !errors.Any())
                throw new ArgumentException("No error for failed operation was provided");

            return new OperationResult<T>(value, errors);
        }

        public static OperationResult Ok()
        {
            return new OperationResult();
        }

        public static OperationResult Ok<T>(T value)
        {
            return new OperationResult<T>(value);
        }
    }

    public class OperationResult<T> : OperationResult
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (IsFailure)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        protected internal OperationResult(T value, params string[] errors) : base(errors)
        {
            this._value = value;
        }
    }
}
