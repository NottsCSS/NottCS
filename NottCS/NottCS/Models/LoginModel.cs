using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using NottCS.Validations;

namespace NottCS.Models
{
    internal class LoginModel
    {
        public ValidatableObject<string> Username { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<string> Password { get; set; } = new ValidatableObject<string>();
    }
}
