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

        // Tudo começa aqui. O usuário coloca seu login e senha e ao clicar no botão Entrar, passamos os valores das
        // TextBoxes para a função MovieStoreManager.TryLogin, que retornará true caso a senha e login existam no banco de dados
        private void DoLogin(object sender, RoutedEventArgs e)
        {
            if (!MovieStoreManager.Initialized)
                return;

            // Armazenando as informações das texboxes
            string login = txt_login.Text;
            string pw = txt_pw.Password;

            if (MovieStoreManager.TryLogin(login, pw))
            {
                MainWindow win = new MainWindow();
                // Abre a Window inicial onde podemos começar as operações CRUD
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
            // Executa trecho de código assíncrono para evitar bloqueio da interface gráfica
            Task.Factory.StartNew(() =>
            {
                // Função que ira mapear e inicializar a conexão com banco de dados. É usada apenas uma vez
                MovieStoreManager.Initialize();
            });
        }
    }
}
