using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;

using Res.Common.Entities;
using Res.Common.Exceptions;
using Res.Common.Interfaces.Services;
using Res.Common.Interfaces.Repositories;

using Res.Common.Interfaces.Entities;
using Res.Domain.Interfaces;
using Res.Domain.Entities;

namespace Res.Application.Services;

public class CrudService<T> : ICrudService<T> where T : BaseEntity
{
    protected ICrudRepository<T> _repository;
    protected readonly IUnitOfWork _unitOfWork;

    public CrudService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
        this._repository = GetRepository();
    }

    protected virtual ICrudRepository<T> GetRepository()
    {
        var typeRep = typeof(T);

        if (typeRep == typeof(Address))
            return (ICrudRepository<T>)this._unitOfWork.AddressRepository;

        if (typeRep == typeof(BoxCash))
            return (ICrudRepository<T>)this._unitOfWork.BoxCashRepository;

        if (typeRep == typeof(BranchStore))
            return (ICrudRepository<T>)this._unitOfWork.BranchStoreRepository;

        if (typeRep == typeof(BranchStoreEmployee))
            return (ICrudRepository<T>)this._unitOfWork.BranchStoreEmployeeRepository;

        if (typeRep == typeof(Cart))
            return (ICrudRepository<T>)this._unitOfWork.CartRepository;

        // if (typeRep == typeof(CartDrink))
        //     return (ICrudRepository<T>)this._unitOfWork.CartDrinkRepository;

        // if (typeRep == typeof(CartFood))
        //     return (ICrudRepository<T>)this._unitOfWork.CartFoodRepository;

        if (typeRep == typeof(Category))
            return (ICrudRepository<T>)this._unitOfWork.CategoryRepository;

        if (typeRep == typeof(Customer))
            return (ICrudRepository<T>)this._unitOfWork.CustomerRepository;

        if (typeRep == typeof(CustomerAddress))
            return (ICrudRepository<T>)this._unitOfWork.CustomerAddressRepository;

        if (typeRep == typeof(CustomerType))
            return (ICrudRepository<T>)this._unitOfWork.CustomerTypeRepository;

        if (typeRep == typeof(Drink))
            return (ICrudRepository<T>)this._unitOfWork.DrinkRepository;

        if (typeRep == typeof(Employee))
            return (ICrudRepository<T>)this._unitOfWork.EmployeeRepository;

        if (typeRep == typeof(Food))
            return (ICrudRepository<T>)this._unitOfWork.FoodRepository;

        if (typeRep == typeof(GeographicalZone))
            return (ICrudRepository<T>)this._unitOfWork.GeographicalZoneRepository;

        if (typeRep == typeof(Job))
            return (ICrudRepository<T>)this._unitOfWork.JobRepository;

        if (typeRep == typeof(ManagerZoneBranchStore))
            return (ICrudRepository<T>)this._unitOfWork.ManagerZoneBranchStoreRepository;

        if (typeRep == typeof(Menu))
            return (ICrudRepository<T>)this._unitOfWork.MenuRepository;

        if (typeRep == typeof(Order))
            return (ICrudRepository<T>)this._unitOfWork.OrderRepository;

        if (typeRep == typeof(OrderDrink))
            return (ICrudRepository<T>)this._unitOfWork.OrderDrinkRepository;

        if (typeRep == typeof(OrderFood))
            return (ICrudRepository<T>)this._unitOfWork.OrderFoodRepository;

        if (typeRep == typeof(PayBox))
            return (ICrudRepository<T>)this._unitOfWork.PayBoxRepository;

        if (typeRep == typeof(Payment))
            return (ICrudRepository<T>)this._unitOfWork.PaymentRepository;

        if (typeRep == typeof(Reservation))
            return (ICrudRepository<T>)this._unitOfWork.ReservationRepository;

        if (typeRep == typeof(Rol))
            return (ICrudRepository<T>)this._unitOfWork.RolRepository;

        if (typeRep == typeof(UserAccount))
            return (ICrudRepository<T>)this._unitOfWork.UserAccountRepository;

        if (typeRep == typeof(ActiveUserAccountEmployee))
            return (ICrudRepository<T>)this._unitOfWork.ActiveUserAccountEmployeeRepository;

        if (typeRep == typeof(ActiveUserAccountCustomer))
            return (ICrudRepository<T>)this._unitOfWork.ActiveUserAccountCustomerRepository;

        return (ICrudRepository<T>)this._unitOfWork.TicketRepository;
    }

    public virtual async Task<int> Create(T entity)
    {
        var result = await _repository.Create(entity);
        await _unitOfWork.SaveChangesAsync();
        return result;
    }

    public virtual async Task CreateRange(IEnumerable<T> entities)
    {
        await _repository.CreateRange(entities);
        await _unitOfWork.SaveChangesAsync();
    }

    public virtual async Task<int> Delete(int id)
    {
        return await _repository.Delete(id);
    }

    public virtual async Task<int> DeleteBy(Expression<Func<T, bool>> filter)
    {
        return await _repository.DeleteBy(filter);
    }

    public virtual async Task<int> DeleteRange(IEnumerable<int> idList)
    {
        return await _repository.DeleteRange(idList);
    }

    public virtual async Task<bool> Exist(Expression<Func<T, bool>> filters)
    {
        return await _repository.Exist(filters);
    }

    public virtual IQueryable<T> Get(Expression<Func<T, bool>>? filters = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        return _repository.Get(filters, orderBy, includeProperties);
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _repository.GetAll();
    }

    public virtual async Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filters, string includeProperties = "")
    {
        return await _repository.GetBy(filters, includeProperties);
    }

    public virtual async Task<T> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    public virtual async Task<PagedList<T>> GetPaged(T entity)
    {
        var pagedControl = (IPaginationQueryable)entity;
        var result = await _repository.GetPaged(entity);
        var pagedItems = PagedList<T>.Create(result, pagedControl.PageNumber, pagedControl.PageSize);
        return pagedItems;
    }

    public virtual async Task Update(T entity)
    {
        var currentEntity = await this.GetById(entity.Id) ?? throw new BusinessException("No se encontró el elemento que se desea modificar, verifique su información");
        var updateEntity = this.MapCurrentEntityToUpdate(entity, currentEntity);

        _repository.Update(updateEntity);

        await _unitOfWork.SaveChangesAsync();
    }

    public virtual T MapCurrentEntityToUpdate(T source, T target)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            if (!property.IsDefined(typeof(KeyAttribute)) && property.CanWrite) //&& !property.PropertyType.IsClass  && !typeof(ICollection).IsAssignableFrom(property.PropertyType)
            {
                if (property.Name.CompareTo("CreatedDate") != 0 && property.Name.CompareTo("CreatedBy") != 0)
                {
                    var value = property.GetValue(source);
                    property.SetValue(target, value);
                }
            }
        }
        return target;
    }
}