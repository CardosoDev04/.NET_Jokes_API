using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JokesApp.Models
{
    public class JokesContext : IdentityDbContext
    {
        public DbSet<Joke> Jokes { get; set; }
        public new DbSet<User> Users { get; set; }

        public JokesContext(DbContextOptions options) : base (options)
        {
            
        }
    }
}
