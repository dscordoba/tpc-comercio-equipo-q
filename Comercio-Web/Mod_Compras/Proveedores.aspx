<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="Presentacion.Compras.Proveedores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <asp:GridView ID="dgvProveedores" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover border align-middle mb-0">
                            <HeaderStyle CssClass="table-info" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" />
                                <asp:BoundField DataField="RazonSocial" HeaderText="Razón Social" />
                                <asp:BoundField DataField="Cuit" HeaderText="CUIT" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                                <asp:BoundField DataField="Activo" HeaderText="Activo" />
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
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">CUIT</span>
                        <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">Email</span>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 130px;">Teléfono</span>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold justify-content-center" style="width: 130px;">Activo</span>
                        <div class="form-control bg-white d-flex align-items-center justify-content-center gap-3">
                            <div class="form-check form-check-inline mb-0 d-flex align-items-center">
                                <asp:RadioButton ID="rbActivoSI" runat="server" GroupName="rbGrupoActivoProv" Checked="true" />
                                <label class="form-check-label ms-2" for="<%= rbActivoSI.ClientID %>">
                                    <span class="badge bg-success px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">SI</span>
                                </label>
                            </div>
                            <div class="form-check form-check-inline mb-0 d-flex align-items-center">
                                <asp:RadioButton ID="rbActivoNO" runat="server" GroupName="rbGrupoActivoProv" />
                                <label class="form-check-label ms-2" for="<%= rbActivoNO.ClientID %>">
                                    <span class="badge bg-danger px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">NO</span>
                                </label>
                            </div>
                        </div>
                    </div>

                    <!-- Botones de Acción -->
                        <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary px-3" OnClick="btnGrabar_Click" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger px-3" OnClick="btnEliminar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" OnClick="btnCancelar_Click" />
                        </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>