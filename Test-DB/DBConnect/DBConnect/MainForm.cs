using System.Windows.Forms;
using DBConnect.controller;
using DBConnect.view;

namespace DBConnect
{
    public partial class MainForm : Form
    {
        private MainFrameController baseController;
        public MainForm()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            this.Text = "Kosta DB";
            stuffEditButton.Enabled = false;
            departmentButton.Enabled = false;
            structureButton.Enabled = false;

            baseController = new MainFrameController();
            settingsButton.Click    += (o, e) => { baseController.GetSettings(new Settings());};
            testButton.Click        += (o, e) => 
            {
                using (DBUtil util = new DBUtil())
                {
                    if (util.IsServerConnected())
                    {
                        stuffEditButton.Enabled = true;
                        departmentButton.Enabled = true;
                        structureButton.Enabled = true;
                        MessageBox.Show("Соединение установлено.",
                                "Проверка соединения",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        stuffEditButton.Enabled = false;
                        departmentButton.Enabled = false;
                        structureButton.Enabled = false;
                        MessageBox.Show("Ошибка соединения.",
                                "Проверка соединения",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    }
                }
            };
            structureButton.Click   += (o, e) => { baseController.GetStructure(new StructureView()); };
            stuffEditButton.Click   += (o, e) => { baseController.GetStuff(); };
            departmentButton.Click  += (o, e) => { baseController.GetDepartments(); };
        }
    }
}
