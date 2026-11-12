
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace groupProject.Items
{
    public class clsItemsSQL
    {
        // Database connection string
        private string connectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            Directory.GetCurrentDirectory() +
            "\\Invoice.accdb";

        // Retrieves all items from the ItemDesc table
        public DataTable GetAllItems()
        {
            DataTable dtItems = new DataTable();

            string sql = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";

            using (OleDbConnection connection =
                   new OleDbConnection(connectionString))
            {
                using (OleDbDataAdapter adapter =
                       new OleDbDataAdapter(sql, connection))
                {
                    adapter.Fill(dtItems);
                }
            }

            return dtItems;
        }
    }
}
