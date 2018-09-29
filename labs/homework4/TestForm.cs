using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace homework4
{
    
    public partial class TestForm : Form
    {
        public Random rnd2 = new Random();
        Testing test = new Testing();
        FlowLayoutPanel pnl = new FlowLayoutPanel();
        
        delegate string Ru_word(int r);
        delegate string[] Wrong_words(int r);
        int timeleft;
        public TestForm()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000;
            FormClosing += (o, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                    e.Cancel = true;
                Hide();
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {

            RadioButton rbSelected = pnl.Controls
                        .OfType<RadioButton>()
                        .FirstOrDefault(r => r.Checked);
            if (!test.under_testing)
            {
                //Random rnd = new Random();
                try
                { test.FillArr(rnd2); }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                    return;
                }
                button1.Text = "  >  ";
                timeleft = test.counter;
                test.current_step = 0;
                test.under_testing = true;
                timer1.Start();
                label3.Text = test.current_step + 1 + "/" + test.n_of_steps;
                FillGroupBox(test.current_step, test.GetTestWord(test.current_step));
                test.current_step += 1;
            }
            else if (test.current_step < test.n_of_steps)
            {
                test.GetTestWord(test.current_step - 1).user_select = 
                    (rbSelected != null) ? rbSelected.Text.Trim() : "";
                label3.Text = test.current_step + 1 + "/" + test.n_of_steps;
                FillGroupBox(test.current_step, test.GetTestWord(test.current_step));
                test.current_step += 1;
            }
            else
            {
                test.GetTestWord(test.current_step - 1).user_select =
                    (rbSelected != null) ? rbSelected.Text.Trim() : "";
                StopTesting();
                label1.Text = "Поздравляем, вы закончили досрочно.";
                ShowResults();
            }
        }

        void ShowResults()
        {
            int res = test.test_arr.Count(s => s.rus_word == s.user_select);
            const string cap = "Результаты тестирования";
            string msg = "Ваша результат " + res + " из " + test.n_of_steps + '\n';
            string stat = "";
            for (int i = 0; i < test.n_of_steps; i++)
            {
                if (test.GetTestWord(i).rus_word != test.GetTestWord(i).user_select)
                { stat += i + 1 + " анг. слово: " + test.GetTestWord(i).eng_word + ", ваш ответ " + test.GetTestWord(i).user_select + " , правильный ответ " + test.GetTestWord(i).rus_word + '\n'; }
                else
                stat += i + 1 + " анг. слово: " + test.GetTestWord(i).eng_word + ", ваш ответ " + test.GetTestWord(i).user_select + '\n';
            }
            var results = MessageBox.Show(
                msg + stat
                , cap);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeleft--;
            if (timeleft == 0)
            {
                StopTesting();
                label1.Text = "Ваше время вышло!";
                ShowResults();
            }
            else
            {
                label1.Text = "Оставшееся время " + timeleft.ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            StopTesting();
            Hide();
        }

        void StopTesting ()
        {
            timer1.Stop();
            test.under_testing = false;
            timeleft = test.counter;
            test.current_step = 0;
            label2.Text = "";
            label3.Text = "-/" + test.n_of_steps;
            label1.Text = "Тестирование не запущено.";
            pnl.Controls.Clear();
            button1.Text = "Начать";
        }
        void FillGroupBox(int step, TestWord word) //на каждом этапе заполнить динамически radioButton's
        {
            
            pnl.Dock = DockStyle.Left;
            pnl.Controls.Clear();
            word.wrong_word[word.wrong_word.Length - 1] = word.rus_word;
            Random r = new Random();
            string[] random_wrong_words = word.wrong_word.OrderBy(x => rnd2.Next()).ToArray();
            for (int i = 0; i < random_wrong_words.Length; i++)
            {
                pnl.Controls.Add(new RadioButton() { Text = random_wrong_words[i] });
            }
            label2.Text = word.eng_word + " (" + word.word_type + ")";
            this.groupBox1.Controls.Add(pnl);
            //this.label1.Text = "Оставшееся время " + test.counter.ToString();
        }

        class TestWord
        {
            public string eng_word;
            public string user_select = ""; //выбор пользователя
            public Word.WordType word_type;
            public string rus_word; //правильный ответ
            public string[] wrong_word; //массив неверных слов

            public TestWord(string eng_word
                            , Word.WordType type
                            , string ru_word
                            , string[] str
                            )
            {
                this.eng_word = eng_word;
                this.word_type = type;
                this.rus_word = ru_word;
                this.wrong_word = str;
            }
        }

        class Testing
        {
            public const int arr_size = 4; //количество вариантов ответа
            public bool under_testing = false;  //в процессе тестирования
            public int current_step = 0;
            public int n_of_steps = 5; //количество вопросов
            public int counter = 120;    //время на тест, секунд
            public double[] grade_levels = { 0.5, 0.75, 0.9 };  //градации оценок
            public List<TestWord> test_arr = new List<TestWord>();    //массив со словами и ответами
            public void FillArr(Random rnd)  //заполнить тестовый массив
            {
                
                if (Dictionary.myDic.Count == 0)
                    return;
                
                Ru_word ru;
                ru = (int r) =>
                {
                   int n =  rnd.Next(Dictionary.myDic[r].rus_words.Split(';').Length);
                    return Dictionary.myDic[r].rus_words.Split(';')[n].Trim();
                };

                Wrong_words ww;
                ww = (int r) =>
                {
                    const int arr_size = 4;
                    int[] wrong_ids = new int[arr_size];
                    string[] words = new string[arr_size];
                    for (int i = 0; i < arr_size - 1;)  //переделать на словарь!
                    {
                        int ran = 0;
                        do
                        {
                            ran = rnd.Next(Dictionary.myDic.Count - 1);
                            if (!wrong_ids.Contains(Dictionary.myDic[ran].ID))
                            {
                                words[i] = Dictionary.myDic[ran].rus_words.Split(';')[rnd.Next(Dictionary.myDic[ran].rus_words.Split(';').Length)];
                                wrong_ids[i] = Dictionary.myDic[ran].ID;
                            }
                            else continue;
                        }
                        while (ran == r);
                        i++;
                    }
                    return words;
                };

                for (int i = 0; i < n_of_steps; i++)
                {
                    int next_word = rnd.Next(Dictionary.myDic.Count - 1);
                    test_arr.Add(new TestWord (
                                    Dictionary.myDic[next_word].eng_word
                                    , Dictionary.myDic[next_word].type
                                    , ru(next_word)
                                    , ww(next_word))
                                    );
                    
                }
            }

            public TestWord GetTestWord (int n)
            {
                return test_arr[n];
            }
        }
    }
}
