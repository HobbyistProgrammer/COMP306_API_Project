using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP306_API_Demo.Models
{
    public class EmployeeSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }

        public string ToString()
        {
            return ("Id: " + Id + " - Name: " + Name +
                "\nEmail: $" + Email +
                "\nPhonenumber: " + Phonenumber + "\n");
        }
    }
}
