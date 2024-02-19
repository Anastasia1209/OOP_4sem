using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml.Serialization;
using Path = System.IO.Path;

namespace WpfApp2
{
    public enum typeClothes { 
    BlackWork,
    Minimalism,
    Xedpoyk
    }
    [Serializable]
    public class Clothes { 
        public int Id { get; set; }
        public string FIO { get; set; } 
        public int Experience { get; set; }
        public string StyleWork { get; set ; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public typeClothes type;
    }
    public partial class MainWindow : Window
    {
        
        public ObservableCollection<Clothes> Cloth { get; set; }
        private readonly List<ObservableCollection<Clothes>> _mementos = new();
        private int cursor = 0;
        public MainWindow()
        {
            InitializeComponent();
            List<string> styles = new List<string> { "Light", "Dark" };
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = styles;
            styleBox.SelectedItem = "Dark";
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            //combine projectDir and Cursor.cur
            string cursorPath = Path.Combine(projectDir, "Cursor.cur");

            string iconPath = Path.Combine(projectDir, "Icon.ico");

            Icon = new BitmapImage(new Uri(iconPath));
            Cursor = new Cursor(cursorPath);


            this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Dictionary1.xaml") };
            styleComboBox.ItemsSource = Enum.GetValues(typeof(typeClothes)).Cast<typeClothes>();
            typeComboBox.ItemsSource = Enum.GetValues(typeof(typeClothes)).Cast<typeClothes>();
            Cloth = new ObservableCollection<Clothes>
            {
            new Clothes{Id=1, FIO="Дима Круглый1", Experience=1, ImagePath="D:\\4sem\\ООП\\WpfApp6\\WpfApp2\\image\\1.jpg",StyleWork="Реализм / Графика",
                Description="Техника хэндпоук более медленная и медитативная. Это сакральный обмен энергии между мной и вами."
                ,type = typeClothes.BlackWork},
             new Clothes{Id=1, FIO="Дима Круглый2", Experience=2, ImagePath="D:\\4sem\\ООП\\WpfApp6\\WpfApp2\\image\\1.jpg",StyleWork="Реализм / Графика",
                Description="Техника хэндпоук более медленная и медитативная. Это сакральный обмен энергии между мной и вами."
                ,type = typeClothes.BlackWork},
              new Clothes{Id=1, FIO="Дима Круглый", Experience=10, ImagePath="D:\\4sem\\ООП\\WpfApp6\\WpfApp2\\image\\2.png",StyleWork="Реализм / Графика",
                Description="Техника хэндпоук более медленная и медитативная. Это сакральный обмен энергии между мной и вами."
                ,type = typeClothes.Xedpoyk},
               new Clothes{Id=1, FIO="Дима Круглый", Experience=10, ImagePath="D:\\4sem\\ООП\\WpfApp6\\WpfApp2\\image\\1.jpg",StyleWork="Реализм / Графика",
                Description="Техника хэндпоук более медленная и медитативная. Это сакральный обмен энергии между мной и вами."
                ,type = typeClothes.Minimalism},
        };
            phonesList.ItemsSource = Cloth;
            _mementos.Add(new ObservableCollection<Clothes>(Cloth));
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            var buttonName = (sender as Button).Name;

            var sourceUri = buttonName switch
            {
                "EnglishButton" => this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Dictionary1.xaml") },
                "RussianButton" => this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Dictionary2.xaml") }

            };
        }
        private void Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var SearchResult = from p in Cloth
                               select p;
            if (SliderEx.Value == 0)
            {
                SearchResult = from p in Cloth
                                   where p.type == (typeClothes)styleComboBox.SelectedItem
                                   select p;
                if (SearchResult != null)
                    phonesList.ItemsSource = SearchResult;
            }
            System.Windows.MessageBox.Show(SliderEx.Value.ToString());
            if (styleComboBox.SelectedItem == null)
            {
                SearchResult = from p in Cloth
                                   where p.Experience == SliderEx.Value
                                   select p;
                if (SearchResult != null)
                    phonesList.ItemsSource = SearchResult;
            }
            else {
                SearchResult = from p in Cloth
                                   where p.type == ((typeClothes)styleComboBox.SelectedItem) && (p.Experience == SliderEx.Value)
                                   select p;
                if (SearchResult != null)
                    phonesList.ItemsSource = SearchResult;
            }
           
        }
        private void Add_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Cloth.Add(new Clothes
            {
                Id = int.Parse(ID.Text),
                FIO = FIO.Text,
                Experience = int.Parse(Experience.Text),
                ImagePath = ImageUrl.Text,
                StyleWork = Stylework.Text,
                Description = Description.Text,
                type = (typeClothes)typeComboBox.SelectedItem
            });
            _mementos.Add(new ObservableCollection<Clothes>(Cloth));
            cursor++;
        }
        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            Clothes selectedItem = (Clothes)phonesList.SelectedItem;
            if (selectedItem != null)
            {
                Cloth.Remove(selectedItem as Clothes);
            }
            _mementos.Add(new ObservableCollection<Clothes>(Cloth));
            cursor++;
        }
        private void Change_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            Clothes selectedItem = (Clothes)phonesList.SelectedItem;
            if (selectedItem == null) return;
            var number = Cloth.IndexOf(selectedItem as Clothes);
            if (number == -1) return;
            Clothes newCloth = new Clothes
            {
                Id = int.Parse(ID.Text),
                FIO = FIO.Text,
                Experience = int.Parse(Experience.Text),
                ImagePath = ImageUrl.Text,
                StyleWork = Stylework.Text,
                Description = Description.Text,
                type = (typeClothes)typeComboBox.SelectedItem
            };
            Cloth.RemoveAt(number);
            Cloth.Insert(number, newCloth);
            _mementos.Add(new ObservableCollection<Clothes>(Cloth));
            cursor++;
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var items = phonesList.ItemsSource;
            string jsonString = JsonSerializer.Serialize(items);
            using var sw = new StreamWriter(@"../../../Files/data.json");
            sw.Write(jsonString);
        }
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;
            // определяем путь к файлу ресурсов
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
        private void Redo_Click(object sender, RoutedEventArgs e)
        {

            if (cursor == _mementos.Count - 1) return;

            cursor++;
            Cloth = new ObservableCollection<Clothes>(_mementos[cursor]);
            phonesList.ItemsSource = Cloth;
        }
        private void Undo_Click(object sender, RoutedEventArgs e)
        {

            if (cursor == 0) return;


            cursor--;
            Cloth = new ObservableCollection<Clothes>(_mementos[cursor]);
            phonesList.ItemsSource = Cloth;
        }
    }
}
