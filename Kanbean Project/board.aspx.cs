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
        OleDbCommand mySelectCommand = new OleDbCommand();
        OleDbCommand myDeleteCommand = new OleDbCommand();
        OleDbCommand myInsertCommand = new OleDbCommand();
        OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        DataSet myDataSet = new DataSet();

        private void getBacklogs()
        {
            mySelectCommand.CommandText = "SELECT * FROM Backlogs";
            myAdapter.Fill(myDataSet, "myBacklogs");
            DataTable backlogsTable = myDataSet.Tables["myBacklogs"];
            foreach (DataRow row in backlogsTable.Rows)
            {
                createBacklog(row["BacklogID"].ToString(), row["Complexity"].ToString(), row["BacklogTitle"].ToString(), row["DueDate"].ToString(), row["BacklogColor"].ToString(), row["BacklogColorHeader"].ToString(), row["Swimlane"].ToString());
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|LanbanDatabase.mdb;";
            myConnection.Open();
            mySelectCommand.Connection = myConnection;
            myAdapter.SelectCommand = mySelectCommand;
            getBacklogs();
            
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addBacklogPopup.Show();
        }

        protected void btnAddBacklog_Click(object sender, EventArgs e)
        {
            addBacklogPopup.Hide();

            DataRow row = myDataSet.Tables["myBacklogs"].NewRow();
            row["ProjectID"] = 1;
            row["Swimlane"] = swimlaneDropDownList.SelectedValue;
            row["BacklogTitle"] = titleTextBox.Text;
            row["BacklogDescription"] = descriptionTextBox.Text;
            row["BacklogColor"] = colorDropDownList.SelectedValue.Split(',')[1].ToString();
            row["BacklogColorHeader"] = colorDropDownList.SelectedValue.Split(',')[0].ToString();
            if (complexityTextBox.Text != "")
                row["Complexity"] = Convert.ToInt32(complexityTextBox.Text);
            row["StartDate"] = DateTime.Today;
            if (deadlineTextBox.Text != "")
                row["DueDate"] = Convert.ToDateTime(deadlineTextBox.Text);
            myDataSet.Tables["myBacklogs"].Rows.Add(row);
            myAdapter.SelectCommand.CommandText = "Select * From Backlogs";
            OleDbCommandBuilder myCommandBuilder = new OleDbCommandBuilder(myAdapter);
            myAdapter.InsertCommand = myCommandBuilder.GetInsertCommand();
            myAdapter.Update(myDataSet, "myBacklogs");

            myDataSet.Clear();
            mySelectCommand.Connection = myConnection;
            myAdapter.SelectCommand = mySelectCommand;
            getBacklogs();
           
        }

        private void createBacklog(string id, string complexity, string title, string deadline, string color, string colorHeader, string swimlane)
        {
            Panel newBacklog = new Panel();
            newBacklog.CssClass = "backlogArea";
            newBacklog.ID = "backlogArea" + id;

            Panel backlog = new Panel();
            backlog.CssClass = "backlog";
            backlog.Style.Add("background-color", color);
            backlog.ID = "backlog" + id;
            newBacklog.Controls.Add(backlog);

            Panel backlogHeader = new Panel();
            backlogHeader.CssClass = "backlog-header";
            backlogHeader.Style.Add("background-color", colorHeader);
            backlogHeader.ID = "backlogHeader" + id;
            backlog.Controls.Add(backlogHeader);

            Panel backlogContent = new Panel();
            backlogContent.CssClass = "backlog-content";
            backlogContent.ID = "backlogContent" + id;
            backlog.Controls.Add(backlogContent);

            Panel backlogFooter = new Panel();
            backlogFooter.CssClass = "backlog-footer";
            backlogFooter.ID = "backlogFooter" + id;
            backlog.Controls.Add(backlogFooter);

            LinkButton btnComplexity = new LinkButton();
            btnComplexity.CssClass = "btnComplexity";
            btnComplexity.ID = "btnComplexity" + id;
            btnComplexity.ToolTip = "Complexity";
            btnComplexity.Text = complexity;
            btnComplexity.Click += new EventHandler(btnComplexity_Click);
            backlogHeader.Controls.Add(btnComplexity);

            LinkButton btnDelete = new LinkButton();
            btnDelete.CssClass = "backlogIcon iconDelete";
            btnDelete.ID = "btnDelete" + id;
            btnDelete.ToolTip = "Delete the backlog";
            btnDelete.Click += new EventHandler(btnDelete_Click);
            backlogHeader.Controls.Add(btnDelete);

            LinkButton btnEdit = new LinkButton();
            btnEdit.CssClass = "backlogIcon iconEdit";
            btnEdit.ID = "btnEdit" + id;
            btnEdit.ToolTip = "Edit the backlog";
            btnEdit.Click += new EventHandler(btnEdit_Click);
            backlogHeader.Controls.Add(btnEdit);

            LinkButton backlogTitle = new LinkButton();
            backlogTitle.CssClass = "backlog-title";
            backlogTitle.ID = "backlogTitle" + id;
            backlogTitle.Text = title;
            backlogTitle.ToolTip = "View the backlog";
            backlogTitle.Click += new EventHandler(backlogTitle_Click);
            backlogContent.Controls.Add(backlogTitle);

            LinkButton btnDueDate = new LinkButton();
            btnDueDate.CssClass = "btnDueDate";
            btnDueDate.ID = "btnDueDate" + id;
            btnDueDate.ToolTip = "Due Date";
            if (deadline != "")
                btnDueDate.Text = Convert.ToDateTime(deadline).ToString("dd.MM.yyyy");
            btnDueDate.Click += new EventHandler(btnDueDate_Click);
            backlogFooter.Controls.Add(btnDueDate);

            LinkButton btnTask = new LinkButton();
            btnTask.CssClass = "backlogIcon iconTask";
            btnTask.ID = "btnTask" + id;
            btnTask.ToolTip = "Show the tasks";
            btnTask.Click += new EventHandler(btnTask_Click);
            backlogFooter.Controls.Add(btnTask);

            if (swimlane == "productBacklog")
                productBacklog.Controls.Add(newBacklog);
            if (swimlane == "sprintBacklog")
                sprintBacklog.Controls.Add(newBacklog);
            if (swimlane == "toDo")
                toDo.Controls.Add(newBacklog);
            if (swimlane == "workInProcess")
                workInProcess.Controls.Add(newBacklog);
            if (swimlane == "done")
                done.Controls.Add(newBacklog);
        }

        protected void backlogTitle_Click(object sender, EventArgs e)
        {
            string id = ((Control)sender).ID.Remove(0,12);
            backlogIDview.Text = id;
            DataTable backlogsTable = myDataSet.Tables["myBacklogs"];
            foreach (DataRow row in backlogsTable.Rows)
            {
                if (row["BacklogID"].ToString() == id)
                {
                    viewBacklog.Text = "<ul><li><b>Title: </b>" + row["BacklogTitle"].ToString() + "</li>";
                    if (row["BacklogDescription"].ToString() != "")
                        viewBacklog.Text += "<li><b>Description: </b>" + row["BacklogDescription"].ToString() + "</li>";
                    if (row["Complexity"].ToString() != "")
                        viewBacklog.Text += "<li><b>Complexity: </b>" + row["Complexity"].ToString() + "</li>";
                    viewBacklog.Text += "<li><b>Column on the board: </b>" + row["Swimlane"].ToString() + "</li>";
                    viewBacklog.Text += "<li>Created on <b>" + row["StartDate"].ToString() + "</b></li>";
                    if (row["DueDate"].ToString() != "")
                        viewBacklog.Text += "<li>Must be done before <b>" + row["DueDate"].ToString() + "</b></li>";
                    viewBacklog.Text += "</ul>";
                }
            }
            viewBacklogPopup.Show();
        }

        protected void btnComplexity_Click(object sender, EventArgs e)
        {
            //lblTest.Text = ((Control)sender).ID;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //lblTest.Text = ((Control)sender).ID;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //lblTest.Text = ((Control)sender).ID;
        }

        protected void btnDueDate_Click(object sender, EventArgs e)
        {
            //lblTest.Text = ((Control)sender).ID;
        }

        protected void btnTask_Click(object sender, EventArgs e)
        {
            //lblTest.Text = ((Control)sender).ID;
        }

    }
}