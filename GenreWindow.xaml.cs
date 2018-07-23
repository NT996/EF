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
    /// Interaction logic for GenreWindow.xaml
    /// </summary>
    public partial class GenreWindow : Window
    {
        public static DataGrid DataGridView; 
        PublishingHous _context = new PublishingHous(); //Deklarisanje DbContext promenljive kako bi pristupili bazi

        public GenreWindow()
        {
            InitializeComponent();
            ucitajGrid();
        }

        private void ucitajGrid()
        {
            dgvZanr.ItemsSource = _context.Genres.ToList();
            DataGridView = dgvZanr;
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
                sacuvajZanr();
            }
            else
            {
                dodajZanr();
            }
        }

        private void dodajZanr()
        {
            Genre dodajZanr = new Genre()
            {
                Name = txtIme.Text,
            };
            _context.Genres.Add(dodajZanr);
            _context.SaveChanges();
            dgvZanr.ItemsSource = _context.Genres.ToList();
            osvezi();
        }

        private void sacuvajZanr()
        {
            int ID = Convert.ToInt32(txtID.Text);
            Genre sacuvajZanr = (from g in _context.Genres where g.ID == ID select g).Single();
            sacuvajZanr.Name = txtIme.Text;
            _context.SaveChanges();
            dgvZanr.ItemsSource = _context.Genres.ToList();
            osvezi();
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            traziZanr();
        }

        private void traziZanr()
        {
            int ID = Convert.ToInt32(txtPretraga.Text);
            var zanr = _context.Genres.SingleOrDefault(g => g.ID == ID);
            if (zanr == null)
            {
                MessageBox.Show("Greska! U bazi nemamo trazeni zanr.");
                return;
            }
            txtID.Text = zanr.ID.ToString();
            txtIme.Text = zanr.Name;
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            obrisiZanr();
        }

        private void obrisiZanr()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Prvo morate da odaberete zanr koji zelite da obrisete!");
            }
            else
            {
                int ID = Convert.ToInt32(txtID.Text);
                var zanr = _context.Genres.Where(g => g.ID == ID).Single();
                _context.Genres.Remove(zanr);
                _context.SaveChanges();
                dgvZanr.ItemsSource = _context.Genres.ToList();
            }
        }
    }
}
