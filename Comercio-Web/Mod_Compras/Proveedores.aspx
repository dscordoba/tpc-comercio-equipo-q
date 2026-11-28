<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="Presentacion.Compras.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
function validarProveedores() { 
    const valRazonSocial = validarRequerido(document.getElementById("txt.RazonSocial"));
    const valCuit = validarCuit(document.getElementById("txt.Cuit"));
    const valEmail = validarEmail(document.getElementById("txt.Email"));
    const valTelefono = validarTelefono(document.getElementById("txt.Telefono"));

    return valRazonSocial && valCuit && valEmail && valTelefono;
}
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<!-- Contenedor General / Breadcrumb -->
    <div class="row mb-3">
        <div class="col-12">
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb mb-0">
                    <li class="breadcrumb-item text-secondary">COMPRAS</li>
                    <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Gestión de Proveedores</li>
                </ol>
            </nav>
        </div>
    </div>

    <!-- PRINCIPAL -->
    <div class="row">

        <!-- COLUMNA IZQUIERDA: Listado -->
        <div class="col-12 col-lg-7 mb-3">
            <div class="card shadow-sm border-3 h-100">
                <div class="card-body p-3 p-md-4">

                    <h3 class="text-center mb-3 mb-md-4">Listado de Proveedores</h3>

                    <div class="table-responsive">
                        <asp:GridView ID="dgvProveedores" runat="server" AutoGenerateColumns="false" DataKeyNames="Id" OnSelectedIndexChanged="dgvProveedores_SelectedIndexChanged" CssClass="table table-striped table-hover border align-middle mb-0">
                            <HeaderStyle CssClass="table-info" />
                            <Columns>
                                <asp:TemplateField HeaderText="Id" >
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkId" runat="server"
                                            Text='<%# Eval("Id") %>'
                                            CommandName="Select"
                                            CssClass="fw-bold text-decoration-none" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
                                <asp:BoundField DataField="Cuit" HeaderText="CUIT" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
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

                    <h3 class="text-center mb-3 mb-md-4">Proveedor</h3>
                    
                    <asp:HiddenField ID="hfProveedorId" runat="server" />
                    
                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">Id</span>
                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" Enabled="false"></asp:TextBox>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">Razón Social</span>
                        <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="invalid-feedback">La razón social es requerida.</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">CUIT</span>
                        <asp:TextBox ID="txtCuit" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="13" oninput="soloNumeros(this);"></asp:TextBox>
                        <div class="invalid-feedback">Ingrese un CUIT válido (11 dígitos).</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">Email</span>
                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        <div class="invalid-feedback">Ingrese un email válido (ej: usuario@dominio.com).</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">Teléfono</span>
                        <asp:TextBox ID="txtTelefono" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="15" oninput="soloNumeros(this);"></asp:TextBox>
                        <div class="invalid-feedback">Ingrese un teléfono válido.</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold justify-content-center" style="width: 85px;">Activo</span>
                        <div class="form-control bg-white d-flex align-items-center justify-content-center gap-3">

                            <div id="divRbActivoSi" runat="server" class="form-check form-check-inline mb-0 d-flex align-items-center">
                                <asp:RadioButton ID="rbActivoSI" runat="server" GroupName="rbGrupoActivoProv" Checked="true" Enabled="false" />
                                <label class="form-check-label ms-2" for="<%= rbActivoSI.ClientID %>">
                                    <span class="badge bg-success px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">SI</span>
                                </label>
                            </div>
                            <div id="divRbActivoNo" runat="server" class="form-check-inline mb-0 d-flex align-items-center">
                                <asp:RadioButton ID="rbActivoNO" runat="server" GroupName="rbGrupoActivoProv" Enabled="false" />
                                <label class="form-check-label ms-2" for="<%= rbActivoNO.ClientID %>">
                                    <span class="badge bg-danger px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">NO</span>
                                </label>
                            </div>
                        </div>
                    </div>

                    <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-2" Visible="false"></asp:Label>

                    <!-- Botones de Acción -->
                        <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary" OnClientClick="return validarProveedores();" OnClick="btnGrabar_Click" />
                            <asp:Button ID="btnInactivar" runat="server" Text="Inactivar" CssClass="btn btn-outline-warning px-3" CausesValidation="false" OnClick="btnInactivar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" CausesValidation="false" OnClick="btnCancelar_Click" />
                        </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>