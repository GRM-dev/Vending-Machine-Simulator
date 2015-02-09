
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using VendingMachine.Core.Configuration;

namespace VendingMachine.VMDialogs
{
    partial class ServisInputDialog : BaseMetroDialog
    {
        private bool OK_Clicked=false;

        internal ServisInputDialog(MetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentWindow"></param>
        /// <param name="settings"></param>
        public ServisInputDialog(MetroWindow parentWindow, MetroDialogSettings settings)
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
        
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestCloseAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool wasOKClicked()
        {
            return OK_Clicked;
        }
        
        /// <summary>
        /// Window of app
        /// </summary>
        public MainWindow MainVMWindow { get; set; }
    }
}
