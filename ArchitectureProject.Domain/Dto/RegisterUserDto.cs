using System;
using System.Collections.Generic;
using System.Text;

namespace ArchitectureProject.Domain.Dto
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
