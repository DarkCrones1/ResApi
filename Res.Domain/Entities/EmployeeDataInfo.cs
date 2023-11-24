namespace Res.Domain.Entities;

public partial class Employee
{
    public string FullName
    {
        get
        {
            var fullName = $"{FirstName} {MiddleName} {LastName}";
            return fullName.Trim();
        }
    }
}