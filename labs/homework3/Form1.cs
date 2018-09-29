using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//using System.Timers;

namespace homework3
{
    public partial class Form1 : Form
    {
        Timer moveTimer = new Timer();
    #region Form1_Paint
    void Draw_points (Graphics g)
        {
            for (int i = 0; i < MyDrawing.points.Count; i++)
                g.FillRectangle(    MyDrawing.pointPen.Brush, 
                                    MyDrawing.points[i].X - (float)MyDrawing.pointSize / 2,
                                    MyDrawing.points[i].Y - (float)MyDrawing.pointSize / 2, 
                                    (float)MyDrawing.pointSize, 
                                    (float)MyDrawing.pointSize
                               );
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g  = e.Graphics;
            Graphics g = CreateGraphics();
            Pen myPen   = new Pen(Color.Red);
            Brush brush = (Brush)Brushes.Black;
            myPen.Width = 1;

            if (MyDrawing.points.Count > 0)
            {
                if (MyDrawing.lineType == MyDrawing.eLineType.Curved)
                {
                    g.DrawClosedCurve(MyDrawing.linePen , MyDrawing.points.ToArray());
                    Draw_points(g);
                }
                else if (MyDrawing.lineType == MyDrawing.eLineType.Filled)
                {
                    g.FillClosedCurve(MyDrawing.linePen.Brush , MyDrawing.points.ToArray());
                    Draw_points(g);
                }
                else if (MyDrawing.lineType == MyDrawing.eLineType.None)
                    Draw_points(g);
                else if (MyDrawing.lineType == MyDrawing.eLineType.Polygone)
                {
                    g.DrawPolygon(MyDrawing.linePen, MyDrawing.points.ToArray());
                    Draw_points(g);
                }
                else if (MyDrawing.lineType == MyDrawing.eLineType.Bezier)
                {
                    if (MyDrawing.points.Count == 4)
                    g.DrawBezier(MyDrawing.linePen, 
                                            (float)MyDrawing.points[0].X, (float)MyDrawing.points[0].Y,
                                            (float)MyDrawing.points[1].X, (float)MyDrawing.points[1].Y,
                                            (float)MyDrawing.points[2].X, (float)MyDrawing.points[2].Y,
                                            (float)MyDrawing.points[3].X, (float)MyDrawing.points[3].Y
                                );
                    Draw_points(g);
                }
            }
            else
            {
                MyDrawing.lineType = MyDrawing.eLineType.None;
            }
        }
        #endregion 
        #region form1
        public Form1()
        {
            InitializeComponent();
            //label2.Hide();
            //label3.Hide();
            //label4.Hide();
            moveTimer.Interval = MyDrawing.Interval;
            moveTimer.Tick  += new EventHandler(TimerTickHandler);
            Paint           += new PaintEventHandler(Form1_Paint);
            MouseMove       += new System.Windows.Forms.MouseEventHandler(Form1_MouseMove);
            //MouseDown       += new System.Windows.Forms.MouseEventHandler(Form1_MouseDown);
            MouseUp         += new System.Windows.Forms.MouseEventHandler(Form1_MouseUp);
            KeyPreview      = true;
            KeyDown         += new KeyEventHandler (Form1_KeyDown);
            if (MyDrawing.input_mode)
            {
                label1.Text = "Ввод точек мышкой";
            }
            else
            {
                label1.Text = "Ввод точек запрещён!";
            }
        }


        #endregion

