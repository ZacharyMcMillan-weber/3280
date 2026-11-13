
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

    }
}
