using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace groupProject
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public wndMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens the Items window so the user can update the item definitions table.
        /// </summary>
        private void mnuEdit_Click(object sender, RoutedEventArgs e)
        {
            // wndItems lives in the groupProject.Items namespace, so we qualify it with "Items."
            Items.wndItems itemsWindow = new Items.wndItems();

            this.Hide();              
            itemsWindow.ShowDialog();
            this.Show();                

            // INTERFACE - Refreshing the items combo box after the Items window closes:
            // The Items window has a public property, HasItemsBeenChanged (bool, { get; private set; }),
            // that it sets to true whenever the user adds, edits, or deletes an item.
            // Because ShowDialog() doesn't return until the Items window closes, the next line
            // here can read itemsWindow.HasItemsBeenChanged. The window is off screen, but the object
            // still exists because our itemsWindow variable still refers to it.
            // If HasItemsBeenChanged is true, the main window calls clsMainLogic to re-query all items
            // and resets the items combo box's ItemsSource so new or changed items and costs appear.
            // If an invoice is currently displayed, it is also reloaded, because one of its
            // item descriptions may have changed.
            //
            // We would implement something like this:
            // if (itemsWindow.HasItemsBeenChanged)
            // {
            //     // reload combo box + current invoice
            // }
        }

        /// <summary>
        /// Opens the Search window so the user can find an existing invoice.
        /// </summary>
        private void mnuSearch_Click(object sender, RoutedEventArgs e)
        {
            wndSearch searchWindow = new wndSearch();

            this.Hide();
            searchWindow.ShowDialog();  // pauses here until the user clicks Select Invoice or Cancel
            this.Show();

            // INTERFACE - Getting the selected Invoice ID back from the Search window:
            // The Search window has a public property, SelectedInvoiceID (int, { get; private set; }).
            // When the user clicks "Select Invoice", the Search window stores that invoice's ID
            // in SelectedInvoiceID and closes itself. If the user cancels, it stays 0.
            // Because ShowDialog() blocks until the Search window closes, the main window reads
            // searchWindow.SelectedInvoiceID on the next line. If it isn't 0, the main window calls
            // clsMainLogic to load that invoice (number, date, line items, total) from the database
            // and displays it in read-only (viewing) mode, where the user may then click Edit.
            //
            // We would implement something like this:
            // if (searchWindow.SelectedInvoiceID != 0)
            // {
            //     // load and display the invoice
            // }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