        private void ParallelMove()
        {
            int tx = MyDrawing.vx;
            int ty = MyDrawing.vy;
            for (int i = 0; i < MyDrawing.points.Count; i++)
            {
                MyDrawing.points[i] = new Point(MyDrawing.points[i].X + MyDrawing.vx, 
                                                MyDrawing.points[i].Y + MyDrawing.vy);

                if (MyDrawing.points[i].X >= this.Size.Width - 20 ||
                    MyDrawing.points[i].X <= 0)
                    tx = -MyDrawing.vx;
                if (MyDrawing.points[i].Y >= this.Size.Height - 40 ||
                    MyDrawing.points[i].Y <= 0)
                    ty = -MyDrawing.vy;
            }

            MyDrawing.vx = tx;
            MyDrawing.vy = ty;
            Refresh();
        }
        private void RandomMove()
        {
            label4.Text = MyDrawing.speed.Count.ToString();
            for (int i = 0; i < MyDrawing.points.Count; i++)
            {
                MyDrawing.points[i] = new Point(MyDrawing.points[i].X + MyDrawing.speed[i].X,
                                                MyDrawing.points[i].Y + MyDrawing.speed[i].Y);

                if (MyDrawing.points[i].X >= this.Size.Width - 20 ||
                    MyDrawing.points[i].X <= 0)
                    MyDrawing.speed[i] = new Point(-MyDrawing.speed[i].X, MyDrawing.speed[i].Y);
                if (MyDrawing.points[i].Y >= this.Size.Height - 40 ||
                    MyDrawing.points[i].Y <= 0)
                    MyDrawing.speed[i] = new Point(MyDrawing.speed[i].X, -MyDrawing.speed[i].Y);
            }
            Refresh();
        }
        private void TimerTickHandler(object sender, EventArgs e)
        {

            if (MyDrawing.isMoving == false) return;
            else
            {
                if (!MyDrawing.motionType) ParallelMove();
                else
                    RandomMove();
               // Refresh();
            }
        }

