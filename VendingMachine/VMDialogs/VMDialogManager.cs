using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using VendingMachine.Core.Configuration;
using VendingMachine.Core.Misc;

namespace VendingMachine.VMDialogs
{
    public class VMDialogManager
    {
        public static void ShowInfoMessage(string message)
        {
            ShowInfoMessage("Info", message);
        }

        public static async void ShowInfoMessage(string title, string message)
        {
            await ShowMessage(title, message, MessageDialogStyle.Affirmative);
        }

        public static void ShowExceptionMessage(Exception e)
        {
            string message = e.Message;
            string exception = e.ToString();
            ShowExceptionMessage(message, exception);
        }

        public static async void ShowExceptionMessage(String message, string exception)
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            if (metroWindow.IsLoaded)
            {
                await DialogManager.ShowMetroDialogAsync(metroWindow, new HOExceptionDialog(message, metroWindow));
            }
            else
            {
                MessageBox.Show(message);
            }
            Console.WriteLine(exception);
            Logger.Log(exception);
        }

        public static async Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            await metroWindow.ShowMessageAsync(title, message, dialogStyle);
            return MessageDialogResult.Affirmative;
        }

        public static async void ShowInputMessage(string title, string message, ConfigPropertyType propertyType)
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            await DialogManager.ShowMetroDialogAsync(metroWindow, new HOInputDialog(metroWindow));
        }

        public static async void ShowClosingDialog(MainWindow window)
        {
            MessageDialogStyle bt;
            String messageBoxText;
            String titleMessage = "Zamykanie programu";
            MessageDialogResult result;
            MetroDialogSettings diagSettings = new MetroDialogSettings();
            diagSettings.AnimateShow = true;
            bt = MessageDialogStyle.AffirmativeAndNegative;
            messageBoxText = "Napewno chcesz wyjść z programu?";
            diagSettings.AffirmativeButtonText = "Wyjdź";
            diagSettings.NegativeButtonText = "Anuluj";
            result = await window.ShowMessageAsync(titleMessage, messageBoxText, bt, diagSettings);
            if (result == MessageDialogResult.Affirmative)
            {
                Logger.Log(titleMessage);
                Application.Current.Shutdown();
            }
        }
    }
}
