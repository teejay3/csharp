using DBConnect.view;
using System;
using System.Threading;
using System.Windows.Forms;

namespace DBConnect.controller
{
    class MainFrameController
    {
        private StuffView stuffView;
        private DepartmentsView departmentsView;
        public MainFrameController()
        {
            Init();
        }
        private void ShowFrame(Form form)
        {
            try
            {
                System.Type type = form.GetType();
                form = (Form)System.Activator.CreateInstance(type);
                form.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void Init()
        {
            departmentsView = new DepartmentsView();
            stuffView = new StuffView();
        }
        public void GetSettings(Settings settings)
        {
            ShowFrame(settings);
        }
        public void TestConnection()
        {
            using (DBUtil util = new DBUtil())
            {
                new Thread(()=>util.TestConnection()).Start();
            }
        }
        public void GetStructure(StructureView structure)
        {
            ShowFrame(structure);
        }
        public void GetStuff()
        {
                ShowFrame(stuffView);
        }
        public void GetDepartments()
        {
            ShowFrame(departmentsView);
        }
    }
}
