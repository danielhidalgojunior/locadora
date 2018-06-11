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
    /// Lógica interna para winWithDraw.xaml
    /// </summary>
    public partial class winWithDraw : Window
    {
        public winWithDraw(MovieItem movie, EmployeeModel employee)
        {
            InitializeComponent();
        }
    }
}
