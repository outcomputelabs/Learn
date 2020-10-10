using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Globalization;

namespace Learn.Server.Data.SqlServer
{
    internal class StringToGuidValueConverter : ValueConverter<string, Guid>
    {
        public StringToGuidValueConverter() : base(
            x => Guid.Parse(x),
            x => x.ToString("D", CultureInfo.InvariantCulture))
        {
        }
    }
}