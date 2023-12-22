using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Grafics.Classes
{
    public class ErrorMessage
    {
        public static void ShowError(string error)
        {
            TrueMessageBox message = new TrueMessageBox();
            message.View = "Information";
            message.Title = "Ошибка";
            message.Icon = BitmapFrame.Create(new Uri("./Icons and pictures/Ошибка.png", UriKind.RelativeOrAbsolute));
            message.MessageText = error;
            message.ShowMessageWindow();
        }
    }
}
