using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivingStream.Domain.Dto.User
{
    public class CreateUserDto
    {

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsPolicyAccepted { get; set; } = false;
    }
}
