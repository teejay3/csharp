using DBConnect.controller;
using DBConnect.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DBConnect.view
{
    public partial class DepartmentsView : MyView
    {
        private List<Department> dept;
        private DepartmentController deptController;
        private Guid currentID;
        public DepartmentsView()
        {
            InitializeComponent();
            try
            {
                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message,
                        "Ошибка запроса",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
        }
        private void Init()
        {
            this.deptController = new DepartmentController();
            dept = new List<Department>();
            DisableButtons();

            this.Shown += (o, e) => { UpdateDepartments(); };
            refreshButton.Click += (o, e) => {  UpdateDepartments(); ClearEdits(); DisableButtons(); };
            updateButton.Click += (o, e) => { EditDepartment(); ClearEdits(); };
            addButton.Click += (o, e) => { AddDepartment(); };
            deleteButton.Click += (o, e) => { DeleteDepartment(); };
//            dataGridView1.KeyPress += DataGridView1_KeyPress;
//            dataGridView1.RowEnter += (o, e) =>
            dataGridView1.CellClick += (o, e) =>
            {
                if (e.RowIndex >= 0 &&
                e.RowIndex < dept.Count && 
                dataGridView1.Rows[e.RowIndex].Cells[0] != null)
                {
                    try
                    {
                        //Guid.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out currentID);   //c 4.5
                        currentID = new Guid(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        statusLabel.Text = "Ошибка, такого отдела нет";
                    }
                    deleteButton.Enabled = true;
                    updateButton.Enabled = true;
                    addButton.Enabled = false;
                    deptBox.Enabled = true;
                    FillEdits(e.RowIndex);
                    Console.WriteLine(e.RowIndex);
                }
                else if (e.RowIndex == dept.Count)
                {
                    deleteButton.Enabled = false;
                    updateButton.Enabled = false;
                    addButton.Enabled = true;
                    deptBox.Enabled = true;
                    Console.WriteLine("new row " + e.RowIndex);
                    dataGridView1.SendToBack();
                    ClearEdits();
                    
                }
            };
        }
        private void DisableButtons()
        {
            updateButton.Enabled = false;
            addButton.Enabled = false;
            deleteButton.Enabled = false;
        }
        private void DataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        private void ClearEdits()
        {
            //statusLabel.Text = "";
            codeBox.Text = "";
            nameBox.Text = "";
        }
        private void UpdateDepartments()
        {
            dept.Clear();
            dept = deptController.RefreshDepartmentsList();
            try
            {
                if (dept.Count == 0) throw new Exception("Ошибка: список отделов пуст");
                else
                {
                    Console.WriteLine("departments filled, total number: " + dept.Count);
                    FillGrid();
                    FillComboBox();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message,
                                       "Ошибка запроса",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Error);
            }

        }
        private void FillGrid()
        {
            DataSet sds = deptController.GetGridSet();

            if (sds == null)
            {
                MessageBox.Show("Ошибка получения списка отделов. Проверьте подключение.",
                        "Ошибка запроса",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                throw new Exception("Ошибка получения списка отделов");
            }
            Console.WriteLine("grid updated");
            dataGridView1.DataSource = sds.Tables[0];
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[0].Visible = false;
        }
        private void FillEdits(int idx)
        {
            try
            {
                Department dep = dept.First(p => p.ID == currentID);
                nameBox.Text = dep.Name;
                codeBox.Text = dep.Code;
                deptBox.SelectedValue = dep.ParentDepartmentID != null ? dep.ParentDepartmentID : dep.ID;
            }
            catch (Exception ex)
            {
                ClearEdits();
                UpdateDepartments();
                statusLabel.Text = "Ошибка, такого отдела нет";
            }
        }
        private bool ValidateEdits()
        {
            if (string.IsNullOrEmpty(nameBox.Text.Trim()) || nameBox.Text.Trim().Length > 50) { statusLabel.Text = "Слишком длинное имя (до 50 символов)";  return false; }
            if (string.IsNullOrEmpty(codeBox.Text.Trim()) || codeBox.Text.Trim().Length > 10) { statusLabel.Text = "Слишком длинный мнемокод (до 10 символов)"; return false; }
            return true;
        }
        private void FillComboBox()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dept;
            deptBox.DataSource = bs;
            deptBox.DisplayMember = "UniqueDept";
            deptBox.ValueMember = "ID";
        }
        private void EditDepartment()
        {
            List<SqlParameter> list = new List<SqlParameter>();
            if (ValidateEdits())
            {
                bool isRoot = dept.Where(p => p.ID == currentID).Select(d => d.ParentDepartmentID).Contains(null);
                string str = Department.updateDepartment;

                SqlParameter idd = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                idd.Value = currentID;
                list.Add(idd);
                SqlParameter name = new SqlParameter("@name", SqlDbType.NVarChar);
                name.Value = nameBox.Text.Trim();
                list.Add(name);
                SqlParameter code = new SqlParameter("@code", SqlDbType.NVarChar);
                code.Value = codeBox.Text.Trim();
                list.Add(code);
                SqlParameter parentid = new SqlParameter("@parentid", SqlDbType.UniqueIdentifier);
                parentid.IsNullable = true;
                Department patentDept = (Department)deptBox.SelectedItem;
                parentid.Value = patentDept.ID;

                if (!isRoot)
                {
                        Console.WriteLine("update with parent id");
                        str = Department.updateDepartmentWithParent;
                        list.Add(parentid);
                }
                if (!deptController.UpdateDepartment(str, list))
                {
                    statusLabel.Text = "Ошибка обновления";
                    return;
                }
                try
                {
                    statusLabel.Text = "Отдел " +
                    dept.First<Department>(p => p.ID == currentID).Name +
                    " обновлён";
                }
                catch { }
                UpdateDepartments();
                ClearEdits();
            }
        }
        private void DeleteDepartment()
        {
            Department dep;
            try
            {
                dep = dept.First(p => p.ID == currentID);
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Департамент не найден";
                return;
            }

            SqlParameter id = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
            id.Value = dep.ID;

            if (dep.ParentDepartmentID != null && deptController.DeleteDepartment(id))
            {
                statusLabel.Text = "Департамент " + dep.UniqueDept + " удалён";
                ClearEdits();
            }
            else
            {
                statusLabel.Text = "Нельзя удалить " + dep.UniqueDept;
            }
            FillGrid();
        }
        private void AddDepartment()
        {
            Department dept = (Department)deptBox.SelectedItem;
            if (ValidateEdits())
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    
                SqlParameter idd = new SqlParameter("@id", SqlDbType.UniqueIdentifier);
                idd.IsNullable = false;
                idd.Value = Guid.NewGuid();
                sqlParameters.Add(idd);

                SqlParameter name = new SqlParameter("@name", SqlDbType.NVarChar);
                name.Value = nameBox.Text.Trim();
                name.IsNullable = false;
                sqlParameters.Add(name);

                SqlParameter code = new SqlParameter("@code", SqlDbType.NVarChar);
                code.IsNullable = false;
                code.Value = codeBox.Text.Trim();
                sqlParameters.Add(code);

                SqlParameter parentid = new SqlParameter("@parentid", SqlDbType.UniqueIdentifier);
                parentid.IsNullable = true;
                parentid.Value = dept.ID;
                sqlParameters.Add(parentid);

                if (deptController.AddDepartment(sqlParameters))
                {
                    statusLabel.Text = "Новывй отдел добавлен";
                    ClearEdits();
                    FillGrid();
                }
                else
                {
                    statusLabel.Text = "Отдел добавлен не добавлен";
                }
            }
        }
    }
}