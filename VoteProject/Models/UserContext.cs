using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoteProject.Models
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                new User { Id=1, Login="fillial1", Password="159753"},
                new User { Id=2, Login="fillial2", Password="159753"},
                new User { Id=3, Login="fillial3", Password="159753"}
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
