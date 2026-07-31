using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab6
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private static bool isShowCaptcha = true;
        private TetrisWindow tetrisWindow;
        private Captcha captcha;
        private GuessTheAnswerWindow guessTheAnswerWindow;
        private ComparingArraySortsWindow comparingArraySortsWindow;
        private AuthorWindow authorWindow;
        private ExitWindow exitWindow;
        private string pathImageGuess = @"D:\Учеба\Самарский уневерситет\Основы программирования\Лабораторные работы\Lab6\Lab6\Images\matan.png";
        private string pathImageAuthor = @"D:\Учеба\Самарский уневерситет\Основы программирования\Лабораторные работы\Lab6\Lab6\Images\oscar2.jpg";
        private string pathImageArray = @"D:\Учеба\Самарский уневерситет\Основы программирования\Лабораторные работы\Lab6\Lab6\Images\sort.jpg";
        private string pathImageTetris = @"D:\Учеба\Самарский уневерситет\Основы программирования\Лабораторные работы\Lab6\Lab6\Images\tetrismenu1.jpg";
        private string pathImageExit = @"D:\Учеба\Самарский уневерситет\Основы программирования\Лабораторные работы\Lab6\Lab6\Images\exit.jpg";

        public Menu()
        {
            InitializeComponent();
            if (isShowCaptcha)
            {
                captcha = new Captcha();
                captcha.ShowDialog();
                isShowCaptcha = false;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /// <summary>
        /// При наведении меняет фон на мем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseEnter_InputMeme(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "GuessTheAnswerButton":
                    memeImage.Source = BitmapFrame.Create(new Uri(pathImageGuess));
                    break;
                case "OutputAutorInfoButton":
                    memeImage.Source = BitmapFrame.Create(new Uri(pathImageAuthor));
                    break;
                case "ComparingArraySortsButton":
                    memeImage.Source = BitmapFrame.Create(new Uri(pathImageArray));
                    break;
                case "TetrisButton":
                    memeImage.Source = BitmapFrame.Create(new Uri(pathImageTetris));
                    break;
                case "ExitButton":
                    memeImage.Source = BitmapFrame.Create(new Uri(pathImageExit));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Открывает окно тетриса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TetrisButton_Click(object sender, RoutedEventArgs e)
        {
            tetrisWindow = new TetrisWindow();
            this.Close();
            tetrisWindow.ShowDialog();
        }

        /// <summary>
        /// Открывает окно работы с массивами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComparingArraySortsButton_Click(object sender, RoutedEventArgs e)
        {
            comparingArraySortsWindow = new ComparingArraySortsWindow();
            this.Close();
            comparingArraySortsWindow.ShowDialog();
        }

        /// <summary>
        /// открывает окно "Авторы"
        /// </summary>
        private void AuthorButton_Click(object sender, RoutedEventArgs e)
        {
            authorWindow = new AuthorWindow();
            this.Close();
            authorWindow.ShowDialog();
        }

        /// <summary>
        /// Открывает окно Матанализа
        /// </summary>
        private void GuessTheAnswerButton_Click(object sender, RoutedEventArgs e)
        { 
            guessTheAnswerWindow = new GuessTheAnswerWindow();
            this.Close();
            guessTheAnswerWindow.ShowDialog();
        }

        /// <summary>
        /// Открывает окно выхода из лабы
        /// </summary>
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            exitWindow = new ExitWindow();
            this.Close();
            exitWindow.ShowDialog();
        }
    }
}
