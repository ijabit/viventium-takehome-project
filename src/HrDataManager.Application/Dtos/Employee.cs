using System;

namespace HrDataManager.Application.Dtos
{
    public class Employee : EmployeeHeader
    {
        public string Email { get; set; } // EmployeeEmail
        public string Department { get; set; } // EmployeeDepartment
        public DateTime? HireDate { get; set; } // HireDate
        public EmployeeHeader[] Managers { get; set; } // List of EmployeeHeaders of the managers, ordered ascending by seniority(i.e.the immediate manager first)
    }
}
