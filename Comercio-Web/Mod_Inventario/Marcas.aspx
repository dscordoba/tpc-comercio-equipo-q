<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="Presentacion.Mod_Inventario.Marcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Marcas</h2>

    <asp:GridView ID="dgvMarcas" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="Id" />
            <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
            <asp:CheckBoxField HeaderText="Activo" DataField="Activo" />
        </Columns>
    </asp:GridView>
</asp:Content>
