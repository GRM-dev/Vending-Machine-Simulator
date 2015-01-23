using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VendingMachine.VMDialogs
{
    /// <summary>
    /// Interaction logic for ExceptionDialog.xaml
    /// </summary>
    public partial class HOExceptionDialog : BaseMetroDialog
    {
        internal HOExceptionDialog(MetroWindow parentWindow)
            : this(null, parentWindow)
        {
        }
        internal HOExceptionDialog(string msg, MetroWindow parentWindow)
            : this(msg, parentWindow, null)
        {
        }
        internal HOExceptionDialog(string msg, MetroWindow parentWindow, MetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            InitializeComponent();
            MainVMWindow = (MainWindow)parentWindow;
            ExceptionTextBlock.Text = msg;
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestCloseAsync();
        }

        public MainWindow MainVMWindow { get; set; }
    }
}
