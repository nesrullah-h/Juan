using ExamProject.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Validators
{
    public class FeaturesVMValidators:AbstractValidator<FeaturesVM>
    {
        public FeaturesVMValidators()
        {
            RuleFor(f => f.Description).NotEmpty().NotNull().MaximumLength(255);
            RuleFor(f => f.Title).NotEmpty().NotNull().MaximumLength(55);
            RuleFor(f => f.IconUrl).NotEmpty().NotNull();
           
        }
    }
}
