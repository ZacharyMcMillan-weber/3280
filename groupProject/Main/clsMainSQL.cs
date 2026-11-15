using System;
using System.Globalization;

namespace groupProject.Main
{
    /// <summary>
    /// A class that provides methods for generating SQL queries.
    /// </summary>
    public class clsMainSQL
    {
        /// <summary>
        /// Gets a SQL query that selects all item codes, descriptions, and costs ordered by item description.
        /// </summary>
        /// <returns>The SQL query string.</returns>
        public string SelectAllItems()
        {
            return "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc ORDER BY ItemDesc";
        }

        /// <summary>
        /// Gets a SQL query that selects the number, date, and total cost for a specific invoice.
        /// </summary>
        /// <param name="iInvoiceNum">The invoice number to search for.</param>
        /// <returns>The SQL query string.</returns>
        public string SelectInvoice(int iInvoiceNum)
        {
            return "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + iInvoiceNum;
        }

        /// <summary>
        /// Builds a SQL query that selects the line items for an invoice, along with each item's description and cost.
        /// </summary>
        /// <param name="iInvoiceNum">The invoice number used to filter the line items.</param>
        /// <returns>A SQL query string that joins line items with item descriptions and filters by invoice number.</returns>
        public string SelectLineItems(int iInvoiceNum)
        {
            return "SELECT LineItems.LineItemNum, LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost " +
                   "FROM LineItems, ItemDesc " +
                   "WHERE LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + iInvoiceNum + " " +
                   "ORDER BY LineItems.LineItemNum";
        }

        /// <summary>
        /// Builds a SQL query that inserts a new invoice into the Invoices table.
        /// </summary>
        /// <param name="dtDate">The date of the invoice.</param>
        /// <param name="dTotalCost">The total cost of all items on the invoice.</param>
        /// <returns>A SQL query string that inserts a new invoice into the Invoices table.</returns>
        public string InsertInvoice(DateTime dtDate, decimal dTotalCost)
        {
            return "INSERT INTO Invoices (InvoiceDate, TotalCost) VALUES (#" + dtDate.ToString("yyyy-MM-dd") + "#, " +
                   dTotalCost.ToString(CultureInfo.InvariantCulture) + ")";
        }

        /// <summary>
        /// Gets a SQL query that selects the maximum invoice number.
        /// </summary>
        /// <returns>A SQL query string that selects the maximum value of InvoiceNum from the Invoices table.</returns>
        public string SelectMaxInvoiceNum()
        {
            return "SELECT MAX(InvoiceNum) FROM Invoices";
        }

        /// <summary>
        /// Builds an SQL INSERT statement for a line item.
        /// </summary>
        /// <param name="iInvoiceNum">The invoice number associated with the line item.</param>
        /// <param name="iLineItemNum">The position of the line item on the invoice (1, 2, 3, ...).</param>
        /// <param name="sItemCode">The item code to insert.</param>
        /// <returns>An SQL INSERT statement containing the specified line-item values.</returns>
        public string InsertLineItem(int iInvoiceNum, int iLineItemNum, string sItemCode)
        {
            return "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" +
                   iInvoiceNum + ", " + iLineItemNum + ", '" + sItemCode.Replace("'", "''") + "')";
        }

        /// <summary>
        /// Generates a SQL statement that updates an invoice's date and total cost.
        /// </summary>
        /// <param name="iInvoiceNum">The invoice number to update.</param>
        /// <param name="dtDate">The date to assign to the invoice.</param>
        /// <param name="dTotalCost">The total cost to assign to the invoice.</param>
        /// <returns>A SQL UPDATE statement for the specified invoice.</returns>
        public string UpdateInvoice(int iInvoiceNum, DateTime dtDate, decimal dTotalCost)
        {
            return "UPDATE Invoices SET InvoiceDate = #" + dtDate.ToString("yyyy-MM-dd") + "#, " +
                   "TotalCost = " + dTotalCost.ToString(CultureInfo.InvariantCulture) + " " +
                   "WHERE InvoiceNum = " + iInvoiceNum;
        }

        /// <summary>
        /// Generates a SQL statement that deletes all line items for an invoice.
        /// </summary>
        /// <param name="iInvoiceNum">The invoice number whose line items are deleted.</param>
        /// <returns>A SQL DELETE statement for the specified invoice number.</returns>
        public string DeleteLineItems(int iInvoiceNum)
        {
            return "DELETE FROM LineItems WHERE InvoiceNum = " + iInvoiceNum;
        }
    }
}
