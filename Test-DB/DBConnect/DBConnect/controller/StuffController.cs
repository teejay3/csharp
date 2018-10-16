using DBConnect.model;
using DBConnect.view;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBConnect.controller
{
    class StuffController : MyController
    {
        public StuffController()
        {
        }
        public bool RequestEmployee(string request, List<SqlParameter> sqlParameters)
        {
                DBUtil util = new DBUtil();
                try
                {
                    SqlCommand cmd = new SqlCommand(request, util.GetDBConnection());
                    foreach (var p in sqlParameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                    util.OpenConnection();
                    cmd.ExecuteNonQuery();
                    util.CloseConnection();
                    return true;
                }
                catch (InvalidCastException ex)
                {
                    MessageBox.Show("Ошибка преобразования данных: " + ex.Message + ' ' + ex.TargetSite,
                                       "Ошибка запроса",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    util.CloseConnection();
                    return false;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка запроса: " + ex.Message,
                                       "Ошибка запроса",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    util.CloseConnection();
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка : " + ex.Message,
                                       "Ошибка",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
                    util.CloseConnection();
                    return false;
                }
        }
        public Employee GetEmployee(int rowID)
        {
            if (rowID < 0) return null;
            string sqlRequest = Employee.getEmployeeData + rowID;
            Employee emp = new Employee();
            DbDataReader reader;
            DBUtil util = new DBUtil();
            try
            {
                reader = util.GetDbDataReader(sqlRequest);
                if (reader == null) throw new Exception("Connection probled");
                reader.Read();

                emp.ID              = (Decimal)reader["ID"];
                emp.FirstName       = (string)reader["FirstName"];
                emp.SurName         = (string)reader["SurName"];
                emp.Patronymic      = (string)(!DBNull.Value.Equals(reader["Patronymic"]) ? reader["Patronymic"] : ""); //NULL
                emp.DateOfBirth     = (string)reader["DateOfBirth"];
                emp.DocSeries       = (string)(!Convert.IsDBNull(reader["DocSeries"]) ? reader["DocSeries"] : ""); //NULL
                emp.DocNumber       = (string)(!Convert.IsDBNull(reader["DocNumber"]) ? reader["DocNumber"] : ""); //NULL
                emp.Position        = (string)reader["Position"];
                emp.DepartmentID    = (Guid)reader["DepartmentID"];

                reader.Close();
                util.CloseConnection();
                return emp;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                util.CloseConnection();
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                util.CloseConnection();
                return null;
            }
        }
        public DataSet GridData(string sqlRequest)
        {
            DataSet sds;
            SqlDataAdapter dataAdapter;
            DataTable table;
            using (DBUtil util = new DBUtil())
            {
                SqlCommand sCommand = new SqlCommand(sqlRequest, util.GetDBConnection());
                dataAdapter = new SqlDataAdapter(sCommand);
                SqlCommandBuilder sBuilder = new SqlCommandBuilder(dataAdapter);
                sds = new DataSet();
                try
                {
                    dataAdapter.Fill(sds, "Empoyee");
                    table = sds.Tables["Empoyee"];
                    return sds;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public List<Department> GetDepartments()
        {
            List<Department> result = new List<Department>();
            DBUtil util = new DBUtil();
            try
            {
                DbDataReader reader = util.GetDbDataReader(Department.getDepartments);
                while (reader.Read())
                {
                    result.Add(new Department((System.Guid)reader["ID"],
                                                    //(System.Guid)reader["ParentDepartmentID"],
                                                    (string)reader["Code"],
                                                    (string)reader["Name"])
                                    );
                }
                reader.Close();
                util.CloseConnection();
                return result;
            }
            catch (Exception ex)
            {
                util.CloseConnection();
                return null;
            }
        }
    }
}
