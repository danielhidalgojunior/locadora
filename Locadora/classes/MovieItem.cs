using Locadora.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Locadora.classes
{
    // Classe que controla o container onde ficam os filmes
    public class MovieItem
    {
        public MovieModel MovieModel { get; }

        public SolidColorBrush BackColor { get; }
        public SolidColorBrush ForeColor { get; }
        public ImageSource Img { get; }

        public MovieItem(MovieModel model)
        {
            MovieModel = model;

            Img = new BitmapImage(new Uri(MovieModel.ImgPath));

            switch (model.AgeRating)
            {
                case 0:
                    {
                        BackColor = new SolidColorBrush(Colors.LightGreen);
                        ForeColor = new SolidColorBrush(Colors.White);
                        break;
                    }
                case 10:
                    {
                        BackColor = new SolidColorBrush(Colors.LightBlue);
                        ForeColor = new SolidColorBrush(Colors.White);
                        break;
                    }
                case 12:
                    {
                        BackColor = new SolidColorBrush(Colors.DarkGoldenrod);
                        ForeColor = new SolidColorBrush(Colors.White);
                        break;
                    }
                case 14:
                    {
                        BackColor = new SolidColorBrush(Colors.Orange);
                        ForeColor = new SolidColorBrush(Colors.White);
                        break;
                    }
                case 16:
                    {
                        BackColor = new SolidColorBrush(Colors.OrangeRed);
                        ForeColor = new SolidColorBrush(Colors.White);
                        break;
                    }
                case 18:
                    {
                        BackColor = new SolidColorBrush(Colors.Black);
                        ForeColor = new SolidColorBrush(Colors.White);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
