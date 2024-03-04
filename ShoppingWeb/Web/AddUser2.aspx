<%@ Page Title="" Language="C#" MasterPageFile="~/Web/MainMaster.Master" AutoEventWireup="true" CodeBehind="AddUser2.aspx.cs" Inherits="ShoppingWeb.Web.AddUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 class="text-center">新增帳號</h2>
        <br />
        <div class="row">
            <div class="mx-auto col-12 col-md-8 mt-2">
                <label for="exampleInputPassword1" class="form-label">管理員名稱</label>
                <input id="txbUserName" class="form-control" />

            </div>
            <div class="mx-auto col-12 col-md-8 mt-2">
                <label for="exampleInputPassword1" class="form-label">密碼</label>
                <input id="txbPwd" class="form-control" />
            </div>
            <div class="mx-auto mt-3 col-12 col-md-8 mt-2">
                <label for="exampleInputPassword1" class="form-label">角色</label>

                <select id="ddlRoles" class="form-select">
                    <option value="1">超級管理員</option>
                    <option value="2">會員管理員</option>
                    <option value="3">商品管理員</option>
                </select>
            </div>

            <%--<asp:Button ID="txbAddUser" runat="server" Text="新增" OnClick="txbAddUser_Click" CssClass="btn btn-outline-primary mx-auto mt-3 col-12 col-md-5" />--%>
            <button id="btnAddUser" class="btn btn-outline-primary mx-auto mt-3 col-12 col-md-5">新增</button>
        </div>
        <br />
        <div class="row">
            <%--<asp:Label ID="labAddUser" runat="server" Text="" Font-Size="21px" CssClass="col-12 col-sm-12 text-center text-success"></asp:Label>--%>
            <label id="labAddUser" class="col-12 col-sm-12 text-center text-success"></label>
        </div>
    </div>
</asp:Content>

