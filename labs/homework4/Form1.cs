using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace homework4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            String ver = "0.6";
            Text = "Англо-русский словарь " + ver;
            KeyPreview = true;
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            
            //add_card.Load += Add_card_Load;
            //add_card.LostFocus += Add_card_LostFocus; 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Delete): { DeleteWord(); UpdateStats(); break; }
                case (Keys.Insert): { button11_Click(sender, e); break; }
            }
            e.Handled = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Object s = new object();
            EventArgs e = new EventArgs();
            switch (keyData)
                {
                case Keys.Left:
                    {
                        button8_Click(s, e);
                        return true;
                    }
                case Keys.Right:
                    {

                        button7_Click(s, e);
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Add_card_LostFocus(object sender, EventArgs e)
        {
            UpdateStats();
        }

        private void Add_card_Load(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            const string msg = "Сохранить словарь перед выходом?";
            const string cap = "Сохранение";
            if (!Dictionary.isSynced)
            {
                Dictionary.isSynced = true;
                var result = MessageBox.Show(msg, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StandardToXml(Dictionary.myDic, Dictionary.dicPath);
                    Application.Exit();
                }
                else Application.Exit();
            }
           else
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Dictionary.current_position < Dictionary.myDic.Count - 1)
            {
                Dictionary.current_position += 1;
                View_Card();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Dictionary.current_position > 0)
            {
                Dictionary.current_position -= 1;
                View_Card();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Dictionary.current_position = 0;
            View_Card();
        }

        private void View_Card ()
        {
            label1.Text = Dictionary.myDic[Dictionary.current_position].eng_word.Trim();
            textBox1.Lines = Dictionary.myDic[Dictionary.current_position].rus_words.Split(';');
            textBox1.AppendText("*");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Dictionary.current_position = Dictionary.myDic.Count - 1;
            View_Card();
        }
        AddCard add_card = new AddCard();
        private void button11_Click(object sender, EventArgs e)
        {
            
            add_card.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StandardToXml(Dictionary.myDic, Dictionary.dicPath);
            UpdateStats();
            Dictionary.isSynced = true;
        }
        static void StandardToXml (List<Word> f, string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Word>));
            TextWriter stream = new StreamWriter(path);
            ser.Serialize(stream, f);
            stream.Close();
        }
        static void LinqToXml(List<Word> f)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            Stream sr = null;
            XmlSerializer xmlSer = null;
            xmlSer = new XmlSerializer(typeof(List<Word>));
            try
            {
                sr = new FileStream(Dictionary.dicPath, FileMode.Open);
                Dictionary.myDic = (List<Word>)xmlSer.Deserialize(sr);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Неудалось открыть файл: " + exc.Message, "Ошибка!");
                return;
            }
            finally
            {
                sr.Close();
            }
            Dictionary.isSynced = true;
            UpdateStats();
            label1.Text = Dictionary.myDic[Dictionary.current_position].eng_word;
            textBox1.Lines = Dictionary.myDic[Dictionary.current_position].rus_words.Split(';');
            Dictionary.myDic = Dictionary.myDic.OrderBy(item => item.eng_word).ToList();
            RemoveNulls();
            //Dictionary.myDic = Dictionary.myDic.Select(s => s.rus_words.Split(';').Select(t => t != "")).ToArray();
            button3.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
        }

        void RemoveNulls()
        {
            foreach (var e in Dictionary.myDic)
            {
                string[] tmp = e.rus_words.Split(';');
                tmp = tmp.Where(w => w != "").ToArray();
                tmp = tmp.Select(t => t.Trim()).ToArray();
                e.rus_words = tmp.Aggregate((w1,w2) => w1 + ";" + w2);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeleteWord();
            UpdateStats();
        }

        void DeleteWord()
        {
            if (Dictionary.myDic.Count() == 0) return;
            Dictionary.myDic.RemoveAt(Dictionary.current_position);
            if (Dictionary.myDic.Count > 0)
            {
                Dictionary.current_position = Dictionary.current_position > 0 ? Dictionary.current_position - 1 : Dictionary.current_position;
                Dictionary.isSynced = false;
                View_Card();
            }
            else
            {
                label1.Text = "Словарь пуст";
                Dictionary.current_position = 0;
                textBox1.Clear();
            }
        }

        public void UpdateStats()
        {
            if (Dictionary.myDic.Count() == 0) label2.Text = "Словарь пуст";
            else
            {
                int nWords = Dictionary.myDic.Count();
                var wordStat = Dictionary.myDic.Aggregate(
                    new Dictionary<Word.WordType, int>(),
                    (dic, w) =>
                    {
                        dic[w.type] = dic.ContainsKey(w.type) ? dic[w.type] + 1 : 1;
                        return dic;
                    });
                var len = Dictionary.myDic.Average(s => s.eng_word.Length);

                label2.Text = "Всего слов " + nWords +
                    " Средняя длинна английского слова " + Math.Round(len) +
                    "\nсуществительных " + wordStat[Word.WordType.noun] + 
                    "\nприлагательных " + wordStat[Word.WordType.adjective] + 
                    "\nглаговлов " + wordStat[Word.WordType.verb];
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string msg = "Сохранить словарь перед выходом?";
            const string cap = "Сохранение";
            if (!Dictionary.isSynced)
            {
                var result = MessageBox.Show(msg, cap, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    StandardToXml(Dictionary.myDic, Dictionary.dicPath);
                    Dictionary.isSynced = true;
                }
            }
            Application.Exit();
        }

        TestForm tForm = new TestForm();
        private void button3_Click(object sender, EventArgs e)
        {
            tForm.Show();
        }
    }

    [Serializable]
    public class Word
    {
        public enum WordType { noun = 0, adjective, verb };
        //public const string XmlName = "word";
        [XmlAttribute("id")]
        public int ID;
        [XmlAttribute("English word")]
        public string eng_word;
        [XmlAttribute("Translation")]
        public string rus_words;
        [XmlAttribute("Word type")]
        public WordType type;
//        Word() { }
    }
    public static class Dictionary
    {
        static public bool isSynced = true;
        static public bool IsSynced
        {
            get { return Dictionary.isSynced; }
            set
            {
                Dictionary.isSynced = value;
                if (Dictionary.isSynced == false)
                {
                    //UpdateStats();
                }
            }
        }
        static public int current_position = 0;
        static public string dicPath = @".\word.xml";
        static public List<Word> myDic = new List<Word>();
    }
}
