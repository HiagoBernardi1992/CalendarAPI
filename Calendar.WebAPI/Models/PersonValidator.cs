using Calendar.WebAPI.Enums;
using FluentValidation;

namespace Calendar.WebAPI.Models
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("The entity person needs a name.")
                .Length(5, 100).WithMessage("The property name must have between 5 and 100 characteres.");

            RuleFor(p => p.Role)
                .Must(RoleEnumValidator).WithMessage("The property role needs to be a Candidate(1) or an Interviewer(0)");  
        }

        private static bool RoleEnumValidator(Role role)
        {
            return role != Role.Candidate && role != Role.Interviewer ? false : true;
        }
    }
}