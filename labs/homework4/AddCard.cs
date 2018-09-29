using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace homework4
{
    public partial class AddCard : Form
    {
        public AddCard()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(Word.WordType));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string english_word = (textBox1.Text).ToLower().Trim();
            string russian_words = (textBox2.Text).ToLower().Trim();
            Regex reg_eng = new Regex(@"^[a-z\-]+");
            Regex reg_rus = new Regex(@"[\w+\s*'*\-*;*]+");
            if (reg_eng.IsMatch(english_word) == false)
            {
                MessageBox.Show("Проверьте английское слово!", "Ошибка");
                textBox1.SelectAll();
                return;
            }
            if (!reg_rus.IsMatch(russian_words))
            {
                MessageBox.Show("Проверьте перевод!", "Ошибка");
                textBox2.SelectAll();
                return;
            }
            if (!Dictionary.myDic.Any(t => t.eng_word == english_word))
            {
                Dictionary.myDic.Add(new Word()
                {
                    ID = Dictionary.myDic.Count
                    ,
                    eng_word = english_word
                    ,
                    rus_words = russian_words
                    ,
                    type = (Word.WordType)comboBox1.SelectedValue
                }
                );

                textBox1.Clear();
                textBox2.Clear();
                Dictionary.myDic = Dictionary.myDic.OrderBy(item => item.eng_word).ToList();
                Dictionary.isSynced = false;
            }
            else
            {
                MessageBox.Show("Слово уже есть в словаре", "Ошибка");
            }
        }

        private void AddCard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
