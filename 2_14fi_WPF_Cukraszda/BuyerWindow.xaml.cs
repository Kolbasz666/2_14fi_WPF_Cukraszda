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
    /// Interaction logic for BuyerWindow.xaml
    /// </summary>
    public partial class BuyerWindow : Window
    {
        List<Cake> allCake = new List<Cake>();
        public BuyerWindow()
        {
            InitializeComponent();
            Start();
        }
        void Start()
        {
            allCake.Add(new Cake("Diós krémes", 1000, "https://www.szakacsreceptek.hu/_site_media/com_strava/mod_recepty/img_list-by-category/32.jpg") { id = "Kolbász" });
            BuildItems();
        }
        void BuildItems()
        {
            foreach (Cake item in allCake)
            {
                Grid oneCake = new Grid();
                oneCake.Height = 180;
                oneCake.Width = 120;
                for (int i = 0; i < 4; i++)
                {
                    RowDefinition row = new RowDefinition();
                    if (i == 1)
                        row.Height = new GridLength(2, GridUnitType.Star);
                    oneCake.RowDefinitions.Add(row);
                }
                Label title = new Label() { Content = item.name };
                oneCake.Children.Add(title);
                Image picture = new Image();
                picture.Source = new BitmapImage(new Uri(item.picture));
                picture.Stretch = Stretch.UniformToFill;
                oneCake.Children.Add(picture);
                oneCake.Background = new SolidColorBrush(Colors.Bisque);
                Grid.SetRow(picture, 1);
                Label price = new Label() { Content = item.price + " Ft" };
                oneCake.Children.Add(price);
                Grid.SetRow(price, 2);
                Border BuyBorder = new Border() { CornerRadius = new CornerRadius(10), Background = new SolidColorBrush(Colors.GreenYellow), Margin = new Thickness(10, 2, 10, 2) };
                oneCake.Children.Add(BuyBorder);
                Grid.SetRow(BuyBorder, 3);
                Label BuyLabel = new Label() { Content = "Kosárba" };
                BuyLabel.Tag = item.id;
                BuyLabel.MouseLeftButtonUp += Buy;
                BuyBorder.Child = BuyLabel;

                Items.Children.Add(oneCake);
            }
        }
        void Buy(object sender, EventArgs e)
        {
            if (!(sender is Label)) {
                return;
            }
            Label oneLabel = sender as Label;
            string id = oneLabel.Tag.ToString();
            //Cake oneCake = allCake.Where(cake => cake.id == id).First();
            //ugyanaz mint alatta
            MessageBox.Show("Az elem ára: " + allCake.Where(cake => cake.id == id).First().price);
        }
    }
}
