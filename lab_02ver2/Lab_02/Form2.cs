using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_02
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static Lector lect = new Lector();

        public bool ValidateText()
        {
            bool OkToSave = true;
            var textList = new List<TextBox> { textBox4, textBox6 };
            string str = "+/*><=";
            string str0 = "1234567890";


            foreach (var temp in textList)
            {
                if (temp.Text.Any(symb => str.Contains(symb)))
                {
                    temp.BackColor = Color.LightCoral;
                    OkToSave = false;
                    throw new Exception("В строке недопустимые символы");
                }
                else
                {
                    temp.BackColor = Color.White;
                }
            }

            if(textBox4.Text.Any(symb => str0.Contains(symb)))
            {
                textBox4.BackColor = Color.LightCoral;
                OkToSave = false;
                throw new Exception("В строке недопустимые символы");
            }
            else
            {
                textBox4.BackColor = Color.White;
            }

            return OkToSave;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateText())
            {

                lect.Name = textBox4.Text;
                lect.Auditorium = textBox6.Text;
                lect.Department = listBox1.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK);
            }
        }
    }
}
