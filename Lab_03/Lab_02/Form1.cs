using System.Drawing;
using System.Windows.Forms;
using System;
using System.Xml.Serialization;
using static System.Windows.Forms.DataFormats;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Lab_02
{
    public partial class Form1 : Form
    {
        public List<Discipline> listOfDisc = new List<Discipline>();
        Discipline discipline;   /// � ���� ������ ����� ��� ���� �� �����
        string path = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\text.xml";
        /// ���� �� �������� ����� ���������� � xml-���� ������
        /// ������� ���-�� �������� ��� ������ ������ ���������
        public int numOfObjects = 0;
        /// ���� ��� ����\�������
        ToolStripLabel dateLabel;
        ToolStripLabel timeLabel;
        ToolStripLabel infoLabel;
        ToolStripLabel eventLabel;
        System.Windows.Forms.Timer timer;
        /// ���� ��� �������� ������ ��� ����������
        IEnumerable<Discipline> listSearchLectorNameToSave = new List<Discipline>();
        public Form1()
        {
            InitializeComponent();
            /// ������� ���-�� �������� (������� 0)
            toolStripStatusLabel1.Text += "0   ";
            /// ��� � �������� ��� ������� ���� � �������
            infoLabel = new ToolStripLabel();
            infoLabel.Text = "������� ���� � �����:";
            dateLabel = new ToolStripLabel();
            timeLabel = new ToolStripLabel();
            eventLabel = new ToolStripLabel();

            statusStrip1.Items.Add(infoLabel);
            statusStrip1.Items.Add(dateLabel);
            statusStrip1.Items.Add(timeLabel);
            statusStrip1.Items.Add(eventLabel);

            timer = new System.Windows.Forms.Timer() { Interval = 1000 };
            timer.Tick += timer_Tick;
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            dateLabel.Text = DateTime.Now.ToLongDateString();
            timeLabel.Text = DateTime.Now.ToLongTimeString();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            toolStrip1.Visible = false;
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
        private bool ValidateForm()     // ����� �������� �������� �����, ��������� ��� ��������� � ������ �� �������������
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
            string str0 = "1234567890";

            foreach (var temp in textBoxes)
            {
                if (temp.Text.Any(symb => str.Contains(symb)))
                {
                    temp.BackColor = Color.LightCoral;
                    OKtoSave = false;
                    throw new Exception("� ������ ������������ �������");
                }
                else
                {
                    temp.BackColor = Color.White;
                }
            }
            if (textBox1.Text.Any(symb => str0.Contains(symb)))
            {
                OKtoSave = false;
                this.textBox1.BackColor = Color.LightCoral;
                throw new Exception("� ������ ���� �����");
            }
            else
            {
                this.textBox1.BackColor = Color.White;
            }

            int num;
            if (!Int32.TryParse(textBox2.Text, out num) || num < 1 || num > 27)
            {
                this.textBox2.BackColor = Color.LightCoral;
                throw new Exception("� ������ ������������ �������");
            }
            if (!Int32.TryParse(textBox3.Text, out num) || num < 1 || num > 27)
            {
                this.textBox3.BackColor = Color.LightCoral;
                throw new Exception("� ������ ������������ �������");
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
                errorProvider1.SetError(radioButton2, "������� ��������");
                OKtoSave = false;

            }

            Regex auditReg = new Regex(@"^\d{3}[�-��-�]?$");

            MatchCollection matches = auditReg.Matches(textBox6.Text);

            if (matches.Count == 0)
            {
                textBox6.BackColor = Color.Red;
                OKtoSave = false;
                throw new Exception("� ������ ������������ �������");

            }

            Regex fioReg = new Regex(@"[�-�]{1}[�-�]*");
            matches = fioReg.Matches(textBox4.Text);

            if (matches.Count == 0)
            {
                textBox4.BackColor = Color.Red;
                OKtoSave = false;
                throw new Exception("� ������ ������������ �������");
            }

            Regex nameReg = new Regex(@"[�-��-�]*");
            matches = fioReg.Matches(textBox1.Text);

            if (matches.Count == 0)
            {
                textBox1.BackColor = Color.Red;
                OKtoSave = false;
                throw new Exception("� ������ ������������ �������");
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

                /*discipline = new Discipline(textBox1.Text, Int32.Parse(comboBox1.Text),
                    tempCourse, tempSpec, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text),
                    tempRadio, tempLect);
                listOfDisc.Add(discipline);*/

                /// ���� ���������� ����� ������� ����������� ����� �����������
                var discToAdd = new Discipline(textBox1.Text, Int32.Parse(comboBox1.Text),
                    tempCourse, tempSpec, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text),
                    tempRadio, tempLect);

                var results = new List<ValidationResult>();
                var context = new ValidationContext(discToAdd);
                if (Validator.TryValidateObject(discToAdd, context, results, true))
                {
                    /// ����� ��������� ��� �� ����
                    listOfDisc.Add(discToAdd);
                    MessageBox.Show("���������� �������� ���������.\n���������� �������� � ������!", "DisciplineRedact", MessageBoxButtons.OK);
                }
                else
                {
                    foreach (var error in results)
                        MessageBox.Show($"������: {error.ErrorMessage}");
                    MessageBox.Show("���������� �� �������� ���������.\n���������� �� ��������.");
                }
                eventLabel.Text = "���������";
                /// � ��� � ��� � ������� ���� ��� ������ ������ ���������
                toolStripStatusLabel1.Text = "���-�� ��������: " + listOfDisc.Count + "   ";

                /// ��� ��� ������������ � XML
                XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fs, listOfDisc);
                }
                MessageBox.Show("���������� �������� � XML!", "DisciplineRedact", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("��������� ��� ����", "������", MessageBoxButtons.OK);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///��� ��� �������������� �� XML
            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            List<Discipline> disOut = new List<Discipline>();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                disOut = (List<Discipline>)formatter.Deserialize(fs);
            }
            foreach (Discipline disc in disOut)
            {
                richTextBox1.Text += disc.ToString();
            }
            eventLabel.Text = "�������� �� �����";

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
            eventLabel.Text = "�������";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }
        //������ ����� -> ����� �� �������
        private void ���toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 newForm = new Form2();
            eventLabel.Text = "����� �� �������.";
            newForm.Show();
        }

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("������: lab3\n �����������: ������� ���������", "� ���������");
        }
        //������ ����� -> ����� �� ��������
        private void ���toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form3 newForm = new Form3();
            eventLabel.Text = "����� �� ��������.";
            newForm.Show();
        }
        //��������� ����� -> ����� �� �����
        private void ���toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form4 newForm = new Form4();
            eventLabel.Text = "����� �� �����.";
            newForm.Show();
        }
        //���������� �� ���-�� ������
        private void ���toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<Discipline> listSortLect;
            var res = from temp in listOfDisc
                      orderby temp.NumOfLectures
                      select temp;
            listSortLect = res.ToList();
            string result = "====������������� �� �������====\n\n";
            foreach (Discipline disc in listSortLect)
                result += disc.ToString();


            string pathSortLect = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\sortLect.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            using (FileStream fs = new FileStream(pathSortLect, FileMode.Create))
            {
                formatter.Serialize(fs, listSortLect);
            }
            eventLabel.Text = "������������� �� �������.";
            MessageBox.Show(result);
        }
        //���������� �� ���� ��������
        private void ����toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            List<Discipline> listSortContr;
            var res = from temp in listOfDisc
                      orderby temp.TypeOfControl
                      select temp;
            listSortContr = res.ToList();

            string result = "===������������� �� ���� ��������===\n\n";
            foreach (Discipline disc in listSortContr)
                result += disc.ToString();

            string pathSortContr = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\sortContr.xml";
            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            using (FileStream fs = new FileStream(pathSortContr, FileMode.Create))
            {
                formatter.Serialize(fs, listSortContr);
            }
            eventLabel.Text = "������������� �� ��������.";
            MessageBox.Show(result);
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
        }

        private void toolStripMenuItem1_Click_2(object sender, EventArgs e)
        {
            string pathSearchLector = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\searchLectName.xml";

            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            List<Discipline> listOutOfFileSearchLector = new List<Discipline>();

            using (FileStream fs = new FileStream(pathSearchLector, FileMode.OpenOrCreate))
            {
                listOutOfFileSearchLector = (List<Discipline>)formatter.Deserialize(fs);
            }

            string result = "==============================================\n\n";
            foreach (Discipline dist in listOutOfFileSearchLector)
                result += dist.ToString();
            eventLabel.Text = "��������� �� ��������.";
            MessageBox.Show(result);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
             string pathSearchSemester = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\searchSemester.xml";

            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            List<Discipline> listOutOfFileSearchSemester = new List<Discipline>();

            using (FileStream fs = new FileStream(pathSearchSemester, FileMode.OpenOrCreate))
            {
                listOutOfFileSearchSemester = (List<Discipline>)formatter.Deserialize(fs);
            }

            string result = "==============================================\n\n";
            foreach (Discipline dist in listOutOfFileSearchSemester)
                result += dist.ToString();
            eventLabel.Text = "��������� �� ���������.";
            MessageBox.Show(result);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string pathSearchCourse = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\searchCourse.xml";

            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            List<Discipline> listOutOfFileSearchCourse = new List<Discipline>();

            using (FileStream fs = new FileStream(pathSearchCourse, FileMode.OpenOrCreate))
            {
                listOutOfFileSearchCourse = (List<Discipline>)formatter.Deserialize(fs);
            }

            string result = "==============================================\n\n";
            foreach (Discipline dist in listOutOfFileSearchCourse)
                result += dist.ToString();
            eventLabel.Text = "��������� �� ������.";
            MessageBox.Show(result);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string pathSortLectures = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\sortLect.xml";

            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            List<Discipline> listOutOfFileSortLectures = new List<Discipline>();

            using (FileStream fs = new FileStream(pathSortLectures, FileMode.OpenOrCreate))
            {
                listOutOfFileSortLectures = (List<Discipline>)formatter.Deserialize(fs);
            }

            string result = "==============================================\n\n";
            foreach (Discipline dist in listOutOfFileSortLectures)
                result += dist.ToString();
            eventLabel.Text = "�������� ���� �� �������.";
            MessageBox.Show(result);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string pathSortControl = @"D:\������\4���\OOP_4sem\Lab_03\Lab_02\sortContr.xml";

            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            List<Discipline> listOutOfFileSortControl = new List<Discipline>();

            using (FileStream fs = new FileStream(pathSortControl, FileMode.OpenOrCreate))
            {
                listOutOfFileSortControl = (List<Discipline>)formatter.Deserialize(fs);
            }

            string result = "==============================================\n\n";
            foreach (Discipline dist in listOutOfFileSortControl)
                result += dist.ToString();
            eventLabel.Text = "�������� ���� �� ��������.";
            MessageBox.Show(result);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ������_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = (������.Visible) ? false : true;
            button5.Visible = (button5.Visible) ? false : true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            toolStrip1.Visible = true;
            button5.Visible = false;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
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

        private void �������_Click(object sender, EventArgs e)
        {
           File.WriteAllText(path, "");
            listOfDisc.Clear();
           
        }

        private void �����_Click(object sender, EventArgs e)
        {

        }
    }
}