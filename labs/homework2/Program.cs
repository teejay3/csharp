using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace homework2
{
    class Matrix
    {
        private double[,] data;
        private int x;
        private int y;

        public Matrix(int nRows, int nCols)
        {
            x = nRows;
            y = nCols;
            data = new double[x,y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    data[i,j] = 0;
                }
            }
        }

        public Matrix(double[,] initData)
        {
            this.x = initData.Rank;
            this.y = initData.Length / initData.Rank;
            data = new double[this.x, this.y];
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    this.data[i, j] = initData[i, j];
                }

            }
        }

        public double GetValue(int x, int y) => this.data[x,y];

        public void SetValue(int x, int y, double d) => this.data[x,y] = d;

        public void Print()
        {
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    //Console.Write(this.data[i,j].ToString("#.00") + ' ');
                    Console.Write(this.data[i, j].ToString() + "  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void GetRandom(Random rnd)
        {
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    data[i,j] = rnd.Next(0, 10);
                }
            }
        }

        public double this[int x, int y]
        {
            get
            {
                return this.data[x, y];
            }
            set
            {
                this.data[x,y] = value;
            }

        }

        public int Rows
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        public int Columns
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public int? Size
        {
            get
            {
                if (this.x == this.y) return x;
                else return null;
            }
        }
         
        public bool IsSquared
        {
            get
            {
                if (this.x == this.y) return true;
                else return false;
            }
        }

        public bool IsEmpty
        {
            get
            {
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < y; j++)
                    {
                        if (this.data[i,j] != 0) return false;
                    }
                return true;
            }
        }

        public bool IsUnity
        {
            get
            {
                if (this.IsSquared == false || 
                    this.IsEmpty == true || 
                    this.IsDiagonal == false) return false;
                for (int i = 0; i < x; i++)
                {
                    if (this.data[i,i] != 1) return false;
                }
                return true;
            }
        }

        public bool IsDiagonal
        {
            get
            {
                if (this.IsSquared == false || this.IsEmpty == true) return false;
                for (int i = 0; i < x; i++)
                {
                    if (this.data[i,i] == 0) return false;
                }
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < y; j++)
                    {
                        if (i == j) continue;
                        if (this.data[i,j] != 0) return false;
                    }
                return true;
            }
        }

        public bool IsSymmetric
        {
            get
            {
                if (this.IsSquared == false || this.IsEmpty == true) return false;
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < y; j++)
                    {
                        if (i == j) continue;
                        if (this.data[i,j] != this.data[j,i]) return false;
                    }
                return true;
            }
        }

        public static Matrix operator+(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                Console.WriteLine("Разные размерности матриц!");
                return null;
            }
            Matrix result = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j]; 
                }
            return result;
        }

        public static Matrix operator-(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                Console.WriteLine("Разные размерности матриц!");
                return null;
            }
            Matrix result = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Columns; i++)
                for (int j = 0; j < m1.Rows; j++)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            return result;
        }

        public static Matrix operator *(Matrix m1, double d)
        {
            Matrix result = new Matrix(m1.Rows, m1.Columns);
            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m1.Columns; j++)
                {
                    result[i, j] = m1[i, j] * d;
                }
            return result;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows) return null;
            Matrix result = new Matrix(m1.Rows, m2.Columns);

            for (int i = 0; i < m1.Rows; i++)
                for (int j = 0; j < m2.Columns; j++)
                {
                        for (int k = 0; k < m1.Columns; k++)
                        {
                            result[i, j] += m1[i, k] * m2[k, j];
                        }
                }
            return result;
        }

        public static explicit operator Matrix(double[,] arr)
        {
            Matrix tmp = new Matrix(arr.GetLength(0), arr.Length / arr.GetLength(0));
            for (int i = 0; i < tmp.Rows; i++)
            {
                for (int j = 0; j < tmp.Columns; j++)
                {
                    tmp[i,j] = arr[i,j];
                }
            }
            return tmp;
        }

        public Matrix Transpose()
        {
            Matrix result = new Matrix(this.y, this.x);
            for (int i = 0; i < this.x; i++)
                for (int j = 0; j < this.y; j++)
                {
                    result[j, i] = this.data[i,j];
                }
            return result;
        }

        public double Trace()
        {
            if (IsSquared == false) return 0;
            double res = 0;
            for (int i = 0; i < this.x; i++)
            {
                res += this.data[i,i];
            }
            return res;
        }

        public override string ToString()
        {
            string str = null;
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    str += this.data[i,j].ToString() + ' ';
                }
                if (i != (this.x - 1)) str += ", ";
                else str += '\n';
            }
            return str;
        }

        public static Matrix GetUnity(int Size)
        {
            Matrix result = new Matrix(Size, Size);
            for (int i = 0; i < result.Size; i++)
                for (int j = 0; j < result.Size; j++)
                {
                    if (i == j) result.SetValue(i, j, 1);
                    else result.SetValue(i, j, 0);
                }
            return result;
        }

        public static Matrix GetEmpty(int Size)
        {
            Matrix result = new Matrix(Size, Size);
            for (int i = 0; i < result.Size; i++)
                for (int j = 0; j < result.Size; j++)
                {
                    result.SetValue(i, j, 0);
                }
            return result;
        }

        public static Matrix Parse(string str)
        {
//            try
//            {
                string[] rows = str.Split(',');
            
                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i] = rows[i].Trim();
                };

                string[] cols = rows[0].Split(' ');
                if (cols.Length == 0) throw new FormatException("error split 0 rows in ");

                Matrix result = new Matrix(rows.Length, cols.Length);
                for (int i = 0; i < result.Rows; i++)
                {
                    cols = rows[i].Split(' ');

                    if (cols.Length != result.Columns)
                    {
                        throw new FormatException("error split rows "
                              + cols.Length + "!="
                              + result.Columns
                              + " in ");
                    }

                    for (int k = 0; k < result.Columns; k++)
                    {
                        result[i, k] = double.Parse(cols[k].Trim());
                    }
                }
                return result;
