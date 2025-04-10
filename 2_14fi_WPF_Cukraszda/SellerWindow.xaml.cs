﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _2_14fi_WPF_Cukraszda
{
    /// <summary>
    /// Interaction logic for SellerWindow.xaml
    /// </summary>
    public partial class SellerWindow : Window
    {
        ServerConnection connection;
        public SellerWindow()
        {
            InitializeComponent();
            connection = new ServerConnection("http://127.1.1.1:3000");
            
        }
        void OnFocus(object s, EventArgs e)
        {
            TextBox oneTextbox = s as TextBox;
            if (oneTextbox.Text == oneTextbox.Tag.ToString())
            {
                oneTextbox.Clear();
                oneTextbox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
        void OffFocus(object s, EventArgs e)
        {
            TextBox oneTextbox = s as TextBox;
            if (oneTextbox.Text == "")
            {
                oneTextbox.Text = oneTextbox.Tag.ToString();
                oneTextbox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
        async void AddCake(object s, EventArgs e)
        {
            Cake oneCake = new Cake()
            {
                name = NameInput.Text,
                price = int.Parse(PriceInput.Text),
                stock = int.Parse(StockInput.Text),
                picture = PicrureInput.Text,
                description = DescriptionInput.Text,
                allergenes = AllergenesInput.Text
            };
            await connection.PostCake(oneCake);

        }
    }
}
