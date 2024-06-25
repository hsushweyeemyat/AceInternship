using AceInternship.RestApi.Model;
using AceInternship.RestApi.Models;
using AceInternship.RestApi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceInternship.RestApi.Database;

internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        public DbSet<FoodModel> Food { get; set; }
        public DbSet<BlogDto> Blog { get; set; }
    }
