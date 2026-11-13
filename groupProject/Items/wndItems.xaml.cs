
using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using groupProject.Common;

namespace groupProject.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        // Tracks whether an item has been added, edited, or deleted.
        // Used by the Main window to determine whether it needs to
        // refresh its Items list.
        private bool bHasItemsBeenChanges = false; //Set to true when an item is added/edited/deleted.  Used by main window to know if it needs to refresh items list

        // Public property that allows the Main window to access
        // the item-changed status.
        public bool HasItemsBeenChanged //Property to allow main window to access the item-changed status
        {
            get { return bHasItemsBeenChanges; }
            set { bHasItemsBeenChanges = value; }
        }

        public wndItems()
        {
            InitializeComponent();

            dgItems.SelectionChanged += dgItems_SelectionChanged;

            loadItems();
        }

        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgItems.SelectedItem != null)
            {
                if (dgItems.SelectedItem != null)
                {
                    clsItems selectedItem = (clsItems)dgItems.SelectedItem;

                    txtItemCode.Text = selectedItem.ItemCode;
                    txtDescription.Text = selectedItem.ItemDesc;
                    txtCost.Text = selectedItem.Cost.ToString();
                }
            }
        }

        private clsItemsLogic logic = new clsItemsLogic();

        private void loadItems()
        {
            List<clsItems> lstItems = logic.GetAllItems();

            dgItems.ItemsSource = lstItems;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            bAddingNewitem = true; //Set to true to indicate we are adding a new item
            // clear the fields for a new item
            txtItemCode.Clear();
            txtDescription.Clear();
            txtCost.Clear();

            // clear current data grid selection
            dgItems.SelectedItem = null;

            // set focus to the item code field
            txtItemCode.Focus();
        }

        private bool bAddingNewitem = false; //Tracks whether we are adding a new item or editing an existing one

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Only handle new items
            if (bAddingNewitem)
            {
                // ensure all textboxes contain data
                if (string.IsNullOrWhiteSpace(txtCost.Text) ||
                    string.IsNullOrWhiteSpace(txtDescription.Text) ||
                    string.IsNullOrWhiteSpace(txtItemCode.Text))
                 {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                // validate cost 
                if (!decimal.TryParse(txtCost.Text, out decimal cost))
                {
                    MessageBox.Show("Please enter a valid cost.");
                    return;
                }

                // create new clsitem using user inputs
                clsItems newItem = new clsItems
                {
                    ItemCode = txtItemCode.Text.Trim(),
                    ItemDesc = txtDescription.Text.Trim(),
                    Cost = cost
                };

                // try to add the new item to the database
                try
                {
                    logic.AddItem(newItem);

                    HasItemsBeenChanged = true;

                    loadItems();

                    bAddingNewitem = false;

                    txtItemCode.Clear();
                    txtDescription.Clear();
                    txtCost.Clear();

                    MessageBox.Show("Item has been successfully added.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding item: " + ex.Message);
                }
            }
        }

    }
}
