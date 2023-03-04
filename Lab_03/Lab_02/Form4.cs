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


namespace Lab_02
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        List<Discipline> listOfDisc = new List<Discipline>();
        List<Discipline> listSearchCourseToSave = new List<Discipline>();
        string path = @"D:\Универ\4сем\OOP_4sem\Lab_03\Lab_02\searchCourse.xml";

        private void button1_Click(object sender, EventArgs e)
        {

            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));

            string path2 = @"D:\Универ\4сем\OOP_4sem\Lab_032\Lab_02\text.xml";
            using (FileStream fs = new FileStream(path2, FileMode.OpenOrCreate))
            {
                listOfDisc = (List<Discipline>)formatter.Deserialize(fs);
            }

            DataTransfer.courseSearch = textBox1.Text;
            string searchCourseOut = "==============================================\n\n";
            string courseSearch = DataTransfer.courseSearch;

            var res = from temp in listOfDisc
                      where temp.Course.Contains(courseSearch + " ")
                      select temp;
            foreach (Discipline temp in res)
                searchCourseOut += temp.ToString();
            MessageBox.Show(searchCourseOut);

            listSearchCourseToSave = res.ToList();

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, listSearchCourseToSave);
            }

            Close();
        }
    }
}
