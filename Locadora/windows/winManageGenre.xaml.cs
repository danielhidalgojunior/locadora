using Locadora.classes;
using Locadora.models;
using MongoDB.Bson;
using MongoDB.Driver;
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
    /// Lógica interna para winManageGenre.xaml
    /// </summary>
    public partial class winManageGenre : Window
    {
        public winManageGenre()
        {
            InitializeComponent();

            MovieStoreManager.Genres.Genres.ForEach(x => lb_genres.Items.Add(x));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!lb_genres.Items.Contains(txt_genre.Text) && txt_genre.Text != null)
            {
                lb_genres.Items.Insert(0, txt_genre.Text);
                MovieStoreManager.Genres.Genres.Insert(0, txt_genre.Text);
            }

            txt_genre.Clear();
        }

        private void lb_genres_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var obj = sender as ListBox;
            var selected = obj.SelectedIndex;

            if (MessageBox.Show(string.Format("Deseja realmente remover {0} da lista de gêneros?", obj.Items.GetItemAt(selected)), "Aviso", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                return;

            lb_genres.Items.RemoveAt(selected);
            MovieStoreManager.Genres.Genres.RemoveAt(selected);
            lb_genres.UpdateLayout();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var genres = lb_genres.Items.OfType<string>().ToList();
            var model = new GenreModel() { Genres = genres, Id = MovieStoreManager.Genres.Id };

            Task.Factory.StartNew(() =>
            {
                var filter = Builders<GenreModel>.Filter.Eq(x => x.Id, MovieStoreManager.Genres.Id);
                MongoConnection.genrecollection.ReplaceOne(filter, model);
                MovieStoreManager.Genres = MovieStoreManager.GetAllGenres();
            });

            Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
