namespace HrDataManager.Application.Dtos
{
    public class Company : CompanyHeader
    {
        public EmployeeHeader[] Employees { get; set; } // List of EmployeeHeader objects in the given company
    }
}
