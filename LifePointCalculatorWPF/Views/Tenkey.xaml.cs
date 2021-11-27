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

namespace LifePointCalculatorWPF.Views
{
    /// <summary>
    /// Tenkey.xaml の相互作用ロジック
    /// </summary>
    public partial class Tenkey : Window
    {
        public Tenkey(int lifePoint)
        {
            InitializeComponent();
            FrontTextBox.Text = lifePoint.ToString();
        }

        /// <summary>
        /// 電卓の文字列
        /// </summary>
        public int Result { get; set; } = 0;

        /// <summary>
        /// 閉じるときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Result = (int.Parse(FrontTextBox.Text) - int.Parse(BackTextBox.Text));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = (string)((Button)e.Source).Content;
            BackTextBox.Text += input;
        }
    }
}
