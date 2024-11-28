using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLibrary.Entities
{
    [DynamoDBTable("Products")]
    class Employee
    {
        [DynamoDBHashKey]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [DynamoDBProperty]
        public string Name { get; set; }
        [DynamoDBProperty]
        public long EmployeeId { get; set; }
        [DynamoDBProperty]
        public string EmployeeBirthdate { get; set; }
    }
}
