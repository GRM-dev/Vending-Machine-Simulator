
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using VendingMachine.Core.Configuration;

namespace VendingMachine.VMDialogs
{
    partial class HOInputDialog : BaseMetroDialog
    {
        private bool OK_Clicked=false;

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
            OK_Clicked = true;
            RequestCloseAsync();
        }

        public MainWindow MainHOWindow { get; set; }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            RequestCloseAsync();
        }

        protected void setQuestion(string ask)
        {
            QuestionBlock.Text = ask;
        }

        public string getInputText()
        {
            return TextInputBox.Text;
        }

        public bool wasOKClicked()
        {
            return OK_Clicked;
        }
    }
}
