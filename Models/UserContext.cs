using Microsoft.EntityFrameworkCore;

namespace cd_c_loginRegistration.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get;set;}
    }
}