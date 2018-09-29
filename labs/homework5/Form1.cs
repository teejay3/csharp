using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace homework5
{
    public partial class Form1 : Form
    {
        List<Emp> lst;
        BindingSource bs = new BindingSource();
        public Form1()
        {

            var book = new
            {
                title = "Programming guide",
                author = "Author",
                year = 2010
            };
            

            InitializeComponent();
            Stream sr = new FileStream("data.xml", FileMode.Open);
            XmlSerializer xmlser = new XmlSerializer(typeof(List<Emp>));
            lst = (List<Emp>)xmlser.Deserialize(sr);
            foreach (var i in lst)
            {
                i.pic = @".\" + i.employee_id + ".jpg";
            }
            sr.Close();
            textBox1.Text = book.author + ' ' + book.title;
            bs.DataSource = lst;
            dataGridView1.DataSource = bs;
            bindingNavigator1.BindingSource = bs;
            propertyGrid1.DataBindings.Add("SelectedObject", bs, "");
            pictureBox1.DataBindings.Add("ImageLocation", bs, "pic", true);
            var top_dep = lst.GroupBy(w => w.department_name).Select(g => new { Department = g.Key, Average = g.Average(w => w.salary) });
            chart1.DataSource = top_dep;
            chart1.Series[0].XValueMember = "Department";
            chart1.Series[0].YValueMembers = "Average";
            bs.CurrentChanged += (o, e) => chart1.DataBind();
            comboBox1.Items.Add("last_name");
            comboBox1.SelectedItem = "last_name";
            comboBox1.Items.Add("first_name");
            comboBox1.Items.Add("hire_date");
            comboBox1.Items.Add("salary");
            comboBox1.Items.Add("manager");
            comboBox1.Items.Add("department_name");
            comboBox1.Items.Add("job_title");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream sr = null;
            try
            {
                sr = new FileStream("data.xml", FileMode.Create);
                XmlSerializer xmlser = new XmlSerializer(typeof(List<Emp>));
                xmlser.Serialize(sr, lst);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия файла " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sr.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.DataSource = lst;
            string str = textBox1.Text;
            try
            {
                switch (comboBox1.SelectedItem.ToString())
                {
                    case ("last_name"):
                        {
                            bs.DataSource = (bs.DataSource as List<Emp>).Where(w => w.last_name == str);
                            break;
                        }
                    case ("first_name"):
                        {
                            bs.DataSource = (bs.DataSource as List<Emp>).Where(w => w.first_name == str);
                            break;
                        }
                    case ("hire_date"):
                        {
                            bs.DataSource = (bs.DataSource as List<Emp>).Where(w => w.hire_date == str);
                            break;
                        }
                    case ("salary"):
                        {
                            bs.DataSource = (bs.DataSource as List<Emp>).Where(w => w.salary == int.Parse(str));
                            break;
                        }
                    case ("manager"):
                        {
                            bs.DataSource = (bs.DataSource as List<Emp>).Where(w => w.manager == str);
                            break;
                        }
                    case ("department_name"):
                        {
                            bs.DataSource = (bs.DataSource as List<Emp>).Where(w => w.department_name == str);
                            break;
                        }
                    case ("job_title"):
                        {
                            bs.DataSource = (bs.DataSource as List<Emp>).Where(w => w.job_title == str);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                textBox1.Text = "no data found ";
                bs.DataSource = lst;
            }
        }
    }
    [Serializable]
    public class Emp
    {
        [Browsable(false)]
        public int employee_id { get; set; }
        [DisplayName("Фамилия"), Category("Personal")]
        public string last_name { get; set; }
        [DisplayName("Имя"), Category("Personal")]
        public string first_name { get; set; }
        public string hire_date { get; set; }
        public int salary { get; set; }
        public string manager { get; set; }
        public string department_name { get; set; }
        public string job_title { get; set; }
        public string pic { get; set; }
        public Emp() { }
        public Emp(string str)
        {
            string[] tmp = str.Split('\t');
            employee_id = int.Parse(tmp[0]);
            last_name = tmp[1];
            first_name = tmp[2];
            hire_date = tmp[3];
            salary = int.Parse(tmp[4]);
            manager = tmp[5];
            department_name = tmp[6];
            job_title = tmp[7];
        }
        public bool Find(string str)
        {

            return false;
        }
    }
}
