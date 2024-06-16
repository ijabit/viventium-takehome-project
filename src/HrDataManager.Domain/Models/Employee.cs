using System;

namespace HrDataManager.Domain.Models
{
    public class Employee
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime? HireDate { get; set; }
        public string ManagerEmployeeNumber { get; set; }
        public Company Company { get; set; }
        public int CompanyId { get; set; }
    }
}
