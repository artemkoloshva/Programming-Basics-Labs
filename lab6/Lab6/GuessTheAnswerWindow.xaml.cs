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

namespace Lab6
{

    public partial class GuessTheAnswerWindow : Window
    {
        private double resultFunction;
        private byte trys;
        public GuessTheAnswerWindow()
        {
            InitializeComponent();
            BreakTheText();
        }

        /// <summary>
        /// Проверка ответа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            if(InputControl.ControlDoubleTextBox(AnswerTextBox, "#FF25252A") && trys > 0)
            {
                double answer = double.Parse(AnswerTextBox.Text);
                if(resultFunction != answer)
                {
                    UpdateTrysBar(--trys);
                    AnswerTextBox.Text = "";
                }
                else
                {
                    WinOrLoseTB.Visibility = Visibility.Visible;
                    SolidColorBrush scb = new SolidColorBrush(Colors.Yellow);
                    WinOrLoseTB.Foreground = scb;
                    BackButton.Content = "Уйти довольным";
                    WinOrLoseTB.Text = "Сессия сдана!!!";
                }
                if (trys == 0)
                {
                    WinOrLoseTB.Visibility = Visibility.Visible;
                    SolidColorBrush scb = new SolidColorBrush(Colors.Red);
                    WinOrLoseTB.Foreground = scb;
                    BackButton.Content = "Плакать";
                    WinOrLoseTB.Text = "ПОТРАЧЕНО!";
                }
            }
        }

        /// <summary>
        /// Посчитать функцию
        /// </summary>
        private void CalculeteBatton_Click(object sender, RoutedEventArgs e)
        {
            if (InputControl.ControlDoubleTextBox(AlfaTextBox, "#FF25252A") && 
                InputControl.ControlDoubleTextBox(SigmaTextBox, "#FF25252A") && 
                InputControl.ControlByteTextBox(TrysTextBox, "#00542E2E"))
            {
                resultFunction = GuessTheAnswer.CalculationFunction(double.Parse(AlfaTextBox.Text), double.Parse(SigmaTextBox.Text));
                trys = byte.Parse(TrysTextBox.Text);
                if (trys == 0) trys++;

                DemidovichButton.Visibility = Visibility.Collapsed;
                DemidovichTB.Visibility = Visibility.Collapsed;
                DemidovichTB2.Visibility = Visibility.Collapsed;
                TrysTextBox.Visibility = Visibility.Collapsed;
                TrysTB.Visibility = Visibility.Collapsed;
                CalculeteBatton.Visibility = Visibility.Collapsed;
                AlfaTB.Visibility = Visibility.Collapsed;
                AlfaTextBox.Visibility = Visibility.Collapsed;
                SigmaTB.Visibility = Visibility.Collapsed;
                SigmaTextBox.Visibility = Visibility.Collapsed;
                TrysBarTB.Visibility = Visibility.Visible;
                AnswerTextBox.Visibility = Visibility.Visible;
                AnswerTB.Visibility = Visibility.Visible;
                CheckButton.Visibility = Visibility.Visible;

                UpdateTrysBar(trys);
            }
        }

        /// <summary>
        /// Вы учите матанализ и весь текст для вас становится адекватным
        /// </summary>
        private void Demidovich_Click(object sender, RoutedEventArgs e)
        {
            RestoreText();
            DemidovichTB2.Visibility = Visibility.Visible;
            DemidovichButton.Visibility = Visibility.Collapsed;
            DemidovichTB.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Возвращает пользователя в меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обновляет попытки на экране
        /// </summary>
        /// <param name="trys"></param>
        private void UpdateTrysBar(byte trys)
        {
            TrysBarTB.Text = $"Попыток осталось: {trys}";
        }

        /// <summary>
        /// Ломает текст в окне
        /// </summary>
        private void BreakTheText()
        {
            FunctionTB.Text = "世(界, 上) = (最 · 好上的) ÷ (实界 + 验)";
            TrysTB.Text = "复试试次数 次数复试次数 试数:";
            AlfaTB.Text = "鱼水生 (界)";
            SigmaTB.Text = "活沸水 (上)";
            CalculeteBatton.Content = "好的上";
            AnswerTB.Text = "正在 钻橙 色玉米";
            CheckButton.Content = "成 熟 的 梨";
        }

        /// <summary>
        /// Нормализует текст
        /// </summary>
        private void RestoreText()
        {
            FunctionTB.Text = "f(α, σ) = (π · lnσ⁵) ÷ (sinα + 1)";
            TrysTB.Text = "Введите количество попыток:";
            AlfaTB.Text = "Альфа (α)";
            SigmaTB.Text = "Сигма (σ)";
            CalculeteBatton.Content = "Посчитать";
            AnswerTB.Text = "Введите предполагаемый ответ";
            CheckButton.Content = "П р о в е р и т ь";
        }
    }
}
