
using System.Collections.Generic;


namespace LivingStream.Data.Entities
{

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsPolicyAccepted { get; set; } = false;

        public ICollection<FcmToken> FCMTokens { get; set; } = new List<FcmToken>();
    }
}
