using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using VendingMachine.Core.Configuration;
using VendingMachine.Core.Misc;
using VendingMachine.Core.Products;

namespace VendingMachine.VMDialogs
{
    /// <summary>
    /// Manager of program all dialog boxes
    /// </summary>
    public class VMDialogManager
    {
        /// <summary>
        /// Shows Info Dialog Box with OK button
        /// </summary>
        /// <param name="message">Message to display</param>
        public static void ShowInfoMessage(string message)
        {
            ShowInfoMessage("Info", message);
        }

        /// <summary>
        /// Shows Info Dialog Box with OK button
        /// </summary>
        /// <param name="title">Title of Box</param>
        /// <param name="message">Message to display</param>
        public static async void ShowInfoMessage(string title, string message)
        {
            await ShowMessage(title, message, MessageDialogStyle.Affirmative);
        }

        /// <summary>
        /// Shows Message with only confirm action
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="dialogStyle"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowMessage(string title, string message, MessageDialogStyle dialogStyle)
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            if (metroWindow.IsLoaded)
            {
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
                await metroWindow.ShowMessageAsync(title, message, dialogStyle);
            }
            else
            {
                MessageBox.Show(message);
            }
            return MessageDialogResult.Affirmative;
        }

        /// <summary>
        /// Shows Input Coin Dialog Box
        /// </summary>
        public static async void ShowInputCoinDialog()
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            await DialogManager.ShowMetroDialogAsync(metroWindow, new InputCoinDialog(metroWindow));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        public static async void ShowDeployProductMessage(Product product)
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            await DialogManager.ShowMetroDialogAsync(metroWindow, new VMDeployProductDialog(product,metroWindow));
        }

        /// <summary>
        /// Show exception message
        /// </summary>
        /// <param name="e">Exception</param>
        public static void ShowExceptionMessage(Exception e)
        {
            string message = e.Message;
            string exception = e.ToString();
            ShowExceptionMessage(message, exception);
        }

        /// <summary>
        /// Show exception message
        /// </summary>
        /// <param name="message">Additional message</param>
        /// <param name="exception">Exception</param>
        public static async void ShowExceptionMessage(String message, string exception)
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            if (metroWindow.IsLoaded)
            {
                await DialogManager.ShowMetroDialogAsync(metroWindow, new ExceptionDialog(message, metroWindow));
            }
            else
            {
                MessageBox.Show(message);
            }
            Console.WriteLine(exception);
            Logger.Log(exception);
        }

        /// <summary>
        /// Shows input dialog box to input value
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="propertyType"></param>
        public static async void ShowInputMessage(string title, string message, ConfigPropertyType propertyType)
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            await DialogManager.ShowMetroDialogAsync(metroWindow, new VMInputDialog(metroWindow));
        }

        /// <summary>
        /// Shows servis dialog box
        /// </summary>
        public static async void ShowServiceDialog()
        {
            var metroWindow = (Application.Current.MainWindow as MainWindow);
            await DialogManager.ShowMetroDialogAsync(metroWindow, new ServisInputDialog(metroWindow));
        }

        /// <summary>
        /// Shows closing box to confirm exiting
        /// </summary>
        /// <param name="window"></param>
        public static async void ShowClosingDialog(MainWindow window)
        {
            MessageDialogStyle bt;
            String messageBoxText;
            String titleMessage = "Zamykanie programu";
            MessageDialogResult result;
            MetroDialogSettings diagSettings = new MetroDialogSettings();
            diagSettings.AnimateShow = true;
            bt = MessageDialogStyle.AffirmativeAndNegative;
            messageBoxText = "Jesteś pewny, że chcesz wyjść z programu?";
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
