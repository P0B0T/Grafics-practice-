using Grafics.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Grafics
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        Dictionary<int, Student> listStudents = new Dictionary<int, Student>();

        public Settings()
        {
            InitializeComponent();

            Width += 30;
            Height += 20;
        }

        void OutputInf()
        {
            lbStudents.Items.Clear();

            listStudents = JsonConvert.DeserializeObject<Dictionary<int, Student>>(System.IO.File.ReadAllText("dictionary.json"));

            foreach (var student in listStudents)
            {
                string inf = (student.Key + " " + student.Value.FullName).ToString();

                lbStudents.Items.Add(inf);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OutputInf();
        }

        private void lbStudents_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            foreach (var student in listStudents)
            {
                if (lbStudents.SelectedItem == null)
                    return;

                if ((student.Key + " " + student.Value.FullName) == lbStudents.SelectedItem.ToString())
                {
                    TrueMessageBox message = new TrueMessageBox();
                    message.View = "Information";
                    message.Title = "Подробная информация";
                    message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Информация.png", UriKind.RelativeOrAbsolute));
                    message.MessageText = $"{student.Value.FullName}\nКол-во сделанных работ: {student.Value.CountWorksDone}\nЗаработок: " + student.Value.Sum.ToString("0.#############################");
                    message.ShowMessageWindow();

                    return;
                }
            }
        }

        void Save()
        {
            System.IO.File.WriteAllText("dictionary.json", JsonConvert.SerializeObject(listStudents, Formatting.Indented));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNumber(tbNumber.Text) == 0 || CheckCountWork(tbCountWork.Text) == 0 || CheckSum(tbSum.Text) == 0 || !CheckFullName()) return;

            if (listStudents.ContainsKey(CheckNumber(tbNumber.Text))) { ErrorMessage.ShowError("Номер уже есть в списке."); return; }

            listStudents.Add(CheckNumber(tbNumber.Text), new Student(tbFullName.Text, CheckCountWork(tbCountWork.Text), CheckSum(tbSum.Text)));

            TrueMessageBox message1 = new TrueMessageBox();
            message1.View = "Information";
            message1.Title = "Добавление";
            message1.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Информация.png", UriKind.RelativeOrAbsolute));
            message1.MessageText = "Успешно.";
            message1.ShowMessageWindow();

            Save();

            OutputInf();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNumber(tbNumber.Text) == 0 || CheckCountWork(tbCountWork.Text) == 0 || CheckSum(tbSum.Text) == 0 || !CheckFullName()) return;

            if (!listStudents.ContainsKey(CheckNumber(tbNumber.Text))) { ErrorMessage.ShowError("Номера нет в списке."); return; }

            listStudents.Remove(CheckNumber(tbNumber.Text));
            listStudents.Add(CheckNumber(tbNumber.Text), new Student(tbFullName.Text, CheckCountWork(tbCountWork.Text), CheckSum(tbSum.Text)));

            TrueMessageBox message1 = new TrueMessageBox();
            message1.View = "Information";
            message1.Title = "Редактирование";
            message1.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Информация.png", UriKind.RelativeOrAbsolute));
            message1.MessageText = "Успешно.";
            message1.ShowMessageWindow();

            Save();

            OutputInf();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (CheckNumber(tbRemoveNumber.Text) == 0) return;

            if (!CheckAvailability(tbRemoveNumber.Text)) return;

            listStudents.Remove(CheckNumber(tbRemoveNumber.Text));

            TrueMessageBox message1 = new TrueMessageBox();
            message1.View = "Information";
            message1.Title = "Удаление";
            message1.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Информация.png", UriKind.RelativeOrAbsolute));
            message1.MessageText = "Успешно.";
            message1.ShowMessageWindow();

            Save();

            OutputInf();
        }

        int CheckNumber(string strNumber)
        {
            if (!int.TryParse(strNumber, out int number)) { ErrorMessage.ShowError("Введите число!"); return 0; }

            if (number <= 0) { ErrorMessage.ShowError("Номер должен быть положительным и целым."); return 0; }

            return number;
        }

        int CheckCountWork(string strCountWork)
        {
            if (!int.TryParse(strCountWork, out int countWork)) { ErrorMessage.ShowError("Количество работ должно быть положительным и целым."); return 0; }

            if (countWork < 0) { ErrorMessage.ShowError("Количество работ должно быть положительным значением."); return 0; }

            return countWork;
        }

        decimal CheckSum(string strSum)
        {
            if (!decimal.TryParse(strSum, out decimal sum)) { ErrorMessage.ShowError("Сумма должна быть числом."); return 0; }

            if (sum < 0) { ErrorMessage.ShowError("Сумма должна быть положительная."); return 0; }

            if(sum.ToString().Contains(','))
                if ((strSum.Length - sum.ToString().IndexOf(',')) > 3) { ErrorMessage.ShowError("После зяпятой должно быть не более 2 знаков"); return 0; }

            return sum;
        }

        bool CheckAvailability(string number)
        {
            if (!listStudents.ContainsKey(CheckNumber(number))) { ErrorMessage.ShowError("Этого номера нет в списке."); return false; }
            
            return true;
        }

        bool CheckFullName()
        {
            if (string.IsNullOrWhiteSpace(tbFullName.Text)) { ErrorMessage.ShowError("Заполните ФИО."); return false; }

            return true;
        }

        private void tbNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (CheckNumber(tbNumber.Text) == 0) return;

            if (!CheckAvailability(tbNumber.Text)) return;

            tbFullName.Text = listStudents[CheckNumber(tbNumber.Text)].FullName;
            tbCountWork.Text = listStudents[CheckNumber(tbNumber.Text)].CountWorksDone.ToString();
            tbSum.Text = listStudents[CheckNumber(tbNumber.Text)].Sum.ToString();
        }

        private void cbSwitch_Checked(object sender, RoutedEventArgs e)
        {
            tbNumber.TextChanged += tbNumber_TextChanged;

            btnAdd.Visibility = Visibility.Hidden;
            btnEdit.Visibility = Visibility.Visible;
        }

        private void cbSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            tbNumber.TextChanged -= tbNumber_TextChanged;

            btnAdd.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Hidden;

            tbFullName.Clear();
            tbCountWork.Clear();
            tbSum.Clear();
        }

        MessageBoxResult ShowMessageYesNo()
        {
            TrueMessageBox message = new TrueMessageBox();
            message.View = "YES/NO";
            message.Title = "Вопрос";
            message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Вопрос.png", UriKind.RelativeOrAbsolute));
            message.MessageText = "Изменение этих настроек может привести к поломке программы.  Продолжить?";
            message.ShowMessageWindow();

            return message.Result;
        }

        private void btnProSettings_Click(object sender, RoutedEventArgs e)
        {
            if (ShowMessageYesNo() == MessageBoxResult.Yes)
            {
                ProSettings settings = new ProSettings();

                settings.ShowDialog();

                OutputInf();
            }
        }
    }
}
