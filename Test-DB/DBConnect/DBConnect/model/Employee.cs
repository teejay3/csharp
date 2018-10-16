using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBConnect.model
{
    public class Employee
    {
        public static string getEmployees = "select ID as 'ИД', " +
            "FirstName as 'Имя', " +
            "SurName as 'Фамилия', " +
            "Patronymic as 'Отчество',"
            + " Position as 'Должность', " +
            "cast(datediff(mm, dateofbirth, getdate()) / 12 as int) as 'Возраст, лет'"
            + " from Empoyee";
        public static string deleteItem = "delete from empoyee where id = @id";
        public static string getEmployeeData = "select ID, FirstName, SurName, Patronymic, FORMAT(DateOfBirth,'yyyy-MM-dd') as DateOfBirth, DocSeries, DocNumber, Position, DepartmentID from empoyee where id=";
        public static string insertEmployee = "insert into empoyee values ( " +
                    "@firstname" +
                    ", @surname" +
                    ", @patronymic" +
                    ", @dateofbirth" +
                    ", @docseries" +
                    ", @docnumber" +
                    ", @position" +
                    ", @departmentid )";
        public static string updateEmployee = "update empoyee set " +
                    " FirstName=@firstname" +
                    ", SurName=@surname" +
                    ", Patronymic=@patronymic" +
                    ", DateOfBirth=@dateofbirth" +
                    ", DocSeries=@docseries" +
                    ", DocNumber=@docnumber" +
                    ", Position=@position" +
                    ", DepartmentID=@departmentid " +
                    "where ID =@id";
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
