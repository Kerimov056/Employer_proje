using CompanyInfastructuure.DbContext;
using CompanyInfastructuure.Entityes.Exceptioon;
using CompanyInfastructuure.logical;
using CompanyInfastructuure.Utilitis.Exceptioon;
using CompanyInfastructuure.Utilitis.Helper;
using Employeer.Core;
using System.Xml.Linq;

namespace CompanyInfastructuure.Servies;

public class EmployerService
{

    private DepartmendService departmendService;
    public EmployerService employerService;
    public static int _count = 0;

    EmployerKartTransactions KartTransactions = new EmployerKartTransactions();
    public EmployerService()
    {
        departmendService= new DepartmendService();
    }

    private static int _EmpLimitCount = 0;
    public void Create(string _name, string _surname, double _salary, int _departmentId,string password)
    {
        foreach (var department1 in AppDbContext.departments)
        {
            if (department1 is null)
            {
                throw new AddDepartmentFailedException("Not Found Department");
            }
            if (department1.Id == _departmentId) break;
        }
        _name.Trim();
        _surname.Trim();
        if (String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_surname))
        {
            throw new NotNullExceptions("You did not enter a valid name and surname:");
        }
        foreach (var department in AppDbContext.departments)
        {
            if (_departmentId == department.Id)
            {
                foreach (var item in AppDbContext.employers)
                {
                    if (item is null) break;
                    if (item.DeparmentId == _departmentId)
                    {
                    if (_EmpLimitCount < department.EmployerLimit)
                    {
                        _EmpLimitCount++;
                        break;
                    }
                    else
                    {
                        throw new CapacityLimitException("\nThe Limit is Already Full");
                    }
                    }
                }
                break;
            }
        }
        Employer employer = new Employer(_name,_surname,_salary,_departmentId,password);
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
            if (AppDbContext.employers[i].Name == name)
            {
                isSearch = true;
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




    public void EmployerLogIn(string name,string password)
    {
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i] is null)
            {
                throw new NotFoundEmployerExciptioon("\nNot Found Employer!!!");
            }
            if (AppDbContext.employers[i].Name==name && AppDbContext.employers[i].Password==password)
            {
                Console.Clear();
                Console.WriteLine("Welcome:");
                while (true)
                {
                option:
                    Console.WriteLine("0 -> Exit" +
                        "\n1 -> WithdrawMoney" +
                        "\n2 -> Deposit Money" +
                        "\n3 -> Balance" +
                        "\n4 -> Money Transfer");
                    
                    string? check=Console.ReadLine();
                    int Check;
                    bool TryToCheck=int.TryParse(check, out Check);
                    if (TryToCheck)
                    {
                        switch (Check)
                        {
                            #region Exit
                            case (int)LogInMenu.Exit:
                                Environment.Exit(0);
                                break;
                            #endregion

                            #region WithdrawMoney
                            case (int)LogInMenu.WithdrawMoney:
                                Console.WriteLine("The Balance you want to withdraw:");
                                string? salary=Console.ReadLine();
                                int Salary;
                                bool TryToSalary=int.TryParse(salary, out Salary);
                                if (!TryToSalary)
                                {
                                    Console.WriteLine("Enter Correct Money");
                                    goto case (int)LogInMenu.WithdrawMoney;
                                }
                                if (Salary < 0)
                                {
                                    Console.WriteLine("You can't enter negative numbers");
                                    goto case (int)LogInMenu.WithdrawMoney;
                                }
                                try
                                {
                                    KartTransactions.WithdrawMoney(AppDbContext.employers[i].Id,Salary);
                                    Console.WriteLine("successful");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            #endregion

                            #region DepositMoney
                            case (int)LogInMenu.Depositmoney:
                                Console.WriteLine("How much money do you want to invest?");
                                string? inverst = Console.ReadLine();
                                double Inverst;
                                bool TryToInverst = double.TryParse(inverst, out Inverst);
                                if (!TryToInverst)
                                {
                                    Console.WriteLine("Enter Correct Money");
                                    goto case (int)LogInMenu.Depositmoney;
                                }
                                if (Inverst < 0)
                                {
                                    Console.WriteLine("You can't enter negative numbers");
                                    goto case (int)LogInMenu.Depositmoney;
                                }
                                try
                                {
                                    KartTransactions.Depositmoney(AppDbContext.employers[i].Id, Inverst);
                                    Console.WriteLine("Successful");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                                case (int)LogInMenu.Balance:
                                Console.WriteLine("Balance:");
                                KartTransactions.Balance(AppDbContext.employers[i].Id);
                                break;
                            #endregion

                            #region MoneyTransfer
                            case (int)LogInMenu.MoneyTransfer:
                                GetAll();
                                Console.WriteLine("******************");
                                Console.WriteLine($"Balance:");
                                KartTransactions.Balance(AppDbContext.employers[i].Id);
                                Console.WriteLine("Who do you want to send money to(id)?");
                                string? employers = Console.ReadLine();
                                int Employers;
                                bool TryToWhoEmp = int.TryParse(employers, out Employers);
                                if (!TryToWhoEmp)
                                {
                                    Console.WriteLine("Enter Correct Id");
                                    goto case (int)LogInMenu.Balance;
                                }
                                if (Employers < 0)
                                {
                                    Console.WriteLine("You can't enter negative numbers");
                                    goto case (int)LogInMenu.Balance;
                                }
                                foreach (var user in AppDbContext.employers)
                                {
                                    if (user is null)
                                    {
                                        Console.WriteLine("There is no other account");
                                        goto option;
                                    }
                                    break;
                                }
                            Enter_TransferMoney:
                                Console.WriteLine("How much money do you want to send?");
                                string? transferMoney = Console.ReadLine();
                                double TransferMoney;
                                bool TryToTransferMoney = double.TryParse(transferMoney, out TransferMoney);
                                if (!TryToTransferMoney)
                                {
                                    Console.WriteLine("Enter Correct Money");
                                    goto Enter_TransferMoney;
                                }
                                if (TransferMoney < 0)
                                {
                                    Console.WriteLine("You can't enter negative numbers");
                                    goto Enter_TransferMoney;
                                }
                                try
                                {
                                    KartTransactions.MoneyTransfer(AppDbContext.employers[i].Id, Employers, TransferMoney);
                                    Console.WriteLine("Transfer Completed");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                break;
                            #endregion
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("\"Select coreet ones from menu:");
                    }
                }
            }
        }
    }
}

