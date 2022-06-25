using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.ViewModel.Account
{
    public class RegisterVM
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required, MaxLength(100)]
        public string UserName { get; set; }
        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string Pasword { get; set; }
        [DataType(DataType.Password), Compare(nameof(Pasword))]
        public string ConfirmPasword { get; set; }
    }
}
