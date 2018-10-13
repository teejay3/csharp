using System.Windows.Forms;
using DBConnect.controller;

namespace DBConnect
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            this.Text = "Kosta DB";
            MainFrameController baseController = new MainFrameController();
            settingsButton.Click    += (o, e) => { baseController.GetSettings(); };
            testButton.Click        += (o, e) => { baseController.TestConnection(); };
            structureButton.Click   += (o, e) => { baseController.GetStructure(); };
            stuffEditButton.Click   += (o, e) => { baseController.GetStuff(); };
            departmentButton.Click  += (o, e) => { baseController.GetDepartments(); };
        }
    }
}
