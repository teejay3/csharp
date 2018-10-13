using DBConnect.model;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;
using DBConnect.controller;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System;

namespace DBConnect.view
{
    public partial class StructureView : Form
    {
        const string sqlRequest = "WITH DirectReports(ParentDepartmentID, ID, Name, EmployeeLevel) AS"
                            + " ( SELECT ParentDepartmentID"
                                + " , ID, CONVERT(varchar(255), Name)"
                                + " , 0 AS EmployeeLevel FROM Department"
                            + " WHERE ParentDepartmentID IS NULL"
                            + " UNION ALL SELECT"
                            + " e.ParentDepartmentID, e.ID"
                            + " , CONVERT(varchar(255), REPLICATE('|    ', EmployeeLevel + 1) + e.Name)"
                            + " , EmployeeLevel + 1 FROM Department AS e"
                            + " INNER JOIN DirectReports AS d ON e.ParentDepartmentID = d.ID)"
                            + " SELECT Name FROM DirectReports";
        private StringBuilder structure;
        Thread thr;
        public StructureView()
        {
            InitializeComponent();
            structure = new StringBuilder();

            this.Shown += (o, e) => { FillStructure(); };

            this.SizeChanged += (o, e) => { UpdateTextBox(); };
            refreshButton.Click += (o, e) => { FillStructure(); };
        }


        private void Threading(Thread thr)
        {
            if (thr.ThreadState == ThreadState.Unstarted) { thr.Start(); return; }
            if (thr.IsAlive || thr.ThreadState == ThreadState.Running || thr.ThreadState == ThreadState.WaitSleepJoin)
            {
                Console.WriteLine("running");
                return;
            }
            if (thr.ThreadState == ThreadState.Stopped)
            {
                Console.WriteLine("restarting");
                thr = new Thread(FillStructure);
                thr.Start();
                return;
            }
        }

        private void FillStructure()
        {
            structure.Clear();
            lock (structure)
            {
                {
                    using (DBUtil util = new DBUtil())
                    {
                        DbDataReader reader = util.GetDbDataReader(sqlRequest);
                        while (reader.Read())
                        {
                            structure.AppendLine((string)reader["Name"] + '\n');
                        }
                        //Thread.Sleep(5000);
                    }

                }
            }
            UpdateTextBox();
        }

        private void UpdateTextBox()
        {
            try
            {
                BeginInvoke(new Action(() => textBox1.Text = structure.ToString()));
            }
            catch { }
        }
    }
}
