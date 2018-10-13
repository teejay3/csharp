using DBConnect.controller;
using DBConnect.model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Windows.Forms;

namespace DBConnect.view
{
    public partial class DepartmentsView : Form
    {
        private List<Department> dept;
        private DepartmentController deptController;
        private const string getDepartments = "select * from department";
        public DepartmentsView()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            deptController = new DepartmentController(this);
            dept = new List<Department>();
//            dept = UpdateDepartments(); //  проверить массив
            refreshButton.Click += (o, e) => { ClearEdits(); };
            updateButton.Click += (o, e) => { };
            addButton.Click += (o, e) => { };
            deleteButton.Click += (o, e) => { };
        }

        private void ClearEdits()
        {
            statusLabel.Text = "";
            codeBox.Text = "";
            nameBox.Text = "";
        }

        private List<Department> UpdateDepartments()
        {
            List<Department> result = new List<Department>();
            DBUtil util = new DBUtil();
            try
            {
                DbDataReader reader = util.GetDbDataReader(getDepartments);
                if (reader == null) { statusLabel.Text = "Ошибка!"; return result; }
                while (reader.Read())
                {
                    result.Add(new Department((Guid)reader["ID"],
                                            (string)reader["Name"],
                                            (string)(!DBNull.Value.Equals(reader["Code"]) ? reader["Code"] : ""),
                                            (Guid)(!DBNull.Value.Equals(reader["ParentDepartmentID"]) ? reader["ParentDepartmentID"] : "")
                        ));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                util.CloseConnection();
            }
            RefreshGrid();
            return result;
        }

        private void RefreshGrid()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = dept;
            dataGridView1.DataSource = bs;
        }
    }
}
