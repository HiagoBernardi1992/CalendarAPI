using System;
using FluentValidation;

namespace Calendar.WebAPI.Models
{
    public class AvailabilityValidator: AbstractValidator<Availability>
    {
        public AvailabilityValidator()
        {
            RuleFor(p => p.Start)
                .NotEmpty().WithMessage("The entity Availability needs a Start.")
                .Must(ValidStartedDate).WithMessage("The property Start needs to be a beginner of a hour and higher than now.");

            RuleFor(p => p.End)
                .NotEmpty().WithMessage("The entity Availability needs a End.")
                .Must(ValidStartedDate).WithMessage("The property End needs to be a beginner of a hour and higher than now.");

            RuleFor(p => p.PersonId)
                .NotEmpty().WithMessage("The entity Availability needs a personId.");
        }

        private static bool ValidStartedDate(DateTime date)
        {
            if(date.Minute != 0 || date.Second != 0) 
                return false;
            if(date < DateTime.Now)
                return false;
            return true;
        }
    }
}