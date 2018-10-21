using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DBConnect.model
{
    public class Department : ICloneable
    {
        public static string updateDepartmentWithParent = "update [dbo].[Department] set " +
                                                        "Name=@name" +
                                                        ", Code=@code" +
                                                        ", ParentDepartmentID=@parentid" +
                                                        " where ID=@id";
        public static string updateDepartment = "update [dbo].[Department] set " +
                                    "Name=@name, Code=@code where ID=@id";
        public static string deleteDepartment = "delete from [dbo].[Department] where [ID] =@id";
        public static string insertDepartment = "INSERT [dbo].[Department] VALUES (@id, @name, @code, @parentid)";
        public static string getDepartments = "select Name, Code, ID, ParentDepartmentID from department";
        public static string getDepartmentsWithNames = "SELECT " +
            "a.[ID] as ID, " +
            "a.[Name] as Name , " +
            "a.[Code] as Code, " +
            "a.[ParentDepartmentID] as ParentDepartmentID , " +
            "concat(b.[Name], b.[Code]) as ParentName " +
            "FROM [dbo].[Department] a left join[dbo].[Department] b on a.ParentDepartmentID = b.ID";
        public Department()
        {

        }
        public Department(  Guid ID,
                            string Name, 
                            string Code)
        {
            this.ID = ID;
            this.Code = Code;
            this.Name = Name;
            this.UniqueDept = this.Name + " (" + this.Code + ')';
        }

        public Department(  Guid ID,
                            string Name, 
                            string Code,      
                            Guid? ParentDepartmentID)
        {
            this.ID = ID;
            this.Name = Name;
            this.Code = Code;
            this.ParentDepartmentID = ParentDepartmentID;
            this.UniqueDept = this.Name + " (" + this.Code + ')';
        }

        public Guid ID { get; set; }
        public Guid? ParentDepartmentID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string UniqueDept { get; set; }
        public object Clone()
        {
            Object other = new Department(this.ID, this.Name, this.Code, this.ParentDepartmentID);
            return other;
        }
        public static implicit operator TreeNode(Department dept)
        {
            TreeNode node = new TreeNode(dept.UniqueDept);
            node.Name = dept.ID.ToString();
            return node;
        }
    }
}
