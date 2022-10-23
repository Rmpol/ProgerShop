using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CloudIpspSDK;
using CloudIpspSDK.Checkout;
using System;
using ProgerShop.Models;

namespace ProgerShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private AppDbContext _db;
        private int _countItems = 0, _priceItems = 0;
        public MainWindow()
        {
            InitializeComponent();
            _db = new AppDbContext();

            List<Item> items = _db.Items.ToList();
            //Addtems();                                //для создания таблицы в базе данных

            // Далее создаем объект на основе ObservableCollection
            // Такой объект можно будет поместить в качестве элементов списка ListBox
            ObservableCollection<Item> Fields = new ObservableCollection<Item>();

            foreach (Item item in items)
                Fields.Add(item);

            FieldsListBox.ItemsSource = Fields;
        }

        private void MakePayment_Click(object sender, RoutedEventArgs e)
        {
            if (_priceItems == 0)
            {
                MessageBox.Show("Добавьте товары");
                return;
            }
            Config.MerchantId = 1396424;
            Config.SecretKey = "test";

            var req = new CheckoutRequest
            {
                order_id = Guid.NewGuid().ToString("N"),
                amount = _priceItems * 100,
                order_desc = "checkout json demo",
                currency = "USD"
            };
            var resp = new Url().Post(req);
            if (resp.Error == null)
            {
                string url = resp.checkout_url;
                System.Diagnostics.Process.Start(url);
            }
        }

       public void Addtems()
        {
            Item item1 = new Item("Кружка для программиста", "Каждый глоток с этой кружки делает ваш код чище", 20, "cup.jpg");
            Item item2 = new Item("Футболка Прогер", "Футболка для настоящего программиста", 30, "hashtag-proger.png");
            Item item3 = new Item("Футболка С#", "Футболка для любителей языка С#", 30, "tshirt-csharp.jpg");
            _db.Items.AddRange(item1, item2, item3);
            _db.SaveChanges();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LabelForCart.Text = "Ваша корзина сейчас пуста";
            _priceItems = 0;
            _countItems = 0;
            TagCleaner.Visibility = Visibility.Hidden;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            _countItems++;
            int itemId = int.Parse(((Button)sender).Tag.ToString());
            _priceItems += (int)_db.Items.Where(el => el.Id == itemId).Select(el => el.Price).First();
            LabelForCart.Text = $"В корзине есть товары\nКоличество: {_countItems}шт.\nОбщая стоимость: {_priceItems}$";
            TagCleaner.Visibility = Visibility.Visible;
            TagCleaner.Content = "Очистить корзину";
        }
    }
}
