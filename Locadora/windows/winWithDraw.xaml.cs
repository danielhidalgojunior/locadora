using Locadora.classes;
using Locadora.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Locadora.windows
{
    /// <summary>
    /// Lógica interna para winWithDraw.xaml
    /// </summary>
    public partial class winWithDraw : Window
    {
        // Essa tela funciona de modo parecido com a de clientes e funcionários


        ObservableCollection<WithDrawModel> Withdraws { get; set; }
        MovieItem Movie { get; set; }
        EmployeeModel Employee { get; set; }

        public winWithDraw(MovieItem movie = null, EmployeeModel employee = null)
        {
            InitializeComponent();

            if (movie == null)
            {
                (tab.Items[0] as TabItem).IsEnabled = false;
                tab.SelectedIndex = 1;
                return;
            }

            Movie = movie;
            Employee = employee;

            txt_id.Text = movie.MovieModel.Id.ToString();
            txt_movietitle.Text = movie.MovieModel.Title;
            img_image.Source = movie.Img;
            txt_units.Text = movie.MovieModel.Units.ToString();

            dt_returndate.SelectedDate = DateTime.Today.AddDays(5);
        }

        private void UpdateTable()
        {
            Withdraws = new ObservableCollection<WithDrawModel>(MovieStoreManager.GetAllWithDraws());
            table.ItemsSource = Withdraws;
        }

        private void ClearTxts()
        {
            cb_clients.SelectedIndex = -1;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (cb_clients.SelectedIndex == -1)
                return;

            WithDrawModel withdraw = new WithDrawModel()
            {
                MovieTitle = Movie.MovieModel.Title,
                ClientId = (cb_clients.SelectedItem as ClientModel).Id,
                Employee = Employee.Id,
                WithDrawReturn = (DateTime)dt_returndate.SelectedDate,
                WithDrawDate = DateTime.Today
            };

            if (Movie.MovieModel.Units < 1)
            {
                MessageBox.Show("Não existem unidades disponíveis para este filme!", "Falta de unidades", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (WithDrawModel.Save(withdraw))
            {
                // Decrementa unidades disponíveis do filme
                Movie.MovieModel.Units--;

                // Realiza update do filme com as unidades novas
                MovieModel.Replace(Movie.MovieModel);
                MovieStoreManager.Movies = MovieStoreManager.GetAllMovies();

                MessageBox.Show("Retirada bem sucessida!");
                txt_units.Text = Movie.MovieModel.Units.ToString();

                ClearTxts();
                UpdateTable();
            }
            else
            {
                MessageBox.Show("Erro ao tentar retirar filme! Tente novamente.");
            }
        }

        // Ao abrir a tela, inicializar as informações na tabela e carrega a combobox com os clientes disponíveis
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var clients = MovieStoreManager.GetAllClients();

            cb_clients.ItemsSource = clients;

            UpdateTable();
        }
    }
}
