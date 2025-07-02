using MySql.Data.MySqlClient;
using Support_L_PACK.Factory;
using Support_L_PACK.Halpers;
using Support_L_PACK.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Support_L_PACK.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<ICartridge> Cartridges { get; set; } = new ObservableCollection<ICartridge>();

        private string _inputModel;
        public string InputModel
        {
            get => _inputModel;
            set
            {
                _inputModel = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Команды, привязанные к кнопкам в интерфейсе.
        /// По нажатию на соответствующую кнопку вызывается метод создания и добавления картриджа.
        /// </summary>
        public ICommand AddBlackCommand { get; }
        public ICommand AddColorCommand { get; }

        /// <summary>
        /// Конструктор, в котором задаётся логика для кнопок.
        /// </summary>
        public MainViewModel()
        {

            AddBlackCommand = new RelayCommand(_ =>
            {
                var creator = new BlackCartridgeCreator(); // создаём фабрику для лазерных
                var cartridge = creator.Create(InputModel);  // создаём картридж, передаём модель из текстбокса
                Cartridges.Add(cartridge);                   // добавляем в список
            });

            AddColorCommand = new RelayCommand(_ =>
            {
                var creator = new ColorCartridgeCreator();
                var cartridge = creator.Create(InputModel);
                Cartridges.Add(cartridge);
            });
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await DBConnection.InitAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при инициализации базы данных: " + ex.Message);
            }
        }

        private async void AddCartridgeButton_Click(object sender, RoutedEventArgs e)
        {
            string model = CartridgeModelTextBox.Text.Trim();
            if (!int.TryParse(CartridgeModelTextBox.Text.Trim(), out int quantity))
            {
                MessageBox.Show("Введите корректное количество картриджей.");
                return;
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                MessageBox.Show("Введите модель картриджа.");
                return;
            }

            try
            {
                string query = "INSERT INTO cartridges (model, quantity) VALUES (@model, @quantity)";
                MySqlParameter[] parameters = new MySqlParameter[]
                {
                    new MySqlParameter("@model", model),
                    new MySqlParameter("@quantity", quantity)
                };

                int rowsAffected = await DBConnection.ExecuteNonQueryAsync(query, parameters); // предполагаем, что методы находятся в классе DbHelper

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Картридж добавлен.");
                }
                else
                {
                    MessageBox.Show("Не удалось добавить картридж.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
