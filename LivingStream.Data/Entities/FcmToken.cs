namespace LivingStream.Data.Entities
{
    public class FcmToken
    {
        public int Id { get; set; }

        public string Token { get; set; } = string.Empty;

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
