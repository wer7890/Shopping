<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MainMaster.Master" AutoEventWireup="true" CodeBehind="SearchUser2.aspx.cs" Inherits="ShoppingWeb.Web.SearchUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="text-center">查詢帳號</h2>
        <br />
        <div class="row">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" OnSorting="GridView1_Sorting" AllowSorting="true" CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="f_userId" HeaderText="管理者ID" SortExpression="f_userId" />
                    <asp:BoundField DataField="f_userName" HeaderText="名稱" SortExpression="f_userName" />
                    <asp:BoundField DataField="f_pwd" HeaderText="密碼" SortExpression="f_pwd" />
                    <asp:BoundField DataField="f_roles" HeaderText="角色" SortExpression="f_roles" />
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="labSearch" runat="server" Text="" Font-Size="21px" CssClass="col-12 col-sm-12 text-center text-success"></asp:Label>
        </div>
    </div>

</asp:Content>
