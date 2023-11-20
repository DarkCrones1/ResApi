namespace Res.Domain.Entities;

public partial class Customer
{
    public string FullName { get => $"{FirstName} {MiddleName} {LastName}".Trim(); }
}