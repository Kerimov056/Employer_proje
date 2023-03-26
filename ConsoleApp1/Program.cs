using CompanyInfastructuure.DbContext;
using CompanyInfastructuure.Entityes.Exceptioon;
using CompanyInfastructuure.Servies;
using CompanyInfastructuure.Utilitis.Helper;
using Employeer.Core;
using Employeer.Core.Entity;
using Microsoft.VisualBasic;
using System.Drawing;            
                                                                         
CompanyService companyService = new CompanyService();
DepartmendService departmendServis = new DepartmendService();
EmployerService employerService = new EmployerService();


Console.WriteLine("Welcome");
while (true)                                                               
{                            //enumnan yazmiwam swice case'i reqemleri sira oxunsun deye yazmisam.
    Start:

    Console.WriteLine("_______________________");
    Console.WriteLine("0 -> Exit " +                                        //Exit ->Cixis etmek:
        "\n1 -> Created Company " +                                        //Company yaradir:
        "\n2 -> List Company" +                                           //Yaradilan Company Listini cixardir:
        "\n3 -> Created Department " +                                   //Department yaradir:
        "\n4 -> List Department" +                                      //Yaradilan Department Listini cixardir:
        "\n5 -> Created Employer" +                                    //Employer yaradir:
        "\n6 -> List Employer" +                                      //Yaradilan Department Listini cixardir:
        "\n7 -> Names of department employees" +                     //Departmentde olan Employerlerin siyahisini cixardir:
        "\n8 -> Common departments in the companys" +               //Company'de olan Department'lerin siyahisini cixardir:
        "\n9 -> Update Department" +                               //Departmentlerden birini secir ve name,employerlimit'in Update edir:
        "\n10 -> Update Company" +                                //Company'den birini secir ve name'ni Update edir:
                                                                 //--------------------------------------------------------------------
        "\n11 -> Common departments in the companys" +          //--8'ci case'le eynidir lakin 8'de companyleri ekran'a cixirdir secib,
                                                               //departmenleri gorursen. Lakin burda company'nin adini yazmalisanki olan,
                                                              //Departmentleri cixarsin. YENI 2cur yazmiwam hem nam'le hem id'le.
                                                             //-----------------------------------------------------------------------
        "\n12 -> Delete Company" +                          //DataBase'deki movcud bir Company'ni silmek.  //yarimciq qalib
        "\n13 -> Employee Information" +                   //bir employeri'n butun melumatlari:   
        "\n14 -> Employerin Department'in deyis" +        //Employerin departmentini deyismek.          //yarimciq qalib 
        "\n15 -> Company Info");                         //Bir Company'deki butun departmentleri hemen departmendeki Employerler'i ekrana cixaardir.


    Console.WriteLine("-----------------------");
    string? answer = Console.ReadLine();
    int menu;
    bool TryToInt = int.TryParse(answer, out menu);
    if (TryToInt)
    {
        switch (menu)
        {
            case (int)Menu.Exit:
                Environment.Exit(0);
                break;
            case (int)Menu.CreatedCompany:
                Console.WriteLine("Enter Company Name:");
                string? Name = Console.ReadLine();
                try
                {
                    companyService.Create(Name);
                    Console.WriteLine("Succesfully created");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.CreatedCompany;
                }
                break;
            case (int)Menu.ListCompany:
                for (int i = 0; i <= AppDbContext.companies.Length; i++)
                {
                    if (AppDbContext.companies[i] is null)
                    {
                        Console.WriteLine("A Company Does not Exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Company'i mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                Console.WriteLine("Company List:");
                companyService.GetAll();
                break;
            case (int)Menu.CreatedDepartment:
                for (int i = 0; i <= AppDbContext.companies.Length; i++)
                {
                    if (AppDbContext.companies[i] is null)
                    {
                        Console.WriteLine("A Company does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Company'i mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                Select_DepName:
                Console.WriteLine("Enter Department name:");
                string? name = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("You did not enter a valid name:");
                    goto Select_DepName;
                }
            EmployerLimit:
                Console.WriteLine("Enter Max Number");
                string? employer_limit = Console.ReadLine();
                int EmployerLimit;
                bool TryToMax = int.TryParse(employer_limit, out EmployerLimit);
                if (EmployerLimit<0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto EmployerLimit;
                }
                if (!TryToMax)
                {
                    Console.WriteLine("Enter correct Format");
                    goto EmployerLimit;
                }
            Select_CompanyID:
                Console.WriteLine("Enter Company (Id):");
                companyService.GetAll();
                string? company_id = Console.ReadLine();
                int Company_Id;
                bool TryToCompanyId = int.TryParse(company_id, out Company_Id);
                if (Company_Id < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_CompanyID;
                }
                if (!TryToCompanyId)
                {
                    Console.WriteLine("Enter Company ID:");
                    goto Select_CompanyID;
                }
                try
                {
                    departmendServis.Created(name, EmployerLimit, Company_Id);
                    Console.WriteLine("Succesfully created");
                }
                catch (AddCompanyFailedExceptions ex)
                {
                    Console.WriteLine(ex.Message);
                    goto Select_CompanyID;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.CreatedDepartment;
                }
                break;
            case (int)Menu.ListDepartment:
                for (int i = 0; i <= AppDbContext.departments.Length; i++)
                {
                    if (AppDbContext.departments[i] is null)
                    {
                        Console.WriteLine("A Deparments does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Department mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                Console.WriteLine("List Department:");
                departmendServis.GetAll();
                break;
            case (int)Menu.CreatedEmployer:
                for (int i = 0; i <= AppDbContext.departments.Length; i++)
                {
                    if (AppDbContext.departments[i] is null)
                    {
                        Console.WriteLine("A Deparments does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Department mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
            Select_empName:
                Console.WriteLine("Enter Employer Name:");
                string? EmployerName = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(EmployerName))
                {
                    Console.WriteLine("You did not enter a valid name:");
                    goto Select_empName;
                }
            Select_empSurname:
                Console.WriteLine("Enter Employer Surname:");
                string? EmployerSurname = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(EmployerSurname))
                {
                    Console.WriteLine("You did not enter a valid Surname:");
                    goto Select_empSurname;
                }
            Select_Salary:
                Console.WriteLine("Enter Salary:");
                string? Value = Console.ReadLine();
                int Salary;
                bool TryToSalary = int.TryParse(Value, out Salary);
                if (!TryToSalary)
                {
                    Console.WriteLine("Enter restart Salary");
                    goto Select_Salary;
                }
                if (Salary < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_Salary;
                }
            Select_DepartmenId:
                Console.WriteLine("Enter Depatrment (Id):");
                departmendServis.GetAll();
                string? result = Console.ReadLine();
                int DepartmentId;
                bool TryToDepartmentId = int.TryParse(result, out DepartmentId);
                if (!TryToDepartmentId)
                {
                    Console.WriteLine("Enter restart Department Id:");
                    goto Select_DepartmenId;
                }
                if (DepartmentId < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_DepartmenId;
                }
                try
                {
                    employerService.Create(EmployerName, EmployerSurname, Salary, DepartmentId);
                    Console.WriteLine("Succesfully created");
                }
                catch (AddCompanyFailedExceptions ex)
                {
                    Console.WriteLine(ex.Message);
                    goto Select_DepartmenId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto Select_DepartmenId;
                }
                break;
            case (int)Menu.ListEmployer:
                for (int i = 0; i <= AppDbContext.employers.Length; i++)
                {
                    if (AppDbContext.employers[i] is null)
                    {
                        Console.WriteLine("A Employers does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Employer mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                Console.WriteLine("Employer List");
                employerService.GetAll();
                break;
            case (int)Menu.GetDepartmentEmployees:
                for (int i = 0; i <= AppDbContext.employers.Length; i++)
                {
                    if (AppDbContext.employers[i] is null)
                    {
                        Console.WriteLine("There is no Employer in the System");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Employer mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                departmendServis.GetAll();                      
            Select_Department:
                Console.WriteLine("Enter Department (Id):");
                string? departmentId = Console.ReadLine();
                int Department_id;
                bool TryToDepartmentid = int.TryParse(departmentId, out Department_id);
                if (!TryToDepartmentid)
                {
                    Console.WriteLine("Enter restart Department Id:");
                    goto case (int)Menu.GetDepartmentEmployees;
                }
                if (Department_id < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_Department;
                }
                foreach (var employer1 in AppDbContext.employers)
                {
                    if (employer1 is null) break;
                    if (employer1.DeparmentId != Department_id)
                    {
                        Console.WriteLine("There is No Employer");
                        break;
                    }
                }
                try
                {
                    departmendServis.GetDepartmentEmployees(Department_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto Select_Department;
                }
                break;
            case (int)Menu.GetCompanyDepartment:
                for (int i = 0; i <= AppDbContext.departments.Length; i++)
                {
                    if (AppDbContext.departments[i] is null)
                    {
                        Console.WriteLine("A departments does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Department mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                companyService.GetAll();
            Select_company:
                Console.WriteLine("Enter company (Id):");
                string? companyId = Console.ReadLine();
                int Company_id;
                bool TryToCompanyid = int.TryParse(companyId, out Company_id);
                if (!TryToCompanyid)
                {
                    Console.WriteLine("Enter restart Company Id:");
                    goto Select_company;
                }
                if (Company_id < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_company;
                }
                foreach (var departments in AppDbContext.departments)
                {
                    if (departments is null) break;
                    if (departments.CompanyId != Company_id)
                    {
                        Console.WriteLine("There is No Employer");
                        break;
                    }
                }
                try
                {
                    companyService.GetAllDepartment(Company_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto Select_company;
                }
                break;
            case (int)Menu.UpdateDepartment:
                for (int i = 0; i <= AppDbContext.departments.Length; i++)
                {
                    if (AppDbContext.departments[i] is null)
                    {
                        Console.WriteLine("A Departments does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Department mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                departmendServis.GetAll();
            Select_Update:
                Console.WriteLine("Select Which Update Will Wappen");
                string? update = Console.ReadLine();
                int update_option;
                bool TryToUpdate = int.TryParse(update, out update_option);
                if (!TryToUpdate)
                {
                    Console.WriteLine("Enter Restart Which Update Will Wappen");
                    goto Select_Update;
                }
                if (update_option < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_Update;
                }
                foreach (var department in AppDbContext.departments)
                {
                    if (department is null) break;
                    if (department.Id != update_option)
                    {
                        Console.WriteLine("Not Found Department");
                        goto Select_Update;
                    }
                }
                Console.WriteLine("New Department name:");
                string? newName = Console.ReadLine();
            Enter_Limit:
                Console.WriteLine("New Employer Limit:");
                string? employeer_limit = Console.ReadLine();
                int EmployerLimiit;
                bool TryToLimit = int.TryParse(employeer_limit, out EmployerLimiit);
                Console.WriteLine("*******************");
                if (EmployerLimiit < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Enter_Limit;
                }
                if (!TryToLimit)
                {
                    Console.WriteLine("Enter Dpartment");
                    goto Enter_Limit;
                }
                try
                {
                    departmendServis.UpdateDepartment(update_option, newName, EmployerLimiit);
                    Console.WriteLine("Succesfully Update");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.UpdateDepartment;
                }
                break;
            case (int)Menu.UpdateCompany:
                for (int i = 0; i <= AppDbContext.companies.Length; i++)
                {
                    if (AppDbContext.companies[i] is null)
                    {
                        Console.WriteLine("A Company does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Company mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                companyService.GetAll();
            Select_CompanyUpdate:
                Console.WriteLine("Select Which Update Will Wappen");
                string? companyName = Console.ReadLine();
                int companyName_option;
                bool TryToCompanyUpdate=int.TryParse(companyName, out companyName_option);
                Console.WriteLine("*******************");
                if (companyName_option < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_CompanyUpdate;
                }
                if (!TryToCompanyUpdate)
                {
                    Console.WriteLine("Enter Restart Which Update Will Wappen");
                    goto Select_CompanyUpdate;
                }
                foreach (var company in AppDbContext.companies)
                {
                    if (company is null) break;
                    if (company.Id != companyName_option)
                    {
                        Console.WriteLine("Not Found Company");
                        goto Select_CompanyUpdate;
                    }
                }
                Console.WriteLine("New Company name:");
                string? newCompanyName = Console.ReadLine();
                try
                {
                    companyService.CompanyUpdate(companyName_option, newCompanyName);
                    Console.WriteLine("Succesfully Update");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.UpdateCompany;
                }
                break;
            case (int)Menu.GetCompanyDepartmentName:
                Console.WriteLine("Enter Company Name:");
                string? companyname = Console.ReadLine();
                for (int i = 0; i <= AppDbContext.departments.Length; i++)
                {
                    if (AppDbContext.departments[i] is null)
                    {
                        Console.WriteLine("A departments does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Department mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                try
                {
                    companyService.GetAllDepartmentName(companyname);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.GetCompanyDepartmentName;
                }
                break;
            case (int)Menu.DeleteCompany:
                for (int i = 0; i < AppDbContext.companies.Length; i++)
                {
                    if (AppDbContext.companies[i] is null)
                    {
                        Console.WriteLine("A departments does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Department mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                Console.WriteLine("Select a Company");                
            Select_Delete:
                companyService.GetAll();
                string? delete= Console.ReadLine();                                  
                int delete_option;
                bool TryToDelete=int.TryParse(delete, out delete_option);
                if (delete_option < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_Delete;
                }
                if (!TryToDelete)                                                  
                {                                                                
                    Console.WriteLine("Make the right choice");
                    goto Select_Delete;
                }
                try
                {
                    companyService.DeleteCompany(delete_option);
                    Console.WriteLine("Successful");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.DeleteCompany;
                }
                break;
            case (int)Menu.EmployerInformation:
                for (int i = 0; i <= AppDbContext.employers.Length; i++)
                {
                    if (AppDbContext.employers[i] is null)
                    {
                        Console.WriteLine("A Employer does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Department mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                Console.WriteLine("Enter Employer Name:");
                string? employer_name= Console.ReadLine();
                try
                {
                    Console.WriteLine("___________________________");
                    Console.WriteLine("Employer Info:");
                    employerService.Employer_Info(employer_name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.EmployerInformation;
                }
                break;
            case (int)Menu.EmployerDepartmentChange:
                for (int i = 0; i <= AppDbContext.employers.Length; i++)
                {
                    if (AppDbContext.employers[i] is null)
                    {
                        Console.WriteLine("A Employer does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Employer mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
                employerService.GetAll();
             Select_emp:
                Console.WriteLine("Make a Selection (Employer Id):");
                string? employer= Console.ReadLine();
                int employer_option;
                bool TryToEmployer=int.TryParse(employer, out employer_option);
                if (employer_option<0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_emp;
                }
                if (!TryToEmployer)
                {
                    Console.WriteLine("Choose Correctly");
                    goto Select_emp;
                }
                foreach (var employer1 in AppDbContext.employers)
                {
                    if (employer1 is null) break;
                    if (employer1.Id != employer_option)
                    {
                        Console.WriteLine("Not Found Other Employer");
                        goto Select_emp;
                    }
                }
                departmendServis.GetAll();
            Select_DeparmentId:
                Console.WriteLine("***********************************************************");
                Console.WriteLine(" Enter Department To Change Name (Enter Department Name):");
                string? DepartmenttId= Console.ReadLine();
                int departmentId_option;
                bool TryTodepartmentId = int.TryParse(DepartmenttId,out departmentId_option);
                if (departmentId_option < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Select_DeparmentId;
                }
                if (!TryTodepartmentId)
                {
                    Console.WriteLine("Choose Correctly");
                    goto Select_DeparmentId;
                }
                foreach (var employer2 in AppDbContext.employers)
                {
                    if (employer2 is null) break;
                    if (employer2.DeparmentId != departmentId_option)
                    {
                        Console.WriteLine("Not Found Other Department");
                        goto Select_DeparmentId;
                    }
                }
                try
                {
                    employerService.EmployerTransfer(employer_option, departmentId_option);
                    Console.WriteLine("Transferred");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Error");
                    goto case (int)Menu.EmployerDepartmentChange;
                }
                break;
            case (int)Menu.CompanyInfo:
                for (int i = 0; i <= AppDbContext.companies.Length; i++)
                {
                    if (AppDbContext.companies[i] is null)
                    {
                        Console.WriteLine("A Employer does not exist");
                        Console.WriteLine("-------Translate--------");
                        Console.WriteLine("Employer mövcud deyil:");
                        goto Start;
                    }
                    break;
                }
            Company_info:
                companyService.GetAll();
                Console.WriteLine("Enter Company Id:");
                string? Company_info= Console.ReadLine();
                int Company_info_option;
                bool TryToCompanyInfo=int.TryParse(Company_info,out Company_info_option);
                if (Company_info_option < 0)
                {
                    Console.WriteLine("You can't enter negative numbers!!!");
                    goto Company_info;
                }
                if (!TryToCompanyInfo)
                {
                    Console.WriteLine("Enter Company Id:");
                    goto case (int)Menu.CompanyInfo;
                }
                try
                {
                    companyService.CompanyInfo(Company_info_option);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    goto case (int)Menu.CompanyInfo;
                }
                break;
            default:
                Console.WriteLine("Select coreet ones from menu:");
                break;
        }
    }
    else
    {
        Console.WriteLine("Enter Corret Menu item!");
    }
    Console.WriteLine("_______________________");
}



///hamsini yoxlaaa!!!!!!!!


                                                 

//1)Departmentdeki bir employeri bawqa bir departmente kocurmek-14.
//2)Company'yaz ve butun sobeler hansi sobelerde hasni iscilerin oldugu cixsin.
//3)Delete Company'i-12.___eger company'i silinirse daxilineki depatmentler e employerlerde silinsin.
//4)Delete employer,Delete department.
//5)Meselen:Departmentde Employer limitine gore Yuxarda asagi siralamaq.
//6)program.cs dekilerin bawqa bir classa atmaq.
//6)gorunus vermek.
