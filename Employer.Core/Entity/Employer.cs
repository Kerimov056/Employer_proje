using Employeer.Core.Interfeys;

namespace Employeer.Core;

public class Employer:iEntity
{
    public int Id { get ;set ; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public double Salary { get; set; }
    public int DeparmentId { get; set; }
    public static int Counter { get;private set; }

    public Employer()
    {
        Id=Counter++;
    }

    public Employer(string name,string surname,double salary,int DepartmentId):this() 
    {
        this.Name=name;
        this.Surname=surname;
        this.Salary=salary;
        this.DeparmentId=DepartmentId;
    }
}
