using FluentValidation.Results;

namespace Raika.Common.SharedApplicationServices.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]> Failures { get; }
        public ValidationException()
            : base(ApplicationExceptionMessages.ValidationError())
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
