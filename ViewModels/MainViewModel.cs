using Support_L_PACK.Factory;
using Support_L_PACK.Halpers;
using Support_L_PACK.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
