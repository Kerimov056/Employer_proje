using CompanyInfastructuure.DbContext;
using CompanyInfastructuure.Entityes.Exceptioon;
using Employeer.Core;
using System.Xml.Linq;

namespace CompanyInfastructuure.Servies;

public class EmployerService
{
    private DepartmendService departmendService;
    public EmployerService employerService;
    private static int _EmpLimitCount = 0;
    public EmployerService()
    {
        departmendService=new DepartmendService();
    }
    public static int _count = 0;
    public void Create(string _name, string _surname, double _salary, int _departmentId)
    {
        _name.Trim();
        _surname.Trim();
        if (String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_surname))
        {
            throw new NotNullExceptions("You did not enter a valid name and surname:");
        }
        foreach (var department in AppDbContext.departments)
        {
            if (department is null)
            {
                throw new AddDepartmentNotExistException("This Department is not exist");
            }
            if (_departmentId == department.Id)
            {
                if (_EmpLimitCount < department.EmployerLimit)                   
                {
                    _EmpLimitCount++;
                     break;
                }
                else
                {
                    throw new CapacityLimitException("The Limit is Already Full");
                }
            }
        }
        Employer employer = new Employer(_name,_surname,_salary,_departmentId);
        AppDbContext.employers[_count++]=employer;
    }


    public void GetAll()
    {
        for (int i = 0; i <= AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i] is null) break;
            Console.WriteLine("________________________________________");
            Console.WriteLine($"Id:{AppDbContext.employers[i].Id} " +
                $"\nName:{AppDbContext.employers[i].Name}" +
                $"\nSurname:{AppDbContext.employers[i].Surname} " +
                $"\nSalary:{AppDbContext.employers[i].Salary}");
            departmendService.GetById(AppDbContext.employers[i].DeparmentId);
            Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
        }
    }

    
    public void Employer_Info(string name)                       
    {
        foreach (var employer in AppDbContext.employers)
        {
            if (employer is null)
            {
                throw new AddEmployerFailedException("Not Found");
            }
            if (employer.Name == name) break;
        }
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException();
        }
        bool isSearch = false;
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i].Name==name)
            {
                isSearch=true;
                Console.WriteLine($"Id:{AppDbContext.employers[i].Id} " +
                $"\nName:{AppDbContext.employers[i].Name}" +
                $"\nSurname:{AppDbContext.employers[i].Surname} " +
                $"\nSalary:{AppDbContext.employers[i].Salary}");
                departmendService.GetById(i);
                break;
            }
        }
        if (!isSearch)
        {
            throw new NotFoundExceptioon("Not Found Employer");
        }

    }

    public void EmployerInfo(int id)                      // //Company info ucun bir metod
    {
        foreach (var employer in AppDbContext.employers)
        {
            if (employer is null)
            {
                Console.WriteLine("Not Found Employer:");
                break;
            }
            if (employer.Id == id) break;
        }
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i] is null) break;
            if (AppDbContext.employers[i].DeparmentId == id)
            {
                Console.WriteLine($"Employer Id:{AppDbContext.employers[i].Id}" +
                    $"\nEmployer Name:{AppDbContext.employers[i].Name}" +
                    $"\nEmployer Employer Limit:{AppDbContext.employers[i].Surname}" +
                    $"\nEmployer Salary:{AppDbContext.employers[i].Salary}");
                Console.WriteLine("*******************************");
            }
        }
    }


    public void EmployerTransfer(int id,int DepartmentId)    
    {                                                                                            
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i] is null) break;
            if (AppDbContext.employers[i].Id==id)                        
            {                                                             
                AppDbContext.employers[i].DeparmentId = DepartmentId;
                break;
            }
        }
    }

}

