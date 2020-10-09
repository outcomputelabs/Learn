using System;
using System.ComponentModel.DataAnnotations;

namespace Learn.WebApp.Server.Orleans
{
    public class LearnClusterClientHostedServiceOptions
    {
        [Range(0, int.MaxValue)]
        public int MaxConnectionAttempts { get; set; } = 10;
    }
}