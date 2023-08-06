
using GrowUp.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowUp.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


            public DbSet<Service> Services { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<ApplicationUser> ApplicationUsers { get; set; }
            public DbSet<Contentube> Contentubes { get; set; }
            public DbSet<Reactube> Reactubes { get; set; }
            public DbSet<Watchtube> Watchtubes { get; set; }
            public DbSet<Country> Countries { get; set; }
            public DbSet<PayMonthlyPlan> PayMonthlyPlans { get; set; }
            public DbSet<OrderHeader> OrderHeaders { get; set; }
            public DbSet<OrderDetail> OrderDetaills { get; set; }


    }
}

