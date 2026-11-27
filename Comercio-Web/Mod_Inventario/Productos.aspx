<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="Presentacion.Inventario.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validarProducto() {
            const valCodigo         = validarRequerido(document.getElementById("txtCodigoArt"));
            const valNombre         = validarRequerido(document.getElementById("txtNombre"));
            const valCategoria      = validarRequerido(document.getElementById("ddlCategoria"));
            const valMarca          = validarRequerido(document.getElementById("ddlMarca"));
            const valProveedores    = validarListaMultiple(document.getElementById("lbProveedores"));
            const valPorcentaje     = validarNumeroPositivo(document.getElementById("txtPorcentajeGanancia"), true, 999.99);

            const txtStockMin       = document.getElementById("txtStockMinimo");
            const valStockMin       = validarRequerido(txtStockMin) && soloNumeros(txtStockMin);

            return valCodigo && valNombre && valCategoria && valMarca && valProveedores && valPorcentaje && valStockMin;
        }
    </script>
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- BREADCRUMB (Ruta superior) -->
    <div class="row mb-3">
        <div class="col-12">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb mb-0">
                    <li class="breadcrumb-item text-secondary">INVENTARIO</li>
                    <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Gestión de Productos</li>
                </ol>
            </nav>
        </div>
    </div>

    <!-- PRINCIPAL -->
    <asp:UpdatePanel ID="updProductos" runat="server">
          <ContentTemplate>
            <div class="row">

                <!-- COLUMNA IZQUIERDA -->
                <div class="col-12 col-lg-7 mb-3">
                    <div class="card shadow-sm border-3 h-100">
                        <div class="card-body p-3 p-md-4">

                            <h3 class="text-center mb-3 mb-md-4">Listado de Productos</h3>

                            <div class="row">
                                <div class="col-12 col-md-8 col-lg-12">
                                    <asp:Panel ID="pnlFiltro" runat="server" DefaultButton="btnBuscar">
                                        <div class="input-group mb-3">
                                            <span class="input-group-text fw-bold">Filtrar</span>
                                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="Filtrar código o nombre..." AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged">
                                            </asp:TextBox>
                                            <asp:Button ID="btnBuscar" runat="server" OnClick="txtFiltro_TextChanged" Style="display:none;" />
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnSelectedIndexChanged="dgvProductos_SelectedIndexChanged" 
                                    CssClass="table table-striped table-hover border align-middle mb-0"  ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron productos.">
                                    <HeaderStyle CssClass="table-info" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Id">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkId" runat="server" 
                                                    Text='<%# Eval("Id") %>' 
                                                    CommandName="Select" 
                                                    CssClass="fw-bold text-decoration-none" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CodigoProducto" HeaderText="Código" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" />
                                        <asp:BoundField DataField="PorcentajeGanancia" HeaderText="Porcentaje Ganancia" />
                                        <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>

                <!-- COLUMNA DERECHA: Formulario de Carga -->
                <div class="col-12 col-lg-5 mb-3">
                    <div class="card shadow-sm border-3">
                        <div class="card-body p-3 p-md-4">

                            <h3 class="text-center mb-3 mb-md-4">Producto</h3>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;">Id</span>
                                <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" Disabled="true"></asp:TextBox>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;" title="Código Artículo">Cod. Art.</span>
                                <asp:TextBox ID="txtCodigoArt" ClientIDMode="Static" runat="server" CssClass="form-control" oninput="limitarCaracteres(this, 9)"></asp:TextBox>
                                <div class="invalid-feedback">El código de artículo es requerido.</div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;">Nombre</span>
                                <asp:TextBox ID="txtNombre" ClientIDMode="Static" runat="server" CssClass="form-control" oninput="limitarCaracteres(this, 50)"></asp:TextBox>
                                <div class="invalid-feedback">El nombre es requerido.</div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;">Descripción</span>
                                <asp:TextBox ID="txtDescripcion" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2" oninput="limitarCaracteres(this, 250)"></asp:TextBox>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;">Categoría</span>
                                <asp:DropDownList ID="ddlCategoria" ClientIDMode="Static" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Seleccione una categoría" Value="" />
                                </asp:DropDownList>
                                <div class="invalid-feedback">Debe seleccionar una categoría.</div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;">Marca</span>
                                <asp:DropDownList ID="ddlMarca" ClientIDMode="Static" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Seleccione una marca" Value="" />
                                </asp:DropDownList>
                                <div class="invalid-feedback">Debe seleccionar una marca.</div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;">Proveedores</span>
                                <asp:ListBox ID="lbProveedores" ClientIDMode="Static" runat="server" SelectionMode="Multiple" CssClass="form-select" Rows="4">
                                </asp:ListBox>
                                <div class="invalid-feedback">Debe seleccionar al menos un proveedor.</div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;">Precio Actual</span>
                                <asp:TextBox ID="txtPrecioActual" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;" data-bs-toggle="tooltip" title="Porcentaje Ganancia">% Ganancia</span>
                                <asp:TextBox ID="txtPorcentajeGanancia" ClientIDMode="Static" runat="server" CssClass="form-control" oninput="soloDecimales(this, 3); limitarCaracteres(this, 8);"></asp:TextBox>
                                <div class="invalid-feedback">Ingrese un porcentaje de ganancia válido.</div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;" title="Stock Actual">Stock Act.</span>
                                <asp:TextBox ID="txtStockActual" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold" style="width: 110px;" data-bs-toggle="tooltip" title="Stock Mínimo">Stock Mín.</span>
                                <asp:TextBox ID="txtStockMinimo" ClientIDMode="Static" runat="server" CssClass="form-control" oninput="soloNumeros(this); limitarCaracteres(this, 6);"></asp:TextBox>
                                <div class="invalid-feedback">Ingrese un stock mínimo válido.</div>
                            </div>

                            <div class="input-group mb-3">
                                <span class="input-group-text fw-bold justify-content-center" style="width: 110px;">Activo</span>

                                <div class="form-control bg-white d-flex align-items-center justify-content-center gap-3">
        
                                    <div id="divRbActivoSi" runat="server" class="form-check form-check-inline mb-0 d-flex align-items-center">
                                        <asp:RadioButton ID="rbActivoSI" runat="server" GroupName="rbGrupoActivo" Checked="true" Enabled="false" />
                                        <label class="form-check-label ms-2" for="<%= rbActivoSI.ClientID %>">
                                            <span class="badge bg-success px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">SI</span>
                                        </label>
                                    </div>

                                    <div id="divRbActivoNo" runat="server" class="form-check form-check-inline mb-0 d-flex align-items-center">
                                        <asp:RadioButton ID="rbActivoNO" runat="server" GroupName="rbGrupoActivo" Enabled="false" />
                                        <label class="form-check-label ms-2" for="<%= rbActivoNO.ClientID %>">
                                            <span class="badge bg-danger px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">NO</span>
                                        </label>
                                    </div>

                                </div>
                            </div>

                            <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-2" Visible="false"></asp:Label>

                            <!-- Botones de Acción -->
                            <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                                <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClientClick="return validarProducto();" OnClick="btnGrabar_Click" />
                                <asp:Button ID="btnInactivar" runat="server" Text="Inactivar" CssClass="btn btn-outline-warning px-3" CausesValidation="false" OnClick="btnInactivar_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" CausesValidation="false" OnClick= "btnCancelar_Click" />
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>