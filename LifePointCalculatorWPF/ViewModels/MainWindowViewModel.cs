using LifePointCalculatorWPF.Models;
using LifePointCalculatorWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePointCalculatorWPF.ViewModels
{
    internal class MainWindowViewModel: ViewModelBase
    {
        /// <summary>
        /// プレイヤー1
        /// </summary>
        Player _player1 = new();
        public Player Player1
        {
            get => _player1;
            set => SetProperty(ref _player1, value);
        }

        /// <summary>
        /// プレイヤー2
        /// </summary>
        Player _player2 = new();
        public Player Player2
        {
            get => _player2;
            set => SetProperty(ref _player2, value);
        }

        public MainWindowViewModel()
        {
        }
    }
}
