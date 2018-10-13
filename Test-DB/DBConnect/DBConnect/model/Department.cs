using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnect.model
{
    class Department
    {
        public Department(  Guid ID,
                            string Code, 
                            string Name)
        {
            this.ID = ID;
            this.Code = Code;
            this.Name = Name;
            this.UniqueDept = this.Name + " (" + this.Code + ')';
        }

        public Department(  Guid ID,
                            string Name, 
                            string Code,      
                            Guid ParentDepartmentID)
        {
            this.ID = ID;
            this.Name = Name;
            this.Code = Code;
            this.ParentDepartmentID = ParentDepartmentID;
            this.UniqueDept = this.Name + " (" + this.Code + ')';
        }

        public Guid ID { get; set; }
        public Guid ParentDepartmentID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UniqueDept { get; set; }
    }
}
