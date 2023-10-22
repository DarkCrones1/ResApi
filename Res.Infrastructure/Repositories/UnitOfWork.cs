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
    private readonly ResDbContext _dbContext;

    private readonly IConfiguration _configuration;

    protected ICrudRepository<Address> _addressRepository;

    protected ICrudRepository<BoxCash> _boxCashRepository;

    protected ICrudRepository<BranchStore> _branchStoreRepository;

    protected ICrudRepository<BranchStoreEmployee> _branchStoreEmployeeRepository;

    protected ICrudRepository<Cart> _cartRepository;

    // protected ICrudRepository<CartDrink> _cartDrinkRepository;

    // protected ICrudRepository<CartFood> _cartFoodRepository;

    protected ICategoryRepository _categoryRepository;

    protected ICrudRepository<Customer> _customerRepository;

    protected ICrudRepository<CustomerAddress> _customerAddressRepository;

    protected ICrudRepository<CustomerType> _customerTypeRepository;

    protected ICrudRepository<Drink> _drinkRepository;

    protected ICrudRepository<Employee> _employeeRepository;

    protected ICrudRepository<Food> _foodRepository;

    protected ICrudRepository<GeographicalZone> _geographicalZoneRepository;

    protected ICrudRepository<Job> _jobRepository;

    protected ICrudRepository<ManagerZoneBranchStore> _managerZoneBranchStoreRepository;

    protected ICrudRepository<Menu> _menuRepository;

    protected ICrudRepository<Order> _orderRepository;

    protected ICrudRepository<OrderDrink> _orderDrinkRepository;

    protected ICrudRepository<OrderFood> _orderFoodRepository;

    protected ICrudRepository<PayBox> _payBoxRepository;

    protected ICrudRepository<Payment> _paymentRepository;

    protected ICrudRepository<Reservation> _reservationRepository;

    protected ICrudRepository<Rol> _rolRepository;

    protected ICrudRepository<Ticket> _ticketRepository;

    protected IUserAccountRepository _userAccountRepository;

    protected IRetrieveRepository<ActiveUserAccountEmployee> _activeUserAccountEmployeeRepository;

    private bool disposed;

    public UnirOfWork(ResDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;

        this._configuration = configuration;

        disposed = false;

        _addressRepository = new CrudRepository<Address>(_dbContext);

        _boxCashRepository = new CrudRepository<BoxCash>(_dbContext);

        _branchStoreRepository = new CrudRepository<BranchStore>(_dbContext);

        _branchStoreEmployeeRepository = new CrudRepository<BranchStoreEmployee>(_dbContext);

        _cartRepository = new CrudRepository<Cart>(_dbContext);

        // _cartDrinkRepository = new CrudRepository<CartDrink>(_dbContext);

        // _cartFoodRepository = new CrudRepository<CartFood>(_dbContext);

        _categoryRepository = new CategoryRepository(_dbContext);

        _customerRepository = new CrudRepository<Customer>(_dbContext);

        _customerAddressRepository = new CrudRepository<CustomerAddress>(_dbContext);

        _customerTypeRepository = new CrudRepository<CustomerType>(_dbContext);

        _drinkRepository = new CrudRepository<Drink>(_dbContext);

        _employeeRepository = new CrudRepository<Employee>(_dbContext);

        _foodRepository = new CrudRepository<Food>(_dbContext);

        _geographicalZoneRepository = new CrudRepository<GeographicalZone>(_dbContext);

        _jobRepository = new CrudRepository<Job>(_dbContext);

        _managerZoneBranchStoreRepository = new CrudRepository<ManagerZoneBranchStore>(_dbContext);

        _menuRepository = new CrudRepository<Menu>(_dbContext);

        _orderRepository = new CrudRepository<Order>(_dbContext);

        _orderDrinkRepository = new CrudRepository<OrderDrink>(_dbContext);

        _orderFoodRepository = new CrudRepository<OrderFood>(_dbContext);

        _payBoxRepository = new CrudRepository<PayBox>(_dbContext);

        _paymentRepository = new CrudRepository<Payment>(_dbContext);

        _reservationRepository = new CrudRepository<Reservation>(_dbContext);

        _rolRepository = new CrudRepository<Rol>(_dbContext);

        _ticketRepository = new CrudRepository<Ticket>(_dbContext);

        _userAccountRepository = new UserAccountRepository(_dbContext);

        _activeUserAccountEmployeeRepository = new RetrieveRepository<ActiveUserAccountEmployee>(_dbContext);
    }

    public ICrudRepository<Address> AddressRepository => _addressRepository;

    public ICrudRepository<BoxCash> BoxCashRepository => _boxCashRepository;

    public ICrudRepository<BranchStore> BranchStoreRepository => _branchStoreRepository;

    public ICrudRepository<BranchStoreEmployee> BranchStoreEmployeeRepository => _branchStoreEmployeeRepository;

    public ICrudRepository<Cart> CartRepository => _cartRepository;

    // public ICrudRepository<CartDrink> CartDrinkRepository => _cartDrinkRepository;

    // public ICrudRepository<CartFood> CartFoodRepository => _cartFoodRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;

    public ICrudRepository<Customer> CustomerRepository => _customerRepository;

    public ICrudRepository<CustomerAddress> CustomerAddressRepository => _customerAddressRepository;

    public ICrudRepository<CustomerType> CustomerTypeRepository => _customerTypeRepository;

    public ICrudRepository<Drink> DrinkRepository => _drinkRepository;

    public ICrudRepository<Employee> EmployeeRepository => _employeeRepository;

    public ICrudRepository<Food> FoodRepository => _foodRepository;

    public ICrudRepository<GeographicalZone> GeographicalZoneRepository => _geographicalZoneRepository;

    public ICrudRepository<Job> JobRepository => _jobRepository;

    public ICrudRepository<ManagerZoneBranchStore> ManagerZoneBranchStoreRepository => _managerZoneBranchStoreRepository;

    public ICrudRepository<Menu> MenuRepository => _menuRepository;

    public ICrudRepository<Order> OrderRepository => _orderRepository;

    public ICrudRepository<OrderDrink> OrderDrinkRepository => _orderDrinkRepository;

    public ICrudRepository<OrderFood> OrderFoodRepository => _orderFoodRepository;

    public ICrudRepository<PayBox> PayBoxRepository => _payBoxRepository;

    public ICrudRepository<Payment> PaymentRepository => _paymentRepository;

    public ICrudRepository<Reservation> ReservationRepository => _reservationRepository;

    public ICrudRepository<Rol> RolRepository => _rolRepository;

    public ICrudRepository<Ticket> TicketRepository => _ticketRepository;

    public IUserAccountRepository UserAccountRepository => _userAccountRepository;

    public IRetrieveRepository<ActiveUserAccountEmployee> ActiveUserAccountEmployeeRepository => _activeUserAccountEmployeeRepository;

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