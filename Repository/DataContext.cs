using Microsoft.EntityFrameworkCore;
using C__tutorials.Models;
#pragma warning disable
namespace C__tutorials.Repository
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> user { get; set; }   
    }
}