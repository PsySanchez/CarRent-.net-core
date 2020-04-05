using System;
using System.Collections.Generic;

namespace CarRent.DAL.Models
{
    public partial class Tokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime? LifeTime { get; set; }
        public int UserId { get; set; }

        public virtual Users User { get; set; }
    }
}
