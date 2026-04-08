using Microsoft.EntityFrameworkCore;
using ProjectManagement.Domain.Common;
using System.Reflection;

namespace ProjectManagement.Infrastructure
{
    public sealed class ManagementDbContext 
        : DbContext, IDbContext
    {
        public ManagementDbContext(DbContextOptions options)
            :base(options) 
        { 
        }

        DbSet<T> IDbContext.Set<T>()
            => base.Set<T>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
