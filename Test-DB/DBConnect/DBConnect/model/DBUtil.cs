using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.Data.Common;
using System.Collections.Generic;
using DBConnect.view;

namespace DBConnect
{
//
// Data Source=.....\SQLEXPRESS;
// Initial Catalog=TestDB;
// Persist Security Info=True;
// User ID=sa;
// Password =12345
//
    class DBUtil : IDisposable
    {
        private SqlConnection conn;
        private DbDataReader dataReader;
        private const int timeout = 6;
        private List<MyView> viewList;

        public DBUtil()
        {
            viewList = new List<MyView>();
            conn = new SqlConnection(GetString());
            //conn = GetDBConnection();
        }
        public void GetState()
        {

        }
        public bool IsServerConnected()
        {
            using (var conn = new SqlConnection(GetString()))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        private string GetString()
        {
            string s = ConfigurationManager.AppSettings["Data source"];
            if (String.IsNullOrEmpty(s))
            {
                MessageBox.Show("Конфигурационный файл не найден.",
                                "Ошибка файла конфигурации",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            try
            {
                string result = @"Data Source=" + ConfigurationManager.AppSettings["Data source"] +
                        ";Initial Catalog=" + ConfigurationManager.AppSettings["Initial catalog"] +
                        ";Persist Security Info=True;User ID=" + ConfigurationManager.AppSettings["User name"] +
                        ";Password=" + ConfigurationManager.AppSettings["Password"] +
                        ";Connect Timeout=" + timeout;
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                MessageBox.Show("Настройки недоступны: " + ex.Message, 
                                "Ошибка файла конфигурации", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Error);
                Console.WriteLine("Error reading app settings");
                return "";
            }
            finally
            {
                //
            }
                        
        }
        public SqlConnection GetDBConnection()
        {
            //return new SqlConnection(GetString());
            return conn;
        }
        public void TestConnection()
        {
            if (IsServerConnected())
            {
                MessageBox.Show("Соединение установлено.",
                                "Проверка соединения",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Ошибка соединения.",
                                "Проверка соединения",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
        public DbDataReader GetDbDataReader (string sqlRequest)
        {
            OpenConnection();
            try
            {
                SqlCommand cmd = new SqlCommand(sqlRequest, conn);
                cmd.CommandTimeout = 5;
                dataReader = cmd.ExecuteReader();
                return dataReader;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка запроса: " + ex.Message,
                                       "Ошибка запроса",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
            }
            return null;
        }
        public void OpenConnection()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show("Ошибка открытия соединения: " + ex.Message,
                                    "Ошибка соединения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    Console.WriteLine("Error opening connection: InvalidOperationException");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка открытия соединения: " + ex.Message,
                                    "Ошибка соединения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    Console.WriteLine("Error opening connection: SqlException");
                }
            }
        }
        public void CloseConnection ()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                try
                {
                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Ошибка закрытия соединения: " + ex.Message,
                                    "Ошибка соединения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    //Console.WriteLine("Error closing connection");
                }
            }
        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}

/*              //SqlCommand blobSQL = new SqlCommand("SELECT [DocumentData] FROM [dbo].[tbl] WHERE [Part.PartID] = @GUID", dataConnection);
                            //Guid guid = new Guid(comboBox1.SelectedValue);
                            //blobSQL.Parameters.Add("@GUID", SqlDbType.UniqueIdentifier);
                            //blobSQL.Parameters["@GUID"].Value = guid;
            */

/*                        BindingSource bs2 = new BindingSource();
                        dataGridView1.DataSource = bs2;

                        dataAdapter = new SqlDataAdapter(sql, conn);
                        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                        table = new DataTable();
                        table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                        dataAdapter.Fill(table);
                        bs2.DataSource = table;
                        dataGridView1.AutoResizeColumns(
                        DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            dataGridView1.ReadOnly = false;
*/
