<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="Presentacion.Ventas.Clientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validar() {
            const valNombre = validarRequerido(document.getElementById("txtNombre"));
            const valApellido = validarRequerido(document.getElementById("txtApellido"));
            const valDni = validarDni(document.getElementById("txtDni"));
            const valEmail = validarEmail(document.getElementById("txtEmail"));

            return valNombre && valApellido && valDni && valEmail;
        }
    </script>
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- CONTENEDOR GENERAL -->
    <div class="container-fluid p-2 p-sm-3 bg-transparent">

        <!-- BREADCRUMB (Ruta superior) -->
        <div class="row mb-3">
            <div class="col-12">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0">
                        <li class="breadcrumb-item text-secondary">VENTAS</li>
                        <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Gestión de Clientes</li>
                    </ol>
                </nav>
            </div>
        </div>

        <!-- PRINCIPAL -->
        <div class="row">

            <!-- COLUMNA IZQUIERDA -->
            <div class="col-12 col-lg-7 mb-3">
                <div class="card shadow-sm border-0 h-100">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Listado de Clientes</h3>

                        <asp:UpdatePanel ID="updClientes" runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-12 col-md-8 col-lg-12">
                                        <asp:Panel ID="pnlFiltro" runat="server" DefaultButton="btnBuscar">
                                            <div class="input-group mb-3">
                                                <span class="input-group-text fw-bold">Filtrar</span>
                                                <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" 
                                                    placeholder="Filtrar apellido..." AutoPostBack="true" 
                                                    OnTextChanged="txtFiltro_TextChanged">
                                                </asp:TextBox>
                                                <asp:Button ID="btnBuscar" runat="server" OnClick="txtFiltro_TextChanged" Style="display:none;" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <asp:GridView ID="dgvClientes" runat="server" 
                                        AutoGenerateColumns="False" 
                                        DataKeyNames="Id" 
                                        ShowHeaderWhenEmpty="true"
                                        EmptyDataText="No se encontraron clientes."
                                        OnSelectedIndexChanged="dgvClientes_SelectedIndexChanged"
                                        CssClass="table table-striped table-hover border align-middle mb-0">
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

                                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                            <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                            <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>

            <!-- COLUMNA DERECHA: Formulario de Carga -->
            <div class="col-12 col-lg-5 mb-3">
                <div class="card shadow-sm border-0">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Cliente</h3>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 85px;">Id</span>
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" Disabled="true"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 85px;">Nombre</span>
                            <asp:TextBox ID="txtNombre" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                            <div class="invalid-feedback">El nombre es requerido.</div>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 85px;">Apellido</span>
                            <asp:TextBox ID="txtApellido" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                            <div class="invalid-feedback">El apellido es requerido.</div>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 85px;">DNI</span>
                            <asp:TextBox ID="txtDni" ClientIDMode="Static" runat="server" CssClass="form-control" 
                                MaxLength="8" oninput="soloNumeros(this);"></asp:TextBox>
                            <div class="invalid-feedback">Ingrese un DNI válido (7 u 8 números).</div>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 85px;">Email</span>
                            <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                            <div class="invalid-feedback">Ingrese un email válido (ej: usuario@dominio.com).</div>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold justify-content-center" style="width: 85px;">Activo</span>

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
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClientClick="return validar();" OnClick="btnGrabar_Click" />
                            <asp:Button ID="btnInactivar" runat="server" Text="Inactivar" CssClass="btn btn-outline-warning px-3" CausesValidation="false" OnClick="btnInactivar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>