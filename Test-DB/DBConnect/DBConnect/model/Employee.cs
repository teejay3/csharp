using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnect.model
{
    class Employee
    {
        public Decimal ID { get; set; }
        public Guid DepartmentID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string DateOfBirth { get; set; }
        public string DocSeries { get; set; }
        public string DocNumber { get; set; }
        public string Position { get; set; }
    }
}
