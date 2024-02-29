<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="ShoppingWeb.Web.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="text-center">新增帳號</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-8 mt-2">
                <label for="exampleInputPassword1" class="form-label">管理員名稱</label>
                <asp:TextBox ID="txbUserName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mx-auto col-12 col-md-8 mt-2">
                <label for="exampleInputPassword1" class="form-label">密碼</label>
                <asp:TextBox ID="txbPwd" runat="server" type="password" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mx-auto mt-3 col-12 col-md-8 mt-2">
                <label for="exampleInputPassword1" class="form-label">角色</label>

                <asp:DropDownList ID="ddlRoles" runat="server">
                    <asp:ListItem Value="1">超級管理員</asp:ListItem>
                    <asp:ListItem Value="2">會員管理員</asp:ListItem>
                    <asp:ListItem Value="3">商品管理員</asp:ListItem>
                </asp:DropDownList>

            </div>

            <asp:Button ID="txbAddUser" runat="server" Text="新增" OnClick="txbAddUser_Click" CssClass="btn btn-outline-primary mx-auto mt-3 col-12 col-md-5" />
        </div>
        <br />
        <div class="row">
            <asp:Label ID="labAddUser" runat="server" Text="" Font-Size="21px" CssClass="col-12 col-sm-12 text-center text-success"></asp:Label>
        </div>
    </div>
</asp:Content>
