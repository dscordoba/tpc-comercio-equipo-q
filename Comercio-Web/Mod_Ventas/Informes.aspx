<%@ Page Title="Informe de Ventas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="Comercio_Web.Mod_Ventas.InformeVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row mb-3">
        <div class="col-12">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb mb-0">
                    <li class="breadcrumb-item text-secondary">VENTAS</li>
                    <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Informes</li>
                </ol>
            </nav>
        </div>
    </div>

    <div class="d-flex justify-content-between align-items-center mb-3">
        <asp:Label ID="lblError" runat="server" CssClass="alert alert-danger py-1 px-3 mb-0" Visible="false"></asp:Label>
    </div>

    <!-- Filtros -->
    <div class="card border-0 shadow-sm mb-4">
        <div class="card-body">

            <div class="d-flex justify-content-center align-items-center mb-4">
                <h3 class="fw-bold text-primary mb-0">Informe de Ventas</h3>
            </div>

            <div class="row g-3 align-items-end">
                <div class="col-md-2">
                    <label class="form-label fw-bold">Desde</label>
                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label class="form-label fw-bold">Hasta</label>
                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="form-label fw-bold">Cliente o N° Factura</label>
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Ej: Pérez o 00000001"></asp:TextBox>
                </div>
                <!-- Filtro Vendedor (Solo visible para Admin) -->
                <div id="divFiltroVendedor" runat="server" class="col-md-2">
                    <label class="form-label fw-bold">Vendedor</label>
                    <asp:DropDownList ID="ddlVendedor" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="col-md-3 d-flex gap-2 ms-auto">
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary w-100" OnClick="btnLimpiar_Click" />
                </div>
            </div>
        </div>
    </div>

    <!-- Resumen -->
    <div class="row g-3 mb-4">
        <div class="col-md-6">
            <div class="card border-0 shadow-sm bg-light">
                <div class="card-body d-flex justify-content-between align-items-center">
                    <span class="fw-bold text-secondary">Cantidad de Ventas</span>
                    <asp:Label ID="lblCantidadVentas" runat="server" CssClass="fs-4 fw-bold text-dark" Text="0"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="card border-0 shadow-sm bg-light">
                <div class="card-body d-flex justify-content-between align-items-center">
                    <span class="fw-bold text-secondary">Total Facturado</span>
                    <asp:Label ID="lblTotalFacturado" runat="server" CssClass="fs-4 fw-bold text-success" Text="$ 0,00"></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <!-- Grilla de Resultados -->
    <div class="card border-0 shadow-sm">
        <div class="card-body">
            <div class="table-responsive">
                <asp:GridView ID="dgvVentas" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                CssClass="table table-hover align-middle mb-0" GridLines="None"
                                EmptyDataText="No se encontraron ventas para los filtros seleccionados."
                                OnSelectedIndexChanged="dgvVentas_SelectedIndexChanged">
                    <EmptyDataRowStyle CssClass="text-center py-3 text-muted" />
                    <HeaderStyle CssClass="table-light fw-bold" />
                    <Columns>
                        <asp:BoundField DataField="NumeroFactura" HeaderText="N° Factura" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Cliente">
                            <ItemTemplate>
                                <%# Eval("ClienteAsociado.Apellido") + ", " + Eval("ClienteAsociado.Nombre") %>
                                <small class="text-muted d-block">DNI: <%# Eval("ClienteAsociado.Dni") %></small>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vendedor">
                            <ItemTemplate>
                                <%# Eval("UsuarioAsociado.Apellido") + ", " + Eval("UsuarioAsociado.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TotalFinal" HeaderText="Total" DataFormatString="{0:C2}" ItemStyle-CssClass="fw-bold text-success" />
                        <asp:CommandField ShowSelectButton="true" SelectText="Ver Detalle" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <!-- MODAL DETALLE DE FACTURA -->
    <div class="modal fade" id="modalDetalleFactura" tabindex="-1" aria-labelledby="modalDetalleFacturaLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title fw-bold" id="modalDetalleFacturaLabel">
                        Detalle de Factura N° <asp:Label ID="lblModalNroFactura" runat="server"></asp:Label>
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                </div>
                <div class="modal-body">
                    <!-- Cabecera de la factura -->
                    <div class="row mb-3 p-2 bg-light rounded">
                        <div class="col-md-6">
                            <p class="mb-1"><strong>Fecha:</strong> <asp:Label ID="lblModalFecha" runat="server"></asp:Label></p>
                            <p class="mb-0"><strong>Vendedor:</strong> <asp:Label ID="lblModalVendedor" runat="server"></asp:Label></p>
                        </div>
                        <div class="col-md-6">
                            <p class="mb-1"><strong>Cliente:</strong> <asp:Label ID="lblModalCliente" runat="server"></asp:Label></p>
                            <p class="mb-0"><strong>DNI:</strong> <asp:Label ID="lblModalDni" runat="server"></asp:Label></p>
                        </div>
                    </div>

                    <!-- Tabla de Productos -->
                    <div class="table-responsive">
                        <asp:GridView ID="dgvDetalleFactura" runat="server" AutoGenerateColumns="false"
                                      CssClass="table table-sm table-bordered align-middle mb-0">
                            <HeaderStyle CssClass="table-light fw-bold" />
                            <Columns>
                                <asp:BoundField DataField="ProductoAsociado.CodigoProducto" HeaderText="Código" />
                                <asp:BoundField DataField="ProductoAsociado.Nombre" HeaderText="Producto" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cant." ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." DataFormatString="{0:C2}" ItemStyle-CssClass="text-end" HeaderStyle-CssClass="text-end" />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C2}" ItemStyle-CssClass="text-end fw-bold" HeaderStyle-CssClass="text-end" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <!-- Total -->
                    <div class="d-flex justify-content-end mt-3">
                        <h4 class="fw-bold text-success mb-0">
                            Total: <asp:Label ID="lblModalTotal" runat="server"></asp:Label>
                        </h4>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>