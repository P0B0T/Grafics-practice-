using Grafics.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Grafics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<int, Student> students = new Dictionary<int, Student>();

        DispatcherTimer timer = new DispatcherTimer();
        
        int columnCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            Height += 20;
            Width += 20;
        }

        void ClearTb()
        {
            tbName.Clear();
            tbCountWorks.Clear();
            tbSum.Clear();

            gInf.Visibility = Visibility.Hidden;
        }

        private void tbNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbNumber.Text == "") 
            {
                tbNumber.Clear();
                ClearTb();
                
                return; 
            }

            if (!int.TryParse(tbNumber.Text, out int number))
            {
                ErrorMessage.ShowError("Введите целое число!");

                tbNumber.Clear();

                return;
            }

            foreach (var item in students)
            {
                if (!students.TryGetValue(number, out Student student))
                {
                    ErrorMessage.ShowError($"Этого номера в списке нет. Всего: {students.Count}");

                    ClearTb();

                    return;
                }

                gInf.Visibility = Visibility.Visible;
                tbName.Text = student.FullName;
                tbCountWorks.Text = student.CountWorksDone.ToString();
                tbSum.Text = student.Sum.ToString("0.#############################");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingData();

            Check();

            SearchBestAndWorst();

            StartingDraw();
        }

        void LoadingData()
        {
            students = JsonConvert.DeserializeObject<Dictionary<int, Student>>(System.IO.File.ReadAllText("dictionary.json"));
        }

        void Check()
        {
            if (DataUser.UserRole == DataUser.Rights)
                btnSettings.Visibility = Visibility.Visible;
        }

        void SearchBestAndWorst()
        {
            tbBest.Text = students.Aggregate((s1, s2) => s1.Value.Sum > s2.Value.Sum ? s1 : s2).Value.FullName;
            tbWorst.Text = students.Aggregate((s1, s2) => s1.Value.Sum < s2.Value.Sum ? s1 : s2).Value.FullName;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();

            settings.ShowDialog();

            LoadingData();

            SearchBestAndWorst();

            cGrafic.Children.Clear();

            StartingDraw();
        }

        void DrawColumn()
        {
            decimal maxCount = students.Values.Max(student => student.Sum);

            if (columnCount == students.Count)
            {
                timer.Stop();

                btnSettings.IsEnabled = true;

                return;
            }

            var student = students.Values.ElementAt(columnCount);

            double columnWidth = 30;
            double xPosition = 10 + columnCount * (columnWidth + 10);
            double columnHeight = (double)(student.Sum / maxCount) * cGrafic.ActualHeight;

            Rectangle rectangle = new Rectangle
            {
                Width = columnWidth,
                Height = columnHeight,
                Fill = Brushes.DarkBlue
            };

            Canvas.SetTop(rectangle, cGrafic.ActualHeight - columnHeight);
            Canvas.SetLeft(rectangle, xPosition);

            TextBlock studentInf = new TextBlock
            {
                Text = student.FullName + " - " + student.Sum.ToString("0.#############################") + " руб.",
                RenderTransform = new RotateTransform(-90)
            };

            double textLeft = xPosition + (columnWidth - studentInf.ActualHeight) / 2 - 10;

            Canvas.SetTop(studentInf, cGrafic.ActualHeight);
            Canvas.SetLeft(studentInf, textLeft);

            cGrafic.Children.Add(rectangle);
            cGrafic.Children.Add(studentInf);

            columnCount++;
        }

        void StartingDraw()
        {
            columnCount = 0;

            btnSettings.IsEnabled = false;

            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += (s, e) => DrawColumn();
            timer.Start();
        }
    }
}
