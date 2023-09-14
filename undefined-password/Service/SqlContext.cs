using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using undefined_password.Models;

namespace undefined_password.Service
{
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CreatePassword> CreatePasswords { get; set; }

    }
}
