using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Text.RegularExpressions;


namespace Lab_02
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        List<Discipline> listOfDisc = new List<Discipline>();
        List<Discipline> listSearchSemesterToSave = new List<Discipline>();
        string path = @"D:\Универ\4сем\OOP_4sem\Lab_03\Lab_02\searchSemester.xml";


        private void button1_Click(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));
            // ^ - начало, \d - цифра, {1} - только одна цифра, $ - конец строки
            Regex userReg = new Regex(@"^\d{1}$");

            string path2 = @"D:\Универ\4сем\OOP_4sem\Lab_032\Lab_02\text.xml";
            using (FileStream fs = new FileStream(path2, FileMode.OpenOrCreate))
            {
                listOfDisc = (List<Discipline>)formatter.Deserialize(fs);
            }

            DataTransfer.semSearch = textBox1.Text;
            string searchSemesterOut = "==============================================\n\n";
            string semSearch = DataTransfer.semSearch;
            MatchCollection matching = userReg.Matches(semSearch);
            if (matching.Count > 0)
            {
                List<Discipline> semesterList = new List<Discipline>();

                var res = from dist in listOfDisc
                          where dist.Semester == Convert.ToInt32(semSearch)
                          select dist;
                foreach (Discipline temp in res)
                    searchSemesterOut += temp.ToString();
                MessageBox.Show(searchSemesterOut);

                listSearchSemesterToSave = res.ToList();

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(fs, listSearchSemesterToSave);
                }

                Close();
            } else
            {
                MessageBox.Show("Входные данные введены неверно");
            }
        }
    }
}
