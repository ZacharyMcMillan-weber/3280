using System.Globalization;
using groupProject.Common;

namespace groupProject.Items
{
    public class clsItemsSQL
    {
        // Returns a list of all items in the database
        public string GetAllItems()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }

        // Adds a new item to the database
        public string AddItem(clsItems clsNewItem)
        {
            return "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) " +
                   "VALUES ('" + clsNewItem.ItemCode.Replace("'", "''") + "', '" +
                   clsNewItem.ItemDesc.Replace("'", "''") + "', " +
                   clsNewItem.Cost.ToString(CultureInfo.InvariantCulture) + ")";
        }

        // Updates an existing item
        public string EditItem(clsItems clsOldItem, clsItems clsNewItem)
        {
            return "UPDATE ItemDesc SET ItemDesc = '" + clsNewItem.ItemDesc.Replace("'", "''") + "', " +
                   "Cost = " + clsNewItem.Cost.ToString(CultureInfo.InvariantCulture) + " " +
                   "WHERE ItemCode = '" + clsOldItem.ItemCode.Replace("'", "''") + "'";
        }

        // Deletes an item from the database
        public string DeleteItem(clsItems clsItemToDelete)
        {
            return "DELETE FROM ItemDesc WHERE ItemCode = '" + clsItemToDelete.ItemCode.Replace("'", "''") + "'";
        }

        // Returns the invoices that an item is used on
        public string GetInvoicesForItem(clsItems clsItemToCheck)
        {
            return "SELECT DISTINCT InvoiceNum FROM LineItems " +
                   "WHERE ItemCode = '" + clsItemToCheck.ItemCode.Replace("'", "''") + "'";
        }
    }
}
