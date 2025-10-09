using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectTest.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int IdUsuario { get; set; }

        [Column("User_Name")]
        public string UserName { get; set; }

        [Column("Full_Name")]
        public string FullName { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Refresh_Token")]
        public string? RefreshToken { get; set; }

        [Column("Refresh_Token_expiry_time")]
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}
