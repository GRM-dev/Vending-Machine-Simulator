
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;
using VendingMachine.Core;
using VendingMachine.Core.Configuration;

namespace VendingMachine.VMDialogs
{
    /// <summary>
    /// Input Coin Dialog Box in which client throw coin
    /// </summary>
    partial class InputCoinDialog : BaseMetroDialog
    {
        private bool OK_Clicked = false;

        internal InputCoinDialog(MetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }
        internal InputCoinDialog(MetroWindow parentWindow, MetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            InitializeComponent();
            MainVMWindow = (MainWindow)parentWindow;
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            OK_Clicked = true;
            string coinS = TextInputBox.Text;
            if (CoinController.isCoinCorrect(coinS))
            {
                double coin = Convert.ToDouble(coinS);
                Console.WriteLine(coin);
                CoinController.ThrowInCoin(coin);
                string consoleTitle = "Stan konta: ";
                string currentTempDepo = CoinController.TempCoinDepository.ToString();
                MainVMWindow.VMMainPage.Console_1.Text = consoleTitle + currentTempDepo;
                RequestCloseAsync();
            }
            else
            {
                InfoBlock.Text = "Zła moneta!  Przyjmuje tylko: 10,20,50gr 1,2,5zł";
            }
            
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestCloseAsync();
        }

        /// <summary>
        /// Ensures if money was throw in
        /// </summary>
        /// <returns></returns>
        public bool wasThrowCoinButtonClicked()
        {
            return OK_Clicked;
        } 
        
        /// <summary>
        /// Window of app
        /// </summary>
        public MainWindow MainVMWindow { get; set; }
    }
}
