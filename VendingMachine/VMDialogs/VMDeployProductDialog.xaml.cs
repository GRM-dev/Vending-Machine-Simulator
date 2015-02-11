
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using VendingMachine.Core.Configuration;
using VendingMachine.Core.Products;

namespace VendingMachine.VMDialogs
{
    partial class VMDeployProductDialog : BaseMetroDialog
    {
        private bool OK_Clicked=false;

        internal VMDeployProductDialog(MetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }
        internal VMDeployProductDialog(MetroWindow parentWindow, MetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            InitializeComponent();
            MainHOWindow = (MainWindow)parentWindow;
        }

        internal VMDeployProductDialog(Product product,MetroWindow parentWindow)
            : this(parentWindow)
        {
            ProductDeployGrid.Children.Add(product);
        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            OK_Clicked = true;
            RequestCloseAsync();
        }

        public MainWindow MainHOWindow { get; set; }

        public bool wasOKClicked()
        {
            return OK_Clicked;
        }
    }
}
