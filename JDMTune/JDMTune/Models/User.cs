using System;
using System.Linq;
using LinqToDB.Mapping;

namespace JDMTune.Models
{
    [Table(Name = "Users")]
    public class User
    {
        [Column("user_id")]
        public Guid Id { get; set; }
        [Column("user_email")]
        public string Email { get; set; }
        [Column("user_password")]
        public string Password { get; set; }
        [Column("user_role")]
        public string Role { get; set; }
    }
}