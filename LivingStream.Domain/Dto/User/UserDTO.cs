﻿

namespace LivingStream.Data.Entities
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? FCMToken { get; set; }

        public bool IsPolicyAccepted { get; set; } = false;
    }
}
