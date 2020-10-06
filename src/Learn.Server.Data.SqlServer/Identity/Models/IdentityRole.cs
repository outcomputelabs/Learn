using Microsoft.AspNetCore.Identity;
using System;

namespace Learn.Server.Data.SqlServer.Identity.Models
{
    public class IdentityRole : IdentityRole<int>
    {
        public override string ConcurrencyStamp
        {
            get => base.ConcurrencyStamp;
            set
            {
                if (value != null && !Guid.TryParse(value, out var _))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                base.ConcurrencyStamp = value;
            }
        }
    }
}