using System.Runtime.Serialization;
using C__tutorials.DTO;
using C__tutorials.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace C__tutorials.Repository;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    public DbSet<User> user { get; set; }

    public DbSet<BankUser> BankUsers { get; set; }
    public DbSet<Accounts> Accounts { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<ClientDetails> ClientDetailsEnumerable { get; set; }
}