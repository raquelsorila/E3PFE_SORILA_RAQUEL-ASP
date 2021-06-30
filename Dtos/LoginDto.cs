using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    // data transfer objects are used to avoid passing directly to the model
    public class LoginDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