//            }
//            catch (FormatException ex)
//            {
//                Console.WriteLine(ex.Message + ex.Source);
//                return null;
//            }
                
        }

        public static bool TryParse(string str, out Matrix m)
        {
            string[] rows = str.Split(',');

            for (int i = 0; i < rows.Length; i++)
                rows[i] = rows[i].Trim();

            string[] cols = rows[0].Split(' ');

            Matrix result = new Matrix(rows.Length, cols.Length);
            for (int i = 0; i < result.Rows; i++)
            {
                cols = rows[i].Split(' ');
                for (int k = 0; k < result.Columns; k++)
                {
                    double d = 0;
                    if (double.TryParse(cols[k].Trim(), out d))
                    {
                        result[i, k] = d;
                    }
                    else
                    {
                        m = null;
                        return false;
                    }
                }
            }

            if (result != null) { m = result; return true; }
            else {m = null; return false; }
        }

    }

    class Program
    {
        static void Calculate(Dictionary<string, Matrix> mDic)
        {
            Console.WriteLine("Введите строку, например: m1 = m2 * d");
            string str = Console.ReadLine();
            str = str.Trim();
            string pattern_names = @"[^=\-+* ]+";
            string operations = @"[=\-+*]{1}";

            Regex def_reg = new Regex(pattern_names);
            MatchCollection names = def_reg.Matches(str);
//            for (int ctr = 0; ctr < names.Count; ctr++)
//                Console.WriteLine("{0}. {1}", ctr, names[ctr].Value);

            def_reg = new Regex(operations);
            MatchCollection oper = def_reg.Matches(str);
//            for (int ctr = 0; ctr < oper.Count; ctr++)
//                Console.WriteLine("{0}. {1}", ctr, oper[ctr].Value);

            Matrix tmp = null;
            double d = 0;

//            if (mDic.ContainsKey(names[0].Value.Trim()))
//            {
//                Console.WriteLine("Имя '{0}' уже занято!", names[0].Value.Trim() );
//                return;
//            } else
            if(!mDic.ContainsKey(names[1].Value.Trim()))
            {
                Console.WriteLine("Имя '{0}' не найдено!", names[1].Value.Trim());
                return;
            }
            else
            if (oper[1].Value.Trim() == "*" && double.TryParse(names[2].Value.Trim(), out d))
            {
                tmp = mDic[names[1].Value.Trim()] * d;
            }
            else
            if (!mDic.ContainsKey(names[2].Value.Trim()))
            {
                Console.WriteLine("Имя '{0}' не найдено!", names[2].Value.Trim());
                return;
            }
            else
            {
                switch (oper[1].Value.Trim())
                {
                    case "+":
                        {
                            tmp = mDic[names[1].Value.Trim()] +
                                                            mDic[names[2].Value.Trim()];
                            break;
                        }
                    case "-":
                        {
                             tmp = mDic[names[1].Value.Trim()] -
                                                            mDic[names[2].Value.Trim()];
                            break;
                        }
                    case "*":
                        {
                            tmp = mDic[names[1].Value.Trim()] *
                                                            mDic[names[2].Value.Trim()];
                            
                            break;
                        }
                }
            }
            if (tmp != null)
            {
                mDic[names[0].Value.Trim()] = tmp;
                Console.WriteLine("Вычисление матрицы завершено..");
                mDic[names[0].Value.Trim()].Print();
            }
            else
            {
                Console.WriteLine("Ошибка вычислений");
                return;
            }

        }

        static void PrintDic(Dictionary<string, Matrix> mDic)
        {
            if (mDic.Count == 0)
            {
                Console.WriteLine("Матриц в памяти нет.");
            }
            else
            {
                Console.WriteLine("Введённые матрицы:");
                foreach (var d in mDic)
                {
                    Console.WriteLine(d.Key);
                    d.Value.Print();
                    Console.WriteLine();
                }
            }
        }

        static void CreateMatrix(Dictionary<string, Matrix> mDic)
        {
            Console.WriteLine("Введите название матрицы и её элементы в строку\n" +
                "например: m1 = 1 1 1, 2 2 2 и нажмите клавишу ввод");
            string str = Console.ReadLine();
            str = str.Trim();
            string name = "";
            string data = "";
            string pattern_name = @"^[a-zA-Z0-9]+";
            string pattern_data = @"([0-9\- ]+,)+([0-9\- ]+){1}$";

            Regex def_reg = new Regex(pattern_name);
            MatchCollection match = def_reg.Matches(str);

//            for (int ctr = 0; ctr < match.Count; ctr++)
//                    Console.WriteLine("{0}. {1}", ctr, match[ctr].Value);
            if (match.Count == 0)
            {
                Console.WriteLine("Ошибка: имя не обнаружено, выхожу");
                return;
            }
            else
            {
                name = match[0].Value;
                name = name.Trim();
            }

            def_reg = new Regex(pattern_data);
            match = def_reg.Matches(str);

//            for (int ctr = 0; ctr < match.Count; ctr++)
//                Console.WriteLine("{0}. {1}", ctr, match[ctr].Value);

            if (match.Count == 0)
            {
                Console.WriteLine("Ошибка: данные не найдены, выхожу");
                return;
            }
            else
            {
                data = match[0].Value;
                data = data.Trim();
            }

            if (mDic.ContainsKey(name))
            {
                Console.WriteLine("Ошибка: имя уже занято '{0}', выхожу", name);
                return;
            }
            else
            {
                
                if (Matrix.TryParse(data, out Matrix tmp) != false)
                {
                    mDic[name] = tmp;
                    Console.WriteLine("Матрица "
                            + name
                            + " создана ("
                            + mDic[name].Rows
                            + 'x'
                            + mDic[name].Columns + ')');
                }
                else
                {
                    Console.WriteLine("Ошибка создания матрицы!");
                    return;
                }
            }
        }

        static void MatrixProp (Dictionary<string, Matrix> mDic)
        {
            if (mDic.Count == 0) { Console.WriteLine("Нет доступных матриц"); return; }
            Console.Write("Доступные матрицы: ");
            foreach (var d in mDic)
            {
                Console.Write(d.Key + " ");
            }
            Console.WriteLine();
            string name = Console.ReadLine();
            Console.WriteLine("Квадратная " + mDic[name.Trim()].IsSquared + "\n" +
                "нулевая " + mDic[name.Trim()].IsEmpty + "\n" +
                "единичная " + mDic[name.Trim()].IsUnity + "\n" +
                "диагональная " + mDic[name.Trim()].IsDiagonal + "\n" +
                "симметричная " + mDic[name.Trim()].IsSymmetric + "\n" +
                "след " + mDic[name.Trim()].Trace());
            mDic[name.Trim()].Transpose().Print();
        }
        static void MainMenu(Dictionary<string, Matrix> mDic)
        {

            while (true)
            {
                Console.WriteLine("Работа с матрицами\n" +
                    "______________________________\n" +
                    "   1 - Ввод матрицы\n" +
                    "   2 - Операции\n" +
                    "   3 - Свойства матрицы\n" +
                    "   4 - Печать введённых матриц\n" +
                    "   0 - Выход\n" +
                    "______________________________\n");
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '0': return;
                    case '1': { CreateMatrix(mDic); break; }
                    case '2': { Calculate(mDic);  break; }
                    case '3': { MatrixProp(mDic); break; }
                    case '4': { PrintDic(mDic); break; }
                    default: { Console.WriteLine("Неверный выбор! Повторите попытку.\n"); break; }
                }
            }
        }

        static void Main(string[] args)
        {
            Dictionary<string, Matrix> mDic = new Dictionary<string, Matrix>();
            MainMenu(mDic);
            /*            while (true)
                        {
                            Console.WriteLine("введите стороку");
                            string str = Console.ReadLine();
                            Matrix tmp = Matrix.Parse(str);
                            if (tmp != null) tmp.Print();
                            else Console.WriteLine("null matrix");
                        }
            */
        }
    }
}
