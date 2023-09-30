using HackYeahGWIZDapi.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace HackYeahGWIZDapi.AppContext
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {

        }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventPhoto> EventPhotos { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
