using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DAL
{
   public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) { }

        public DbSet<Features> Features { get; set; }
       // protected override void OnModelCreating(ModelBuilder modelBuilder)
       // {
       //     modelBuilder.Entity<Features>().HasData(
       //       new Features { Id = 1,Title= "Responsive Bootstrap Template",Description= "orem ipsum dolizzle sit tellivizzle, boom shackalack adipiscing elit. Nullam mofo velizzle, get down get down stuff, sure cool, dizzle vizzle, arcu. Yo mamma izzle tortizzle.", IconUrl= $"<i class={"fas fa - tablet - alt"}></i>" },
       //       new Features { Id = 2, Title = "Responsive Bootstrap Template", Description = "orem ipsum dolizzle sit tellivizzle, boom shackalack adipiscing elit. Nullam mofo velizzle, get down get down stuff, sure cool, dizzle vizzle, arcu. Yo mamma izzle tortizzle.", IconUrl = $"<i class={"fas fa - tablet - alt"}></i>" },
       //          new Features { Id = 3, Title = "Responsive Bootstrap Template", Description = "orem ipsum dolizzle sit tellivizzle, boom shackalack adipiscing elit. Nullam mofo velizzle, get down get down stuff, sure cool, dizzle vizzle, arcu. Yo mamma izzle tortizzle.", IconUrl = $"<i class={"fas fa - tablet - alt"}></i>" }





       //);
       //     base.OnModelCreating(modelBuilder);
       // }

    }
}
