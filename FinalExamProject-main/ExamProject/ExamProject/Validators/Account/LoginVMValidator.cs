using ExamProject.ViewModel.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProject.Validators.Account
{
    public class LoginVMValidator:AbstractValidator<LoginVM>
    {
        public LoginVMValidator()
        {
            RuleFor(r => r.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(r => r.Password).NotEmpty().NotNull();

        }
    }
}
