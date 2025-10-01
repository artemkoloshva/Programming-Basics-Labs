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
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        private Button[] xButtons;
        private int cycleTry = 0;
        public Captcha()
        {
            InitializeComponent();
            xButtons = new Button[9]{ XButton1, XButton2, XButton3, XButton4, XButton5, XButton6, XButton7, XButton8, XButton9 };
            FillButtonsX(4);
        }
        private void onClickXButton(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.Content = "";
            if (GetXFromButtons() == 0)
            {
                FillButtonsX(4);
                CycleTrysTB.Text = $"Осталось: {++cycleTry}/1000";
                if (cycleTry == 1)
                    ShowIndiaVirusWindow();
            }   
        }

        /// <summary>
        /// Показывает окно индуского вируса
        /// </summary>
        private void ShowIndiaVirusWindow()
        {
            MessageBox.Show("Здравствуйте, я - индусский вирус. В виду бедности моего" +
                        "\r\nсоздателя и общей отсталости развития высоких технологий" +
                        "\r\nнашей страны, я не в силах причинить какой-либо вред вашему" +
                        "\r\nкомпьютеру. Пожалуйста, сотрите сами несколько самых нужных" +
                        "\r\nвам файлов, а затем разошлите меня по почте своим друзьям." +
                        "\r\nБлагодарю за понимание и сотрудничество.");
        }

        /// <summary>
        /// Заполнение с минимальным колличеством Х
        /// </summary>
        /// <param name="minCountX"></param>
        private void FillButtonsX(int minCountX)
        {
            do
            {
                FillButtonsContent();
            }
            while (GetXFromButtons() < minCountX);
        }

        /// <summary>
        /// Рандомно заполняет кнопки элементами Х
        /// </summary>
        private void FillButtonsContent()
        {
            System.Random random = new System.Random();

            foreach (Button button in xButtons)
            {
                switch (random.Next(2))
                {
                    case 0:
                        button.Content = "X";
                        break;
                    case 1:
                        button.Content = "";
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Получает колличество элементов Х содержится в кнопках
        /// </summary>
        /// <returns></returns>
        private int GetXFromButtons()
        {
            int counterX = 0;

            foreach (Button button in xButtons)
                if (button.Content == "X")
                    counterX++;

            return counterX;
        }
    }
}
