using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework3
{
    public partial class Params : Form
    {
        Color tmpPen = MyDrawing.pointPen.Color;
        Color tmpLine = MyDrawing.linePen.Color;

        public Params()
        {
            InitializeComponent();
            numericUpDown1.Value = (int)MyDrawing.linePen.Width;
            numericUpDown2.Value = MyDrawing.pointSize;
            radioButton1.Checked = true;
            Click += Params_Click;
            //radioButton1.
        }

        private void Params_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.AllowFullOpen = false;
            colorDialog1.ShowHelp = true;
            colorDialog1.Color = tmpPen;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                 tmpPen = colorDialog1.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyDrawing.pointPen.Color = tmpPen;
            MyDrawing.linePen.Color = tmpLine;
            MyDrawing.linePen.Width = (float)numericUpDown1.Value;
            MyDrawing.pointSize = (int)numericUpDown2.Value;
            if (radioButton1.Checked) MyDrawing.motionType = false;
            else MyDrawing.motionType = true;
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog2.AllowFullOpen = false;
            colorDialog2.ShowHelp = true;
            colorDialog2.Color = tmpLine;
            if (colorDialog2.ShowDialog() == DialogResult.OK)
                tmpLine = colorDialog2.Color;
        }

    }
}
