<%@ Page Title="Iniciar Sesión" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Presentacion.Login" %>

<!DOCTYPE html>
<html lang="es" xmlns="http://w3.org">
    <head runat="server">
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Iniciar Sesión - COMERCIO</title>
        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" runat="server" />
        <link href="~/Content/bootstrap.min.css" rel="stylesheet" runat="server" />
        <script src='<%= ResolveUrl("~/Scripts/Validaciones.js") %>'></script>
        <script>
            function validar() {
                const valUsuario = validarRequerido(document.getElementById("txtUsuario"));
                const valPassword = validarRequerido(document.getElementById("txtPassword"));

                return valUsuario && valPassword;
            }

        </script>
    </head>
    <body class="d-flex flex-column min-vh-100" style="background-color: #dfe4eb;">

        <form id="formLogin" runat="server" class="d-flex flex-column flex-grow-1">

            <!-- BARRA SUPERIOR (NAV) -->
            <nav class="navbar navbar-dark bg-dark shadow-sm">
                <div class="container-fluid px-3 px-md-4">
                    <a class="navbar-brand fw-bold d-flex align-items-center" href="~/Login.aspx" runat="server">
                        <svg xmlns="http://w3.org" width="22" height="22" fill="currentColor" class="bi bi-shop me-2 text-primary" viewBox="0 0 16 16" aria-hidden="true">
                            <path d="M2.97 1.35A1 1 0 0 1 3.73 1h8.54a1 1 0 0 1 .76.35l2.609 3.044A1.5 1.5 0 0 1 16 5.37v.255a2.375 2.375 0 0 1-4.25 1.458A2.37 2.37 0 0 1 9.875 8 2.37 2.37 0 0 1 8 7.083 2.37 2.37 0 0 1 6.125 8a2.37 2.37 0 0 1-1.875-.917A2.375 2.375 0 0 1 0 5.625V5.37a1.5 1.5 0 0 1 .361-.976zm1.78 4.275a1.375 1.375 0 0 0 2.75 0 .5.5 0 0 1 1 0 1.375 1.375 0 0 0 2.75 0 .5.5 0 0 1 1 0 1.375 1.375 0 1 0 2.75 0V5.37a.5.5 0 0 0-.12-.325L12.27 2H3.73L1.12 5.045A.5.5 0 0 0 1 5.37v.255a1.375 1.375 0 0 0 2.75 0 .5.5 0 0 1 1 0M1.5 8.5A.5.5 0 0 1 2 9v6h1v-5a1 1 0 0 1 1-1h3a1 1 0 0 1 1 1v5h6V9a.5.5 0 0 1 1 0v6h.5a.5.5 0 0 1 0 1H.5a.5.5 0 0 1 0-1H1V9a.5.5 0 0 1 .5-.5M4 15h3v-5H4zm5-5a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v3a1 1 0 0 1-1 1h-2a1 1 0 0 1-1-1zm3 0h-2v3h2z"/>
                        </svg>
                        COMERCIO
                    </a>
                </div>
            </nav>

            <!-- CONTENIDO CENTRAL -->
            <main class="container my-auto py-4">
                <div class="row justify-content-center">
                    <div class="col-12 col-sm-10 col-md-7 col-lg-5 col-xl-4">

                        <div class="text-center mb-4">
                            <div class="d-inline-flex align-items-center justify-content-center bg-primary text-white rounded-circle shadow-sm mb-3" style="width: 60px; height: 60px;">
                                <svg xmlns="http://w3.org" width="30" height="30" fill="currentColor" class="bi bi-person-lock" viewBox="0 0 16 16" aria-hidden="true">
                                    <path d="M11 5a3 3 0 1 1-6 0 3 3 0 0 1 6 0M8 7a2 2 0 1 0 0-4 2 2 0 0 0 0 4m0 5.996V14H3s-1 0-1-1 1-4 6-4q.845.002 1.544.107a4.5 4.5 0 0 0-.803.918A11 11 0 0 0 8 10c-2.29 0-3.516.68-4.168 1.332-.678.678-.83 1.418-.832 1.664zM9 13a1 1 0 0 1 1-1v-1a2 2 0 1 1 4 0v1a1 1 0 0 1 1 1v2a1 1 0 0 1-1 1h-4a1 1 0 0 1-1-1zm3-3a1 1 0 0 0-1 1v1h2v-1a1 1 0 0 0-1-1"/>
                                </svg>
                            </div>
                            <h1 class="fw-bold text-dark fs-3 mb-1">Iniciar Sesión</h1>
                            <p class="text-muted small mb-0">Ingresá tus credenciales para continuar.</p>
                        </div>

                        <!-- Login -->
                        <div class="card border-0 shadow-sm p-3 p-md-4">
                            <div class="card-body p-0">

                                <!-- Mensaje de error / alerta -->
                                <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-2" Visible="false"></asp:Label>

                                <div class="mb-3">
                                    <label for="<%= txtUsuario.ClientID %>" class="form-label fw-bold text-secondary small">Usuario</label>
                                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                                    <div class="invalid-feedback">El usuario es requerido.</div>
                                </div>

                                <div class="mb-2">
                                    <label for="<%= txtPassword.ClientID %>" class="form-label fw-bold text-secondary small">Contraseña</label>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="••••••••"></asp:TextBox>
                                    <div class="invalid-feedback">La contraseña es requerida.</div>                               
                                    </div>

                                <div class="text-end mb-4">
                                    <a href="#" class="text-primary small text-decoration-none fw-semibold" data-bs-toggle="modal" data-bs-target="#modalOlvidePass">
                                        ¿Olvidaste tu contraseña?
                                    </a>
                                </div>

                                <div class="d-grid mb-3">
                                    <asp:Button ID="btnIngresar" runat="server" Text="Ingresar al Sistema »" CssClass="btn btn-primary fw-bold py-2 rounded-pill shadow-sm" OnClientClick="return validar();" OnClick="btnIngresar_Click" />
                                </div>

                                <hr class="text-muted my-3" />

                                <!-- Registro -->
                                <div class="text-center">
                                    <p class="text-muted small mb-2">¿Todavía no tenés una cuenta?</p>
                                    <a href="~/Registro.aspx" runat="server" class="btn btn-outline-dark btn-sm fw-bold px-4 py-2 rounded-pill w-100">
                                        Registrarse &raquo;
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

            <!-- MODAL: RESTABLECER CONTRASEÑA -->
            <div class="modal fade" id="modalOlvidePass" tabindex="-1" aria-labelledby="modalOlvidePassLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content border-0 shadow">
                        <div class="modal-header bg-dark text-white">
                            <h5 class="modal-title fw-bold fs-6" id="modalOlvidePassLabel">
                                <svg xmlns="http://w3.org" width="18" height="18" fill="currentColor" class="bi bi-info-circle-fill me-2 text-primary" viewBox="0 0 16 16" aria-hidden="true">
                                    <path d="M8 16A8 8 0 1 0 8 0a8 8 0 0 0 0 16m.93-9.412-1 4.705c-.07.34.029.533.304.533.194 0 .487-.07.686-.246l-.088.416c-.287.346-.92.598-1.465.598-.703 0-1.002-.422-.808-1.319l.738-3.468c.064-.293.006-.399-.287-.47l-.451-.081.082-.381 2.29-.287zM8 5.5a1 1 0 1 1 0-2 1 1 0 0 1 0 2"/>
                                </svg>
                                Recuperación de Acceso
                            </h5>
                            <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Cerrar"></button>
                        </div>
                        <div class="modal-body text-secondary small">
                            <p class="mb-2">Por razones de seguridad, el restablecimiento de contraseñas lo realiza únicamente el <strong>Administrador del sistema</strong>.</p>
                            <p class="mb-0">Por favor, comunicate con soporte interno o enviá un correo a <strong class="text-dark">admin@comercio.com</strong> indicando tu nombre de usuario y DNI para solicitar el blanqueo de clave.</p>
                        </div>
                        <div class="modal-footer border-0 pt-0">
                            <button type="button" class="btn btn-primary btn-sm fw-bold px-4 rounded-pill" data-bs-dismiss="modal">Entendido</button>
                        </div>
                    </div>
                </div>
            </div>

        </form>

        <script src="<%= ResolveUrl("~/Scripts/bootstrap.bundle.min.js") %>"></script>
        <script type="text/javascript">
            document.addEventListener("keydown", function(event) {
                // Bloquea el Enter solo si el usuario está escribiendo dentro de una caja de texto (TextBox)
                if (event.key === "Enter" && event.target.tagName === "INPUT" && (event.target.type === "text" || event.target.type === "number" || event.target.type === "email")) {
                    event.preventDefault();
                }
            });
        </script>

    </body>
</html>
