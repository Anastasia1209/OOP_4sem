using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace WpfApp2
{
 

    public class WindowCommands
    {
        static WindowCommands()
        {
            Search = new RoutedCommand("Search", typeof(MainWindow));
            Add = new RoutedCommand("Add", typeof(MainWindow));
            Delete = new RoutedCommand("Delete", typeof(MainWindow));
            Change = new RoutedCommand("Change", typeof(MainWindow));
            Save = new RoutedCommand("Save", typeof(MainWindow));
        }
        public static RoutedCommand Search { get; set; }
        public static RoutedCommand Add { get; set; }
        public static RoutedCommand Delete { get; set; }
        public static RoutedCommand Change { get; set; }
        public static RoutedCommand Save { get; set; }
    }
}
