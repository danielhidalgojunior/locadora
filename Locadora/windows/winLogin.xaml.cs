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
    /// Lógica interna para winLogin.xaml
    /// </summary>
    public partial class winLogin : Window
    {
        public winLogin()
        {
            InitializeComponent();
        }


        private void DoLogin(object sender, RoutedEventArgs e)
        {
            if (!MovieStoreManager.Initialized)
                return;

            string login = txt_login.Text;
            string pw = txt_pw.Text;

            if (MovieStoreManager.TryLogin(login, pw))
            {
                MainWindow win = new MainWindow();
                win.Show();
                win.Activate();
                Close();
            }
            else
            {
                MessageBox.Show("Usuário e/ou senha inválido(s)", "Erro de autenticação!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                MovieStoreManager.Initialize();
            });
        }
    }
}
