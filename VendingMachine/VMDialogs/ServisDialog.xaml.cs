
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using VendingMachine.Core.Configuration;

namespace VendingMachine.VMDialogs
{
    partial class ServisDialog : BaseMetroDialog
    {
        private bool OK_Clicked=false;

        internal ServisDialog(MetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }
        internal ServisDialog(MetroWindow parentWindow, MetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            InitializeComponent();
            MainVMWindow = (MainWindow)parentWindow;
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            OK_Clicked = true;
            RequestCloseAsync();
        }

        /// <summary>
        /// Window of app
        /// </summary>
        public MainWindow MainVMWindow { get; set; }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestCloseAsync();
        }

        public bool wasOKClicked()
        {
            return OK_Clicked;
        }
    }
}
