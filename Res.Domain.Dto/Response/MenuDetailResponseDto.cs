using Res.Domain.Dto.Interfaces;

namespace Res.Domain.Dto.Response;

public class MenuDetailResponseDto : BaseCatalogResponseDto
{
    public int BranchStoreId { get; set; }

    public string? BranchStoreName { get; set; }

    public IEnumerable<DrinkResponseDto> Drink { get; } = new List<DrinkResponseDto>();

    public IEnumerable<FoodResponseDto> Food { get; } = new List<FoodResponseDto>();
}