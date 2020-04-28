namespace OpenApiHttpValidation.Validators
{
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult
    {
        protected ValidationResult(IEnumerable<string> errors)
        {
            this.Errors = errors;
        }

        public virtual IEnumerable<string> Errors { get; }

        public static ValidationResult Ok()
        {
            return new ValidationResult(Enumerable.Empty<string>());
        }

        public static ValidationResult WithErrors(IEnumerable<string> errors)
        {
            return new ValidationResult(errors);
        }

        public static ValidationResult WithError(string error)
        {
            return new ValidationResult(new[] { error });
        }
    }

    public class AggregateValidationResult : ValidationResult
    {
        private readonly IEnumerable<ValidationResult> results;

        public AggregateValidationResult(IEnumerable<ValidationResult> results)
            : base(Enumerable.Empty<string>())
        {
            this.results = results;
        }

        public override IEnumerable<string> Errors => results.SelectMany(x => x.Errors);
    }
}
