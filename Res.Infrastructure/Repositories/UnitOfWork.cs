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

    protected ICartRepository _cartRepository;

    protected ICategoryRepository _categoryRepository;

    protected ICustomerRepository _customerRepository;

    protected ICatalogBaseRepository<CustomerType> _customerTypeRepository;

    protected IDrinkRepository _drinkRepository;

    protected IEmployeeRepository _employeeRepository;

    protected IFoodRepository _foodRepository;

    protected ICatalogBaseRepository<GeographicalZone> _geographicalZoneRepository;

    protected ICatalogBaseRepository<Job> _jobRepository;

    protected ICrudRepository<ManagerZoneBranchStore> _managerZoneBranchStoreRepository;

    protected IMenuRepository _menuRepository;

    protected IOrderRepository _orderRepository;

    protected ICrudRepository<OrderDrink> _orderDrinkRepository;

    protected ICrudRepository<OrderFood> _orderFoodRepository;

    protected ICrudRepository<PayBox> _payBoxRepository;

    protected IPaymentRepository _paymentRepository;

    protected IReservationRepository _reservationRepository;

    protected ICatalogBaseRepository<Rol> _rolRepository;

    protected ITicketRepository _ticketRepository;

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

        _cartRepository = new CartRepository(_dbContext);

        _categoryRepository = new CategoryRepository(_dbContext);

        _customerRepository = new CustomerRepository(_dbContext);

        _customerTypeRepository = new CatalogBaseRepository<CustomerType>(_dbContext);

        _drinkRepository = new DrinkRepository(_dbContext);

        _employeeRepository = new EmployeeRepository(_dbContext);

        _foodRepository = new FoodRepository(_dbContext);

        _geographicalZoneRepository = new CatalogBaseRepository<GeographicalZone>(_dbContext);

        _jobRepository = new CatalogBaseRepository<Job>(_dbContext);

        _managerZoneBranchStoreRepository = new CrudRepository<ManagerZoneBranchStore>(_dbContext);

        _menuRepository = new MenuRepository(_dbContext);

        _orderRepository = new OrderRepository(_dbContext);

        _orderDrinkRepository = new CrudRepository<OrderDrink>(_dbContext);

        _orderFoodRepository = new CrudRepository<OrderFood>(_dbContext);

        _payBoxRepository = new CrudRepository<PayBox>(_dbContext);

        _paymentRepository = new PaymentRepository(_dbContext);

        _reservationRepository = new ReservationRepository(_dbContext);

        _rolRepository = new CatalogBaseRepository<Rol>(_dbContext);

        _ticketRepository = new TicketRepository(_dbContext);

        _userAccountRepository = new UserAccountRepository(_dbContext);

        _activeUserAccountEmployeeRepository = new RetrieveRepository<ActiveUserAccountEmployee>(_dbContext);

        _activeUserAccountCustomerRepository = new RetrieveRepository<ActiveUserAccountCustomer>(_dbContext);
    }

    public ICrudRepository<Address> AddressRepository => _addressRepository;

    public IBoxCashRepository BoxCashRepository => _boxCashRepository;

    public IBranchStoreRepository BranchStoreRepository => _branchStoreRepository;

    public ICrudRepository<BranchStoreEmployee> BranchStoreEmployeeRepository => _branchStoreEmployeeRepository;

    public ICartRepository CartRepository => _cartRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;

    public ICustomerRepository CustomerRepository => _customerRepository;

    public ICatalogBaseRepository<CustomerType> CustomerTypeRepository => _customerTypeRepository;

    public IDrinkRepository DrinkRepository => _drinkRepository;

    public IEmployeeRepository EmployeeRepository => _employeeRepository;

    public IFoodRepository FoodRepository => _foodRepository;

    public ICatalogBaseRepository<GeographicalZone> GeographicalZoneRepository => _geographicalZoneRepository;

    public ICatalogBaseRepository<Job> JobRepository => _jobRepository;

    public ICrudRepository<ManagerZoneBranchStore> ManagerZoneBranchStoreRepository => _managerZoneBranchStoreRepository;

    public IMenuRepository MenuRepository => _menuRepository;

    public IOrderRepository OrderRepository => _orderRepository;

    public ICrudRepository<OrderDrink> OrderDrinkRepository => _orderDrinkRepository;

    public ICrudRepository<OrderFood> OrderFoodRepository => _orderFoodRepository;

    public ICrudRepository<PayBox> PayBoxRepository => _payBoxRepository;

    public IPaymentRepository PaymentRepository => _paymentRepository;

    public IReservationRepository ReservationRepository => _reservationRepository;

    public ICatalogBaseRepository<Rol> RolRepository => _rolRepository;

    public ITicketRepository TicketRepository => _ticketRepository;

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