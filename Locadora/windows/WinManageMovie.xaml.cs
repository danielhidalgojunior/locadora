using Locadora.classes;
using Locadora.models;
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

namespace Locadora.windows
{
    /// <summary>
    /// Lógica interna para WinInsertMovie.xaml
    /// </summary>
    public partial class WinManageMovie : Window
    {
        bool EditMode = false;
        MovieItem SelectedMovie { get; set; }
        public WinManageMovie(MovieItem movie = null)
        {
            InitializeComponent();

            SelectedMovie = movie;

            if (movie != null)
            {
                LoadFields();
                EditMode = true;
                btn_add.Content = "Atualizar";
            }

            lb_genres.ItemsSource = MovieStoreManager.Genres.Genres;
        }

        // Carrega as informações do modo de edição
        public void LoadFields()
        {
            txt_title.Text = SelectedMovie.MovieModel.Title;
            txt_originaltitle.Text = SelectedMovie.MovieModel.OriginalTitle;

            SelectedMovie.MovieModel.Cast.Main.ForEach(x => lb_mains.Items.Add(x));
            SelectedMovie.MovieModel.Cast.Others.ForEach(x => lb_others.Items.Add(x));

            txt_provider.Text = SelectedMovie.MovieModel.Provider;
            dt_releaseddate.SelectedDate = SelectedMovie.MovieModel.ReleaseDate;
            txt_language.Text = SelectedMovie.MovieModel.Language;
            cb_hassubtitle.IsChecked = SelectedMovie.MovieModel.HasSubtitle;

            var screenformat = SelectedMovie.MovieModel.ScreenFormat.Split(':');
            txt_screenformath.Text = screenformat[0];
            txt_screenformatw.Text = screenformat[1];

            lb_agerating.Text = SelectedMovie.MovieModel.AgeRating.ToString();

            txt_img.Text = SelectedMovie.MovieModel.ImgPath;
            lb_genres.SelectedItem = SelectedMovie.MovieModel.Genre;
            ud_units.Value = (byte)SelectedMovie.MovieModel.Units;

            txt_stand.Text = SelectedMovie.MovieModel.Location.Stand.ToString();
            ud_shelf.Value = SelectedMovie.MovieModel.Location.Shelf;
            ud_pos.Value = (byte)SelectedMovie.MovieModel.Location.Pos;
        }

        // Adiciona atores principais
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!lb_mains.Items.Contains(txt_main.Text) && txt_main.Text != null)
                lb_mains.Items.Add(txt_main.Text);

            txt_main.Clear();
        }

        // Duplo clique limpa a lista de atores
        private void ClearList(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as ListBox;

            obj.Items.Clear();
        }

        // Adiciona atores secundarios
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!lb_others.Items.Contains(txt_other.Text) && txt_other.Text != null)
                lb_others.Items.Add(txt_other.Text);

            txt_other.Clear();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Save();
        }

        // Gera uma avaliação aleatória de 0 a 5 para o filme (estrelas)
        private int GenerateRating()
        {
             return new Random(DateTime.Today.Second).Next(1, 5);
        }

        // Salva informações no banco
        private void Save()
        {
            MovieModel movie = new MovieModel()
            {
                Title = txt_title.Text,
                OriginalTitle = txt_originaltitle.Text,
                ImgPath = txt_img.Text,
                Genre = (string)lb_genres.SelectedValue,
                Cast = new CastModel() { Main = lb_mains.Items.OfType<string>().ToList(), Others = lb_others.Items.OfType<string>().ToList() },
                Provider = txt_provider.Text,
                ReleaseDate = (DateTime)dt_releaseddate.SelectedDate,
                Language = txt_language.Text,
                HasSubtitle = (bool)cb_hassubtitle.IsChecked,
                ScreenFormat = string.Format("{0}:{1}", txt_screenformatw.Text, txt_screenformath.Text),
                Units = (int)ud_units.Value,
                AgeRating = Convert.ToByte(lb_agerating.SelectedValue),
                Rating = (byte)GenerateRating(),
                Location = new LocationModel() { Stand = txt_stand.Text.ToUpper()[0], Shelf = (byte)ud_shelf.Value, Pos = (int)ud_pos.Value }
            };

            if (!EditMode)
            {
                MovieModel.Save(movie);
            }
            else
            {
                movie.Id = SelectedMovie.MovieModel.Id;
                if (MovieModel.Replace(movie))
                    MessageBox.Show("Filme atualizado!");
                else
                    MessageBox.Show("Erro ao atualizar filme!");
            }

            MovieStoreManager.Movies = MovieStoreManager.GetAllMovies();
            Close();
        }
    }
}
