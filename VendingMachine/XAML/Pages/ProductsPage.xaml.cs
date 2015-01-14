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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VendingMachine.XAML.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// Strona wyswietla glowny panel automatu prezentujacy posiadane produkty.
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            ProductsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            ProductsGrid.RowDefinitions.Add(new RowDefinition());
        }
    }
}
