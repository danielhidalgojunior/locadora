using Locadora.models;
using MongoDB.Bson;
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
    /// Lógica interna para winManageClient.xaml
    /// </summary>
    public partial class winManageClient : Window
    {
        ObservableCollection<ClientModel> Clients { get; set; }
        bool EditMode = false;
        ObjectId SelectedId { get; set; }

        public winManageClient()
        {
            InitializeComponent();
        }

        // Salva dados no banco
        private void Save(object sender, RoutedEventArgs e)
        {
            AddressModel am = new AddressModel()
            {
                City = txt_city.Text.ToLower(),
                District = txt_district.Text.ToLower(),
                Street = txt_street.Text.ToLower(),
                Number = Convert.ToInt32(txt_number.Text)
            };

            ClientModel model = new ClientModel()
            {
                Name = txt_name.Text.ToLower(),
                Address = am,
                Email = txt_email.Text.ToLower(),
                Phone = txt_phone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""),
                BirthDate = (DateTime)dt_birthdate.SelectedDate
            };

            if (!EditMode)
            {
                if (ClientModel.Save(model))
                    MessageBox.Show("Cliente adicionado!");
                else
                    MessageBox.Show("Erro ao adicionar cliente!");
            }
            else
            {
                model.Id = SelectedId;
                if (ClientModel.Replace(model))
                    MessageBox.Show("Cliente atualizado!");
                else
                    MessageBox.Show("Erro ao atualizar cliente!");

                EditMode = false;
                btn_add.Content = "Adicionar";
                btn_remove.Visibility = Visibility.Collapsed;
            }

            ClearTxts();
            UpdateTable();
        }

        // atualiza a tabela
        private void UpdateTable()
        {
            Clients = new ObservableCollection<ClientModel>(MovieStoreManager.GetAllClients());
            table.ItemsSource = Clients;
        }

        // Limpa as textboxes para uma possível nova inserção
        public void ClearTxts()
        {
            txt_name.Clear();
            txt_city.Clear();
            txt_district.Clear();
            txt_number.Clear();
            txt_phone.Clear();
            txt_street.Clear();
            txt_email.Clear();
            dt_birthdate.SelectedDate = null;
        }

        // Duplo clique na linha de alguma tabela vai abrir a edição de dados do cliente
        private void table_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tab.SelectedIndex = 0;

            var row = (ClientModel)table.SelectedItem;

            SelectedId = (ObjectId)row.Id;
            SetupEditMode(row);
        }

        // Remove cliente
        private void Remove(object sender, RoutedEventArgs e)
        {
            if (ClientModel.Remove(SelectedId))
                MessageBox.Show("Cliente removido!");
            else
                MessageBox.Show("Erro ao remover Cliente!");

            EditMode = false;
            btn_add.Content = "Adicionar";
            btn_remove.Visibility = Visibility.Collapsed;
            ClearTxts();
            UpdateTable();
        }

        // Prepara a tela para modo de edição
        private void SetupEditMode(ClientModel model)
        {
            txt_name.Text = model.Name;
            txt_city.Text = model.Address.City;
            txt_district.Text = model.Address.District;
            txt_street.Text = model.Address.Street;
            txt_number.Text = model.Address.Number.ToString();
            txt_phone.Text = model.Phone;
            txt_email.Text = model.Email;
            dt_birthdate.SelectedDate = model.BirthDate;

            btn_remove.Visibility = Visibility.Visible;
            btn_add.Content = "Atualizar";
            EditMode = true;
        }

        // Carrega a tabela com os clientes existentes 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var rawclients = MovieStoreManager.GetAllClients();
            Clients = new ObservableCollection<ClientModel>(rawclients);
            table.ItemsSource = Clients;
        }
    }
}
