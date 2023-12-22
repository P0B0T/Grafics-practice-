using System.Diagnostics;
using System.Windows;
namespace Grafics
{
    /// <summary>
    /// Логика взаимодействия для ProSettings.xaml
    /// </summary>
    public partial class ProSettings : Window
    {
        public ProSettings()
        {
            InitializeComponent();

            Height += 30;
        }

        private void btnEditRole_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", "rights.json");
        }

        private void btnEditListStudents_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", "dictionary.json");
        }

        private void btnEditListUsers_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe", "listUsers.json");
        }
    }
}
