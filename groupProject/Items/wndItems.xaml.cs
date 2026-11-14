
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
        /// <summary>
        /// checks to see if the textboxes have been filled with data or not and sets them to variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgItems.SelectedItem != null)
            {
                clsItems selectedItem = (clsItems)dgItems.SelectedItem;

                txtItemCode.Text = selectedItem.ItemCode;
                txtDescription.Text = selectedItem.ItemDesc;
                txtCost.Text = selectedItem.Cost.ToString();
            }
        }

        private clsItemsLogic logic = new clsItemsLogic();
        /// <summary>
        /// load all items from db into a list
        /// </summary>
        private void loadItems()
        {
            List<clsItems> lstItems = logic.GetAllItems();

            dgItems.ItemsSource = lstItems;
        }
        /// <summary>
        /// verify that we are adding a new item , check textboxes for new data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            bAddingNewitem = true; //Set to true to indicate we are adding a new item
            bEditingExistingitem = false; //Set to false to indicate we are not editing an existing item
            clsOldItem = null;
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
        /// <summary>
        /// on click check to make sure textboxes are not null, verify data is appropriate and try to send into db
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Make sure all fields contain information
            if (string.IsNullOrWhiteSpace(txtItemCode.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtCost.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Make sure Cost is a valid number
            if (!decimal.TryParse(txtCost.Text, out decimal cost))
            {
                MessageBox.Show("Please enter a valid cost.");
                return;
            }

            try
            {
                // Create an item using the information in the TextBoxes
                clsItems newItem = new clsItems
                {
                    ItemCode = txtItemCode.Text.Trim(),
                    ItemDesc = txtDescription.Text.Trim(),
                    Cost = cost
                };

                // Handle adding a new item
                if (bAddingNewitem)
                {
                    logic.AddItem(newItem);
                }
                // Handle editing an existing item
                else if (bEditingExistingitem)
                {
                    logic.EditItem(clsOldItem, newItem);
                }
                else
                {
                    MessageBox.Show("Please select Add or Edit before saving.");
                    return;
                }

                // Indicate that the Items list has changed
                HasItemsBeenChanged = true;

                // Refresh the DataGrid
                loadItems();

                // Reset the operation flags
                bAddingNewitem = false;
                bEditingExistingitem = false;

                // Clear the fields
                txtItemCode.Clear();
                txtDescription.Clear();
                txtCost.Clear();

                MessageBox.Show("Item saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save item: " + ex.Message);
            }
        }
        private bool bEditingExistingitem = false; //Tracks whether we are editing an existing item
        private clsItems clsOldItem;
        /// <summary>
        /// set status to updating existing item, set item values to whatever is in the textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgItems.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to edit.");
                return;
            }

            // Store the original item
            clsOldItem = (clsItems)dgItems.SelectedItem;

            // Indicate that we are editing
            bEditingExistingitem     = true;
            bAddingNewitem = false;

            // Populate the fields
            txtItemCode.Text = clsOldItem.ItemCode;
            txtDescription.Text = clsOldItem.ItemDesc;
            txtCost.Text = clsOldItem.Cost.ToString();
        }

        /// <summary>
        /// make sure item in field is selected. Confirm user choice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Make sure an item is selected
            if (dgItems.SelectedItem == null)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            // Get the selected item
            clsItems selectedItem = (clsItems)dgItems.SelectedItem;

            try
            {
                // Check whether the item is used on an invoice
                if (logic.IsItemOnInvoice(selectedItem))
                {
                    MessageBox.Show(
                        "This item cannot be deleted because it is used on an invoice.");
                    return;
                }

                // Confirm deletion
                MessageBoxResult result = MessageBox.Show(
                    "Are you sure you want to delete this item?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Delete the item
                    logic.DeleteItem(selectedItem);

                    // Mark that the Items list has changed
                    HasItemsBeenChanged = true;

                    // Refresh the DataGrid
                    loadItems();

                    // reset flags
                    bAddingNewitem = false;
                    bEditingExistingitem = false;
                    clsOldItem = null;

                    //clear text boxes
                    dgItems.SelectedItem = null;
                    txtItemCode.Clear();
                    txtDescription.Clear();
                    txtCost.Clear();

                    MessageBox.Show("Item deleted successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to delete item: " + ex.Message);
            }
        }
        /// <summary>
        /// close item window and return to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
