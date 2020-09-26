using System.ComponentModel.DataAnnotations;
using static System.String;

namespace Learn.Server.Data.SqlServer
{
    public class SqlServerRepositoryOptions
    {
        [Required]
        public string ConnectionString { get; set; } = Empty;
    }
}