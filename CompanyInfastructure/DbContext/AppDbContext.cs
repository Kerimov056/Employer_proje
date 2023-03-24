using Employeer.Core;
using Employeer.Core.Entity;

namespace CompanyInfastructuure.DbContext;

public class AppDbContext
{
    public static Company[] companies { get; set; } = new Company[10];
    public static Department[] departments { get; set; } = new Department[30];
    public static Employer[] employers { get; set; }= new Employer[50];
}