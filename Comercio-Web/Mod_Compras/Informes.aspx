<%@ Page Title="Informes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="Presentacion.Mod_Compras.Informes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- todo el contenido de la pagina -->
    <div class="container-fluid p-2 p-sm-3 bg-transparent">

        <!-- el camino de arriba -->
        <div class="row mb-3">
            <div class="col-12">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0">
                        <li class="breadcrumb-item text-secondary">COMPRAS</li>
                        <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Informes</li>
                    </ol>
                </nav>
            </div>
        </div>

        <!-- filtros -->
        <div class="card shadow-sm border-0 mb-3">
            <div class="card-body p-3 p-md-4">

                <h3 class="text-center mb-3 mb-md-4">Informe de Compras</h3>

                <div class="row g-3">
                    <div class="col-12 col-md-4">
                        <div class="input-group">
                            <span class="input-group-text fw-bold">Proveedor</span>
                            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                                <asp:ListItem Text="Todos" Value="0" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-12 col-md-3">
                        <div class="input-group">
                            <span class="input-group-text fw-bold">Desde</span>
                            <asp:TextBox ID="txtDesde" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-12 col-md-3">
                        <div class="input-group">
                            <span class="input-group-text fw-bold">Hasta</span>
                            <asp:TextBox ID="txtHasta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-12 col-md-2 d-grid">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                    </div>
                </div>

                <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mt-3" Visible="false"></asp:Label>

            </div>
        </div>

        <!-- resumen -->
        <div class="row mb-3">
            <div class="col-12 col-md-6 mb-3 mb-md-0">
                <div class="card shadow-sm border-0">
                    <div class="card-body text-center">
                        <div class="text-secondary">Cantidad de compras</div>
                        <asp:Label ID="lblCantidadCompras" runat="server" CssClass="fs-3 fw-bold" Text="0"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-12 col-md-6">
                <div class="card shadow-sm border-0">
                    <div class="card-body text-center">
                        <div class="text-secondary">Total comprado</div>
                        <asp:Label ID="lblTotalComprado" runat="server" CssClass="fs-3 fw-bold" Text="$ 0,00"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

        <!-- listado de compras -->
        <div class="card shadow-sm border-0 mb-3">
            <div class="card-body p-3 p-md-4">

                <h4 class="text-center mb-3">Compras</h4>

                <div class="table-responsive">
                    <asp:GridView ID="dgvCompras" runat="server"
                        OnSelectedIndexChanged="dgvCompras_SelectedIndexChanged"
                        AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="True"
                        EmptyDataText="No hay compras para los filtros elegidos"
                        CssClass="table table-striped table-hover border align-middle mb-0">
                        <HeaderStyle CssClass="table-info" />
                        <Columns>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField DataField="NumeroFactura" HeaderText="Nro. Factura" />
                            <asp:TemplateField HeaderText="Proveedor">
                                <ItemTemplate>
                                    <%# Eval("ProveedorAsociado.RazonSocial") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CantidadProductos" HeaderText="Productos" />
                            <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
                            <asp:CommandField ShowSelectButton="true" SelectText="Ver detalle" />
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
        </div>

        <!-- detalle de la compra elegida -->
        <asp:Panel ID="pnlDetalle" runat="server" Visible="false">
            <div class="card shadow-sm border-0">
                <div class="card-body p-3 p-md-4">

                    <h4 class="text-center mb-3">
                        <asp:Label ID="lblTituloDetalle" runat="server"></asp:Label>
                    </h4>

                    <div class="table-responsive">
                        <asp:GridView ID="dgvDetalle" runat="server"
                            AutoGenerateColumns="False"
                            CssClass="table table-striped table-hover border align-middle mb-0">
                            <HeaderStyle CssClass="table-info" />
                            <Columns>
                                <asp:TemplateField HeaderText="Producto">
                                    <ItemTemplate>
                                        <%# Eval("ProductoAsociado.Nombre") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="PrecioCompra" HeaderText="Precio" DataFormatString="{0:C}" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </asp:Panel>

    </div>

</asp:Content>