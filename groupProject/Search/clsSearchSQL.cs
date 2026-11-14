using System;
using System.Collections.Generic;
using System.Text;

namespace groupProject.Search
{
    internal class clsSearchSQL
    {
        // invoice SQL
        public string SelectAllInvoices()
        {
            return "SELECT * FROM Invoices";
        }

        public string SelectByInvoiceNumber()
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = 5000";
        }

        public string SelectByInvoiceNumberAndDate()
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = 5000 AND InvoiceDate = #4/13/2018#";
        }

        public string SelectByInvoiceNumberDateAndTotal()
        {
            return "SELECT * FROM Invoices WHERE InvoiceNum = 5000 " +
                   "AND InvoiceDate = #4/13/2018# AND TotalCost = 120";
        }

        public string SelectByTotalCost()
        {
            return "SELECT * FROM Invoices WHERE TotalCost = 1200";
        }

        public string SelectByTotalCostAndDate()
        {
            return "SELECT * FROM Invoices WHERE TotalCost = 1300 AND InvoiceDate = #4/13/2018#";
        }

        public string SelectByInvoiceDate()
        {
            return "SELECT * FROM Invoices WHERE InvoiceDate = #4/13/2018#";
        }

        // SQL for the three dropdowns

        public string SelectInvoiceNumbers()
        {
            return "SELECT DISTINCT InvoiceNum FROM Invoices ORDER BY InvoiceNum";
        }

        public string SelectInvoiceDates()
        {
            return "SELECT DISTINCT InvoiceDate FROM Invoices ORDER BY InvoiceDate";
        }

        public string SelectInvoiceTotals()
        {
            return "SELECT DISTINCT TotalCost FROM Invoices ORDER BY TotalCost";
        }
    }
}
