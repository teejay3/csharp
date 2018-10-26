using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlatUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

            SidePanel.Height = button1.Height;
            firstControl1.BringToFront();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button2.Top;
            secondControl1.BringToFront();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SidePanel.Top = button1.Top;
            firstControl1.BringToFront();
        }
    }
}
