using Microsoft.EntityFrameworkCore;

using Res.Domain.Entities;

namespace Res.Infrastructure.Data;

public partial class ResDbContext : DbContext
{
    public ResDbContext()
    {
    }

    public ResDbContext(DbContextOptions<ResDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Address> Address { get; set; }

    public virtual DbSet<BoxCash> BoxCash { get; set; }

    public virtual DbSet<BranchStore> BranchStore { get; set; }

    public virtual DbSet<BranchStoreEmployee> BranchStoreEmployee { get; set; }

    public virtual DbSet<Cart> Cart { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Customer> Customer { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddress { get; set; }

    public virtual DbSet<CustomerType> CustomerType { get; set; }

    public virtual DbSet<Drink> Drink { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<Food> Food { get; set; }

    public virtual DbSet<GeographicalZone> GeographicalZone { get; set; }

    public virtual DbSet<Job> Job { get; set; }

    public virtual DbSet<ManagerZoneBranchStore> ManagerZoneBranchStore { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderDrink> OrderDrink { get; set; }

    public virtual DbSet<OrderFood> OrderFood { get; set; }

    public virtual DbSet<PayBox> PayBox { get; set; }

    public virtual DbSet<Payment> Payment { get; set; }

    public virtual DbSet<Reservation> Reservation { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Ticket> Ticket { get; set; }

    public virtual DbSet<UserAccount> UserAccount { get; set; }

    public virtual DbSet<ActiveUserAccountEmployee> ActiveUserAccountEmployee { get; set; }

    public virtual DbSet<ActiveUserAccountCustomer> ActiveUserAccountCustomer { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            option => option.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery).MigrationsAssembly("Res.Api")
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ResDbContext).Assembly);
    }
}