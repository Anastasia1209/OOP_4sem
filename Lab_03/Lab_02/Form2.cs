using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lab_02
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e) { }

        //[RegularExpression(@"^\+[1-9]\d{3}-\d{3}-\d{4}$")]
        List<Discipline> listOfDisc = new List<Discipline>();
        List<Discipline> listSearchLectNameToSave = new List<Discipline>();
        string path = @"D:\Универ\4сем\OOP_4sem\Lab_032\Lab_02\searchLectName.xml";

        private void button1_Click(object sender, EventArgs e)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Discipline>));

            string path2 = @"D:\Универ\4сем\OOP_4sem\Lab_03\Lab_02\text.xml";
            using (FileStream fs = new FileStream(path2, FileMode.OpenOrCreate))
            {
               listOfDisc = (List<Discipline>)formatter.Deserialize(fs);
            }

            DataTransfer.lectSearch = textBox1.Text;
            string searchLectorOut = "==============================================\n\n";
            string lectorSearch = DataTransfer.lectSearch;
            List<Discipline> lectorList = new List<Discipline>();

            var res = from dist in listOfDisc
                      where dist.lector.Name == lectorSearch
                      select dist;
            foreach (Discipline dis in res)
                searchLectorOut += dis.ToString();
            MessageBox.Show(searchLectorOut);

            listSearchLectNameToSave = res.ToList();

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fs, listSearchLectNameToSave);
            }

            Close();
        }
    }
}
