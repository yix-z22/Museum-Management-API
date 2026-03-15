using A2Template.Models;
using Microsoft.EntityFrameworkCore;

namespace A2Template.Data
{
    public class A2DbContext : DbContext
    {
        public A2DbContext(DbContextOptions<A2DbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Staff> Staff { get; set; }
    }
}