using Microsoft.AspNetCore.Identity;

namespace MultiShop.Mvc.Models.Response
{
    public class LoginResponse
    {
        public string Email { get; set; }
        public Result Result { get; set; }
    }
    public class Result
    {
        public bool succeeded { get; set; }
        public bool isLockedOut { get; set; }
        public bool isNotAllowed { get; set; }
        public bool requiresTwoFactor { get; set; }
    }
}
