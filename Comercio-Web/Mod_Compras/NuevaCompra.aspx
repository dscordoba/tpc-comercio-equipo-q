<%@ Page Title="Nueva Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaCompra.aspx.cs" Inherits="Presentacion.Mod_Compras.NuevaCompra" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- todo el contenido de la pagina -->
    <div class="container-fluid p-2 p-sm-3 bg-transparent">

        <!-- el camino de arriba -->
        <div class="row mb-3">
            <div class="col-12">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0">
                        <li class="breadcrumb-item text-secondary">COMPRAS</li>
                        <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Nueva Compra</li>
                    </ol>
                </nav>
            </div>
        </div>

        <div class="row">

            <!-- datos de la compra y producto a agregar, a la izquierda -->
            <div class="col-12 col-lg-5 mb-3">

                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Datos de la Compra</h3>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Proveedor</span>
                            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Seleccione un proveedor" Value="" />
                            </asp:DropDownList>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Fecha</span>
                            <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Nro. Factura</span>
                            <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                </div>

                <div class="card shadow-sm border-0">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Agregar Producto</h3>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Producto</span>
                            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Seleccione un producto" Value="" />
                            </asp:DropDownList>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Cantidad</span>
                            <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Precio</span>
                            <asp:TextBox ID="txtPrecioCompra" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="d-flex justify-content-center mt-4">
                            <asp:Button ID="btnAgregarLinea" runat="server" Text="Agregar" CssClass="btn btn-primary px-3" />
                        </div>

                    </div>
                </div>

            </div>

            <!-- productos de la compra, a la derecha -->
            <div class="col-12 col-lg-7 mb-3">
                <div class="card shadow-sm border-0 h-100">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Detalle de la Compra</h3>

                        <div class="table-responsive">
                            <asp:GridView ID="dgvLineasCompra" runat="server"
                                AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True"
                                EmptyDataText="Todavía no hay productos cargados"
                                CssClass="table table-striped table-hover border align-middle mb-0">
                                <HeaderStyle CssClass="table-info" />
                                <Columns>
                                    <asp:BoundField DataField="Producto" HeaderText="Producto" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="PrecioCompra" HeaderText="Precio" />
                                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="input-group mt-4">
                            <span class="input-group-text fw-bold" style="width: 110px;">Total</span>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" ReadOnly="true" Disabled="true"></asp:TextBox>
                        </div>

                        <!-- botones -->
                        <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar compra" CssClass="btn btn-primary px-3" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" />
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>