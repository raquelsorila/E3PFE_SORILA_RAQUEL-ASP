﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class UserContext : DbContext //dependency injection
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { set; get; }

        protected override void OnModelCreating(ModelBuilder  modelBuilder) // using modelbuilder to make the email a unique data
        {
            modelBuilder.Entity<AppUser>(
                entity => { entity.HasIndex(e => e.Email).IsUnique(); });
        }
    }
}
