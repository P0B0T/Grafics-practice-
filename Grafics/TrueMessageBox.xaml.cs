using System.Windows;

namespace Grafics
{
    /// <summary>
    /// Логика взаимодействия для TrueMessageBox.xaml
    /// </summary>
    public partial class TrueMessageBox : Window
    {
        public TrueMessageBox()
        {
            InitializeComponent();
            Height += 30;
        }

        public string View;

        public string MessageText { get; set; }

        public MessageBoxResult Result { get; private set; }

        public MessageBoxResult ShowMessageWindow()
        {
            Output.Text = MessageText;
            ShowDialog();
            return Result;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            switch (View)
            {
                case "Information":
                    OK.Visibility = Visibility.Visible;
                    break;

                case "OK/CANSEL":
                    OK.Visibility = Visibility.Visible;
                    Cansel.Visibility = Visibility.Visible;
                    break;

                case "YES/NO":
                    Yes.Visibility = Visibility.Visible;
                    No.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Cansel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
        }
    }
}
