using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.WebApp.Server.Orleans
{
    internal class LearnClusterClientHostedServiceOptions
    {
        [Range(0, int.MaxValue)]
        public int MaxConnectionAttempts { get; set; } = 10;
    }
}