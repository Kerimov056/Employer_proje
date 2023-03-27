using CompanyInfastructuure.DbContext;
using CompanyInfastructuure.Utilitis.Exceptioon;

namespace CompanyInfastructuure.logical;

public class EmployerKartTransactions
{
    public void WithdrawMoney(int id,double money)
    {
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i].Id==id)
            {
                if (AppDbContext.employers[i].Salary>money)
                {
                    AppDbContext.employers[i].Salary -= money;
                    Console.WriteLine($"Balance::{AppDbContext.employers[i].Salary}");
                    break;
                }
                else
                {
                    throw new EnoughNotBallanceException("You don't have enough money in your balance.");
                }
            }
        }
    }

    public void Depositmoney(int id, double money)
    {
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i].Id == id)
            {
                AppDbContext.employers[i].Salary = AppDbContext.employers[i].Salary + money;
                Console.WriteLine("Balance:" + AppDbContext.employers[i].Salary);
                break;
            }
        }
    }


    public void Balance(int id)
    {
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i].Id == id)
            {
                Console.WriteLine(AppDbContext.employers[i].Salary);
                break;
            }
        }
    }

    public void MoneyTransfer(int id, int TransferId, double money)
    {
        for (int i = 0; i < AppDbContext.employers.Length; i++)
        {
            if (AppDbContext.employers[i].Id == id)
            {
                if (AppDbContext.employers[i].Salary >= money)
                {

                    AppDbContext.employers[i].Salary -= money;
                    for (int j = 0; j < AppDbContext.employers.Length; j++)
                    {
                        if (AppDbContext.employers[j].Id == TransferId)
                        {
                            AppDbContext.employers[j].Salary += money;
                            Console.WriteLine("Send:" + money);
                            Console.WriteLine("Balance:" + AppDbContext.employers[i].Salary);
                            break;
                        }
                    }
                    break;
                }
                else
                {
                    throw new EnoughNotBallanceException("You don't have enough balance");
                }
            }
        }
    }
}
