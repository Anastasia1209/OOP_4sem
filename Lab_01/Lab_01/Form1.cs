using System.CodeDom.Compiler;

namespace Lab_01
{
    public partial class Form1 : Form
    {
       public string str
        {
            get; set;
        }
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            string str2 = textBox3.Text;
            string str6 = textBox4.Text;
            if (string.IsNullOrEmpty(str))
            {
                        this.textBox1.BackColor = Color.LightCoral;

                throw new Exception ("Строка пустая");
            }
            else
            {
                this.textBox1.BackColor = Color.White;
            }
            string str4 = "1234567890";
            for (int i = 0; i < str.Length; i++)
                for (int j = 0; j < str4.Length; j++)
                    if (str[i] == str4[j])
                    {
                        this.textBox1.BackColor = Color.LightCoral;
                        throw new Exception("В строке есть цифры");
                    }
            string str5 = "+/*><=";
            for (int i = 0; i < str.Length; i++)
                for (int j = 0; j < str5.Length; j++)
                    if (str[i] == str5[j])
                    {
                        this.textBox1.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
                    else
                    {
                        this.textBox1.BackColor = Color.White;
                    }
            //для второй строки
            /* if (string.IsNullOrEmpty(str2))
             {
                 this.textBox3.BackColor = Color.LightCoral;

                 throw new Exception("Строка пустая");
             }*/
            string str42 = "1234567890";
            for (int i = 0; i < str2.Length; i++)
                for (int j = 0; j < str42.Length; j++)
                    if (str2[i] == str42[j])
                    {
                        this.textBox3.BackColor = Color.LightCoral;
                        throw new Exception("В строке есть цифры");
                    }
                    else
                    {
                        this.textBox3.BackColor = Color.White;
                    }
            string str52 = "+/*><";
            for (int i = 0; i < str2.Length; i++)
                for (int j = 0; j < str52.Length; j++)
                    if (str2[i] == str52[j])
                    {
                        this.textBox3.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
                    else
                    {
                        this.textBox3.BackColor = Color.White;
                    }
            //для третьей строки
            /* if (string.IsNullOrEmpty(str6))
             {
                 this.textBox4.BackColor = Color.LightCoral;

                 throw new Exception("Строка пустая");
             }*/
            string str46 = "1234567890";
            for (int i = 0; i < str6.Length; i++)
                for (int j = 0; j < str46.Length; j++)
                    if (str6[i] == str46[j])
                    {
                        this.textBox4.BackColor = Color.LightCoral;
                        throw new Exception("В строке есть цифры");
                    }
            string str56 = "+/*><";
            for (int i = 0; i < str6.Length; i++)
                for (int j = 0; j < str56.Length; j++)
                    if (str6[i] == str56[j])
                    {
                        this.textBox4.BackColor = Color.LightCoral;
                        throw new Exception("В строке недопустимые символы");
                    }
            switch (comboBox1.Text)
            {
                case "замена подстроки на другую подстроку":
                  //  string[] str3 = str2.Split(' ');
                    for (int i = 0; i < str.Length - str2.Length; i++)
                        for (int j = 0; j < str2.Length; j++)
                            if (str.Substring(i, str2.Length) == str2)
                            {
                                str = str.Remove(i, str2.Length);
                                str = str.Insert(i, str6);
                            }
                    textBox2.Text = str;
                    break;
                case "длина строки":
                    textBox2.Text = str.Length.ToString();
                    break;
                case "удаление заданных подстрок (символов)":
                    //  textBox2.Text = str.Remove(Convert.ToInt32(str2), 2);
    
                  for (int i = 0; i < str.Length - str2.Length; i++)
                        for(int j = 0; j < str2.Length; j++)
                            if (str.Substring(i, str2.Length) == str2)
                            {
                                str = str.Remove(i, str2.Length);
                            }
                    textBox2.Text = str;
                    break;
                case "получение символа по индексу":
                    textBox2.Text = Convert.ToString(str[Convert.ToInt32(str2)]);
                    break;
                case "количество гласных":
                    int num = 0;
                    string vowels = "AaEeIiOoYyUu";
                    for (int i = 0; i < str.Length; i++)
                        for (int j = 0; j < vowels.Length; j++)
                            if (str[i] == vowels[j])
                                ++num;
                    textBox2.Text = Convert.ToString(num);
                    break;
                case "количество согласных":
                    int num2 = 0;
                    string consonants = "qwrtpsdfghjklzxcvbnm";
                    for (int i = 0; i < str.Length; i++)
                        for (int j = 0; j < consonants.Length; j++)
                            if (str[i] == consonants[j])
                                ++num2;
                    textBox2.Text = Convert.ToString(num2);
                    break;
                case "количество предложений":
                    string[] sentences = str.Split( '.', '!', '?' );
                    int countSentences = sentences.Count() - 1;
                    textBox2.Text = countSentences.ToString();
                    break;
                case "количество слов в строке":
                    int countWords = str.Split( ' ', StringSplitOptions.RemoveEmptyEntries).Length;
                    textBox2.Text = countWords.ToString();
                    break;

                    
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}