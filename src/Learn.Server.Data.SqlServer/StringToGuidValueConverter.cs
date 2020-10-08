using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data.SqlTypes;

namespace Learn.Server.Data.SqlServer
{
    internal class StringToGuidValueConverter : ValueConverter<string, SqlGuid>
    {
        public StringToGuidValueConverter() : base(x => new SqlGuid(x), x => x.ToString())
        {
        }
    }
}