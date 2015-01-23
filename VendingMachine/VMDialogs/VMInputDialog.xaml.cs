
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using VendingMachine.Core.Configuration;

namespace VendingMachine.VMDialogs
{
    partial class HOInputDialog : BaseMetroDialog
    {
        internal HOInputDialog(MetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }
        internal HOInputDialog(MetroWindow parentWindow, MetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            InitializeComponent();
            MainHOWindow = (MainWindow)parentWindow;
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            ConfigProperties.Update(ConfigPropertyType.TEST_PROP, PART_TextBox.Text);
            RequestCloseAsync();
        }

        public MainWindow MainHOWindow { get; set; }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestCloseAsync();
        }
    }
}
