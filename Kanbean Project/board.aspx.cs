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
        //OleDbCommand mySelectCommand = new OleDbCommand();
        //OleDbCommand myDeleteCommand = new OleDbCommand();
        //OleDbCommand myInsertCommand = new OleDbCommand();
        //OleDbDataAdapter myAdapter = new OleDbDataAdapter();
        //DataSet myDataSet = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            myConnection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|LanbanDatabase.mdb;";
            myConnection.Open();
            
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            addBacklogPopup.Show();
        }

        protected void btnAddBacklog_Click(object sender, EventArgs e)
        {
            //add backlog into the database
            OleDbCommand myCommand = new OleDbCommand();
            myCommand.Connection = myConnection;
            myCommand.CommandText = "INSERT INTO Backlogs (ProjectID, Swimlane, BacklogTitle, BacklogDescription, BacklogColor, Complexity, StartDate, DueDate) " + "VALUES (1, '" + swimlaneDropDownList.SelectedValue + "','" + titleTextBox.Text + "','" + descriptionTextBox.Text + "','" + colorDropDownList.SelectedItem.Text + "'," + Convert.ToInt32(complexityTextBox.Text) + ",'" + DateTime.Today + "','" + Convert.ToDateTime(deadlineTextBox.Text) + "')";
            myCommand.ExecuteNonQuery();
            addBacklogPopup.Hide();

            myCommand.CommandText = "SELECT LAST(BacklogID) FROM Backlogs";
            var value = myCommand.ExecuteScalar();
            string result = "";
            if (value != null)
                result = value.ToString();

            //create and add backlog into the swimlane
            createNewBacklog(result, Convert.ToInt32(complexityTextBox.Text), titleTextBox.Text, Convert.ToDateTime(deadlineTextBox.Text), swimlaneDropDownList.SelectedValue);

        }

        private void createNewBacklog(string id, int complexity, string title, DateTime deadline, string swimlane)
        {
            Panel newBacklog = new Panel();
            newBacklog.CssClass = "backlogArea";
            newBacklog.ID = "backlogArea" + id;
            newBacklog.Attributes.Add("runat", "server");

            Panel backlog = new Panel();
            backlog.CssClass = "backlog";
            backlog.ID = "backlog" + id;
            backlog.Attributes.Add("runat", "server");
            newBacklog.Controls.Add(backlog);

            Panel backlogHeader = new Panel();
            backlogHeader.CssClass = "backlog-header";
            backlogHeader.ID = "backlogHeader" + id;
            backlogHeader.Attributes.Add("runat", "server");
            backlog.Controls.Add(backlogHeader);

            Panel backlogContent = new Panel();
            backlogContent.CssClass = "backlog-content";
            backlogContent.ID = "backlogContent" + id;
            backlogContent.Attributes.Add("runat", "server");
            backlog.Controls.Add(backlogContent);

            Panel backlogFooter = new Panel();
            backlogFooter.CssClass = "backlog-footer";
            backlogFooter.ID = "backlogFooter" + id;
            backlogFooter.Attributes.Add("runat", "server");
            backlog.Controls.Add(backlogFooter);

            LinkButton btnComplexity = new LinkButton();
            btnComplexity.CssClass = "btnComplexity";
            btnComplexity.ID = "btnComplexity" + id;
            btnComplexity.ToolTip = "Complexity";
            btnComplexity.Text = complexity.ToString();
            btnComplexity.Attributes.Add("runat", "server");
            btnComplexity.Attributes.Add("OnClick", "btnComplexity_Click");
            backlogHeader.Controls.Add(btnComplexity);

            LinkButton btnDelete = new LinkButton();
            btnDelete.CssClass = "backlogIcon iconDelete";
            btnDelete.ID = "btnDelete" + id;
            btnDelete.ToolTip = "Delete the backlog";
            btnDelete.Attributes.Add("runat", "server");
            btnDelete.Attributes.Add("OnClick", "btnDelete_Click");
            backlogHeader.Controls.Add(btnDelete);

            LinkButton btnEdit = new LinkButton();
            btnEdit.CssClass = "backlogIcon iconEdit";
            btnEdit.ID = "btnEdit" + id;
            btnEdit.ToolTip = "Edit the backlog";
            btnEdit.Attributes.Add("runat", "server");
            btnEdit.Attributes.Add("OnClick", "btnEdit_Click");
            backlogHeader.Controls.Add(btnEdit);

            LinkButton backlogTitle = new LinkButton();
            backlogTitle.CssClass = "backlog-title";
            backlogTitle.ID = "backlogTitle" + id;
            backlogTitle.Text = title;
            backlogTitle.Attributes.Add("runat", "server");
            backlogTitle.Attributes.Add("OnClick", "backlogTitle_Click");
            backlogContent.Controls.Add(backlogTitle);

            LinkButton btnDueDate = new LinkButton();
            btnDueDate.CssClass = "lblDueDate";
            btnDueDate.ID = "lblDueDate" + id;
            btnDueDate.ToolTip = "Due Date";
            btnDueDate.Text = deadline.ToString("dd.MM.yyyy");
            btnDueDate.Attributes.Add("runat", "server");
            btnDueDate.Attributes.Add("OnClick", "btnDueDate_Click");
            backlogFooter.Controls.Add(btnDueDate);

            LinkButton btnTask = new LinkButton();
            btnTask.CssClass = "backlogIcon iconTask";
            btnTask.ID = "btnTask" + id;
            btnTask.ToolTip = "Show the tasks";
            btnTask.Attributes.Add("runat", "server");
            btnTask.Attributes.Add("OnClick", "btnTask_Click");
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
            lblTest.Text = ((Control)sender).ID;
        }

        protected void btnComplexity_Click(object sender, EventArgs e)
        {
            lblTest.Text = ((Control)sender).ID;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblTest.Text = ((Control)sender).ID;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            lblTest.Text = ((Control)sender).ID;
        }

        protected void btnDueDate_Click(object sender, EventArgs e)
        {
            lblTest.Text = ((Control)sender).ID;
        }

        protected void btnTask_Click(object sender, EventArgs e)
        {
            lblTest.Text = ((Control)sender).ID;
        }

    }
}