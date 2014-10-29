using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

namespace Kanbean_Project
{
    public partial class board : System.Web.UI.Page
    {
        OleDbConnection myConnection = new OleDbConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|LanbanDatabase.accdb;"; ;
            myConnection.Open();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addBacklogPopup.Show();
        }

        protected void btnAddBacklog_Click(object sender, EventArgs e)
        {

            
        }
    }
}