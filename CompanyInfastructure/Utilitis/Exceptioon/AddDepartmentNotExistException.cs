namespace CompanyInfastructuure.Entityes.Exceptioon;

public class AddDepartmentNotExistException:Exception
{
	public AddDepartmentNotExistException(string mesage):base(mesage)
	{

	}
}
