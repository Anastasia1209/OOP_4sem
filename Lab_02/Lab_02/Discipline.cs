using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lab_02
{
    [Serializable]
    public class Discipline
    {
        public string Name { get; set; }

        public int Semester { get; set; }
        public List<string> Course { get; set; }
        public List<string> Speciality { get; set; }
        public int NumOfLectures { get; set; }
        public int NumOfLabs { get; set; }
        public string TypeOfControl { get; set; }
        public Lector lector { get; set; }


        public Discipline()                             /// корректный конструктор без параметров
        {
            Course = new List<string>();
            Speciality = new List<string>();
            lector = new Lector();

        }
        public Discipline(string name, int sem, List<string> course, List<string> spec, int nLect, int labs, string control, Lector Lector)
        {
            Name = name;
            Semester = sem;
            Course = course;
            Speciality = spec;
            NumOfLectures = nLect;
            NumOfLabs = labs;
            TypeOfControl = control;
            lector = Lector;
        }
        public override string ToString()
        {
            string course = "";
            string speciality = "";
            foreach (string c in Course)
                course += c ;
            foreach (string s in Speciality)
                speciality += s ;

            string res = $"Название: {Name} \nКурс: {course} \nСеместр: {Semester}\n" +
                $"Специальность: {speciality}\nЧасов лекций: {NumOfLectures}\n" +
                $"Часов лабораторных: {NumOfLabs}\nТип контроля: {TypeOfControl}\n" +
                $"ФИО лектора: {lector.Name}\nКафедра: {lector.Department}\n" +
                $"Аудитория: {lector.Auditorium}\n\n=========================================\n\n";
            return res;
        }

    }
}
