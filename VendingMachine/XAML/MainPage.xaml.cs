using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VendingMachine.Core;

namespace VendingMachine
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();
            ProductsView.ShowGridLines = true; //TODO: just to test
            AddProductTo(2, 3, null); // delete after ready
		}

        public void AddProductTo(int row, int column, IProduct product)
        {
            ColumnDefinitionCollection columns=ProductsView.ColumnDefinitions;
            RowDefinitionCollection rows = ProductsView.RowDefinitions;
            while (columns.Count < column)
            {
                columns.Add(new ColumnDefinition());
            }
            while (rows.Count < row)
            {
                rows.Add(new RowDefinition());
            }
            //Grid.SetColumn(product,column);
           // Grid.SetRow(product, row);
        }

        public void RemoveProduct(IProduct product)
        {
            //TODO: implement method
        }

        private void wrzut_Monety_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_1_Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Nmb_2_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_3_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_4_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_5_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_6_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_7_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_8_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_9_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_0_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_C_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Nmb_H_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Zwrot_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reszta_Button_Click(object sender, RoutedEventArgs e)
        {

        }
	}
}