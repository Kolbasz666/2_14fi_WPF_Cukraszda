using System;
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
        List<Cake> allCake = new List<Cake>();
        public SellerWindow()
        {
            InitializeComponent();
            connection = new ServerConnection("http://127.1.1.1:3000");
            Load();
        }
        async void Load()
        {
            allCake = await connection.GetCakes();
            foreach (Cake oneCake in allCake)
            {
                Grid oneGrid = new Grid();
                cakeList.Children.Add(oneGrid);
                for (int i = 0; i < 5; i++) { oneGrid.ColumnDefinitions.Add(new ColumnDefinition()); }
                oneGrid.ColumnDefinitions[2].Width = new GridLength(20);
                oneGrid.ColumnDefinitions[3].Width = new GridLength(50);
                oneGrid.ColumnDefinitions[4].Width = new GridLength(20);
                oneGrid.ColumnDefinitions[0].Width = new GridLength(2, GridUnitType.Star);
                oneGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                Label firstLabel = new Label() { Content = oneCake.name };
                Label secondLabel = new Label() { Content = oneCake.price + " Ft" };
                Button minusButton = new Button() { Content = "-", FontSize = 15 };
                Button plusButton = new Button() { Content = "+", FontSize = 15 };
                TextBox input = new TextBox() { Text = "0", Margin = new Thickness(0) };
                Grid.SetColumn(secondLabel, 1);
                Grid.SetColumn(minusButton, 2);
                Grid.SetColumn(input, 3);
                Grid.SetColumn(plusButton, 4);
                minusButton.Click += minusEvent;
                plusButton.Click += plusEvent;
                minusButton.Tag = input;
                plusButton.Tag = input;
                input.TextChanged += TextChanged;
                input.Tag = 0;
                oneGrid.Children.Add(firstLabel);
                oneGrid.Children.Add(secondLabel);
                oneGrid.Children.Add(minusButton);
                oneGrid.Children.Add(plusButton);
                oneGrid.Children.Add(input);
            }
        }

        void TextChanged(object s, EventArgs e)
        {
            TextBox oneSomething = s as TextBox;
            int number = 0;
            if (!int.TryParse(oneSomething.Tag.ToString(), out number))
                MessageBox.Show($"Hibás tag érték: {oneSomething.Tag}");
            int newNumber = 0;
            if (int.TryParse(oneSomething.Text, out newNumber))
                oneSomething.Tag = newNumber;
            else
                oneSomething.Text = number.ToString();
        }
        void plusEvent(object s, EventArgs e)
        {
            FrameworkElement oneButton = s as FrameworkElement;
            TextBox oneTextbox = oneButton.Tag as TextBox;
            int number = 0;
            if (int.TryParse(oneTextbox.Text, out number))
            {
                number++;
                oneTextbox.Text = number.ToString();
            }
            else
            {
                MessageBox.Show("Hibás érték a textboxban");
            }
        }
        void minusEvent(object s, EventArgs e)
        {
            FrameworkElement oneButton = s as FrameworkElement;
            TextBox oneTextbox = oneButton.Tag as TextBox;
            int number = 0;
            if (int.TryParse(oneTextbox.Text, out number))
            {
                number--;
                if (number >= 0)
                {
                    oneTextbox.Text = number.ToString();
                }
            }
            else
            {
                MessageBox.Show("Hibás érték a textboxban");
            }
        }
        void Save(object s, EventArgs e) { }
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
