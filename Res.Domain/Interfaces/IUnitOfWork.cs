using Res.Common.Interfaces.Repositories;
using Res.Domain.Entities;
// using Res.Domain.Interfaces.Repositories;

namespace Res.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICrudRepository<Address> AddressRepository { get; }

    ICrudRepository<BoxCash> BoxCashRepository { get; }

    ICrudRepository<BranchStore> BranchStoreRepository { get; }

    ICrudRepository<BranchStoreEmployee> BranchStoreEmployeeRepository { get; }

    ICrudRepository<Category> CategoryRepository { get; }

    ICrudRepository<Customer> CustomerRepository { get; }

    ICrudRepository<CustomerAddress> CustomerAddressRepository { get; }

    ICrudRepository<Drink> DrinkRepository { get; }

    ICrudRepository<Employee> EmployeeRepository { get; }

    ICrudRepository<Food> FoodRepository { get; }

    ICrudRepository<GeographicalZone> GeographicalZoneRepository { get; }

    ICrudRepository<Job> JobRepository { get; }

    ICrudRepository<ManagerZoneBranchStore> ManagerZoneBranchStoreRepository { get; }

    ICrudRepository<Menu> MenuRepository { get; }

    ICrudRepository<PayBox> PayBoxRepository { get; }

    ICrudRepository<Payment> PaymentRepository { get; }

    ICrudRepository<Reservation> ReservationRepository { get; }

    ICrudRepository<Ticket> TicketRepository { get; }

    ICrudRepository<UserAccount> UserAccountRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}