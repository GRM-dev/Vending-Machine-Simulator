
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Windows;
using VendingMachine.Core.Configuration;
using VendingMachine.Core.Misc;

namespace VendingMachine.VMDialogs
{
    /// <summary>
    /// Shows Inpput Dialog
    /// </summary>
    partial class VMInputDialog : BaseMetroDialog
    {
        private bool OK_Clicked = false;
        private ConfigPropertyType propType;

        internal VMInputDialog(MetroWindow parentWindow)
            : this(parentWindow, null)
        {
        }
        internal VMInputDialog(MetroWindow parentWindow, MetroDialogSettings settings)
            : base(parentWindow, settings)
        {
            InitializeComponent();
            MainHOWindow = (MainWindow)parentWindow;
        }

        /// <summary>
        /// Called from dialog manager
        /// </summary>
        /// <param name="metroWindow"></param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="propType"></param>
        public VMInputDialog(MainWindow metroWindow, string title, string message, ConfigPropertyType propType)
            : this(metroWindow)
        {
            this.Title = title;
            this.propType = propType;
            this.setQuestion(message);

        }

        private void Ok_Button_Click(object sender, RoutedEventArgs e)
        {
            OK_Clicked = true;
            if (getInputText().Length > 0)
            {
                ValueHandler vh = new ValueHandler(getInputText());
                if (vh.PropertyType != ValueTypes.STRING)
                {
                    ConfigProperties.instance.getProperty(propType).Value = getInputText();
                }
            }
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
