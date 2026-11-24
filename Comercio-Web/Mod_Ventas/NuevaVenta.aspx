<%@ Page Title="NuevaVenta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevaVenta.aspx.cs" Inherits="Presentacion.Mod_Ventas.NuevaVenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
function validarLineaVenta() {
    const valProducto = validarRequerido(document.getElementById("ddlProducto"));
    const txtCantidad = document.getElementById("txtCantidad");
    const valCantidad = validarRequerido(txtCantidad) && validarNumeroPositivo(txtCantidad);

    return valProducto && valCantidad;
}
    function validarVenta() { 
        const valCliente = validarRequerido(document.getElementById("ddlCliente"));
                
        return valCliente;
    }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Contenedor General -->
    <div class="container-fluid p-2 p-sm-3 bg-transparent">

        <!-- BREADCRUMB (Ruta superior) -->
        <div class="row mb-3">
            <div class="col-12">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0">
                        <li class="breadcrumb-item text-secondary">VENTAS</li>
                        <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Nueva Venta</li>
                    </ol>
                </nav>
            </div>
        </div>

        <!-- Venta Exitosa -->
        <asp:Panel ID="pnlVentaExitosa" runat ="server" Visible="false" CssClass="alert alert-success d-flex align-items-center justify-content-between shadow-sm mb-3" role="alert">
            <div>
                <asp:Label ID="lblMensajeExito" runat="server" CssClass="fw-bold mb-0" Text="¡Venta realizada con éxito!"></asp:Label>
            </div>
            <asp:Button ID="btnAceptarMensaje" runat="server" Text="Aceptar" CssClass="btn btn-success btn-sm ms-3" CausesValidation="false" OnClick="btnAceptarMensaje_Click" />
        </asp:Panel>

        <div class="row">

            <!-- datos de la venta -->
            <div class="col-12 col-lg-5 mb-3">

                <div class="card shadow-sm border-0 mb-3">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Datos de la Venta</h3>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Cliente</span>
                            <asp:DropDownList ID="ddlCliente" ClientIDMode="Static" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Seleccione un cliente" Value="" />
                            </asp:DropDownList>
                            <div class="invalid-feedback">Debe seleccionar un cliente</div>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Fecha</span>
                            <asp:TextBox ID="txtFecha" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="Date" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Nro. Factura</span>
                            <asp:TextBox ID="txtNumeroFactura" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                    </div>
                </div>

                <div class="card shadow-sm border-0">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Agregar Producto</h3>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Producto</span>
                            <asp:DropDownList ID="ddlProducto" ClientIDMode="Static" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged">
                                <asp:ListItem Text="Seleccione un producto" Value="" />
                            </asp:DropDownList>
                            <div class="invalid-feedback">Debe seleccionar un producto</div>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Cantidad</span>
                            <asp:TextBox ID="txtCantidad" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                            <div class="invalid-feedback">Ingrese una cantidad mayor a cero</div>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Precio</span>
                            <asp:TextBox ID="txtPrecioVenta" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="d-flex justify-content-center mt-4">
                            <asp:Button ID="btnAgregarLinea" runat="server" Text="Agregar" CssClass="btn btn-primary px-3" OnClientClick="return validarLineaVenta();" OnClick="btnAgregarLinea_Click" />
                        </div>

                        <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block text-center mt-3" Visible="false"></asp:Label>

                    </div>
                </div>

            </div>

            <!-- Columna Derecha: detalle de la venta -->
            <div class="col-12 col-lg-7 mb-3">
                <div class="card shadow-sm border-0 h-100">
                    <div class="card-body p-3 p-md-4 d-flex flex-column justify-content-between">

                        <div>
                            <h3 class="text-center mb-3 mb-md-4">Detalle de la Venta</h3>

                            <div class="table-responsive">
                                <asp:GridView ID="dgvLineasVenta" runat="server"
                                    AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True"
                                    EmptyDataText="Todavía no hay productos cargados"
                                    CssClass="table table-striped table-hover border align-middle mb-0">
                                    <HeaderStyle CssClass="table-info" />
                                    <Columns>
                                        <asp:BoundField DataField="ProductoAsociado.Nombre" HeaderText="Producto" />
                                        <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                                        <asp:TemplateField HeaderText="Subtotal">
                                            <ItemTemplate>
                                                <%# (Convert.ToInt32(Eval("Cantidad")) * Convert.ToDecimal(Eval("PrecioUnitario"))).ToString("C") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <div>
                            <div class="input-group mt-4">
                                <span class="input-group-text fw-bold" style="width: 110px;">Total</span>
                                <asp:TextBox ID="txtTotalFinal" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>

                            <!-- botones -->
                            <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar venta" CssClass="btn btn-primary px-3" OnClientClick="return validarVenta();" OnClick="btnConfirmar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" CausesValidation="false" OnClick="btnCancelar_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>
</asp:Content>
