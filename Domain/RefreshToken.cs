using Domain.ModelBase;

namespace Domain
{
    public class RefreshToken : BaseModel
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
