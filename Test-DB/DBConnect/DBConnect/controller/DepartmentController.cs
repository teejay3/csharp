using DBConnect.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBConnect.controller
{
    class DepartmentController : MyController
    {
        public DepartmentController()
        {
            Init();
        }
        private void Init()
        {
        }
        public List<Department> RefreshDepartmentsList()
        {
            List<Department> result = new List<Department>();
            DBUtil util = new DBUtil();
            try
            {
                DbDataReader reader = util.GetDbDataReader(Department.getDepartments);
                while (reader.Read())
                {
                    Guid id = (Guid)reader["ID"];
                    string name = (string)reader["Name"];
                    string code = (string)(!DBNull.Value.Equals(reader["Code"]) ? reader["Code"] : "");
                    Guid? parent = (Guid?)(!DBNull.Value.Equals(reader["ParentDepartmentID"]) ? reader["ParentDepartmentID"] : null);
                    result.Add(new Department(id, name, code, parent));
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка получения списка отделов " + ex.Message);
            }
            finally
            {
                util.CloseConnection();
            }
            return result;
        }
        public DataSet GetGridSet()
        {
            SqlDataAdapter dataAdapter;
            DataSet sds;
            //DataTable table;
            DBUtil util = new DBUtil();
            SqlCommand sCommand = new SqlCommand(Department.getDepartmentsWithNames, util.GetDBConnection());
            dataAdapter = new SqlDataAdapter(sCommand);
            SqlCommandBuilder sBuilder = new SqlCommandBuilder(dataAdapter);
            try
            {
                sds = new DataSet();
                dataAdapter.Fill(sds);
                //table = sds.Tables[0];
                return sds;

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                util.CloseConnection();
            }
        }
        public bool DeleteDepartment(SqlParameter id)
        {
            DBUtil util = new DBUtil();
            try
            {
                SqlCommand cmd = new SqlCommand(Department.deleteDepartment, util.GetDBConnection());
                cmd.Parameters.Add(id);
                util.OpenConnection();
                cmd.ExecuteNonQuery();
                util.CloseConnection();
                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ошибка удаления: возможно в отделе остались сотрудники или имеется дочерний отдел\n",
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
        public bool AddDepartment(List<SqlParameter> sqlParameters)
        {
            DBUtil util = new DBUtil();
            try
            {
                SqlCommand cmd = new SqlCommand(Department.insertDepartment, util.GetDBConnection());
                foreach(var p in sqlParameters)
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
                catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения запроса: " + ex.Message,
                                           "Ошибка запроса",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                util.CloseConnection();
                return false;
            }
        }
        public bool UpdateDepartment(string sqlRequest, List<SqlParameter> sqlParameters)
        {
            DBUtil util = new DBUtil();

            try { 
            SqlCommand cmd = new SqlCommand(sqlRequest, util.GetDBConnection());
            util.OpenConnection();
                foreach (var p in sqlParameters)
                { cmd.Parameters.Add(p); }
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
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка выполнения запроса: " + ex.Message,
                                           "Ошибка запроса",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                    return false;
                }
        }
    }
}
