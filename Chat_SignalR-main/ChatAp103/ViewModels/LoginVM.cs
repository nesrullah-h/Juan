using System.ComponentModel.DataAnnotations;

namespace ChatAp103.ViewModels
{
    public class LoginVM
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
