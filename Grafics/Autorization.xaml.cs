using Grafics.Classes;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Grafics
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        Enter enter = new Enter();

        public Autorization()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbLogin.Focus();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in enter.ListUsers)
            {
                if (item.Login == tbLogin.Text && item.Password == pbPassword.Password)
                {
                    DataUser.UserRole = item.Role;

                    MainWindow main = new MainWindow();

                    main.Show();

                    Close();

                    return;
                }
            }

            ErrorMessage.ShowError("Неверный логин или пароль!");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
