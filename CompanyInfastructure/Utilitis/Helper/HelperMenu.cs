﻿namespace CompanyInfastructuure.Utilitis.Helper;

public class HelperMenu
{
}

public enum Menu
{ 
    Exit=0,
    CreatedCompany,
    ListCompany,
    CreatedDepartment,
    ListDepartment,
    CreatedEmployer,
    ListEmployer,
    GetDepartmentEmployees,
    GetCompanyDepartment,
    UpdateDepartment,
    UpdateCompany,
    GetCompanyDepartmentName,
    DeleteCompany,
    EmployerInformation,
    EmployerDepartmentChange,
    CompanyInfo,
    EmployerLogIn
}

public enum LogInMenu
{
    Exit = 0,
    WithdrawMoney,
    Depositmoney,
    Balance,
    MoneyTransfer
}