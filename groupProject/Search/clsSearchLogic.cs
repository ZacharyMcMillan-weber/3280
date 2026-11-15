using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace groupProject.Search
{
    /// <summary>
    /// Contains the logic for the Search Window
    /// </summary>
    internal class clsSearchLogic
    {
        private readonly clsDataAccess dataAccess = new clsDataAccess();
        private readonly clsSearchSQL searchSQL = new clsSearchSQL();

        public DataTable GetAllInvoices()
        {
            return RunQuery(searchSQL.SelectAllInvoices());
        }

        public DataTable GetInvoiceNumbers()
        {
            return RunQuery(searchSQL.SelectInvoiceNumbers());
        }

        public DataTable GetInvoiceDates()
        {
            return RunQuery(searchSQL.SelectInvoiceDates());
        }

        public DataTable GetInvoiceTotals()
        {
            return RunQuery(searchSQL.SelectInvoiceTotals());
        }

        private DataTable RunQuery(string sql)
        {
            int rowCount = 0;
            DataSet results = dataAccess.ExecuteSQLStatement(sql, ref rowCount);
            return results.Tables[0];
        }
    }
}
