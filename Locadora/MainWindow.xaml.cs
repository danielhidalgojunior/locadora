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
            grid_details.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[1].Height = new GridLength(Height);

            Task.Factory.StartNew(() =>
            {
                MovieStoreManager.Initialize();
                LoadMovieControls();
            });
        }

        void CreateDirectories()
        {
            Directory.CreateDirectory(@"images");
        }

        public void LoadMovieControls()
        {
            wrap_movies.Dispatcher.BeginInvoke(new Action(delegate () {
                wrap_movies.Children.Clear();
            }));

            wrap_movies.Dispatcher.BeginInvoke(new Action(delegate () {
                wrap_movies.Children.Add(new AddMovieControl());
            }));

            foreach (var movie in MovieStoreManager.Movies)
            {
                wrap_movies.Dispatcher.BeginInvoke(new Action(delegate () {
                    wrap_movies.Children.Add(new MovieItemControl(new MovieItem(movie)));
                }));
            }
        }

        public void ShowMovieDetails(MovieItem item)
        {
            SelectedMovie = item;

            grid_details.Visibility = Visibility.Visible;
            MainGrid.RowDefinitions[1].Height = new GridLength(150);

            img_details_img.Source = item.Img;

            txt_title.Text = item.MovieModel.Title;
            txt_real_title.Text = item.MovieModel.OriginalTitle;
            txt_agerating.Text = item.MovieModel.AgeRating.ToString();
            txt_audiolanguage.Text = item.MovieModel.Language;
            txt_genre.Text = item.MovieModel.Genre;
            txt_hassub.Text = item.MovieModel.HasSubtitle.ToString();
            txt_location.Text = item.MovieModel.Location.ToString();
            txt_provider.Text = item.MovieModel.Provider;
            txt_rating.Text = item.MovieModel.Rating.ToString();
            txt_releaseddate.Text = item.MovieModel.ReleaseDate.Year.ToString();
            txt_screenformat.Text = item.MovieModel.ScreenFormat;
            txt_units.Text = item.MovieModel.Units.ToString();
            txt_mainactors.Text = item.MovieModel.Cast.GetMain();
            txt_sideactors.Text = item.MovieModel.Cast.GetOthers();
        }

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

        private void btn_withdraw_Click(object sender, RoutedEventArgs e)
        {
            winWithDraw win = new winWithDraw(SelectedMovie, MovieStoreManager.LoggedEmployee);
            win.ShowDialog();

            Task.Factory.StartNew(() =>
            {
                LoadMovieControls();
            });
        }

        private void OpenGenres(object sender, RoutedEventArgs e)
        {
            winManageGenre win = new winManageGenre();
            win.ShowDialog();
        }

        private void OpenClients(object sender, RoutedEventArgs e)
        {

        }

        private void OpenEmployees(object sender, RoutedEventArgs e)
        {
            winManageEmployee win = new winManageEmployee();
            win.ShowDialog();
        }

        private void OpenWithdraws(object sender, RoutedEventArgs e)
        {

        }

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

        private void EditMovie(object sender, RoutedEventArgs e)
        {
            WinManageMovie win = new WinManageMovie(SelectedMovie);
            win.ShowDialog();

            grid_details.Visibility = Visibility.Collapsed;
            MainGrid.RowDefinitions[1].Height = new GridLength(Height);

            Task.Factory.StartNew(() =>
            {
                LoadMovieControls();
            });
        }
    }
}
