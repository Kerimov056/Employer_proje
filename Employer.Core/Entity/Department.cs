using Employeer.Core.Interfeys;

namespace Employeer.Core.Entity;

public class Department : iEntity
{
    public int Id { get ; set ; }
    public string Name { get ; set ; }
    public int EmployerLimit { get ; set ; }
    public int CompanyId { get ; set ; }
    public static int Couneter { get;private set ; }
    public Department()
    {
        Id= Couneter++;
    }
    public Department(string name,int employerlimit, int companyId) : this()
    {
        this.Name = name;
        this.EmployerLimit = employerlimit;
        CompanyId = companyId;
    }
}
