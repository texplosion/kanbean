using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace Kanbean_Project
{
    public partial class registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void passwordCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int isNum = args.Value.IndexOfAny("1234567890".ToCharArray());
            int isLetterSmall = args.Value.IndexOfAny("qwertyuiopåäölkjhgfdsazxcvbnm".ToCharArray());
            int isLetterBig = args.Value.IndexOfAny("QWERTYUIOPÅÄÖLKJHGFDSAZXCVBNM".ToCharArray());
            bool isLongEnough = args.Value.Length > 5;
            if ((isNum > -1) && (isLetterSmall > -1) && (isLetterBig > -1) && isLongEnough)
                args.IsValid = true;
            else
                args.IsValid = false; 
        }

        protected void btnRegiter_Click(object sender, EventArgs e)
        {
            if (this.IsValid) {
                string connstr;
                string projectPath = @"C:\Users\s\Desktop\Agile project\team kanbean\Kanbean Project";
                connstr = "Provider = Microsoft.Jet.OLEDB.4.0;" +
                   @"Data Source =" + projectPath + @"\App_Data\LanbanDatabase.mdb";
                OleDbConnection myConnection = new OleDbConnection();
                myConnection.ConnectionString = connstr;

                OleDbCommand myCommand = new OleDbCommand();

                myCommand.CommandText = "INSERT INTO [User]([Username], [password], [Email]) VALUES ('" + usernameTextBox.Text + "','" + passwordTextBox.Text + "','" + emailTextBox.Text + "')";
                myCommand.CommandType = CommandType.Text;
                myCommand.Connection = myConnection;
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
               //Response.Write("<script>alert('The new user is registered');</script>");
                resultLabel.Text = "Information is submitted.";
                registerFormPopup.Show();  
                 Response.Redirect("login.aspx");
            }
            else
            {
                resultLabel.Text = "Check information again.";
                registerFormPopup.Show();
            }
            
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            
        }
    }
}