        private void RandomizeMotion()
        {
            Random rnd = new Random();
            MyDrawing.vx = rnd.Next(-5, 5);
            MyDrawing.vy = rnd.Next(-5, 5);
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            label3.Text     = e.KeyCode.ToString();
            switch (e.KeyCode)
            {
                case (Keys.Escape):
                    {
                        MyDrawing.points.Clear();
                        MyDrawing.speed.Clear();
                        MyDrawing.isMoving = false;
                        Refresh();
                        break;
                    }
                case (Keys.Space):
                    {
                        if (MyDrawing.isMoving == false)
                        {
                            moveTimer.Start();
                            MyDrawing.isMoving = true;
                            RandomizeMotion();
                        }
                        else
                        {
                            moveTimer.Stop();
                            MyDrawing.isMoving = false;
                        }
                        break;
                    }
                case (Keys.Oemplus):
                    {
                        if (MyDrawing.vx > 0) MyDrawing.vx += 1;
                        else if (MyDrawing.vx < 0) MyDrawing.vx -= 1;
                        if (MyDrawing.vy > 0) MyDrawing.vy += 1;
                        else if (MyDrawing.vy < 0) MyDrawing.vy -= 1;
                        break;
                    }
                case (Keys.OemMinus):
                    {
                        if (MyDrawing.vx > 1) MyDrawing.vx -= 1;
                        else if (MyDrawing.vx < -1) MyDrawing.vx += 1;
                        if (MyDrawing.vy > 1) MyDrawing.vy -= 1;
                        else if (MyDrawing.vy < -1) MyDrawing.vy += 1;
                        break;
                    }
            }
            label3.Text = MyDrawing.Interval.ToString();
            e.Handled       = true;
            Refresh();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            label3.Text = keyData.ToString();
            int delta = 5;
            switch (keyData)
            {
                case Keys.Up:
                    {
                        if (!MyDrawing.isMoving)
                        {
                            for (int i = 0; i < MyDrawing.points.Count; i++)
                                MyDrawing.points[i] = new Point(MyDrawing.points[i].X, MyDrawing.points[i].Y - delta);
                        } else
                        MyDrawing.vy -= 1;
                        //break;
                        Refresh();
                        return true;
                    }
                case Keys.Down:
                    {
                        if (!MyDrawing.isMoving)
                        {
                            for (int i = 0; i < MyDrawing.points.Count; i++)
                                MyDrawing.points[i] = new Point(MyDrawing.points[i].X, MyDrawing.points[i].Y + delta);
                        }
                        else
                            MyDrawing.vy += 1;
                        //break;
                        Refresh();
                        return true;
                    }
                case Keys.Left:
                    {
                        if (!MyDrawing.isMoving)
                        {
                            for (int i = 0; i < MyDrawing.points.Count; i++)
                                MyDrawing.points[i] = new Point(MyDrawing.points[i].X - delta, MyDrawing.points[i].Y);
                        }
                        else
                            MyDrawing.vx -= 1;
                        //break;
                        Refresh();
                        return true;
                    }
                case Keys.Right:
                    {
                        if (!MyDrawing.isMoving)
                        {
                            for (int i = 0; i < MyDrawing.points.Count; i++)
                                MyDrawing.points[i] = new Point(MyDrawing.points[i].X + delta, MyDrawing.points[i].Y);
                        }
                        else
                            MyDrawing.vx += 1;
                        //break;
                        Refresh();
                        return true;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!MyDrawing.input_mode)
            {
                MyDrawing.input_mode = true;
                label1.Text = "Ввод точек мышкой";
            }
            else
            {
                MyDrawing.input_mode = false;
                label1.Text = "Ввод точек запрещён!";
            }
            Refresh();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MyDrawing.points.Clear();
            MyDrawing.isMoving = false;
            MyDrawing.speed.Clear();
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyDrawing.lineType = MyDrawing.eLineType.Curved;
            Refresh();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Random rnd = new Random();
            if (MyDrawing.points.Count > 0)
            {
                for (int i = 0; !MyDrawing.bDrag && i < MyDrawing.points.Count; i++)
                {
                    var tx = MyDrawing.points[i].X;
                    var ty = MyDrawing.points[i].Y;
                    if ((tx - MyDrawing.delta <= e.X && e.X <= tx + MyDrawing.delta)
                        && (ty - MyDrawing.delta <= e.Y && e.Y < ty + MyDrawing.delta))
                    {
                        MyDrawing.bDrag = true;
                        MyDrawing.iPointToDrag = i;
                        break;
                    }
                }
            }
            if (!MyDrawing.bDrag && MyDrawing.input_mode)
            {
                MyDrawing.points.Add(new Point(e.X, e.Y));
                RandomizeMotion();
                MyDrawing.speed.Add(new Point(rnd.Next(-5, 5), rnd.Next(-5, 5)));
                label4.Text = e.X + " " + e.Y;
                Refresh();
            }
        }

        private void Form1_MouseMove (object sender, MouseEventArgs e)
        {
            Point pos   = Control.MousePosition;
            label2.Text = pos.ToString();

            if (MyDrawing.bDrag)
            {
                MyDrawing.points[MyDrawing.iPointToDrag] = new Point(e.X, e.Y);
                Refresh();
            }
        }

        private void Form1_MouseUp (object sender, MouseEventArgs e)
        {
            if (MyDrawing.bDrag)
            {
                MyDrawing.points[MyDrawing.iPointToDrag] = new Point(e.X, e.Y);
                MyDrawing.bDrag = false;
                Refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MyDrawing.lineType = MyDrawing.eLineType.Filled;
            Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MyDrawing.lineType = MyDrawing.eLineType.Polygone;
            Refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MyDrawing.lineType = MyDrawing.eLineType.Bezier;
            Refresh();
        }

        Params paramWin = new Params();
        private void button2_Click(object sender, EventArgs e)
        {
            paramWin.Show();
            //paramWin.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MyDrawing.isMoving == false)
            {
                moveTimer.Start();
                MyDrawing.isMoving = true;
                RandomizeMotion();
            }
            else
            {
                MyDrawing.isMoving = false;
                moveTimer.Stop();
            }
        }
    }

    public static class MyDrawing
    {
        public enum eLineType { None, Curved, Filled, Polygone, Bezier };
        public static Pen pointPen          = new Pen(Color.Black);
        public static Pen linePen           = new Pen(Color.Red, 2);
        public static int pointSize         = 4;
        public static eLineType lineType    = eLineType.None;
        public static List<Point> points    = new List<Point>();
        public static List<Point> speed     = new List<Point>();
        public static int vx                = 0;        //"скорость" по оси Х
        public static int vy                = 0;        //"скорость" по оси Y
        public static int Interval          = 30;
        public static bool isMoving         = false;    //false - не двигается
        public static bool bDrag            = false;         //режим перемещения точки
        public static bool input_mode       = false;    //перемещение точек: false (по умолчанию) перемещение точек, true - ввод точек
        public static int delta             = 10;             //радиус для определения попадания мышки в точку
        public static int iPointToDrag      = 0;       //номер элемента массива с точкой, которыю мы хотим перемещать мышкой
        public static bool motionType       = false;    //false - зеркальное перемещение
    }

    public class MyPoints
    {
        //data
    }
}
