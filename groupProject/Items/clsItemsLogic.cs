using System;
using System.Collections.Generic;
using System.Text;
using groupProject.Common;

namespace groupProject.Items
{
    public class clsItemsLogic
    {
        private clsItemsSQL sql = new clsItemsSQL();
        //GetAllItems returns List<clsItems> of all items in the database
        public List<clsItems> GetAllItems()
        {
            // return list of all items in db
            return sql.GetAllItems();
        }
        //AddItem(clsItems)
        public void AddItem(clsItems clsNewItem)
        {
            //Add new item to database
            sql.AddItem(clsNewItem);
        }
        //EditItem(clsItems clsOldItem, clsItems clsNewItem)
        public void EditItem(clsItems clsOldItem, clsItems clsNewItem)
        {
            //Edit item in database
            sql.EditItem(clsOldItem, clsNewItem);
        }
        //DeleteItem(clsItems clsItemToDelete)
        public void DeleteItem(clsItems clsItemToDelete)
        {
            //Delete item from database
            sql.DeleteItem(clsItemToDelete);
        }
        //IsItemOnInvoice(clsItems)
        public bool IsItemOnInvoice(clsItems clsItemToCheck)
        {
            //Check if item is on any invoice
            return sql.IsItemOnInvoice(clsItemToCheck);
        }
    }
}
