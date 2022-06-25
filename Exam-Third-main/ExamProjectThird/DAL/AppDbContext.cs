using ExamProjectThird.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamProjectThird.DAL
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Cheef> Cheefs { get; set; }
        public DbSet<EventSlider> EventSliders { get; set; }
        public DbSet<IntroSlider> IntroSliders { get; set; }
        public DbSet<RestaurantPhoto> RestaurantPhotos { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<WhyChoose> WhyChoose { get; set; }
    }
}
