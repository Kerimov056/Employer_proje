using CompanyInfastructuure.DbContext;
using CompanyInfastructuure.Entityes.Exceptioon;
using Employeer.Core.Entity;
using System.Security.Cryptography.X509Certificates;

namespace CompanyInfastructuure.Servies;

public class DepartmendService
{
   private EmployerService employerService;
    public DepartmendService()
    {
        employerService= new EmployerService();
    }
    public static int _count = 0;
    public void Created(string name, int employer_limit, int Company_Id)
    {
        foreach (var company in AppDbContext.companies)
        {
            if (company is null)
            {
                throw new AddCompanyFailedExceptions("Not Found Company");
            }
            if (company.Id== Company_Id) break;
        }
        name.Trim();
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new NotNullExceptions("You did not enter a valid name:");
        }
        bool isCheck = false;
        for (int i = 0; i < _count; i++)
        {
            if (AppDbContext.departments[i].Name.ToUpper() == name.ToUpper() && Company_Id == AppDbContext.departments[i].CompanyId)
            {
                isCheck = true; break;
            }
        }
        if (isCheck)
        {
            throw new isAvailableExceptioon("The Department  already exists  this name");
        }
        Department department = new Department(name, employer_limit, Company_Id);
        AppDbContext.departments[_count++] = department;
    }
    public void GetAll()
    {
        for (int i = 0; i < _count; i++)
        {
            string temp_company = String.Empty;
            foreach (var company in AppDbContext.companies)
            {
                if (company is null) break;
                if (AppDbContext.departments[i].CompanyId == company.Id)
                {
                    temp_company = company.Name;
                    break;
                }
            }
            Console.WriteLine($"id: {AppDbContext.departments[i].Id}; " +
               $"Department name: {AppDbContext.departments[i].Name}; " +
               $"Department Limit: {AppDbContext.departments[i].EmployerLimit}; " +
               $"Company to: {temp_company}");
        }
    }

    public void GetById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (AppDbContext.departments[i].Id==id)
            {
                String company_ = String.Empty;
                foreach (var compaany in AppDbContext.companies)
                {
                    if (compaany is null) break;
                    if (AppDbContext.departments[i].CompanyId==compaany.Id)
                    {
                        company_ = compaany.Name;
                        break;
                    }
                }
                Console.WriteLine($"Department Id: {AppDbContext.departments[i].Id}; " +
                $"Department name: {AppDbContext.departments[i].Name}; " +
                $"Department Limit: {AppDbContext.departments[i].EmployerLimit}; " +
                $"Company to: {company_}");
                return;
            }
        }
    }

    public void GetDepartmentEmployees(int id)
    {
        foreach (var department in AppDbContext.departments)
        {
            if (department is null)
            {
                throw new AddDepartmentFailedException("Not Found");
            }
            if (department.Id == id) break;
        }
        for (int i = 0; i < AppDbContext.departments.Length; i++)
        {
            if (AppDbContext.departments[i].Id == id)
            {
                foreach (var employer in AppDbContext.employers)
                {
                    if (employer is null) break;
                    if (AppDbContext.departments[i].Id == employer.DeparmentId)
                    {
                        Console.WriteLine("************");
                        Console.WriteLine("Employer Id:"+employer.Id);
                        Console.WriteLine("Employer Name:"+employer.Name);
                        Console.WriteLine("Employer Surname:"+employer.Surname);
                        Console.WriteLine("************");
                    }
                }
                if (true)
                {
                    throw new NotFoundEmployer("Not Found Employer");
                }
            }
        }
        Console.WriteLine("-------------------------------");
    }


    public void UpdateDepartment(int _update, string name, int employer_limit)
    {
        foreach (var department in AppDbContext.departments)
        {
            if (department is null)
            {
                Console.WriteLine("Deyislmedi:");
                throw new AddDepartmentFailedException("Not Found");
            }
            if (department.Id == _update) break;
        }
        for (int i = 0; i < AppDbContext.departments.Length; i++)
        {
            if (AppDbContext.departments[i].Id==_update)
            {
                //bura kimi deyisdirilecek update tapdi:
                AppDbContext.departments[i].Name = name;
                AppDbContext.departments[i].EmployerLimit = employer_limit;
                break;
            }
        }
    }


    public void DepartmentInfo(int id)                 //Company info ucun bir metod
    {
        foreach (var department in AppDbContext.departments)
        {
            if (department is null)
            {
                Console.WriteLine("Not Found Department:");
                break;
            }
            if (department.Id == id) break;
        }
        for (int i = 0; i < AppDbContext.departments.Length; i++)
        {
            if (AppDbContext.departments[i] is null) break;
            if (AppDbContext.departments[i].CompanyId==id)
            {
                Console.WriteLine($"Department Id:{AppDbContext.departments[i].Id}" +
                    $"\nDepartment Name:{AppDbContext.departments[i].Name}" +
                    $"\nDepartment Employer Limit:{AppDbContext.departments[i].EmployerLimit}");
               // employerService.EmployerInfo(AppDbContext.departments[i].Id);
                Console.WriteLine("*******************************");
            }
        }
    }

}

