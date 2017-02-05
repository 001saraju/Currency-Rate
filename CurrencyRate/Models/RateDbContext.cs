using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CurrencyRate.Models
{
    public class RateDbContext : DbContext
    {
        public DbSet<Rate> Rates { get; set; }
    }
}