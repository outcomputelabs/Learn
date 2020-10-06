using Microsoft.AspNetCore.Identity;
using System;

namespace Learn.Server.Data.SqlServer.Identity.Models
{
    public class IdentityUser : IdentityUser<int>
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

        public override string SecurityStamp
        {
            get => base.SecurityStamp;
            set
            {
                if (value != null && !Guid.TryParse(value, out var _))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                base.SecurityStamp = value;
            }
        }
    }
}