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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2_14fi_WPF_Cukraszda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void Buyer(object s, EventArgs e)
        {
            BuyerWindow b = new BuyerWindow() { Top = this.Top, Left = this.Left, Visibility = Visibility.Visible };
            this.Close();
        }
        void Seller(object s, EventArgs e)
        {
            SellerWindow b = new SellerWindow() { Top = this.Top, Left = this.Left, Visibility = Visibility.Visible };
            this.Close();
        }
    }
}
