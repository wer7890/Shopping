<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MainMaster.Master" AutoEventWireup="true" CodeBehind="SearchUser.aspx.cs" Inherits="ShoppingWeb.Web.SearchUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="text-center">查詢帳號</h2>
        <br />
        <div class="row">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="f_userId" HeaderText="管理者ID" SortExpression="Id" />
                    <asp:BoundField DataField="f_pwd" HeaderText="密碼" />
                    <asp:BoundField DataField="f_userName" HeaderText="名稱" />
                    <asp:BoundField DataField="f_roles" HeaderText="角色" />
                    <asp:BoundField DataField="f_permissions" HeaderText="權限" />
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="labSearch" runat="server" Text="" Font-Size="21px" CssClass="col-12 col-sm-12 text-center text-success"></asp:Label>
        </div>
    </div>

</asp:Content>
