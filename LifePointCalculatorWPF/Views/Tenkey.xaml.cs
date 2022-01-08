using System.Windows;
using System.Windows.Controls;

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
            Result = lifePoint;
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

        }

        // 未入力状態か
        bool isNeverEnterd = true;
        /// <summary>
        /// [数字]ボタン押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // 未入力状態の場合、テキストボックスをクリアする
            if (isNeverEnterd)
            {
                isNeverEnterd = false;
                BackTextBox.Text = string.Empty;
            }

            // テキストボックスへ入力値反映
            if ((e.Source as Button)?.Content is string input)
            {
                BackTextBox.Text += input;
            }
        }

        /// <summary>
        /// [=]ボタン押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            Result -= int.Parse(BackTextBox.Text);
            Close();
        }

        /// <summary>
        /// []ボタン押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
