using Locadora.classes;
using Locadora.controls;
using Locadora.models;
using Locadora.windows;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Locadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MovieItem SelectedMovie { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Deixa a barra de informações de filmes invisível por enquanto, já que nenhum filme foi selecionado
            grid_details.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[1].Height = new GridLength(Height);

            // Carrega os filmes encontrados no banco de dados com uma função assíncrona para evitar travamento da interface do usuário
            Task.Factory.StartNew(() =>
            {
                LoadMovieControls();
            });

            cb_genres.ItemsSource = MovieStoreManager.GetAllGenres().Genres;
        }

        public void LoadMovieControls()
        {
            // Isso irá limpar o container que guarda os filmes. Isso evita que os mesmos dupliquem
            wrap_movies.Dispatcher.BeginInvoke(new Action(delegate () {
                wrap_movies.Children.Clear();
            }));

            // Adiciona o primeiro Controle que é aquele botão gigante que permite adicionar um novo filme
            wrap_movies.Dispatcher.BeginInvoke(new Action(delegate () {
                wrap_movies.Children.Add(new AddMovieControl());
            }));

            // Um loop para ir adicionando dentro do container os filmes que foram carregados na lista de filmes
            foreach (var movie in MovieStoreManager.Movies)
            {
                wrap_movies.Dispatcher.BeginInvoke(new Action(delegate () {
                    wrap_movies.Children.Add(new MovieItemControl(new MovieItem(movie)));
                }));
            }
        }

        // Essa função irá trazer a barra de informaões do filme para cima, fazendo ela tampar quase que pela a janela principal
        public void ShowMovieDetails(MovieItem item)
        {
            // Guarda numa variavél qual filme foi selecionado
            SelectedMovie = item;

            grid_details.Visibility = Visibility.Visible;
            MainGrid.RowDefinitions[1].Height = new GridLength(150);

            // Preenche os campos com as informações encontradas na variavel Item, que foi o filme selecionado através do click
            img_details_img.Source = item.Img;

            txt_title.Text = item.MovieModel.Title;
            txt_real_title.Text = item.MovieModel.OriginalTitle;
            txt_agerating.Text = item.MovieModel.AgeRating.ToString();
            txt_audiolanguage.Text = item.MovieModel.Language;
            txt_genre.Text = item.MovieModel.Genre;
            txt_hassub.Text = item.MovieModel.HasSubtitle ? "Sim" : "Não";
            txt_location.Text = item.MovieModel.Location.ToString();
            txt_provider.Text = item.MovieModel.Provider;
            txt_rating.Text = item.MovieModel.Rating.ToString();
            txt_releaseddate.Text = item.MovieModel.ReleaseDate.Year.ToString();
            txt_screenformat.Text = item.MovieModel.ScreenFormat;
            txt_units.Text = item.MovieModel.Units.ToString();
            txt_mainactors.Text = item.MovieModel.Cast.GetMain();
            txt_sideactors.Text = item.MovieModel.Cast.GetOthers();
        }

        // Evento de click do botão que irá fazer a barra de informações suma até que outro filme seja selecionado
        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid_details.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[1].Height = new GridLength(Height);
        }

        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            var obj = sender as Ellipse;

            obj.Stroke = new SolidColorBrush(Colors.Blue);
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            var obj = sender as Ellipse;

            obj.Stroke = new SolidColorBrush(Colors.Black);
        }

        // Evento que vai abrir a tela de controle de gêneros
        private void OpenGenres(object sender, RoutedEventArgs e)
        {
            winManageGenre win = new winManageGenre();
            win.ShowDialog();
        }

        // Abre a tela de controle de clientes
        private void OpenClients(object sender, RoutedEventArgs e)
        {
            winManageClient win = new winManageClient();
            win.ShowDialog();
        }

        // Abre a tela de controle de funcionários
        private void OpenEmployees(object sender, RoutedEventArgs e)
        {
            winManageEmployee win = new winManageEmployee();
            win.ShowDialog();
        }

        // Abre a tela de controle de retiradas (alugar filme)
        private void OpenWithdraws(object sender, RoutedEventArgs e)
        {
            // Mostrará mensagem de erro caso não tenha unidades disponíveis do filme selecionado
            if(SelectedMovie.MovieModel.Units < 1)
            {
                MessageBox.Show("Não existem unidades disponíveis para este filme!", "Falta de unidades", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            winWithDraw win = new winWithDraw(SelectedMovie, MovieStoreManager.LoggedEmployee);
            win.ShowDialog();

            Task.Factory.StartNew(() =>
            {
                LoadMovieControls();
            });
        }

        // Abre a tela de listagem de retiradas (com a parte de aluguel desativada, já que é possivel que nenhum filme tenha sido selecionado)
        private void OpenWithdrawsList(object sender, RoutedEventArgs e)
        {
            winWithDraw win = new winWithDraw();
            win.ShowDialog();

            Task.Factory.StartNew(() =>
            {
                LoadMovieControls();
            });
        }

        // Evento que vai remover o filme selecionado
        private void RemoveMovie(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(string.Format("Deseja realmente remover \"{0}\"?", SelectedMovie.MovieModel.Title), "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            if (MovieModel.Remove((ObjectId)SelectedMovie.MovieModel.Id))
            {
                MessageBox.Show("Filme Excluido!");

                grid_details.Visibility = Visibility.Collapsed;
                MainGrid.RowDefinitions[1].Height = new GridLength(Height);
            }
            else
            {
                MessageBox.Show("Erro ao excluir filme!");
            }

            MovieStoreManager.Movies = MovieStoreManager.GetAllMovies();
            Task.Factory.StartNew(() =>
            {
                LoadMovieControls();
            });
        }

        // Abre a tela de edição para o filme
        private void EditMovie(object sender, RoutedEventArgs e)
        {
            WinManageMovie win = new WinManageMovie(SelectedMovie);
            win.ShowDialog();

            grid_details.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[1].Height = new GridLength(Height);

            LoadMovieControls();

            ShowMovieDetails(SelectedMovie);
        }

        private void QueryMovies(object sender, RoutedEventArgs e)
        {
            List<MovieModel> filteredmovies = MovieStoreManager.GetAllMovies();

            if (txt_filtername.Text != "")
            {
                if (filteredmovies == null)
                    filteredmovies = MovieStoreManager.GetAllMovies();

                filteredmovies = filteredmovies.Where(x => x.Title.Contains(txt_filtername.Text)).ToList();
            }

            if (txt_filteractor.Text != "")
            {
                if (filteredmovies == null)
                    filteredmovies = MovieStoreManager.GetAllMovies();

                filteredmovies = filteredmovies.Where(x => x.Cast.ContainsActor(txt_filteractor.Text)).ToList();
            }
            
            if (cb_genres.SelectedIndex != -1)
            {
                if ((string)cb_genres.SelectedValue != "*")
                {
                    if (filteredmovies == null)
                        filteredmovies = MovieStoreManager.GetAllMovies();

                    filteredmovies = filteredmovies.Where(x => x.Genre == (string)cb_genres.SelectedValue).ToList();
                }
            }

            MovieStoreManager.Movies = filteredmovies;
            LoadMovieControls();
        }
    }
}
