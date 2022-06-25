using ExamProject.ViewModel.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Validators.Account
{
    public class RegisterVMValidator:AbstractValidator<RegisterVM>
    {
        public RegisterVMValidator()
        {
            RuleFor(r => r.Email).NotNull().NotEmpty().EmailAddress().MaximumLength(255);
            RuleFor(r => r.UserName).NotNull().NotEmpty().Length(5,75);
            RuleFor(r => r.Password).NotNull().NotEmpty().Length(8,75);
            RuleFor(r => r.Password).NotNull().NotEmpty().Length(8,75);


        }
    }
}
