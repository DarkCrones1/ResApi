using Res.Common.Interfaces.Repositories;
using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;
// using Res.Domain.Interfaces.Repositories;

namespace Res.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICrudRepository<Address> AddressRepository { get; }

    ICrudRepository<BoxCash> BoxCashRepository { get; }

    IBranchStoreRepository BranchStoreRepository { get; }

    ICrudRepository<BranchStoreEmployee> BranchStoreEmployeeRepository { get; }

    ICrudRepository<Cart> CartRepository { get; }

    // ICrudRepository<CartDrink> CartDrinkRepository { get; }

    // ICrudRepository<CartFood> CartFoodRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    ICrudRepository<Customer> CustomerRepository { get; }

    ICrudRepository<CustomerAddress> CustomerAddressRepository { get; }

    ICrudRepository<CustomerType> CustomerTypeRepository { get; }

    ICrudRepository<Drink> DrinkRepository { get; }

    IEmployeeRepository EmployeeRepository { get; }

    ICrudRepository<Food> FoodRepository { get; }

    ICrudRepository<GeographicalZone> GeographicalZoneRepository { get; }

    ICatalogBaseRepository<Job> JobRepository { get; }

    ICrudRepository<ManagerZoneBranchStore> ManagerZoneBranchStoreRepository { get; }

    ICrudRepository<Menu> MenuRepository { get; }

    ICrudRepository<Order> OrderRepository { get; }

    ICrudRepository<OrderDrink> OrderDrinkRepository { get; }

    ICrudRepository<OrderFood> OrderFoodRepository { get; }

    ICrudRepository<PayBox> PayBoxRepository { get; }

    ICrudRepository<Payment> PaymentRepository { get; }

    ICrudRepository<Reservation> ReservationRepository { get; }

    ICatalogBaseRepository<Rol> RolRepository { get; }

    ICrudRepository<Ticket> TicketRepository { get; }

    IUserAccountRepository UserAccountRepository { get; }

    IRetrieveRepository<ActiveUserAccountEmployee> ActiveUserAccountEmployeeRepository { get; }

    IRetrieveRepository<ActiveUserAccountCustomer> ActiveUserAccountCustomerRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}