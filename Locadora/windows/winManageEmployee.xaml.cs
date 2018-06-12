using Locadora.models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Lógica interna para winManageEmployee.xaml
    /// </summary>
    public partial class winManageEmployee : Window
    {
        ObservableCollection<EmployeeModel> Employees { get; set; }
        ObjectId SelectedId { get; set; }
        bool EditMode = false;

        public winManageEmployee()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                Employees = new ObservableCollection<EmployeeModel>(MovieStoreManager.GetAllEmployees());
                table.Dispatcher.BeginInvoke(new Action(delegate () {
                    table.ItemsSource = Employees;
                }));
            });
        }

        private void txt_number_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = (e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9);
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            AddressModel am = new AddressModel()
            {
                City = txt_city.Text.ToLower(),
                District = txt_district.Text.ToLower(),
                Street = txt_street.Text.ToLower(),
                Number = Convert.ToInt32(txt_number.Text)
            };

            EmployeeModel model = new EmployeeModel()
            {
                Name = txt_name.Text.ToLower(),
                Login = txt_login.Text,
                Address = am,
                Phone = txt_phone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""),
                Role = txt_role.Text.ToLower(),
                BirthDate = (DateTime)dt_birthdate.SelectedDate,
                HiringDate = (DateTime)dt_hiringdate.SelectedDate,
                Password = txt_password.Password,
                Salary = Convert.ToInt32(txt_salary.Text)
            };

            if (!EditMode)
            {
                if (EmployeeModel.Save(model))
                    MessageBox.Show("Funcionário adicionado!");
                else
                    MessageBox.Show("Erro ao adicionar funcionário!");
            }
            else
            {
                model.Id = SelectedId;
                if (EmployeeModel.Replace(model))
                    MessageBox.Show("Funcionário atualizado!");
                else
                    MessageBox.Show("Erro ao atualizar funcionário!");

                EditMode = false;
                btn_add.Content = "Adicionar";
                btn_remove.Visibility = Visibility.Collapsed;
            }

            ClearTxts();
            UpdateTable();
        }

        private void txt_salary_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = (e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9);
        }

        private void UpdateTable()
        {
            Employees = new ObservableCollection<EmployeeModel>(MovieStoreManager.GetAllEmployees());
            table.ItemsSource = Employees;
        }

        private void SetupEditMode(EmployeeModel model)
        {
            txt_name.Text = model.Name;
            txt_city.Text = model.Address.City;
            txt_district.Text = model.Address.District;
            txt_street.Text = model.Address.Street;
            txt_number.Text = model.Address.Number.ToString();
            txt_phone.Text = model.Phone;
            dt_birthdate.SelectedDate = model.BirthDate;
            txt_role.Text = model.Role;
            txt_password.Password = model.Password;
            dt_hiringdate.SelectedDate = model.HiringDate;
            txt_salary.Text = model.Salary.ToString();
            txt_login.Text = model.Login;

            btn_remove.Visibility = Visibility.Visible;
            btn_add.Content = "Atualizar";
            EditMode = true;
        }

        private void table_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tab.SelectedIndex = 0;

            var row = (EmployeeModel)table.SelectedItem;

            //string id = row.Id.ToString();

            //var employee = MovieStoreManager.GetEmployee(id);

            SelectedId = (ObjectId)row.Id;
            SetupEditMode(row);
        }

        private void ClearTxts()
        {
            txt_name.Clear();
            txt_city.Clear();
            txt_district.Clear();
            txt_street.Clear();
            txt_number.Clear();
            txt_phone.Clear();
            dt_birthdate.SelectedDate = null;
            txt_role.Clear();
            txt_password.Clear();
            dt_hiringdate.SelectedDate = null;
            txt_salary.Clear();
            txt_login.Clear();
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (EmployeeModel.Remove(SelectedId))
                MessageBox.Show("Funcionário removido!");
            else
                MessageBox.Show("Erro ao remover funcionário!");

            EditMode = false;
            btn_add.Content = "Adicionar";
            btn_remove.Visibility = Visibility.Collapsed;
            ClearTxts();
            UpdateTable();
        }
    }
}
