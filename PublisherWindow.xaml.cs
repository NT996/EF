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
using CFProject.Model;

namespace CFProject
{
    /// <summary>
    /// Interaction logic for PublisherWindow.xaml
    /// </summary>
    public partial class PublisherWindow : Window
    {
        public static DataGrid DataGridView;
        PublishingHous _context = new PublishingHous();

        public PublisherWindow()
        {
            InitializeComponent();
            ucitajGrid();
        }

        private void ucitajGrid()
        {
            dgvIzdavac.ItemsSource = _context.Publishers.ToList();
            DataGridView = dgvIzdavac;
        }

        private void btnOsvezi_Click(object sender, RoutedEventArgs e)
        {
            osvezi();
        }

        private void osvezi()
        {
            txtID.Clear();
            txtIme.Clear();
            txtPretraga.Clear();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtID.Text))
            {
                sacuvajIzdavaca();
            }
            else
            {
                dodajIzdavaca();
            }
        }

        private void dodajIzdavaca()
        {
            Publisher dodajIzdavaca = new Publisher()
            {
                Name = txtIme.Text,
            };
            _context.Publishers.Add(dodajIzdavaca);
            _context.SaveChanges();
            dgvIzdavac.ItemsSource = _context.Publishers.ToList();
            osvezi();
        }

        private void sacuvajIzdavaca()
        {
            int ID = Convert.ToInt32(txtID.Text);
            Publisher sacuvajIzdavaca = (from p in _context.Publishers where p.ID == ID select p).Single();
            sacuvajIzdavaca.Name = txtIme.Text;
            _context.SaveChanges();
            dgvIzdavac.ItemsSource = _context.Publishers.ToList();
            osvezi();
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            traziIzdavaca();
        }

        private void traziIzdavaca()
        {
            int ID = Convert.ToInt32(txtPretraga.Text);
            var izdavac = _context.Publishers.SingleOrDefault(p => p.ID == ID);
            if (izdavac == null)
            {
                MessageBox.Show("Greska! U bazi nemamo trazenog izdavaca.");
                return;
            }
            txtID.Text = izdavac.ID.ToString();
            txtIme.Text = izdavac.Name;
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            obrisiIzdavaca();
        }

        private void obrisiIzdavaca()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Prvo morate da odaberete zanr koji zelite da obrisete!");
            }
            else
            {
                int ID = Convert.ToInt32(txtID.Text);
                var izdavac = _context.Publishers.Where(p => p.ID == ID).Single();
                _context.Publishers.Remove(izdavac);
                _context.SaveChanges();
                dgvIzdavac.ItemsSource = _context.Publishers.ToList();
            }
        }
    }
}
