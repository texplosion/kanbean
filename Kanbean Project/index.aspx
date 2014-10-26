<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Kanbean_Project.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lanban</title>
    <link rel="Stylesheet" href="style.css" type="text/css" />
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
            <i id="add-button" class="icon" title="Add new task"></i>
            <i id="search-button" class="icon" title="Search"></i>
            <i id="filter-button" class="icon" title="Filter"></i>
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
                    <div class="tasks">TASK</div>

                </td>
                <td class="board-content" id="sprint-backlog"></td>
                <td class="board-content" id="to-do"></td>
                <td class="board-content" id="work-in-process"></td>
                <td class="board-content" id="done"></td>
            </tr>
        </table>
    </form>
</body>
</html>
