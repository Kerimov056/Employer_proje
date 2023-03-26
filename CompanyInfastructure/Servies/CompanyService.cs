using CompanyInfastructuure.DbContext;
using CompanyInfastructuure.Entityes.Exceptioon;
using CompanyInfastructuure.Servies;
using Employeer.Core;
using Employeer.Core.Entity;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace CompanyInfastructuure.Servies;

public class CompanyService
{
    private EmployerService employerService;
    private  DepartmendService departmendService;
    public CompanyService()
    {
        departmendService= new DepartmendService();
        employerService= new EmployerService();
    }
    private static int _count = 0;
    public void Create(string name)
    {
        name.Trim();
        bool check = false;
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException();
        }
        for (int i = 0; i < _count; i++)
        {
            if (AppDbContext.companies[i].Name.ToUpper() == name.ToUpper())
            {
                check = true; break;
            }
        }
        if (check)
        {
            throw new isAvailableExceptioon("The company already exists  this name");
        }
        Company company = new Company(name);
        AppDbContext.companies[_count++] = company;
    }

    public void GetAll()
    {
        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(AppDbContext.companies[i].Id + "-" + AppDbContext.companies[i].Name);
        }
    }

    public void CompanyUpdate(int _update,string name)
    {
        foreach (var company in AppDbContext.companies)
        {
            if (company is null)
            {
                throw new AddCompanyFailedExceptions("Not Found");
            }
            if (company.Id == _update) break;
        }
        for (int i = 0; i < _count; i++)
        {
            if (AppDbContext.companies[i].Id== _update)
            {
                AppDbContext.companies[i].Name = name;
                break;
            }
        }
    }
    public void GetAllDepartment(int id)
    {
        foreach (var company in AppDbContext.companies)
        {
            if (company is null)
            {
                throw new AddCompanyFailedExceptions("Not Found");
            }
            if (company.Id == id) break;
        }
        for (int i = 0; i < AppDbContext.companies.Length; i++)
        {
            if (AppDbContext.companies[i].Id == id)
            {
                foreach (var department in AppDbContext.departments)
                {
                    if (department is null) break;
                    if (AppDbContext.companies[i].Id == department.CompanyId)
                    {
                        Console.WriteLine("************");
                        Console.WriteLine("Department Id:"+department.Id);
                        Console.WriteLine("Department Name:"+department.Name);
                        Console.WriteLine("Department Emlpoyer Limit:"+department.EmployerLimit);
                        Console.WriteLine("************");
                        break;
                    }
                }
                break;
            }
        }
    }


    public void GetAllDepartmentName(string company_name)
    {
        foreach (var company in AppDbContext.companies)
        {
            if (company is null)
            {
                throw new AddCompanyFailedExceptions("Not Found");  //butun departmenti cixatdi tekce oz companisdeki olani yox.
            }
            if (company.Name == company_name) break;
        }
        if (string.IsNullOrWhiteSpace(company_name))
        {
            throw new ArgumentNullException();
        }
        for (int i = 0; i < AppDbContext.companies.Length; i++)
        {
            if (AppDbContext.companies[i].Name==company_name)
            {

                foreach (var department in AppDbContext.departments)
                {
                    if (department is null) break;
                    if (department.CompanyId == AppDbContext.companies[i].Id)
                    {
                        Console.WriteLine("************");
                        Console.WriteLine("Department Id:" + department.Id);
                        Console.WriteLine("Department Name:" + department.Name);
                        Console.WriteLine("Department Emlpoyer Limit:" + department.EmployerLimit);
                        Console.WriteLine("************");
                        break;
                    }
                }
                break;
                
            }
            
        }

    }

    public void DeleteCompany(int id)
    {
        foreach (var company in AppDbContext.companies)
        {
            if (company is null)
            {
                throw new AddCompanyFailedExceptions("Not Found Company");
            }
            if (company.Id==id) break;
        }                                                                                  
        for (int i = 0; i < AppDbContext.companies.Length; i++)        
        {
            if (AppDbContext.companies[i].Id==id)
            {
                AppDbContext.companies[i] = null;
                AppDbContext.companies[i] = AppDbContext.companies[i + 1];
                _count--;
                break;
            }
        }
    }


    public void CompanyInfo(int id)
    {
        foreach (var company in AppDbContext.companies)
        {
            if (company is null)
            {
                throw new AddCompanyFailedExceptions("Not Found");
            }
            if (company.Id == id) break;
        }
        for (int i = 0; i < AppDbContext.companies.Length; i++)
        {
            if (AppDbContext.companies[i].Id==id)
            {
                Console.WriteLine("--------Company Info--------");
                Console.WriteLine($"Company Id:{AppDbContext.companies[i].Id}" +
                    $"\nComoany Name:{AppDbContext.companies[i].Name}");
                Console.WriteLine("--------Department Info--------");
                departmendService.DepartmentInfo(AppDbContext.companies[i].Id);
                Console.WriteLine("--------Employer Info--------");
                employerService.EmployerInfo(AppDbContext.companies[i].Id);
                break;
            }
        }
    }
}








