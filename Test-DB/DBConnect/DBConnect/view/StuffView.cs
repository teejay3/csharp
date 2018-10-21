using DBConnect.controller;
using DBConnect.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DBConnect.view
{
    partial class StuffView : MyView
    {
        private List<Department> departments;
        private StuffController stuffController;
        private int rowID;
        private Guid currentID;
        private bool showAll = false;
        public StuffView()
        {
            InitializeComponent();
            try
            {
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message,
                        "Ошибка инициализации",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
        private void Init()
        {
            this.stuffController = new StuffController();
            departments = new List<Department>();
            DisableButtons();

            dataGridView1.ReadOnly = true;

            //GetDepartments();
            if (departments == null)
            {
                MessageBox.Show("Критическая ошибка: невозможно загрузить список подразделений или он пуст",
                                        "Ошибка",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                this.Hide();
            }
            //FillGrid(Employee.getEmployees);

            this.Shown += (o, e) => {  GetDepartments(); FillGrid(Employee.getEmployees); BuildTree(); };
            //this.FormClosed += (o, e) => { this.Dispose(); };
            showAllButton.Click += (o, e) => { FillGrid(Employee.getEmployees); showAll = true; };
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
                    deptComboBox.SelectedValue = currentID;
                }
            };
            refreshButton.Click += (o, e) => 
            {
                GetDepartments();
                UpdateGrid();
                ClearEdits();
                BuildTree();
                DisableButtons();
                showAll = false;
            };
            addButton.Click += (o, e) => { InsertItem(); };
            saveButton.Click += (o, e) => { UpdateItem(); };
            deleteButton.Click += (o, e) => { DeleteItem(); };
            buttonHide.Click += (o, e) => { if (splitContainer1.Panel2Collapsed) buttonHide.Text = ">"; else buttonHide.Text = "<"; splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed; };
            treeView1.NodeMouseClick += (o, e) => 
            {
                showAll = false;
                try
                {
                    //Guid.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out currentID);   //c NET4.5
                    currentID = new Guid(e.Node.Name);
                }
                catch (Exception ex)
                {
                    statusLabel.Text = "Ошибка, такого отдела нет";
                }
                string name = e.Node.Name + " " + e.Node.Text;
                Console.WriteLine(name);
                UpdateGrid();
                DisableButtons();
            };
        }
        private void DisableButtons()
        {
            deleteButton.Enabled = false;
            saveButton.Enabled = false;
            addButton.Enabled = false;
            deptComboBox.Enabled = false;
        }
        private void GetDepartments()
        {
            departments.Clear();
            try
            {
                departments = stuffController.GetDepartments();
                if (departments == null) throw new Exception("Спсок департаментов не получен");
                if (departments.Count != 0)
                {
                    UpdateComboBox();
                }
                else
                {
                    statusLabel.Text = "Ошибка получения списка подразделений";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        private void UpdateComboBox()
        {
            BindingSource bs2           = new BindingSource();
            bs2.DataSource              = departments;
            deptComboBox.DataSource     = bs2;
            deptComboBox.DisplayMember  = "UniqueDept";
            deptComboBox.ValueMember    = "ID";
        }
        private void FillGrid(string sqlRequest)
        {
            DataSet sds = stuffController.GridData(sqlRequest);
            if (sds == null)
            {
                statusLabel.Text = "Ошибка обновления";
                return;
            }
            dataGridView1.DataSource = sds.Tables["Empoyee"];
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        private void UpdateGrid()
        {
            string sql = Employee.getEmployees + " where departmentid = '" + currentID + "'";
            FillGrid(sql);
        }
        private void UpdateItem()
        {
            if (ValidateEdits())
            {
                List<SqlParameter> list = new List<SqlParameter>();
                SqlParameter firstname = new SqlParameter("@firstname", SqlDbType.NVarChar);
                firstname.Value = firstnameBox.Text.Trim();
                list.Add(firstname);
                SqlParameter surname = new SqlParameter("@surname", SqlDbType.NVarChar);
                surname.Value = surnameBox.Text.Trim();
                list.Add(surname);
                SqlParameter patronymic = new SqlParameter("@patronymic", SqlDbType.NVarChar);
                patronymic.IsNullable = true;
                patronymic.Value = patronymicBox.Text.Trim();
                list.Add(patronymic);
                SqlParameter dateofdirth = new SqlParameter("@dateofbirth", SqlDbType.DateTime);
                dateofdirth.Value = Convert.ToDateTime(dobBox.Text);
                list.Add(dateofdirth);
                SqlParameter docseries = new SqlParameter("@docseries", SqlDbType.NVarChar);
                docseries.IsNullable = true;
                docseries.Value = docseriesBox.Text.Trim();
                list.Add(docseries);
                SqlParameter docnumber = new SqlParameter("@docnumber", SqlDbType.NVarChar);
                docnumber.IsNullable = true;
                docnumber.Value = docnumberBox.Text.Trim();
                list.Add(docnumber);
                SqlParameter position = new SqlParameter("@position", SqlDbType.NVarChar);
                position.Value = positionBox.Text.Trim();
                list.Add(position);
                SqlParameter parentid = new SqlParameter("@departmentid", SqlDbType.UniqueIdentifier);
                Department dept = (Department)deptComboBox.SelectedItem;
                parentid.Value = dept.ID;
                list.Add(parentid);
                SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
                id.Value = rowID;
                list.Add(id);

                if (stuffController.RequestEmployee(Employee.updateEmployee, list))
                {
                    statusLabel.Text = "Запись " + idBox.Text + " обновлена";
                    if (showAll) FillGrid(Employee.getEmployees);
                    else UpdateGrid();
                }
                else
                {
                }
            }
            else
            {
            }
        }
        private void InsertItem()
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (ValidateEdits())
            {
                SqlParameter firstname = new SqlParameter("@firstname", SqlDbType.NVarChar);
                firstname.Value = firstnameBox.Text.Trim();
                list.Add(firstname);
                SqlParameter surname = new SqlParameter("@surname", SqlDbType.NVarChar);
                surname.Value = surnameBox.Text.Trim();
                list.Add(surname);
                SqlParameter patronymic = new SqlParameter("@patronymic", SqlDbType.NVarChar);
                patronymic.IsNullable = true;
                patronymic.Value = patronymicBox.Text.Trim();
                list.Add(patronymic);
                SqlParameter dateofdirth = new SqlParameter("@dateofbirth", SqlDbType.DateTime);
                dateofdirth.Value = Convert.ToDateTime(dobBox.Text);
                list.Add(dateofdirth);
                SqlParameter docseries = new SqlParameter("@docseries", SqlDbType.NVarChar);
                docseries.IsNullable = true;
                docseries.Value = docseriesBox.Text.Trim();
                list.Add(docseries);
                SqlParameter docnumber = new SqlParameter("@docnumber", SqlDbType.NVarChar);
                docnumber.IsNullable = true;
                docnumber.Value = docnumberBox.Text.Trim();
                list.Add(docnumber);
                SqlParameter position = new SqlParameter("@position", SqlDbType.NVarChar);
                position.Value = positionBox.Text.Trim();
                list.Add(position);
                SqlParameter parentid = new SqlParameter("@departmentid", SqlDbType.UniqueIdentifier);
                Department dept = (Department)deptComboBox.SelectedItem;
                parentid.Value = dept.ID;
                list.Add(parentid);

                if (stuffController.RequestEmployee(Employee.insertEmployee, list))
                {
                    statusLabel.Text = "Новая запись добавлена";
                    currentID = dept.ID;

                    TreeNode[] list1 = treeView1.Nodes.Find(dept.ID.ToString(), true);
                    if (list1.Length != 0)
                        treeView1.SelectedNode = list1[0];
                    if (showAll) FillGrid(Employee.getEmployees);
                    else UpdateGrid();
                }
                else
                {
                    statusLabel.Text = "Ошибка добавления записи";
                }
            }
        }
        private void DeleteItem()
        {
            SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
            id.Value = rowID;
            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(id);
            if (stuffController.RequestEmployee(Employee.deleteItem, list))
            {
                statusLabel.Text = "Сотрудник " + rowID + " удалён";
                
                ClearEdits();
                if (showAll) FillGrid(Employee.getEmployees);
                else UpdateGrid();
            }
        }
        private void GetEmployee()
        {
            Employee emp = stuffController.GetEmployee(rowID);
            if (emp != null)
            {
                FillEdits(emp);
            }
            else
            {
                statusLabel.Text = "Ошибка: сотрудник не найден!";
            }
        }
        private void FillEdits(Employee emp)
        {
            idBox.Text                  = emp.ID.ToString();
            firstnameBox.Text           = emp.FirstName;
            surnameBox.Text             = emp.SurName;
            patronymicBox.Text          = emp.Patronymic;
            dobBox.Text                 = emp.DateOfBirth.ToString();
            docseriesBox.Text           = emp.DocSeries;
            docnumberBox.Text           = emp.DocNumber;
            positionBox.Text            = emp.Position;
            deptComboBox.SelectedValue  = emp.DepartmentID;
        }
        private void ClearEdits()
        {
            idBox.Text          = "";
            firstnameBox.Text   = "";
            surnameBox.Text     = "";
            patronymicBox.Text  = "";
            dobBox.Text         = "";
            docseriesBox.Text   = "";
            docnumberBox.Text   = "";
            positionBox.Text    = "";
            //statusLabel.Text  = "";
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
                statusLabel.Text = "Имя слишком длинное (макс. 50 символов)";
                return false;
            }
            if (string.IsNullOrEmpty(surnameBox.Text.ToString().Trim()))
            {
                statusLabel.Text = "Проверьте фамилию!";
                return false;
            } else if (surnameBox.Text.ToString().Trim().Length > 50)
            {
                statusLabel.Text = "Фамилия слишком длинная (макс. 50 символов)";
                return false;
            }
            if (patronymicBox.Text.ToString().Trim().Length > 50)
            {
                statusLabel.Text = "Отчество слишком длинное (макс. 50 символов)";
                return false;
            }
            if(docseriesBox.Text.ToString().Trim().Length > 4)
            {
                statusLabel.Text = "Серия документа слишком длинная (макс. 4 символа)";
                return false;
            }
            if (docnumberBox.Text.ToString().Trim().Length > 6)
            {
                statusLabel.Text = "Номер документа слишком длинный (макс. 6 символов)";
                return false;
            }
            try //проверить
            {
                Convert.ToDateTime(dobBox.Text);
            }
            catch (FormatException)
            {
                statusLabel.Text = "Проверьте дату! ";
                return false;
            }
            if (string.IsNullOrEmpty(positionBox.Text.ToString().Trim()))
            {
                statusLabel.Text = "Проверьте должность!";
                return false;
            }
            if (positionBox.Text.ToString().Trim().Length > 50)
            {
                statusLabel.Text = "Название должности слишком длинное (макс. 50 символов)";
                return false;
            }
            Department dept = (Department)deptComboBox.SelectedItem;
            //var list = departments.Select(p => p.ID).ToList<Guid>();
            //if (!list.Contains(dept.ID))
            if (!departments.Select(p=>p.ID).Contains(dept.ID))
            {
                statusLabel.Text = "Что-то не то с отделом";
                return false;
            }
            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    {
                        Console.WriteLine("up");
                        return true;
                    }
                case Keys.Down:
                    {
                        Console.WriteLine("down");
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void BuildTree()
        {
            treeView1.Nodes.Clear();

            List<Department> tmpArr = new List<Department>();
            for (int i = 0; i < departments.Count; i++)
            {
                //                tmpArr.Add(new Department(departments[i].ID, 
                //                    departments[i].Name, 
                //                    departments[i].Code, 
                //                    departments[i].ParentDepartmentID));
                tmpArr.Add(departments[i].Clone() as Department);
            }
            
            Department rootDept = new Department();
            
            for (int i = 0; i < tmpArr.Count; i++)
            {
                if (tmpArr[i].ParentDepartmentID == null)
                {
                    rootDept = tmpArr[i];
                    tmpArr.RemoveAt(i);
                    break;
                }
            }

            treeView1.Nodes.Add((TreeNode)rootDept);

            Department tmp = new Department();
            List<Department> list = new List<Department>();
            Queue<Department> queue = new Queue<Department>();

            do
            {
                for (int j = 0; j < tmpArr.Count; j++)
                {
                    if (tmpArr[j].ParentDepartmentID == rootDept.ID)
                    {
                        list.Add(tmpArr[j]);
                        queue.Enqueue(tmpArr[j]);
                        //tmpArr.RemoveAt(j);
                    }
                }
                AddNode(rootDept, list);
                if (queue.Count == 0) break;
                rootDept = queue.Dequeue();
                list.Clear();
            } while (true);
            treeView1.ExpandAll();
        }
        private void AddNode(Department dept, List<Department> list)
        {
            TreeNode[] treeArr = new TreeNode[list.Count];
            for (int i = 0; i < treeArr.Length; i++)
            {
                //                treeArr[i] = new TreeNode(list[i].UniqueDept);
                //                treeArr[i].Name = list[i].ID.ToString();
                treeArr[i] = (TreeNode)list[i];
            }
            foreach (var item in treeArr)
            {
                TreeNode[] list1 = treeView1.Nodes.Find(dept.ID.ToString(), true);
                if(list1.Length != 0)
                    list1[0].Nodes.Add(item);
            }
            
        }
        private void StuffView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        public override void DeptUpdate()
        {
            GetDepartments();
            BuildTree();
            FillGrid(Employee.getEmployees);
        }
    }
}
