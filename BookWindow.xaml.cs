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
    /// Interaction logic for BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {
        public static DataGrid DataGridView;
        PublishingHous _context = new PublishingHous();

        public BookWindow()
        {
            InitializeComponent();
            ucitajGrid();
            cmbZanr.ItemsSource = _context.Genres.ToList();
            cmbZanr.DisplayMemberPath = "Name";
            cmbZanr.SelectedValuePath = "Id";
            cmbIzdavac.ItemsSource = _context.Publishers.ToList();
            cmbIzdavac.DisplayMemberPath = "Name";
            cmbIzdavac.SelectedValuePath = "Id";
        }

        private void ucitajGrid()
        {
            dgvKnjiga.ItemsSource = _context.Books.ToList();
            DataGridView = dgvKnjiga;
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
            txtAutor.Clear();
            txtCena.Clear();
            cmbIzdavac.SelectedItem = null;
            cmbZanr.SelectedItem = null;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtID.Text))
            {
                sacuvajKnjigu();
            }
            else
            {
                dodajKnjigu();
            }
        }

        private void dodajKnjigu()
        {
            Book dodajKnjigu = new Book()
            {
                Name = txtIme.Text,
                Price = Convert.ToInt32(txtCena.Text),
                Autor = txtAutor.Text,
                GenreID = Convert.ToInt32(cmbZanr.SelectedValue),
                PublisherID = Convert.ToInt32(cmbIzdavac.SelectedValue)
            };
            _context.Books.Add(dodajKnjigu);
            _context.SaveChanges();
            dgvKnjiga.ItemsSource = _context.Books.ToList();
            osvezi();
        }

        private void sacuvajKnjigu()
        {
            int ID = Convert.ToInt32(txtID.Text);
            Book sacuvajKnjigu = (from b in _context.Books where b.ID == ID select b).Single();
            sacuvajKnjigu.Name = txtIme.Text;
            sacuvajKnjigu.Autor = txtAutor.Text;
            sacuvajKnjigu.Price = Convert.ToInt32(txtCena.Text);
            sacuvajKnjigu.PublisherID = Convert.ToInt32(cmbIzdavac.SelectedIndex);
            sacuvajKnjigu.GenreID = Convert.ToInt32(cmbZanr.SelectedIndex);
            _context.SaveChanges();
            DataGridView.ItemsSource = _context.Books.ToList();
            osvezi();
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            obrisiKnjigu();
        }

        private void obrisiKnjigu()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Prvo morate da odaberete knjigu koji zelite da obrisete!");
            }
            else
            {
                int ID = Convert.ToInt32(txtID.Text);
                var knjiga = _context.Books.Where(b => b.ID == ID).Single();
                _context.Books.Remove(knjiga);
                _context.SaveChanges();
                dgvKnjiga.ItemsSource = _context.Books.ToList();
            }
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            traziKnjigu();
        }

        private void traziKnjigu()
        {
            int ID = Convert.ToInt32(txtPretraga.Text);
            var knjiga = _context.Books.SingleOrDefault(b => b.ID == ID);
            if (knjiga == null)
            {
                MessageBox.Show("Greska! U bazi nemamo trazeni zanr.");
                return;
            }
            txtID.Text = knjiga.ID.ToString();
            txtIme.Text = knjiga.Name;
            txtAutor.Text = knjiga.Autor;
            txtCena.Text = knjiga.Price.ToString();
            cmbIzdavac.SelectedIndex = knjiga.PublisherID;
            cmbZanr.SelectedIndex = knjiga.GenreID;
        }
    }
}
