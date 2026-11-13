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
        //AddItem(clsItems)
        public void AddItem(clsItems clsNewItem)
        {
            //Add new item to database
            db.ExecuteNonQuery(sql.AddItem(clsNewItem));
        }
        //EditItem(clsItems clsOldItem, clsItems clsNewItem)
        public void EditItem(clsItems clsOldItem, clsItems clsNewItem)
        {
            //Edit item in database
            db.ExecuteNonQuery(sql.EditItem(clsOldItem, clsNewItem));
        }
        //DeleteItem(clsItems clsItemToDelete)
        public void DeleteItem(clsItems clsItemToDelete)
        {
            //Delete item from database
            db.ExecuteNonQuery(sql.DeleteItem(clsItemToDelete));
        }
        //IsItemOnInvoice(clsItems)
        public bool IsItemOnInvoice(clsItems clsItemToCheck)
        {
            //Check if item is on any invoice
            int iRowCount = 0;
            db.ExecuteSQLStatement(sql.GetInvoicesForItem(clsItemToCheck), ref iRowCount);
            return iRowCount > 0;
        }
    }
}
