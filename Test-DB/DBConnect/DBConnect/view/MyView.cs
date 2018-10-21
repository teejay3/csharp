using System;
using System.Windows.Forms;

namespace DBConnect.view
{
    public partial class MyView :Form
    {
        public MyView()
        {

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    {
                        Console.WriteLine("up");
                        return true;
                    }
                case Keys.Down:
                    {
                        Console.WriteLine("down");
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public virtual void DeptUpdate()
        {
        }
        public virtual void EmpUpdate()
        {
        }
    }
}
