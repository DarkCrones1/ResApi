using Res.Domain.Dto.Interfaces;

namespace Res.Domain.Dto.Response;

public class BaseCatalogResponseDto : ICatalogBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = null;
    public string Status { get; set; } = string.Empty;
    public bool? isActive { get; set; } = null;
}