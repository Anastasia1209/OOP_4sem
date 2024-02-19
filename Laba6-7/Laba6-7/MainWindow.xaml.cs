using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Laba6_7.Products;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Laba6_7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Product> items;
        private readonly List<ObservableCollection<Product>> _mementos = new();
        private int cursor = 0;
        public MainWindow()
        {
            // get project direcry
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            string projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

            //combine projectDir and Cursor.cur
            string cursorPath = Path.Combine(projectDir, "Cursor.cur");

            string iconPath = Path.Combine(projectDir, "Rainmeter.ico");

            Icon = new BitmapImage(new Uri(iconPath));
            Cursor = new Cursor(cursorPath);

            InitializeComponent();
            this.items = new ObservableCollection<Product>()
            {
                new Product(
                    "Iphone",
                    DeviceType.Smartphone,
                    @"D:\Универ\4сем\OOP_4sem\Laba6-7\Laba6-7\Products\Devices\Photos\Iphone.jpg",
                    "Apple iOS, экран 6.1 OLED (1170x2532), Apple A15 Bionic, ОЗУ 6 ГБ, флэш-память 128 ГБ, камера 12 Мп, аккумулятор 3095 мАч, 1 SIM",
                    599)
            };

            ProductsDataGrid.ItemsSource = items;
            ShopDataGrid.ItemsSource = items;

            _mementos.Add(new ObservableCollection<Product>(items));

            //bind CustomCommand to button

            List<string> styles = new List<string> { "Light", "Dark" };
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = styles;
            styleBox.SelectedItem = "Dark";
        }

        public void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;
            // определяем путь к файлу ресурсов
            var uri = new Uri("/Themes/" + style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }

        private void CostSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ShopCostTextBlock.Text = "Цена:  " + (int)ShopCostSlider.Value;
        }

        private void RedactorCostSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RedactorCostTextBlock.Text = "Цена :  " + (int)RedactorCostSlider.Value;
        }

        private void OpenExplorerButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                //set img url to img
                DeviceImage.Source = new BitmapImage(new System.Uri(openFileDialog.FileName));
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            using var reader = new StreamReader(@"../../../Files/Products.json");
            var json = reader.ReadToEnd();

            var serailizedItems = JsonConvert.DeserializeObject<List<Product>>(json);
            foreach (var serailizedItem in serailizedItems)
            {
                items.Add(serailizedItem);
            }

            _mementos.Add(new ObservableCollection<Product>(items));
            cursor++;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var items = ProductsDataGrid.ItemsSource;
            var serializer = new JsonSerializer();
            using var sw = new StreamWriter(@"../../../Files/Products.json");
            string jsonString = JsonConvert.SerializeObject(items);
            sw.Write(jsonString);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ProductsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                items.Remove(selectedItem as Product);
            }
            _mementos.Add(new ObservableCollection<Product>(items));
            cursor++;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ProductsDataGrid.SelectedItem;
            if (selectedItem == null) return;
            var itemNumberInCollection = items.IndexOf(selectedItem as Product);
            if (itemNumberInCollection == -1) return;

            var type = TypeComboBox.SelectedIndex switch
            {
                0 => DeviceType.Smartphone,
                1 => DeviceType.Tablet,
                2 => DeviceType.Laptop,
                _ => DeviceType.Smartphone
            };

            string fotoUrl = DeviceImage.Source != null ? DeviceImage.Source.ToString() : "";

            var ChangedProduct = new Product(NameTextBox.Text, type, fotoUrl, RedactorDescriptionTextBox.Text, (int)RedactorCostSlider.Value);

            items.RemoveAt(itemNumberInCollection);
            items.Insert(itemNumberInCollection, ChangedProduct);

            _mementos.Add(new ObservableCollection<Product>(items));
            cursor++;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var type = TypeComboBox.SelectedIndex switch
            {
                0 => DeviceType.Smartphone,
                1 => DeviceType.Tablet,
                2 => DeviceType.Laptop,
                _ => DeviceType.Smartphone
            };

            string fotoUrl = DeviceImage.Source != null ? DeviceImage.Source.ToString() : "";

            var newProduct = new Product(NameTextBox.Text, type, fotoUrl, RedactorDescriptionTextBox.Text, (int)RedactorCostSlider.Value);

            items.Add(newProduct);

            _mementos.Add(new ObservableCollection<Product>(items));
            cursor++;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var searchingCost = (int)ShopCostSlider.Value;

            DeviceType deviceType;
            if (SmartphoneRadioButton.IsChecked == true) deviceType = DeviceType.Smartphone;
            else if (TabletRadioButton.IsChecked == true) deviceType = DeviceType.Tablet;
            else deviceType = DeviceType.Laptop;

            var description = ShopDescriptionTextBox.Text;

            var filteredItems = items.Where(x => x.Cost <= searchingCost || (x.DeviceType == deviceType && x.Name == ShopDescriptionTextBox.Text));

            ShopDataGrid.ItemsSource = filteredItems;

        }

        private void LanguageButton_Click(object sender, RoutedEventArgs e)
        {
            var buttonName = (sender as Button).Name;

            var sourceUri = buttonName switch
            {
                "EnglishButton" => new Uri("Resources/Languages/English.xaml", UriKind.Relative),
                "RussianButton" => new Uri("Resources/Languages/Russian.xaml", UriKind.Relative),
                _ => new Uri("Resources/Languages/English.xaml", UriKind.Relative),
            };
            if (Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => x.Source == sourceUri) != null)
            {
                return;
            }

            var newLanguageResource = new ResourceDictionary() { Source = sourceUri };

            var englishLanguageResource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => x.Source == new Uri("Resources/Languages/English.xaml", UriKind.Relative));

            var russianLanguageResource = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => x.Source == new Uri("Resources/Languages/Russian.xaml", UriKind.Relative));

            if (englishLanguageResource != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(englishLanguageResource);
            }
            else
            {
                if (russianLanguageResource != null) Application.Current.Resources.MergedDictionaries.Remove(russianLanguageResource);
            }

            Application.Current.Resources.MergedDictionaries.Add(newLanguageResource);
        }

        private void UndoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            if (cursor == 0) return;


            cursor--;
            items = new ObservableCollection<Product>(_mementos[cursor]);
            ProductsDataGrid.ItemsSource = items;
        }


        private void RedoButton_Click(object sender, ExecutedRoutedEventArgs e)
        {
            if (cursor == _mementos.Count - 1) return;

            cursor++;
            items = new ObservableCollection<Product>(_mementos[cursor]);
            ProductsDataGrid.ItemsSource = items;
        }

    }

}