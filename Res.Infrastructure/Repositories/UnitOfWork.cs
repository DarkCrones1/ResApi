using Microsoft.Extensions.Configuration;

using Res.Common.Interfaces.Repositories;
using Res.Domain.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Interfaces.Repositories;

// using Res.Domain.Interfaces.Repositories;
using Res.Infrastructure.Data;

namespace Res.Infrastructure.Repositories;

public class UnirOfWork : IUnitOfWork
{
    protected readonly ResDbContext _dbContext;

    private readonly IConfiguration _configuration;

    protected ICrudRepository<Address> _addressRepository;

    protected IBoxCashRepository _boxCashRepository;

    protected IBranchStoreRepository _branchStoreRepository;

    protected ICrudRepository<BranchStoreEmployee> _branchStoreEmployeeRepository;

    protected ICrudRepository<Cart> _cartRepository;

    protected ICategoryRepository _categoryRepository;

    protected ICustomerRepository _customerRepository;

    protected ICatalogBaseRepository<CustomerType> _customerTypeRepository;

    protected ICatalogBaseRepository<Drink> _drinkRepository;

    protected IEmployeeRepository _employeeRepository;

    protected ICatalogBaseRepository<Food> _foodRepository;

    protected ICatalogBaseRepository<GeographicalZone> _geographicalZoneRepository;

    protected ICatalogBaseRepository<Job> _jobRepository;

    protected ICrudRepository<ManagerZoneBranchStore> _managerZoneBranchStoreRepository;

    protected ICatalogBaseRepository<Menu> _menuRepository;

    protected ICrudRepository<Order> _orderRepository;

    protected ICrudRepository<OrderDrink> _orderDrinkRepository;

    protected ICrudRepository<OrderFood> _orderFoodRepository;

    protected ICrudRepository<PayBox> _payBoxRepository;

    protected ICrudRepository<Payment> _paymentRepository;

    protected ICrudRepository<Reservation> _reservationRepository;

    protected ICatalogBaseRepository<Rol> _rolRepository;

    protected ICrudRepository<Ticket> _ticketRepository;

    protected IUserAccountRepository _userAccountRepository;

    protected IRetrieveRepository<ActiveUserAccountEmployee> _activeUserAccountEmployeeRepository;

    protected IRetrieveRepository<ActiveUserAccountCustomer> _activeUserAccountCustomerRepository;

    private bool disposed;

    public UnirOfWork(ResDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;

        this._configuration = configuration;

        disposed = false;

        _addressRepository = new CrudRepository<Address>(_dbContext);

        _boxCashRepository = new BoxCashRepository(_dbContext);

        _branchStoreRepository = new BranchStoreRepository(_dbContext);

        _branchStoreEmployeeRepository = new CrudRepository<BranchStoreEmployee>(_dbContext);

        _cartRepository = new CrudRepository<Cart>(_dbContext);

        _categoryRepository = new CategoryRepository(_dbContext);

        _customerRepository = new CustomerRepository(_dbContext);

        _customerTypeRepository = new CatalogBaseRepository<CustomerType>(_dbContext);

        _drinkRepository = new CatalogBaseRepository<Drink>(_dbContext);

        _employeeRepository = new EmployeeRepository(_dbContext);

        _foodRepository = new CatalogBaseRepository<Food>(_dbContext);

        _geographicalZoneRepository = new CatalogBaseRepository<GeographicalZone>(_dbContext);

        _jobRepository = new CatalogBaseRepository<Job>(_dbContext);

        _managerZoneBranchStoreRepository = new CrudRepository<ManagerZoneBranchStore>(_dbContext);

        _menuRepository = new CatalogBaseRepository<Menu>(_dbContext);

        _orderRepository = new CrudRepository<Order>(_dbContext);

        _orderDrinkRepository = new CrudRepository<OrderDrink>(_dbContext);

        _orderFoodRepository = new CrudRepository<OrderFood>(_dbContext);

        _payBoxRepository = new CrudRepository<PayBox>(_dbContext);

        _paymentRepository = new CrudRepository<Payment>(_dbContext);

        _reservationRepository = new CrudRepository<Reservation>(_dbContext);

        _rolRepository = new CatalogBaseRepository<Rol>(_dbContext);

        _ticketRepository = new CrudRepository<Ticket>(_dbContext);

        _userAccountRepository = new UserAccountRepository(_dbContext);

        _activeUserAccountEmployeeRepository = new RetrieveRepository<ActiveUserAccountEmployee>(_dbContext);

        _activeUserAccountCustomerRepository = new RetrieveRepository<ActiveUserAccountCustomer>(_dbContext);
    }

    public ICrudRepository<Address> AddressRepository => _addressRepository;

    public IBoxCashRepository BoxCashRepository => _boxCashRepository;

    public IBranchStoreRepository BranchStoreRepository => _branchStoreRepository;

    public ICrudRepository<BranchStoreEmployee> BranchStoreEmployeeRepository => _branchStoreEmployeeRepository;

    public ICrudRepository<Cart> CartRepository => _cartRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;

    public ICustomerRepository CustomerRepository => _customerRepository;

    public ICatalogBaseRepository<CustomerType> CustomerTypeRepository => _customerTypeRepository;

    public ICatalogBaseRepository<Drink> DrinkRepository => _drinkRepository;

    public IEmployeeRepository EmployeeRepository => _employeeRepository;

    public ICatalogBaseRepository<Food> FoodRepository => _foodRepository;

    public ICatalogBaseRepository<GeographicalZone> GeographicalZoneRepository => _geographicalZoneRepository;

    public ICatalogBaseRepository<Job> JobRepository => _jobRepository;

    public ICrudRepository<ManagerZoneBranchStore> ManagerZoneBranchStoreRepository => _managerZoneBranchStoreRepository;

    public ICatalogBaseRepository<Menu> MenuRepository => _menuRepository;

    public ICrudRepository<Order> OrderRepository => _orderRepository;

    public ICrudRepository<OrderDrink> OrderDrinkRepository => _orderDrinkRepository;

    public ICrudRepository<OrderFood> OrderFoodRepository => _orderFoodRepository;

    public ICrudRepository<PayBox> PayBoxRepository => _payBoxRepository;

    public ICrudRepository<Payment> PaymentRepository => _paymentRepository;

    public ICrudRepository<Reservation> ReservationRepository => _reservationRepository;

    public ICatalogBaseRepository<Rol> RolRepository => _rolRepository;

    public ICrudRepository<Ticket> TicketRepository => _ticketRepository;

    public IUserAccountRepository UserAccountRepository => _userAccountRepository;

    public IRetrieveRepository<ActiveUserAccountEmployee> ActiveUserAccountEmployeeRepository => _activeUserAccountEmployeeRepository;

    public IRetrieveRepository<ActiveUserAccountCustomer> ActiveUserAccountCustomerRepository => _activeUserAccountCustomerRepository;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
            if (disposing)
                _dbContext.Dispose();

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}