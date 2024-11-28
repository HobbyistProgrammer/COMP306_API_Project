using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Models
{
    public class EmployeeDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeBirthdate { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
    }
}
