using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ListBoxDB
{
    public partial class ReportForm : Form
    {
        public static string connectionString =
           "provider=Microsoft.JET.OLEDB.4.0; "
           + "data source = " + Application.StartupPath + "\\LinkListDB.mdb";

        private OleDbDataAdapter dataAdapter;
        private DataSet dataSet;
        private DataTable dataTable;

        // *** End ADO.NET objects ***
        private static string CustId;
        private static string name;
        private static string Address;
        private static string EmpId;

        public ReportForm()
        {
            InitializeComponent();
            // Default select command on the PhoneNumbers table
            string commandstring = "select * from Customer";

            // The link between the sql command and the database connection
            dataAdapter = new OleDbDataAdapter(commandstring, connectionString);

            // Define insert, update, and delete sql commands to use.
            //    BuildCommands();

            // Declare and fill the in-memory dataset from the database
            dataSet = new DataSet();
            dataSet.CaseSensitive = true;
            dataAdapter.Fill(dataSet, "Employee");


            CrystalReport1 Report = new CrystalReport1();
            DataSet1 ds = new DataSet1();
            DataSet1.DataTable1DataTable t = (DataSet1.DataTable1DataTable)ds.Tables[0];

            dataTable = dataSet.Tables[0];


            foreach (DataRow dataRow in dataTable.Rows)
            {
                // Load global strings from column values in the datarow
                CustId = dataRow["CustId"].ToString().Trim();
                name = dataRow["Name"].ToString().Trim();
                Address = dataRow["Address"].ToString().Trim();
                EmpId = dataRow["EmpId"].ToString().Trim();


                t.AddDataTable1Row(CustId, name, Address, EmpId);
            }



            Report.SetDataSource(ds);

            crystalReportViewer1.ReportSource = Report;
            
        }
    }
}