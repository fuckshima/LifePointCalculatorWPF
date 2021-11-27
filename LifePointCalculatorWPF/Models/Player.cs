using LifePointCalculatorWPF.ViewModels;
using LifePointCalculatorWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePointCalculatorWPF.Models
{
    internal class Player : ViewModelBase
    {
        /// <summary>
        /// ライフポイント
        /// </summary>
        private int lifePoint = 8000;
        public int LifePoint 
        { 
            get => lifePoint; 
            set => SetProperty(ref lifePoint, value); 
        }

        private RelayCommand? _showTenkeyCommand;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand ShowTenkeyCommand
        {
            get
            {
                return _showTenkeyCommand ??= new RelayCommand(
                    () =>
                    {
                        var tenkey = new Tenkey(LifePoint)
                        {
                            Owner = App.Current.MainWindow,
                            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner
                        };
                        tenkey.ShowDialog();
                            
                        LifePoint = tenkey.Result;

                    });
            }
        }
    }
}
