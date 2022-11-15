using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTimeSheetManagement.Interface;
using WebTimeSheetManagement.Models;
namespace WebTimeSheetManagement.Concrete
{
    public class ExpenseExportConcrete : IExpenseExport
    {
        public DataSet GetReportofExpense(DateTime? FromDate, DateTime? ToDate, int UserID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnectionString.connectionString))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetReportofExpense", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    cmd.Parameters.AddWithValue("@AssignTo", UserID);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataSet GetAllReportofExpense(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GlobalConnectionString.connectionString))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("Usp_GetAllReportofExpense", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
