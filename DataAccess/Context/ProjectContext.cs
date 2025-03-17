using Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Context
{
    public class ProjectContext : DbContext
    {
          
        public ProjectContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Domain> Domains { get; set; }
    }
}

