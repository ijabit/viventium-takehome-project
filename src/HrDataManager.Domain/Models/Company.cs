using System.Collections.Generic;

namespace HrDataManager.Domain.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
