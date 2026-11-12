
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using groupProject.Common;

namespace groupProject.Items
{
    public class clsItemsSQL
    {
        // Database connection string
        private string connectionString =
            @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            Directory.GetCurrentDirectory() +
            "\\Invoice.accdb";

        // Returns a list of all items in the database
        public List<clsItems> GetAllItems()
        {
            List<clsItems> lstItems = new List<clsItems>();

            string sql =
                "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";

            using (OleDbConnection connection =
                   new OleDbConnection(connectionString))
            {
                using (OleDbCommand command =
                       new OleDbCommand(sql, connection))
                {
                    connection.Open();

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clsItems item = new clsItems();

                            item.ItemCode = reader["ItemCode"].ToString();
                            item.ItemDesc = reader["ItemDesc"].ToString();
                            item.Cost = Convert.ToDecimal(reader["Cost"]);

                            lstItems.Add(item);
                        }
                    }
                }
            }

            return lstItems;
        }

        // Adds a new item to the database
        public void AddItem(clsItems clsNewItem)
        {
            string sql =
                "INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) " +
                "VALUES (?, ?, ?)";

            using (OleDbConnection connection =
                   new OleDbConnection(connectionString))
            {
                using (OleDbCommand command =
                       new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", clsNewItem.ItemCode);
                    command.Parameters.AddWithValue("?", clsNewItem.ItemDesc);
                    command.Parameters.AddWithValue("?", clsNewItem.Cost);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Updates an existing item
        public void EditItem(clsItems clsOldItem, clsItems clsNewItem)
        {
            string sql =
                "UPDATE ItemDesc SET ItemDesc = ?, Cost = ? " +
                "WHERE ItemCode = ?";

            using (OleDbConnection connection =
                   new OleDbConnection(connectionString))
            {
                using (OleDbCommand command =
                       new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", clsNewItem.ItemDesc);
                    command.Parameters.AddWithValue("?", clsNewItem.Cost);
                    command.Parameters.AddWithValue("?", clsOldItem.ItemCode);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Deletes an item from the database
        public void DeleteItem(clsItems clsItemToDelete)
        {
            string sql =
                "DELETE FROM ItemDesc WHERE ItemCode = ?";

            using (OleDbConnection connection =
                   new OleDbConnection(connectionString))
            {
                using (OleDbCommand command =
                       new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", clsItemToDelete.ItemCode);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Checks whether an item is used on an invoice
        public bool IsItemOnInvoice(clsItems clsItemToCheck)
        {
            string sql =
                "SELECT DISTINCT(InvoiceNum) FROM LineItems " +
                "WHERE ItemCode = ?";

            using (OleDbConnection connection =
                   new OleDbConnection(connectionString))
            {
                using (OleDbCommand command =
                       new OleDbCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("?", clsItemToCheck.ItemCode);

                    connection.Open();

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}
