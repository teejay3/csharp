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
        private MyController deptController;
        //private MyController stuffController;
        public MainFrameController()
        {
            Init();
        }
        private void ShowFrame(Form form)
        {
            try
            {
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

            deptController = new DepartmentController(departmentsView);
            deptController.AttachView(departmentsView);
            deptController.AttachView(stuffView);
            //stuffController = new StuffController();
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
