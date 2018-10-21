using System;
using System.Windows.Forms;
using System.Configuration;

namespace DBConnect
{
    public partial class Settings : Form
    {
        private string connString;
        private void fillFields()
        {
            textLogin.Text = ConfigurationManager.AppSettings["User name"];
            textSrvName.Text = ConfigurationManager.AppSettings["Data source"];
            textPort.Text = ConfigurationManager.AppSettings["Initial catalog"];
            textPasswd.Text = ConfigurationManager.AppSettings["Password"];
        }
        public Settings()
        {
            InitializeComponent();
            fillFields();
            connString =
                        @"Data Source=" + textSrvName.Text +
                        ";Initial Catalog=" + textPort.Text +
                        ";Persist Security Info=True;User ID=" + textLogin.Text +
                        ";Password=" + textPasswd.Text;
        }

        public string getString()
        {
            return connString;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Hide();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Add("ModificationDate", DateTime.Now.ToLongTimeString() + " ");
            config.AppSettings.Settings["User name"].Value = textLogin.Text.Trim();
            config.AppSettings.Settings["Initial catalog"].Value = textPort.Text.Trim();
            config.AppSettings.Settings["Password"].Value = textPasswd.Text.Trim();
            config.AppSettings.Settings["Data source"].Value = textSrvName.Text.Trim();

            try
            {
                Console.WriteLine(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                config.Save(ConfigurationSaveMode.Modified, true);
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка кофигурационного файла: " + ex.Message + ' ' + ex.TargetSite,
                                      "Ошибка",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                
            }
            ConfigurationManager.RefreshSection("appSettings");

        }
    }
}
