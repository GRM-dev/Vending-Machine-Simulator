using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VendingMachine.XAML.Pages;

namespace VendingMachine.Core
{
    /// <summary>
    /// Wczytuje, zapisuje dane z/do pliku xml.
    /// Kazda zmiana w maszynie powoduje zmiane w pliku w ramach synchronizacji.
    /// </summary>
    class ConfigManager
    {
        internal static void startSetup()
        {
            MainPage VMmainPage = VMachine.MWindow.VMMainPage;
            Grid productsGrid = VMmainPage.ProductsGrid;
          //  productsGrid.Children.Cast<IProduct>().FirstOrDefault(e => Grid.GetColumn(e) == x && Grid.GetRow(e) == y);
        }
    }
}
