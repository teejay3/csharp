using DBConnect.view;
using System.Threading;
using System.Windows.Forms;

namespace DBConnect.controller
{
    class MainFrameController
    {
        private Settings settings;
        private StructureView structure;
        private StuffView stuff;
        private DepartmentsView dept;

        public MainFrameController()
        {
            Init();
        }
        private void ShowFrame(Form form)
        {
            if (!form.IsDisposed)
            {
                form.Show();
            }
            else
            {
                System.Type type = form.GetType();
                form = (Form)System.Activator.CreateInstance(type);
                form.Show();
            }
            
        }
        private void Init()
        {
            settings = new Settings();
            structure = new StructureView();
            stuff = new StuffView();
            dept = new DepartmentsView();
        }
        public void GetSettings()
        {
            ShowFrame(settings);
        }
        public void TestConnection()
        {
            using (DBUtil util = new DBUtil())
            {
                util.TestConnection();
            }
        }
        public void GetStructure()
        {
            ShowFrame(structure);
        }
        public void GetStuff()
        {
            ShowFrame(stuff);
        }
        public void GetDepartments()
        {
            ShowFrame(dept);
        }
    }
}
