<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Kanbean_Project.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lanban</title>
    <link rel="stylesheet" href="/css/style.css" type="text/css" />
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css" />
</head>
<body>
    <form id="lanbanboard" runat="server">
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

        <table id="kanbanboard" border="1">
            <tr>
                <th class="board-header">Product Backlog</th>
                <th class="board-header">Sprint Backlog</th>
                <th class="board-header">To-Do</th>
                <th class="board-header">Work in Process</th>
                <th class="board-header">Done</th>
            </tr>
            <tr>
                <td class="board-content" id="product-backlog">
                    <div class="backlogs" draggable="true">
                        <div class="backlog-content">Backlog 1</div>
                        <div class="tasks" draggable="true">task 1</div>
                        <div class="tasks" draggable="true">task 2</div>
                        <div class="tasks" draggable="true">task 3</div>
                    </div>
                    <div class="backlogs">
                        <div class="backlog-content">Backlog 2</div>
                        <div class="tasks">task 1</div>
                        <div class="tasks">task 2</div>
                        <div class="tasks">task 3</div>
                    </div>
                </td>
                <td class="board-content" id="sprint-backlog"></td>
                <td class="board-content" id="to-do"></td>
                <td class="board-content" id="work-in-process"></td>
                <td class="board-content" id="done"></td>
            </tr>
        </table>

        <div class="clear"></div>

        <ajaxToolkit:ToolkitScriptManager ID="addBacklogToolkitscript" runat="server" ></ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="addBacklogUpdatePanel" runat="server">
            <ContentTemplate>
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
                                        <asp:TextBox ID="DescriptionTextBox" runat="server" TextMode="MultiLine" Columns="40" Rows="12"></asp:TextBox></p>
                                    </td>
                                    <td style="vertical-align:bottom;padding:0em 0.5em 1.3em 1em">
                                        <label>Line</label><br />
                                        <asp:DropDownList runat="server" ID="lineDropDownList" Width="160">
                                            <asp:ListItem Text="Product Backlog" Value="product-backlog"></asp:ListItem>
                                            <asp:ListItem Text="Sprint Backlog" Value="sprint-backlog"></asp:ListItem>
                                            <asp:ListItem Text="To-Do" Value="to-do"></asp:ListItem>
                                            <asp:ListItem Text="Work in Process" Value="work-in-process"></asp:ListItem>
                                            <asp:ListItem Text="Done" Value="done"></asp:ListItem>
                                        </asp:DropDownList>
                                        <p><label>Assignee</label><br />
                                        <asp:DropDownList runat="server" ID="assigneeDropDownList" Width="160"></asp:DropDownList></p>
                                        <label>Color</label><br />
                                        <asp:DropDownList runat="server" ID="colorDropDownList" Width="160">
                                            <asp:ListItem Text="Red" Value="red"></asp:ListItem>
                                            <asp:ListItem Text="Orange" Value="orange"></asp:ListItem>
                                            <asp:ListItem Text="Yellow" Value="yellow"></asp:ListItem>
                                            <asp:ListItem Text="Green" Value="green"></asp:ListItem>
                                            <asp:ListItem Text="Blue" Value="blue"></asp:ListItem>
                                        </asp:DropDownList>
                                        <p><label>Complexity</label><br />
                                        <asp:TextBox ID="complexityTextBox" runat="server" TextMode="Number" Width="160"></asp:TextBox></p>
                                        <label>Deadline</label><br />
                                        <asp:TextBox ID="deadlineTextBox" runat="server" TextMode="Date" Width="160"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;or&nbsp;
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
