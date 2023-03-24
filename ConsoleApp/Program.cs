using CompanyInfastructuure.Servies;

CompanyService companyService
Console.WriteLine("Welcome");
while (true)
{
    Console.WriteLine("0 - Exit " +
        "\n1 - Created Company " +
        "\n2 - Created Department " +
        "\n3 - Created Employer");
    string answer= Console.ReadLine();
    int menu;
    bool TryToInt=int.TryParse(answer, out menu);
    if (TryToInt)
    {
        switch (menu)
        {
            case 0:
               Environment.Exit(0);
                break;
            case 1:
                Console.WriteLine("Enter Company Name:");
                string? Name= Console.ReadLine();

                break;
            case 2:
               Environment.Exit(0);
                break;
            case 3:
               Environment.Exit(0);
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
}