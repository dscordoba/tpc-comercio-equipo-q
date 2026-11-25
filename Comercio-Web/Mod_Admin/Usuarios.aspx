<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="Presentacion.Mod_Admin.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validarUsuario() {
            const valNombre = validarRequerido(document.getElementById("txtNombre"));
            const valApellido = validarRequerido(document.getElementById("txtApellido"));
            const valDni = validarDni(document.getElementById("txtDni"));
            const valEmail = validarEmail(document.getElementById("txtEmail"));
            const valUsuario = validarRequerido(document.getElementById("txtNombreUsuario"));

            // Validar perfil que no sea "0" (Sin asignar)
            const ddlPerfil = document.getElementById("ddlPerfil");
            let valPerfil = true;

            if (ddlPerfil instanceof HTMLSelectElement) {
                if (ddlPerfil.value === "0" || ddlPerfil.value === "") {
                    ddlPerfil.classList.remove("is-valid");
                    ddlPerfil.classList.add("is-invalid");
                    valPerfil = false;
                } else {
                    ddlPerfil.classList.remove("is-invalid");
                    ddlPerfil.classList.add("is-valid");
                }
            }

            // Validar Contraseña (solo si es usuario nuevo)
            const txtId = document.getElementById("txtId");
            const txtPass = document.getElementById("txtContrasenia");
            let valPass = true;

            if (txtId instanceof HTMLInputElement && txtPass) {
                if (txtId.value.trim() === "") {
                    valPass = validarRequerido(txtPass);
                } else {
                    txtPass.classList.remove("is-invalid");
                }
            }

            return valNombre && valApellido && valDni && valEmail && valUsuario && valPerfil && valPass;
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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

                    <div class="row g-2 mb-3 align-items-end">
                        <div class="col-md-4">
                            <label class="form-label fw-bold">Filtrar por Perfil</label>
                            <asp:DropDownList ID="ddlFiltroPerfil" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtros_Changed">
                                <asp:ListItem Text="Todos" Value="-1" />
                                <asp:ListItem Text="Sin asignar" Value="0" />
                                <asp:ListItem Text="Administrador" Value="1" />
                                <asp:ListItem Text="Vendedor" Value="2" />
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-3">
                            <label class="form-label fw-bold">Estado</label>
                            <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtros_Changed">
                                <asp:ListItem Text="Todos" Value="-1" />
                                <asp:ListItem Text="Activos" Value="1" />
                                <asp:ListItem Text="Inactivos" Value="0" />
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-5 d-flex gap-2">
                            <asp:Button ID="btnPendientes" runat="server" Text="Pendientes de alta" CssClass="btn btn-warning fw-bold" OnClick="btnPendientes_Click" />
                            <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Ver todos" CssClass="btn btn-outline-secondary" OnClick="btnLimpiarFiltros_Click" />
                        </div>
                    </div>

                    <div class="table-responsive">
                        <asp:GridView ID="dgvUsuarios" runat="server"
                            AutoGenerateColumns="False"
                            DataKeyNames="Id"
                            OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged"
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
                                <asp:BoundField DataField="TipoPerfil" HeaderText="Perfil" />
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

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">Id</span>
                        <asp:TextBox ID="txtId" ClientIDMode="Static" runat="server" CssClass="form-control" ReadOnly="true" Disabled="true"></asp:TextBox>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">Nombre</span>
                        <asp:TextBox ID="txtNombre" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="invalid-feedback">El nombre es requerido.</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">Apellido</span>
                        <asp:TextBox ID="txtApellido" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="invalid-feedback">El apellido es requerido.</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">DNI</span>
                        <asp:TextBox ID="txtDni" ClientIDMode="Static" runat="server" CssClass="form-control" oninput="soloNumeros(this);" MaxLength="8"></asp:TextBox>
                        <div class="invalid-feedback">Ingrese un DNI válido (7 u 8 números).</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">Email</span>
                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        <div class="invalid-feedback">Ingrese un email válido (ej: usuario@dominio.com)</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">Usuario</span>
                        <asp:TextBox ID="txtNombreUsuario" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="invalid-feedback">El nombre de usuario es requerido.</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">Contraseña</span>
                        <asp:TextBox ID="txtContrasenia" ClientIDMode="Static" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <button class="btn btn-outline-secondary" type="button" onclick="togglePassword('<%= txtContrasenia.ClientID %>', this)">
                            <!-- Ojo abierto (visible por defecto) -->
                            <svg class="icon-eye" xmlns="http://w3.org" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8M1.173 8a13 13 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5s3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5s-3.879-1.168-5.168-2.457A13 13 0 0 1 1.172 8z"/>
                                <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5M4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0"/>
                            </svg>
                            <!-- Ojo cerrado -->
                            <svg class="icon-eye-slash d-none" xmlns="http://w3.org" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                <path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7 7 0 0 0-2.79.588l.77.771A6 6 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755q-.247.248-.517.486z"/>
                                <path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829"/>
                                <path d="M3.35 5.47q-.27.24-.518.487A13 13 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7 7 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12z"/>
                            </svg>
                        </button>
                        <div class="invalid-feedback">La contraseña es requerida para usuarios nuevos.</div>
                    </div>

                    <div class="input-group mb-3">
                        <span class="input-group-text fw-bold" style="width: 110px;">Perfil</span>
                        <asp:DropDownList ID="ddlPerfil" ClientIDMode="Static" runat="server" CssClass="form-select"></asp:DropDownList>
                        <div class="invalid-feedback">Debe seleccionar un perfil válido.</div>
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

                    <!-- botones -->
                    <div class="d-flex flex-column flex-sm-row justify-content-center gap-2 mt-4">
                        <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-primary px-3" OnClientClick="return validarUsuario();" OnClick="btnGrabar_Click" />
                        <asp:Button ID="btnInactivar" runat="server" Text="Inactivar" CssClass="btn btn-outline-warning px-3" CausesValidation="false" OnClick="btnInactivar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-outline-secondary px-3" OnClick="btnCancelar_Click" />
                    </div>

                </div>
            </div>
        </div>

    </div>

</asp:Content>