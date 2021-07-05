using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    // data transfer objects are used to avoid passing directly to the model
    public class RegisterDto
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Section { set; get; }
        public string Hobby { set; get; }
    }
}
