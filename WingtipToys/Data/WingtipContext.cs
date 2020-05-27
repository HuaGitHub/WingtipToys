using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WingtipToys.Data.Entities;

namespace WingtipToys.Data
{
    public class WingtipContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public WingtipContext(DbContextOptions<WingtipContext> options): base(options)
        {
        }
    }
}
