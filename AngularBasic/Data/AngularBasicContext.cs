
using AngularBasic.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularBasic.Data
{   
    public class AngularBasicContext : IdentityDbContext<StoreUser>
    { 
        public AngularBasicContext(DbContextOptions<AngularBasicContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }  
    }
}
