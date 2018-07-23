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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CFProject
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

        private void btnKnjiga_Click(object sender, RoutedEventArgs e)
        {
            BookWindow book = new BookWindow();
            book.Show();
        }

        private void btnZanr_Click(object sender, RoutedEventArgs e)
        {
            GenreWindow genre = new GenreWindow();
            genre.Show();
        }

        private void btnIzdavac_Click(object sender, RoutedEventArgs e)
        {
            PublisherWindow publisher = new PublisherWindow();
            publisher.Show();
        }
    }
}
