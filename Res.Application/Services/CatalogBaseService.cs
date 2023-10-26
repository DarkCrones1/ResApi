using Res.Common.Entities;
using Res.Common.Interfaces.Entities;
using Res.Common.Interfaces.Repositories;
using Res.Common.Interfaces.Services;
using Res.Domain.Entities;
using Res.Domain.Interfaces;

namespace Res.Application.Services;

public class CatalogBaseService<T> : CrudService<T>, ICatalogBaseService<T> where T : CatalogBaseEntity
{
    protected new ICatalogBaseRepository<T> _repository;
    public CatalogBaseService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        this._repository = GetRepository();
    }

    protected override ICatalogBaseRepository<T> GetRepository()
    {
        var typeRep = typeof(T);

        if (typeRep == typeof(BoxCash))
            return (ICatalogBaseRepository<T>)this._unitOfWork.BoxCashRepository;

        if (typeRep == typeof(Category))
            return (ICatalogBaseRepository<T>)this._unitOfWork.CategoryRepository;

        if (typeRep == typeof(CustomerType))
            return (ICatalogBaseRepository<T>)this._unitOfWork.CustomerTypeRepository;

        if (typeRep == typeof(Drink))
            return (ICatalogBaseRepository<T>)this._unitOfWork.DrinkRepository;

        if (typeRep == typeof(Food))
            return (ICatalogBaseRepository<T>)this._unitOfWork.FoodRepository;

        if (typeRep == typeof(GeographicalZone))
            return (ICatalogBaseRepository<T>)this._unitOfWork.GeographicalZoneRepository;

        if (typeRep == typeof(Job))
            return (ICatalogBaseRepository<T>)this._unitOfWork.JobRepository;

        if (typeRep == typeof(Rol))
            return (ICatalogBaseRepository<T>)this._unitOfWork.RolRepository;

        return (ICatalogBaseRepository<T>)this._unitOfWork.RolRepository;
    }
}