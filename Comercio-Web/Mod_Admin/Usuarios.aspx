<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Presentacion.Mod_Admin.Usuarios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- todo el contenido de la pagina -->
    <div class="container-fluid p-2 p-sm-3 bg-transparent">

        <!-- el camino de arriba -->
        <div class="row mb-3">
            <div class="col-12">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0">
                        <li class="breadcrumb-item text-secondary">ADMIN</li>
                        <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Gestión de Usuarios</li>
                    </ol>
                </nav>
            </div>
        </div>

        <div class="row">

            <!-- lista de usuarios, a la izquierda -->
            <div class="col-12 col-lg-7 mb-3">
                <div class="card shadow-sm border-0 h-100">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Listado de Usuarios</h3>

                        <div class="table-responsive">
                            <asp:GridView ID="dgvUsuarios" runat="server"
                                AutoGenerateColumns="False"
                                DataKeyNames="Id"
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
                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Usuario" />
                                    <asp:BoundField DataField="Perfil" HeaderText="Perfil" />
                                    <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <!-- formulario, a la derecha -->
            <div class="col-12 col-lg-5 mb-3">
                <div class="card shadow-sm border-0">
                    <div class="card-body p-3 p-md-4">

                        <h3 class="text-center mb-3 mb-md-4">Usuario</h3>

                        <asp:HiddenField ID="hfUsuarioId" runat="server" />

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Id</span>
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" Disabled="true"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Nombre</span>
                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Apellido</span>
                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">DNI</span>
                            <asp:TextBox ID="txtDni" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Email</span>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Usuario</span>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Contraseña</span>
                            <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Perfil</span>
                            <asp:DropDownList ID="ddlPerfil" runat="server" CssClass="form-select">
                                <asp:ListItem Text="Seleccione un perfil" Value="" />
                                <asp:ListItem Text="Administrador" Value="Administrador" />
                                <asp:ListItem Text="Vendedor" Value="Vendedor" />
                            </asp:DropDownList>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold justify-content-center" style="width: 110px;">Activo</span>

                            <div class="form-control bg-white d-flex align-items-center justify-content-center gap-3">

                                <div class="form-check form-check-inline mb-0 d-flex align-items-center">
                                    <asp:RadioButton ID="rbActivoSI" runat="server" GroupName="rbGrupoActivo" Checked="true" />
                                    <label class="form-check-label ms-2" for="<%= rbActivoSI.ClientID %>">
                                        <span class="badge bg-success px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">SI</span>
                                    </label>
                                </div>

                                <div class="form-check form-check-inline mb-0 d-flex align-items-center">
                                    <asp:RadioButton ID="rbActivoNO" runat="server" GroupName="rbGrupoActivo" />
                                    <label class="form-check-label ms-2" for="<%= rbActivoNO.ClientID %>">
                                        <span class="badge bg-danger px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">NO</span>
                                    </label>
                                </div>

                            </div>
                        </div>

                        <!-- botones -->
                        <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary px-3" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger px-3" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" />
                        </div>

                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>