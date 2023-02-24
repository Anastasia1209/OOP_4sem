using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_02
{
    [Serializable]
    public class Lector
    {
        public string Department { get; set; }
        public string Name { get; set; }
        public string Auditorium { get; set; }
        public Lector()
        { }

        public Lector(string dep, string name, string aud)
        {
            Department = dep;
            Name = name;
            Auditorium = aud;
        }

    }
}
