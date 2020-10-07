using Microsoft.AspNetCore.Identity;
using System;

namespace Learn.Server.Data
{
    public class ApplicationUser : IdentityUser
    {
        public override string Id
        {
            get => base.Id;
            set
            {
                if (value != null && !int.TryParse(value, out var _))
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }

                base.Id = value;
            }
        }

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