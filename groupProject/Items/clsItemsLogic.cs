using System.Data;
using groupProject.Common;

namespace groupProject.Items
{
    public class clsItemsLogic
    {
        private clsItemsSQL sql = new clsItemsSQL();
        private clsDataAccess db = new clsDataAccess();

        //GetAllItems returns List<clsItems> of all items in the database
        public List<clsItems> GetAllItems()
        {
            List<clsItems> lstItems = new List<clsItems>();
            int iRowCount = 0;

            DataSet ds = db.ExecuteSQLStatement(sql.GetAllItems(), ref iRowCount);

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                lstItems.Add(new clsItems
                {
                    ItemCode = row["ItemCode"].ToString() ?? "",
                    ItemDesc = row["ItemDesc"].ToString() ?? "",
                    Cost = Convert.ToDecimal(row["Cost"])
                });
            }

            return lstItems;
        }
        /// <summary>
        /// add new item 
        /// </summary>
        /// <param name="clsNewItem"></param>
        public void AddItem(clsItems clsNewItem)
        {
            //Add new item to database
            db.ExecuteNonQuery(sql.AddItem(clsNewItem));
        }
        /// <summary>
        /// edit either existing or new item 
        /// </summary>
        /// <param name="clsOldItem"></param>
        /// <param name="clsNewItem"></param>
        public void EditItem(clsItems clsOldItem, clsItems clsNewItem)
        {
            //Edit item in database
            db.ExecuteNonQuery(sql.EditItem(clsOldItem, clsNewItem));
        }
        /// <summary>
        /// delte selected item
        /// </summary>
        /// <param name="clsItemToDelete"></param>
        public void DeleteItem(clsItems clsItemToDelete)
        {
            //Delete item from database
            db.ExecuteNonQuery(sql.DeleteItem(clsItemToDelete));
        }
        /// <summary>
        /// check to see if item is on an invoice or not
        /// </summary>
        /// <param name="clsItemToCheck"></param>
        /// <returns></returns>
        public bool IsItemOnInvoice(clsItems clsItemToCheck)
        {
            //Check if item is on any invoice
            int iRowCount = 0;
            db.ExecuteSQLStatement(sql.GetInvoicesForItem(clsItemToCheck), ref iRowCount);
            return iRowCount > 0;
        }
    }
}
