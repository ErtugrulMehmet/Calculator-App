using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_App
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options): base(options) 
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkingWater>().ToTable("DrinkingWater");
        }
        public DbSet<DrinkingWater> DrinkingWaters { get; set; }
    }



}
