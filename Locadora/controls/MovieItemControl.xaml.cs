using Locadora.classes;
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

namespace Locadora.controls
{
    /// <summary>
    /// Interação lógica para MovieItemControl.xam
    /// </summary>
    public partial class MovieItemControl : UserControl
    {
        public MovieItem Movie { get; set; }

        public MovieItemControl(MovieItem movieitem)
        {
            InitializeComponent();

            Movie = movieitem;

            rating_moviescore.Value = movieitem.MovieModel.Rating;
            img_movieImage.Source = movieitem.Img;
            txt_age.Text = movieitem.MovieModel.AgeRating != 0 ? movieitem.MovieModel.AgeRating.ToString() : "L";
            txt_year.Text = movieitem.MovieModel.ReleaseDate.Year.ToString();
            txt_title.Text = movieitem.MovieModel.Title;

            container.Background = movieitem.BackColor;
            txt_age.Foreground = movieitem.ForeColor;
        }

        private void rating_moviescore_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var main = Window.GetWindow(this) as MainWindow;
            main.ShowMovieDetails(Movie);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            ScaleTransform scaleTransform1 = new ScaleTransform(1.05, 1.05);

            RenderTransform = scaleTransform1;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleTransform scaleTransform1 = new ScaleTransform(1, 1);

            RenderTransform = scaleTransform1;
        }
    }
}
