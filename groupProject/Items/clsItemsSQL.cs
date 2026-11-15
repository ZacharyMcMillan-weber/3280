using System.Globalization;
using groupProject.Common;

namespace groupProject.Items
{
    public class clsItemsSQL
    {
        // Returns a list of all items in the database
        /// <summary>
        /// get all items 
        /// </summary>
        /// <returns> item code, description, and cost</returns>
        public string GetAllItems()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
        }

        /// <summary>
        /// add item to db
        /// </summary>
        /// <param name="clsNewItem"></param>
        /// <returns>nothing</returns>
        public string AddItem(clsItems clsNewItem)
        {
            return "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) " +
                   "VALUES ('" + clsNewItem.ItemCode.Replace("'", "''") + "', '" +
                   clsNewItem.ItemDesc.Replace("'", "''") + "', " +
                   clsNewItem.Cost.ToString(CultureInfo.InvariantCulture) + ")";
        }

        /// <summary>
        /// replaces old item with new item properties using item code as reference
        /// </summary>
        /// <param name="clsOldItem"></param>
        /// <param name="clsNewItem"></param>
        /// <returns></returns>
        public string EditItem(clsItems clsOldItem, clsItems clsNewItem)
        {
            return "UPDATE ItemDesc SET ItemDesc = '" + clsNewItem.ItemDesc.Replace("'", "''") + "', " +
                   "Cost = " + clsNewItem.Cost.ToString(CultureInfo.InvariantCulture) + " " +
                   "WHERE ItemCode = '" + clsOldItem.ItemCode.Replace("'", "''") + "'";
        }

        /// <summary>
        /// deletes item using item code as reference
        /// </summary>
        /// <param name="clsItemToDelete"></param>
        /// <returns></returns>
        public string DeleteItem(clsItems clsItemToDelete)
        {
            return "DELETE FROM ItemDesc WHERE ItemCode = '" + clsItemToDelete.ItemCode.Replace("'", "''") + "'";
        }

        /// <summary>
        /// chekcs to see if item has invoice num 
        /// </summary>
        /// <param name="clsItemToCheck"></param>
        /// <returns>invoice num if available</returns>
        public string GetInvoicesForItem(clsItems clsItemToCheck)
        {
            return "SELECT DISTINCT InvoiceNum FROM LineItems " +
                   "WHERE ItemCode = '" + clsItemToCheck.ItemCode.Replace("'", "''") + "'";
        }
    }
}
