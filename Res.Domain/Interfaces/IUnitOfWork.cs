using Res.Common.Interfaces.Repositories;
using Res.Domain.Entities;
using Res.Domain.Interfaces.Repositories;

namespace Res.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICrudRepository<Address> AddressRepository { get; }

    IBoxCashRepository BoxCashRepository { get; }

    IBranchStoreRepository BranchStoreRepository { get; }

    ICrudRepository<BranchStoreEmployee> BranchStoreEmployeeRepository { get; }

    ICartRepository CartRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    ICustomerRepository CustomerRepository { get; }

    ICatalogBaseRepository<CustomerType> CustomerTypeRepository { get; }

    IDrinkRepository DrinkRepository { get; }

    IEmployeeRepository EmployeeRepository { get; }

    IFoodRepository FoodRepository { get; }

    ICatalogBaseRepository<GeographicalZone> GeographicalZoneRepository { get; }

    ICatalogBaseRepository<Job> JobRepository { get; }

    ICrudRepository<ManagerZoneBranchStore> ManagerZoneBranchStoreRepository { get; }

    IMenuRepository MenuRepository { get; }

    IOrderRepository OrderRepository { get; }

    ICrudRepository<OrderDrink> OrderDrinkRepository { get; }

    ICrudRepository<OrderFood> OrderFoodRepository { get; }

    ICrudRepository<PayBox> PayBoxRepository { get; }

    IPaymentRepository PaymentRepository { get; }

    IReservationRepository ReservationRepository { get; }

    ICatalogBaseRepository<Rol> RolRepository { get; }

    ITicketRepository TicketRepository { get; }

    IUserAccountRepository UserAccountRepository { get; }

    IRetrieveRepository<ActiveUserAccountEmployee> ActiveUserAccountEmployeeRepository { get; }

    IRetrieveRepository<ActiveUserAccountCustomer> ActiveUserAccountCustomerRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}