<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="board.aspx.cs" Inherits="Kanbean_Project.board" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lanban</title>
    <link rel="stylesheet" href="/css/style.css" type="text/css" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" />
</head>
<body>
    <form id="lanbanboard" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="lanbanScriptManager" runat="server" ></ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <div id="header">
                    <div id="logo" title="Lanban">
                        <h1>Lanban</h1>
                    </div>
                    <div id="user-information">
                        <p>Welcome, <span id="username">Username</span>. (<a href="#">logout</a>)</p>
                        <p>Project: <asp:DropDownList runat="server" ID="projectDropDownList"></asp:DropDownList></p>
                    </div>
                </div>

                <div class="clear"></div>

                <div id="toolbar">
                    <asp:LinkButton runat="server" id="btnAdd" class="icon" title="Add new backlog" OnClick="btnAdd_Click"></asp:LinkButton>
                    <asp:LinkButton runat="server" id="btnSearch" class="icon" title="Search"></asp:LinkButton>
                    <asp:LinkButton runat="server" id="btnFilter" class="icon" title="Filter"></asp:LinkButton>
                </div>

                <div class="clear"></div>

                <asp:Table id="kanbanboard" border="1" runat="server">
                    <asp:TableRow>
                        <asp:TableHeaderCell CssClass="board-header">Product Backlog</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="board-header">Sprint Backlog</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="board-header">To-Do</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="board-header">Work in Process</asp:TableHeaderCell>
                        <asp:TableHeaderCell CssClass="board-header">Done</asp:TableHeaderCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell CssClass="board-content" ID="productBacklog">
                            <asp:Panel CssClass="backlogArea" runat="server">
                                <asp:Panel CssClass="backlog" runat="server">
                                    <asp:Panel CssClass="backlog-header" runat="server">
                                        <asp:LinkButton class="btnComplexity" ID="backlogComplexity" runat="server" Text="3" ToolTip="Complexity" OnClick="btnComplexity_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnDelete" class="backlogIcon iconDelete" ToolTip="Delete the backlog" OnClick="btnDelete_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnEdit" class="backlogIcon iconEdit" ToolTip="Edit the backlog" OnClick="btnEdit_Click"></asp:LinkButton>
                                    </asp:Panel>
                                    <asp:Panel CssClass="backlog-content" ToolTip="View the backlog" runat="server">
                                        <asp:LinkButton CssClass="backlog-title" ID="backlogTitle" runat="server" OnClick="backlogTitle_Click">Board UI - As Customer I need a 5-swimlane-kanbanboard, so that I can manage my project efficiently</asp:LinkButton>
                                    </asp:Panel>
                                    <asp:Panel CssClass="backlog-footer" runat="server">
                                        <asp:LinkButton class="btnDueDate" ID="btnDueDate" runat="server" Text="30.10.2014" ToolTip="Due Date" OnClick="btnDueDate_Click"></asp:LinkButton>
                                        <asp:LinkButton runat="server" class="backlogIcon iconTask" ID="btnTask" ToolTip="Show the tasks" Text=" 4" OnClick="btnTask_Click"></asp:LinkButton>
                                    </asp:Panel>
                                </asp:Panel>
                                <!--<asp:Panel CssClass="tasks" runat="server">Task 1</asp:Panel>-->
                            </asp:Panel>
                        </asp:TableCell>
                        <asp:TableCell CssClass="board-content" ID="sprintBacklog"></asp:TableCell>
                        <asp:TableCell CssClass="board-content" ID="toDo"></asp:TableCell>
                        <asp:TableCell CssClass="board-content" ID="workInProcess"></asp:TableCell>
                        <asp:TableCell CssClass="board-content" ID="done"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>

                <asp:Label ID="lblTest" runat="server" Text="Label"></asp:Label>

                <div class="clear"></div>

                <ajaxToolkit:ModalPopupExtender ID="addBacklogPopup" runat="server" TargetControlID="addBacklogHiddenField" PopupControlID="addBacklogPanel" CancelControlID="btnCancel" BackgroundCssClass="popupbackground"></ajaxToolkit:ModalPopupExtender>
                <asp:HiddenField ID="addBacklogHiddenField" runat="server" />
                <asp:Panel ID="addBacklogPanel" runat="server" CssClass="popupmodal">
                    <fieldset>
                        <legend>Add new backlog</legend>
                        <div id="addBacklogTable" runat="server" visible="true">
                            <table>
                                <tr>
                                    <td>
                                        <p><label>Title</label><br />
                                        <asp:TextBox ID="titleTextBox" runat="server" Columns="38"></asp:TextBox></p>
                                        <p><label>Description</label><br />
                                        <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine" Columns="40" Rows="12"></asp:TextBox></p>
                                    </td>
                                    <td style="vertical-align:bottom;padding:0em 0.5em 1.3em 1em">
                                        <label>Column</label><br />
                                        <asp:DropDownList runat="server" ID="swimlaneDropDownList" Width="160">
                                            <asp:ListItem Text="Product Backlog" Value="productBacklog"></asp:ListItem>
                                            <asp:ListItem Text="Sprint Backlog" Value="sprintBacklog"></asp:ListItem>
                                            <asp:ListItem Text="To-Do" Value="toDo"></asp:ListItem>
                                            <asp:ListItem Text="Work in Process" Value="workInProcess"></asp:ListItem>
                                            <asp:ListItem Text="Done" Value="done"></asp:ListItem>
                                        </asp:DropDownList>
                                        <p><label>Assignee</label><br />
                                        <asp:DropDownList runat="server" ID="assigneeDropDownList" Width="160"></asp:DropDownList></p>
                                        <label>Color</label><br />
                                        <asp:DropDownList runat="server" ID="colorDropDownList" Width="160">
                                            <asp:ListItem Text="Yellow" Value="yellow"></asp:ListItem>
                                            <asp:ListItem Text="Red" Value="red"></asp:ListItem>
                                            <asp:ListItem Text="Orange" Value="orange"></asp:ListItem>
                                            <asp:ListItem Text="Green" Value="green"></asp:ListItem>
                                            <asp:ListItem Text="Blue" Value="blue"></asp:ListItem>
                                        </asp:DropDownList>
                                        <p><label>Complexity</label><br />
                                        <asp:TextBox ID="complexityTextBox" runat="server" TextMode="Number" Width="160"></asp:TextBox></p>
                                        <label>Deadline (month/day/year)</label><br />
                                        <asp:TextBox ID="deadlineTextBox" runat="server" TextMode="Date" Width="160"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnAddBacklog" runat="server" Text="Add" OnClick="btnAddBacklog_Click" />&nbsp;or&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                                    </td>
                                </tr>
                            </table>
                        </div>          
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
     </form>
</body>
</html>