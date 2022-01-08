using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace LifePointCalculatorWPF.Behavior
{

        public class InputComplementBehavior : Behavior<TextBox>
        {
            private string currentCandidate;
            private Brush defaultBrush;

            // 補完候補文字列コレクションの依存関係プロパティ
            public static DependencyProperty InputCandidatesProperty;
            static InputComplementBehavior()
            {
                InputComplementBehavior.InputCandidatesProperty =
                    DependencyProperty.Register(
                    "InputCandidates",
                    typeof(IEnumerable),
                    typeof(InputComplementBehavior),
                    new FrameworkPropertyMetadata(String.Empty));
            }
            public IEnumerable InputCandidates
            {
                get { return (IEnumerable)GetValue(InputComplementBehavior.InputCandidatesProperty); }
                set { SetValue(InputComplementBehavior.InputCandidatesProperty, value); }
            }

            // Behavior<T>としての接合部
            protected override void OnAttached()
            {
                this.AssociatedObject.Initialized += AssociatedObject_Initialized;
                this.AssociatedObject.TextChanged += AssociatedObject_TextChanged;
                this.AssociatedObject.KeyDown += AssociatedObject_KeyDown;
                base.OnAttached();
            }
            protected override void OnDetaching()
            {
                this.AssociatedObject.Initialized -= AssociatedObject_Initialized;
                this.AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
                this.AssociatedObject.KeyDown -= AssociatedObject_KeyDown;
                base.OnDetaching();
            }

            // 初期化時にデフォルト背景を取得、保持しておく
            void AssociatedObject_Initialized(object sender, EventArgs e)
            {
                TextBox textBox = sender as TextBox;
                defaultBrush = textBox.Background;
            }

            // 補完処理
            void AssociatedObject_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
            {
                // 候補文字がある状態でTabキーが押されれば補完処理
                if (!string.IsNullOrEmpty(currentCandidate) && Key.Tab == e.Key)
                {
                    TextBox textBox = sender as TextBox;
                    textBox.Text = currentCandidate;
                    textBox.CaretIndex = textBox.Text.Length;
                    e.Handled = true;
                }
            }

            // 文字入力の度に候補を探し、背景描画
            void AssociatedObject_TextChanged(object sender, TextChangedEventArgs e)
            {
                TextBox textBox = sender as TextBox;
                currentCandidate = GetMostProbableComplementText(textBox.Text);
                if (!String.IsNullOrEmpty(currentCandidate))
                {
                    // 候補文字があるのでグレー背景で表示する
                    textBox.Background = GetComplementTextBackgroundBrush();
                }
                else
                {
                    // 元に戻す
                    textBox.Background = defaultBrush;
                }
            }

            private string GetMostProbableComplementText(string currentText)
            {
                // 先頭からの一致を探す(カスタマイズ要素)
                if (string.IsNullOrEmpty(currentText))
                {
                    return String.Empty;
                }
                foreach (var item in InputCandidates)
                {
                    string text = item as String;
                    if (!String.IsNullOrEmpty(text) && text.StartsWith(currentText))
                    {
                        return text;
                    }
                }
                return String.Empty;
            }

            private Brush GetComplementTextBackgroundBrush()
            {
                TextBox textBoxVisual = new TextBox()
                {
                    Text = currentCandidate,
                    BorderBrush = null,
                    Foreground = new SolidColorBrush(Colors.Gray),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center
                };
                return new VisualBrush(textBoxVisual)
                {
                    Stretch = Stretch.None,
                    TileMode = TileMode.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Top
                };
            }

        }
    
}
