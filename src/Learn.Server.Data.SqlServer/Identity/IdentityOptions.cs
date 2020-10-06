using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Learn.Server.Data.SqlServer.Identity
{
    public class IdentityOptions
    {
        [Required]
        public string ConnectionString { get; set; } = Empty;
    }
}