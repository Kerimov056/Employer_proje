using Employeer.Core.Interfeys;

namespace Employeer.Core.Entity;

public class Company : iEntity
{
    public int Id { get ; set ; }
    public string Name { get ; set ; }
    public static int Counter { get;private set ; }
    private Company()
    {
        Id=Counter++;
    }
    public Company(string name):this()
    {
        this.Name = name ;
    }
}
