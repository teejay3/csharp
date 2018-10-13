using DBConnect.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBConnect.view
{
    public partial class StuffView : Form
    {
        private List<Department> departments;
        private List<Employee> emps;
        private const string getDepartments = "Select * from Department";
//        private const string getEmployees = "select ID as 'ИД', FirstName as 'Имя', SurName as 'Фамилия', Patronymic as 'Отчество',"
//                +" Position as 'Должность', DocSeries as 'Серия док.', DocNumber as 'Номер док.',"
//                +" DateOfBirth as 'Дата рождения', datediff(mm, dateofbirth, getdate()) / 12 as 'Возраст, лет' from Empoyee";
        private const string getEmployees = "select ID as 'ИД', FirstName as 'Имя', SurName as 'Фамилия', Patronymic as 'Отчество',"
                + " Position as 'Должность', cast(datediff(mm, dateofbirth, getdate()) / 12 as int) as 'Возраст, лет'"
                + " from Empoyee";
        private const string deleteItem = "delete from empoyee where id =";
        private const string getEmployeeData = "select ID, FirstName, SurName, Patronymic, FORMAT(DateOfBirth,'yyyy-MM-dd') as DateOfBirth, DocSeries, DocNumber, Position, DepartmentID from empoyee where id=";
        private int rowID;
        public StuffView()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            
            deleteButton.Enabled    = false;
            saveButton.Enabled      = false;
            addButton.Enabled       = false;
            deptComboBox.Enabled    = false;

            dataGridView1.ReadOnly = true;
            
            comboBox1.SelectedValueChanged += (o, e) => {
                UpdateGrid();
                deleteButton.Enabled    = false;
                saveButton.Enabled      = false;
                addButton.Enabled       = false;
                deptComboBox.Enabled    = false;
            };
            showAllButton.Click += (o, e) => { FillGrid(getEmployees); };
            dataGridView1.CellClick += (o, e) => 
            {

                string idStr = "0";
                try
                {
                    idStr = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    int.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString(), out rowID);
                }
                catch (Exception ex)
                {
                }
                if (rowID > 0)
                {
                    
                    deleteButton.Enabled    = true;
                    saveButton.Enabled      = true;
                    addButton.Enabled       = false;
                    deptComboBox.Enabled    = true;
                    GetEmployee();
                }
                else
                {
                    deleteButton.Enabled = false;
                    saveButton.Enabled = false;
                    addButton.Enabled = true;
                    deptComboBox.Enabled = true;
                    ClearEdits();
                    deptComboBox.SelectedValue = comboBox1.SelectedValue;
                }
            };
            refreshButton.Click += (o, e) => 
            {
                UpdateGrid();
                ClearEdits();
                deleteButton.Enabled = false;
                saveButton.Enabled = false;
                addButton.Enabled = false;
                deptComboBox.Enabled = false;
            };
            addButton.Click += (o, e) => { InsertItem(); };
            saveButton.Click += (o, e) => { UpdateItem(); };
            deleteButton.Click += (o, e) => { DeleteItem(); };
            departments = new List<Department>();
            emps = new List<Employee>();
            departments = GetDepartments();
            if (departments.Count != 0) { UpdateComboBox(); }
            else {
                MessageBox.Show("Критическая ошибка: невозможно загрузить список подразделений или он пуст",
                                        "Ошибка",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                this.Close();
            }
            FillGrid(getEmployees);
        }
        private List<Department> GetDepartments()
        {
            List<Department> result = new List<Department>();
            using (DBUtil util = new DBUtil())
            {
                DbDataReader reader = util.GetDbDataReader(getDepartments);
                while (reader.Read())
                {
                    result.Add(new Department( (System.Guid)reader["ID"],
                                                    //(System.Guid)reader["ParentDepartmentID"],
                                                    (string)reader["Code"],
                                                    (string)reader["Name"])
                                    );
                }
            }
            return result;
        }
        private void UpdateComboBox()
        {
            BindingSource bs        = new BindingSource();
            bs.DataSource           = departments;
            comboBox1.DataSource    = bs;
            comboBox1.DisplayMember = "UniqueDept";
            comboBox1.ValueMember   = "ID";

            deptComboBox.DataSource = departments;
            deptComboBox.DisplayMember = "UniqueDept";
            deptComboBox.ValueMember = "ID";
        }

        private void FillGrid(string sqlRequest)
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
                dataAdapter.Fill(sds, "Empoyee");
                table = sds.Tables["Empoyee"];
            }
            dataGridView1.DataSource = sds.Tables["Empoyee"];
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void UpdateGrid()
        {
            Department dept = (Department)comboBox1.SelectedItem;
            string sql = getEmployees + " where departmentid = '" + dept.ID + "'";
            FillGrid(sql);
        }
        private void UpdateItem()
        {
            if (ValidateEdits())
            {
                string sqlUpdate = "update empoyee set " +
                    " FirstName=@firstname" +
                    ", SurName=@surname" +
                    ", Patronymic=@patronymic" +
                    ", DateOfBirth=@dateofbirth" +
                    ", DocSeries=@docseries" +
                    ", DocNumber=@docnumber" +
                    ", Position=@position" +
                    ", DepartmentID=@departmentid " +
                    "where ID =" + rowID;
                
                using (DBUtil util = new DBUtil())
                {
                        Department dept = (Department)deptComboBox.SelectedItem;
                        SqlCommand cmd = new SqlCommand(sqlUpdate, util.GetDBConnection());
                        util.OpenConnection();
                        cmd.Parameters.AddWithValue("@firstname", firstnameBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@surname", surnameBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@patronymic", patronymicBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@dateofbirth", Convert.ToDateTime(dobBox.Text));
                        cmd.Parameters.AddWithValue("@docseries", docseriesBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@docnumber", docnumberBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@position", positionBox.Text.Trim());
                        cmd.Parameters.AddWithValue("@departmentid", dept.ID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (InvalidCastException ex)
                    {

                        MessageBox.Show("Ошибка преобразования данных: " + ex.Message + ' ' + ex.TargetSite,
                                           "Ошибка запроса",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка выполнения запроса: " + ex.Message,
                                           "Ошибка запроса",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                        return;
                    }
                }
                statusLabel.Text = "Запись " + idBox.Text + " обновлена";
            }
            else
            {
                statusLabel.Text += " проверьте введённый данные!";
            }
        }
        private void InsertItem()
        {
            if (ValidateEdits())
            {
                string sqlUpdate = "insert into empoyee values (" +
                    " @firstname" +
                    ", @surname" +
                    ", @patronymic" +
                    ", @dateofbirth" +
                    ", @docseries" +
                    ", @docnumber" +
                    ", @position" +
                    ", @departmentid )";

                using (DBUtil util = new DBUtil())
                {
                    Department dept = (Department)deptComboBox.SelectedItem;
                    SqlCommand cmd = new SqlCommand(sqlUpdate, util.GetDBConnection());
                    util.OpenConnection();
                    cmd.Parameters.AddWithValue("@firstname", firstnameBox.Text.Trim());
                    cmd.Parameters.AddWithValue("@surname", surnameBox.Text.Trim());
                    cmd.Parameters.AddWithValue("@patronymic", patronymicBox.Text.Trim());
                    cmd.Parameters.AddWithValue("@dateofbirth", Convert.ToDateTime(dobBox.Text));
                    cmd.Parameters.AddWithValue("@docseries", docseriesBox.Text.Trim());
                    cmd.Parameters.AddWithValue("@docnumber", docnumberBox.Text.Trim());
                    cmd.Parameters.AddWithValue("@position", positionBox.Text.Trim());
                    cmd.Parameters.AddWithValue("@departmentid", dept.ID);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (InvalidCastException ex)
                    {

                        MessageBox.Show("Ошибка преобразования данных: " + ex.Message + ' ' + ex.TargetSite,
                                           "Ошибка запроса",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка выполнения запроса: " + ex.Message,
                                           "Ошибка запроса",
                                           MessageBoxButtons.OK,
                                           MessageBoxIcon.Error);
                        return;
                    }
                }
                statusLabel.Text = "Новая запись добавлена";
            }
        }
        private void DeleteItem()
        {
            try
            {
                DBUtil util = new DBUtil();
            
                string sqlDelItem = deleteItem + rowID;
                SqlCommand cmd = new SqlCommand(sqlDelItem, util.GetDBConnection());
                util.OpenConnection();
                cmd.ExecuteNonQuery();
                statusLabel.Text = "Сотрудник " + rowID + " удалён";
                ClearEdits();
                //UpdateGrid();
            }
            catch (SqlException ex)
            {

            }
            catch (Exception ex)
            {

            }
        }
        private void GetEmployee() //try catch
        {
            Employee emp = new Employee();
            using (DBUtil util = new DBUtil())
            {
                string sqlRequest = getEmployeeData + rowID;
                DbDataReader reader = util.GetDbDataReader(sqlRequest);
                if (reader == null) { statusLabel.Text = "Ошибка!"; return; }
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
            }
            FillEdits(emp);
        }
        private void FillEdits(Employee emp)
        {
            idBox.Text = emp.ID.ToString();
            firstnameBox.Text = emp.FirstName;
            surnameBox.Text = emp.SurName;
            patronymicBox.Text = emp.Patronymic;
            dobBox.Text = emp.DateOfBirth.ToString();
            docseriesBox.Text = emp.DocSeries;
            docnumberBox.Text = emp.DocNumber;
            positionBox.Text = emp.Position;
            deptComboBox.SelectedValue = emp.DepartmentID;
        }
        private void ClearEdits()
        {
            idBox.Text = "";
            firstnameBox.Text = "";
            surnameBox.Text = "";
            patronymicBox.Text = "";
            dobBox.Text = "";
            docseriesBox.Text = "";
            docnumberBox.Text = "";
            positionBox.Text = "";
            statusLabel.Text = "";
        }
        private bool ValidateEdits()
        {
            statusLabel.Text = "";
            if (string.IsNullOrEmpty(firstnameBox.Text.ToString().Trim()))
            {
                statusLabel.Text = "Проверьте имя!";
                return false;
            } else if (firstnameBox.Text.ToString().Trim().Length > 50)
            {
                statusLabel.Text = "Имя слишком длинное";
                return false;
            }
            if (string.IsNullOrEmpty(surnameBox.Text.ToString().Trim()))
            {
                statusLabel.Text = "Проверьте фамилию!";
                return false;
            } else if (surnameBox.Text.ToString().Trim().Length > 50)
            {
                statusLabel.Text = "Фамилия слишком длинная";
                return false;
            }
            if (patronymicBox.Text.ToString().Trim().Length > 50)
            {
                statusLabel.Text = "Отчество слишком длинное";
                return false;
            }
            if(docseriesBox.Text.ToString().Trim().Length > 4)
            {
                statusLabel.Text = "Серия документа слишком длинная";
                return false;
            }
            if (docnumberBox.Text.ToString().Trim().Length > 6)
            {
                statusLabel.Text = "Номер документа слишком длинный";
                return false;
            }
            try
            {
                Convert.ToDateTime(dobBox.Text);
            }
            catch (FormatException)
            {
                statusLabel.Text = "Проверьте дату! ";
                return false;
            }
            if (string.IsNullOrEmpty(positionBox.Text.ToString().Trim())) { statusLabel.Text = "Проверьте должность!"; return false; }
            Department dept = (Department)deptComboBox.SelectedItem;
            if (!departments.Contains(dept)) { statusLabel.Text = "Что-то не то с отделом"; return false; }
            return true;
        }
    }
}
