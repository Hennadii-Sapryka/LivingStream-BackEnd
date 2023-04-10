
using System;
using System.Collections.Generic;


namespace LivingStream.Data.Entities
{

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }

        public bool IsPolicyAccepted { get; set; } = false;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }

        public ICollection<FcmToken> FCMTokens { get; set; } = new List<FcmToken>();
    }
}
