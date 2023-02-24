using System.Drawing;
using System.Windows.Forms;
using System;
using System.Xml.Serialization;

namespace Lab_02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Discipline> listOfDisc = new List<Discipline>();
        Discipline discipline;   /// в этот объект пишем всю инфу из формы
        string path = @"D:\Универ\4сем\OOP_4sem\Lab_02\Lab_02\text.xml";
        /// путь по которому будем записывать в xml-файл объект
        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.CheckOnClick = true;
            checkedListBox2.CheckOnClick = true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e) { }
        private bool ValidateForm()     // метод проверки объектов формы, проверяет все тексбоксы и другие на заполненность
        {
            bool OKtoSave = true;
            List<TextBox> textBoxes = new List<TextBox> { textBox1, textBox2, textBox3, textBox6, textBox4 };
            List<ComboBox> comboBoxes = new List<ComboBox> { comboBox1 };
            List<CheckedListBox> checkedListBoxes = new List<CheckedListBox> { checkedListBox1, checkedListBox2, };

            foreach (TextBox temp in textBoxes)
            {
                if (temp.Text == "")
                {
                    temp.BackColor = Color.Red;
                    OKtoSave = false;
                }
                else
                {
                    temp.BackColor = Color.White;
                }
            }
            string str = "+/*><=";
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = textBox3.Text;
            string str4 = textBox4.Text;
            string str6 = textBox6.Text;
            for (int i = 0; i < str1.Length; i++)
                for (int j = 0; j < str.Length; j++)
                    if (str1[i] == str[j])
                    {
                        this.textBox1.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
                    else
                    {
                        this.textBox1.BackColor = Color.White;
                    }
            string str0 = "1234567890";
            for (int i = 0; i < str1.Length; i++)
                for (int j = 0; j < str1.Length; j++)
                    if (str1[i] == str0[j])
                    {
                        this.textBox1.BackColor = Color.LightCoral;
                        throw new Exception("В строке есть цифры");
                    }

            for (int i = 0; i < str2.Length; i++)
                for (int j = 0; j < str.Length; j++)
                    if (str2[i] == str[j])
                    {
                        this.textBox2.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
                    else
                    {
                        this.textBox2.BackColor = Color.White;
                    }
            for (int i = 0; i < str3.Length; i++)
                for (int j = 0; j < str.Length; j++)
                    if (str3[i] == str[j])
                    {
                        this.textBox3.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
                    else
                    {
                        this.textBox3.BackColor = Color.White;
                    }
            for (int i = 0; i < str4.Length; i++)
                for (int j = 0; j < str.Length; j++)
                    if (str4[i] == str[j])
                    {
                        this.textBox4.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
                    else
                    {
                        this.textBox4.BackColor = Color.White;
                    }
            
            for (int i = 0; i < str4.Length; i++)
                for (int j = 0; j < str4.Length; j++)
                    if (str4[i] == str0[j])
                    {
                        this.textBox4.BackColor = Color.LightCoral;
                        throw new Exception("В строке есть цифры");
                    }
            for (int i = 0; i < str6.Length; i++)
                for (int j = 0; j < str.Length; j++)
                    if (str6[i] == str[j])
                    {
                        this.textBox6.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
                    else
                    {
                        this.textBox6.BackColor = Color.White;
                    }

            int num;
            if (!Int32.TryParse(textBox2.Text, out num) || num < 1 || num > 27)
            {
                this.textBox2.BackColor = Color.LightCoral;
                throw new Exception("В строке недопустимые символы");
            }
            if (!Int32.TryParse(textBox3.Text, out num) || num < 1 || num > 27)
            {
                this.textBox3.BackColor = Color.LightCoral;
                throw new Exception("В строке недопустимые символы");
            }


            foreach (ComboBox temp in comboBoxes)
            {
                if (temp.Text == "")
                {
                    temp.BackColor = Color.Red;
                    OKtoSave = false;
                }
                else
                {
                    temp.BackColor = Color.White;
                }
            }

            foreach (CheckedListBox temp in checkedListBoxes)
            {
                if (temp.Text == "")
                {
                    temp.BackColor = Color.Red;
                    OKtoSave = false;
                }
                else
                {
                    temp.BackColor = Color.White;
                }
            }

            if (!radioButton1.Checked && !radioButton2.Checked || radioButton1.Checked && radioButton2.Checked)
            {
                errorProvider1.SetError(radioButton2, "Введите значение");
                OKtoSave = false;

            }
            return OKtoSave;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                List<string> tempCourse = new List<string>();
                List<string> tempSpec = new List<string>();
                Lector tempLect = new Lector((string)listBox1.SelectedItem, textBox4.Text, textBox6.Text);
                string tempRadio = "";

                foreach (string item in checkedListBox1.CheckedItems)
                    tempSpec.Add(item);
                foreach (string item in checkedListBox2.CheckedItems)
                    tempCourse.Add(item);

                if (radioButton1.Checked)
                    tempRadio = radioButton1.Text;
                if (radioButton2.Checked)
                    tempRadio = radioButton2.Text;

                discipline = new Discipline(textBox1.Text, Int32.Parse(comboBox1.Text),
                    tempCourse, tempSpec, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text),
                    tempRadio, tempLect);
                listOfDisc.Add(discipline);


                /// код для сериализации в XML
                XmlSerializer formatter = new XmlSerializer(typeof(Discipline));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fs, discipline);
                }
                MessageBox.Show("Информация записана в XML!", "DisciplineRedact", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///код для десериализации из XML
            XmlSerializer formatter = new XmlSerializer(typeof(Discipline));
            Discipline disOut = new Discipline();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                disOut = (Discipline)formatter.Deserialize(fs);
            }

            richTextBox1.Text = disOut.ToString();

        }
        private void textBox7_TextChanged(object sender, EventArgs e) { }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            foreach (int i in checkedListBox1.CheckedIndices)
            {
                checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
            }
            foreach (int i in checkedListBox2.CheckedIndices)
            {
                checkedListBox2.SetItemCheckState(i, CheckState.Unchecked);
            }
            textBox2.Text = "";
            textBox3.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox6.Text = "";
            textBox4.Text = "";
            listBox1.ClearSelected();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
    }
}