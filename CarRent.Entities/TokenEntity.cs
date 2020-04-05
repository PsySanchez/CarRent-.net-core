using System;

namespace CarRent.Entities
{
    public class TokenEntity
    {
        public int? Id { get; set; }
        public string Token { get; set; }
        public DateTime LifeTime { get; set; }
        public int UserId { get; set; }
    }
}
