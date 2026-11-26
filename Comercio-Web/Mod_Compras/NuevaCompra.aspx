<%@ Page Title="Nueva Compra" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaCompra.aspx.cs" Inherits="Presentacion.Mod_Compras.NuevaCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
function validarLinea() {
    const valProducto = validarRequerido(document.getElementById("ddlProducto"));
    const valCantidad = validarRequerido(document.getElementById("txtCantidad"));
    const valPrecio = validarRequerido(document.getElementById("txtPrecioCompra"));

    return valProducto && valCantidad && valPrecio;
}

function validarCompra() {
    const valProveedor = validarRequerido(document.getElementById("ddlProveedor"));
    const valFecha = validarRequerido(document.getElementById("txtFecha"));
    const valFactura = validarRequerido(document.getElementById("txtNumeroFactura"));

    return valProveedor && valFecha && valFactura;
}
</script>
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
                            <asp:DropDownList ID="ddlProveedor" ClientIDMode="Static" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                                <asp:ListItem Text="Seleccione un proveedor" Value="" />
                            </asp:DropDownList>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Fecha</span>
                            <asp:TextBox ID="txtFecha" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Nro. Factura</span>
                            <asp:TextBox ID="txtNumeroFactura" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="8" oninput="soloAlfanumerico(this)"></asp:TextBox>
                        </div>

                    </div>
                </div>

                <div class="card shadow-sm border-0">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Agregar Producto</h3>

                       <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Buscar</span>
                            <input type="text" class="form-control" placeholder="Escriba para filtrar productos" oninput="filtrarDesplegable(this, 'ddlProducto');" />
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Producto</span>
                            <asp:DropDownList ID="ddlProducto"  ClientIDMode="Static" runat="server" CssClass="form-select" AppendDataBoundItems="true">


                                <asp:ListItem Text="Seleccione un producto" Value="" />
                            </asp:DropDownList>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Cantidad</span>
                            <asp:TextBox ID="txtCantidad" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="Number" oninput="soloNumeros(this); limitarCaracteres(this, 6);"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Precio</span>
                            <asp:TextBox ID="txtPrecioCompra" ClientIDMode="Static" runat="server" CssClass="form-control"  oninput="soloDecimales(this, 8);"></asp:TextBox>
                        </div>
                        <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-2" Visible="false"></asp:Label>

                        <div class="d-flex justify-content-center mt-4">
                           <asp:Button ID="btnAgregarLinea" runat="server" Text="Agregar" CssClass="btn btn-primary px-3" OnClientClick="return validarLinea();"  OnClick="btnAgregarLinea_Click" />
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
                                OnRowDeleting="dgvLineasCompra_BorrarLinea"
                                AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True"
                                EmptyDataText="Todavía no hay productos cargados"
                                CssClass="table table-striped table-hover border align-middle mb-0">
                                <HeaderStyle CssClass="table-info" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Producto">
                                        <ItemTemplate>
                                            <%# Eval("ProductoAsociado.Nombre") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="PrecioCompra" HeaderText="Precio" />
                                    <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" />
                                    <asp:CommandField ShowDeleteButton="true" DeleteText="Quitar" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="input-group mt-4">
                            <span class="input-group-text fw-bold" style="width: 110px;">Total</span>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="form-control" ReadOnly="true" Disabled="true"></asp:TextBox>
                        </div>

                        <asp:Label ID="lblExito" runat="server" CssClass="text-success fw-bold d-block mt-3 text-center" Visible="false"></asp:Label>
                        <!-- botones -->
                        <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                            <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar compra" CssClass="btn btn-primary px-3" OnClientClick="return validarCompra();" OnClick="btnConfirmar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" OnClick="btnCancelar_Click" />
                        </div>

                    </div>
                </div>
            </div>
           
        </div>

    </div>

</asp:Content>