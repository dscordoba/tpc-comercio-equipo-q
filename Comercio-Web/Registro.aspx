<%@ Page Title="Crear Cuenta" Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Presentacion.Registro" %>

<!DOCTYPE html>
<html lang="es" xmlns="http://w3.org">
    <head runat="server">
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Registrarse - COMERCIO</title>
        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" runat="server" />
        <link href="~/Content/bootstrap.min.css" rel="stylesheet" runat="server" />
    </head>
    <body class="d-flex flex-column min-vh-100" style="background-color: #dfe4eb;">

        <form id="formRegistro" runat="server" class="d-flex flex-column flex-grow-1">

            <!-- BARRA SUPERIOR (NAV) -->
            <nav class="navbar navbar-dark bg-dark shadow-sm">
                <div class="container-fluid px-3 px-md-4">
                    <a class="navbar-brand fw-bold d-flex align-items-center" href="~/Login.aspx" runat="server">
                        <svg xmlns="http://w3.org" width="22" height="22" fill="currentColor" class="bi bi-shop me-2 text-primary" viewBox="0 0 16 16" aria-hidden="true">
                            <path d="M2.97 1.35A1 1 0 0 1 3.73 1h8.54a1 1 0 0 1 .76.35l2.609 3.044A1.5 1.5 0 0 1 16 5.37v.255a2.375 2.375 0 0 1-4.25 1.458A2.37 2.375 0 0 1 9.875 8 2.37 2.37 0 0 1 8 7.083 2.37 2.37 0 0 1 6.125 8a2.37 2.37 0 0 1-1.875-.917A2.375 2.375 0 0 1 0 5.625V5.37a1.5 1.5 0 0 1 .361-.976zm1.78 4.275a1.375 1.375 0 0 0 2.75 0 .5.5 0 0 1 1 0 1.375 1.375 0 0 0 2.75 0 .5.5 0 0 1 1 0 1.375 1.375 0 1 0 2.75 0V5.37a.5.5 0 0 0-.12-.325L12.27 2H3.73L1.12 5.045A.5.5 0 0 0 1 5.37v.255a1.375 1.375 0 0 0 2.75 0 .5.5 0 0 1 1 0M1.5 8.5A.5.5 0 0 1 2 9v6h1v-5a1 1 0 0 1 1-1h3a1 1 0 0 1 1 1v5h6V9a.5.5 0 0 1 1 0v6h.5a.5.5 0 0 1 0 1H.5a.5.5 0 0 1 0-1H1V9a.5.5 0 0 1 .5-.5M4 15h3v-5H4zm5-5a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v3a1 1 0 0 1-1 1h-2a1 1 0 0 1-1-1zm3 0h-2v3h2z"/>
                        </svg>
                        COMERCIO
                    </a>
                </div>
            </nav>

            <!-- CONTENIDO CENTRAL -->
            <main class="container my-auto py-4">
                <div class="row justify-content-center">
                    <div class="col-12 col-sm-11 col-md-8 col-lg-6 col-xl-5">

                        <div class="text-center mb-4">
                            <div class="d-inline-flex align-items-center justify-content-center bg-primary text-white rounded-circle shadow-sm mb-3" style="width: 60px; height: 60px;">
                                <svg xmlns="http://w3.org" width="30" height="30" fill="currentColor" class="bi bi-person-plus-fill" viewBox="0 0 16 16" aria-hidden="true">
                                    <path d="M1 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1zm5-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6"/>
                                    <path fill-rule="evenodd" d="M13.5 5a.5.5 0 0 1 .5.5V7h1.5a.5.5 0 0 1 0 1H14v1.5a.5.5 0 0 1-1 0V8h-1.5a.5.5 0 0 1 0-1H13V5.5a.5.5 0 0 1 .5-.5"/>
                                </svg>
                            </div>
                            <h1 class="fw-bold text-dark fs-3 mb-1">Crear Cuenta</h1>
                            <p class="text-muted small mb-0">Completá tus datos personales para registrarte en el sistema.</p>
                        </div>

                        <!-- Registro -->
                        <div class="card border-0 shadow-sm p-3 p-md-4">
                            <div class="card-body p-0">

                                <!-- Mensaje de error / alerta -->
                                <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-danger d-flex align-items-center small py-2 mb-3" role="alert">
                                    <svg xmlns="http://w3.org" width="16" height="16" fill="currentColor" class="bi bi-exclamation-triangle-fill me-2 flex-shrink-0" viewBox="0 0 16 16" aria-hidden="true">
                                        <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>
                                    </svg>
                                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                                </asp:Panel>

                                <!-- Datos Personales -->
                                <h6 class="fw-bold text-secondary text-uppercase small mb-3">Datos Personales</h6>

                                <div class="row g-2 mb-3">
                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtNombre.ClientID %>" class="form-label fw-bold text-secondary small">Nombre</label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ej. Juan"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtApellido.ClientID %>" class="form-label fw-bold text-secondary small">Apellido</label>
                                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Ej. Pérez"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row g-2 mb-3">
                                    <div class="col-12 col-sm-5">
                                        <label for="<%= txtDni.ClientID %>" class="form-label fw-bold text-secondary small">DNI</label>
                                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" MaxLength="8" placeholder="Ej. 38123456"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-sm-7">
                                        <label for="<%= txtEmail.ClientID %>" class="form-label fw-bold text-secondary small">Correo Electrónico</label>
                                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" placeholder="juan@comercio.com"></asp:TextBox>
                                    </div>
                                </div>

                                <hr class="text-muted my-3" />

                                <!-- Datos de Acceso -->
                                <h6 class="fw-bold text-secondary text-uppercase small mb-3">Datos de Acceso</h6>

                                <div class="row g-2 mb-4">
                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtPassword.ClientID %>" class="form-label fw-bold text-secondary small">Contraseña</label>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="••••••••"></asp:TextBox>
                                    </div>
                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtConfirmarPassword.ClientID %>" class="form-label fw-bold text-secondary small">Repetir Contraseña</label>
                                        <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="••••••••"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="d-grid mb-3">
                                    <asp:Button ID="btnRegistrar" runat="server" Text="Crear Cuenta »" CssClass="btn btn-primary fw-bold py-2 rounded-pill shadow-sm" OnClick="btnRegistrar_Click" />
                                </div>

                                <hr class="text-muted my-3" />

                                <!-- Volver al Login -->
                                <div class="text-center">
                                    <p class="text-muted small mb-2">¿Ya tenés una cuenta registrada?</p>
                                    <a href="~/Login.aspx" runat="server" class="btn btn-outline-dark btn-sm fw-bold px-4 py-2 rounded-pill w-100">
                                        &laquo; Volver a Iniciar Sesión
                                    </a>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </main>

            <footer class="sticky-footer bg-white py-3 border-top mt-auto">
                <div class="container my-auto">
                    <div class="copyright text-center my-auto">
                        <span class="text-muted small">&copy; <%: DateTime.Now.Year %> <strong class="text-dark">Nuestra APP WEB</strong> Todos los derechos reservados.</span>
                    </div>
                </div>
            </footer>

        </form>

        <script src="<%= ResolveUrl("~/Scripts/bootstrap.bundle.min.js") %>"></script>

    </body>
</html>

