<%@ Page Title="Crear Cuenta" Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Presentacion.Registro" %>

<!DOCTYPE html>
<html lang="es" xmlns="http://w3.org">
    <head runat="server">
        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <title>Registrarse - COMERCIO</title>
        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" runat="server" />
        <link href="~/Content/bootstrap.min.css" rel="stylesheet" runat="server" />
        <script src='<%= ResolveUrl("~/Scripts/Validaciones.js") %>'></script>
        <script>
            function validarRegistro() {
                // Oculta cualquier error previo que haya quedado del servidor
                const lblError = document.getElementById("<%= lblError.ClientID %>");
                if (lblError) {
                    lblError.innerText = "";
                    lblError.classList.add("d-none");
                    lblError.style.display = "none";
                }

                const txtPass = document.getElementById("<%= txtPassword.ClientID %>");
                const txtConfirm = document.getElementById("<%= txtConfirmarPassword.ClientID %>");

                const valNombre = validarRequerido(document.getElementById("<%= txtNombre.ClientID %>"));
                const valApellido = validarRequerido(document.getElementById("<%= txtApellido.ClientID %>"));
                const valDni = validarDni(document.getElementById("<%= txtDni.ClientID %>"));
                const valEmail = validarEmail(document.getElementById("<%= txtEmail.ClientID %>"));
                const valPassword = validarRequerido(txtPass);
                const valConfirmar = validarPasswordsIguales(txtPass, txtConfirm);

                return valNombre && valApellido && valDni && valEmail && valPassword && valConfirmar;
            }
        </script>
    </head>
    <body class="d-flex flex-column min-vh-100" style="background-color: #dfe4eb;">

        <asp:Panel ID="pnlExito" runat="server" Visible="false" CssClass="alert alert-success text-center my-4">
            <h5 class="fw-bold">¡Registro enviado con éxito!</h5>
            <p class="mb-3">Tu cuenta fue creada y se encuentra <strong>pendiente de aprobación por un administrador</strong>. Podrás iniciar sesión una vez que sea habilitada.</p>
            <a href="Login.aspx" class="btn btn-primary">Ir al Inicio de Sesión</a>
        </asp:Panel>

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

                                <!-- Datos Personales -->
                                <h6 class="fw-bold text-secondary text-uppercase small mb-3">Datos Personales</h6>

                                <div class="row g-2 mb-3">
                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtNombre.ClientID %>" class="form-label fw-bold text-secondary small">Nombre</label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" oninput="limitarCaracteres(this, 50)" placeholder="Ej. Juan"></asp:TextBox>
                                        <div class="invalid-feedback">El nombre es requerido.</div>
                                    </div>
                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtApellido.ClientID %>" class="form-label fw-bold text-secondary small">Apellido</label>
                                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" oninput="limitarCaracteres(this, 50)" placeholder="Ej. Pérez"></asp:TextBox>
                                        <div class="invalid-feedback">El apellido es requerido.</div>
                                    </div>
                                </div>

                                <div class="row g-2 mb-3">
                                    <div class="col-12 col-sm-5">
                                        <label for="<%= txtDni.ClientID %>" class="form-label fw-bold text-secondary small">DNI</label>
                                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" MaxLength="8" oninput="soloNumeros(this);" placeholder="Ej. 38123456"></asp:TextBox>
                                        <div class="invalid-feedback">Ingrese un DNI válido (7 u 8 números).</div>
                                    </div>
                                    <div class="col-12 col-sm-7">
                                        <label for="<%= txtEmail.ClientID %>" class="form-label fw-bold text-secondary small">Correo Electrónico</label>
                                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" oninput="limitarCaracteres(this, 100)" placeholder="juan@comercio.com"></asp:TextBox>
                                        <div class="invalid-feedback">Ingrese un email válido (ej: usuario@dominio.com).</div>
                                    </div>
                                </div>

                                <hr class="text-muted my-3" />

                                <!-- Datos de Acceso -->
                                <h6 class="fw-bold text-secondary text-uppercase small mb-3">Datos de Acceso</h6>

                                <div class="row g-2 mb-4">
                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtPassword.ClientID %>" class="form-label fw-bold text-secondary small">Contraseña</label>
                                        <div class="input-group has-validation">
                                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" oninput="limitarCaracteres(this, 50)" placeholder="••••••••"></asp:TextBox>
                                            <button class="btn btn-outline-secondary" type="button" onclick="togglePassword('<%= txtPassword.ClientID %>', this)">
                                                <svg class="icon-eye" xmlns="http://w3.org" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                                    <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8M1.173 8a13 13 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5s3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5s-3.879-1.168-5.168-2.457A13 13 0 0 1 1.172 8z"/>
                                                    <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5M4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0"/>
                                                </svg>
                                                <svg class="icon-eye-slash d-none" xmlns="http://w3.org" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                                    <path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7 7 0 0 0-2.79.588l.77.771A6 6 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755q-.247.248-.517.486z"/>
                                                    <path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829"/>
                                                    <path d="M3.35 5.47q-.27.24-.518.487A13 13 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7 7 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12z"/>
                                                </svg>
                                            </button>
                                            <div class="invalid-feedback">La contraseña es requerida.</div>
                                        </div>
                                    </div>

                                    <div class="col-12 col-sm-6">
                                        <label for="<%= txtConfirmarPassword.ClientID %>" class="form-label fw-bold text-secondary small">Repetir Contraseña</label>
                                        <div class="input-group has-validation">
                                            <asp:TextBox ID="txtConfirmarPassword" runat="server" TextMode="Password" CssClass="form-control" oninput="limitarCaracteres(this, 50)" placeholder="••••••••"></asp:TextBox>
                                            <button class="btn btn-outline-secondary" type="button" onclick="togglePassword('<%= txtConfirmarPassword.ClientID %>', this)">
                                                <svg class="icon-eye" xmlns="http://w3.org" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                                    <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8M1.173 8a13 13 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5s3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5s-3.879-1.168-5.168-2.457A13 13 0 0 1 1.172 8z"/>
                                                    <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5M4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0"/>
                                                </svg>
                                                <svg class="icon-eye-slash d-none" xmlns="http://w3.org" width="16" height="16" fill="currentColor" viewBox="0 0 16 16">
                                                    <path d="M13.359 11.238C15.06 9.72 16 8 16 8s-3-5.5-8-5.5a7 7 0 0 0-2.79.588l.77.771A6 6 0 0 1 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13 13 0 0 1 14.828 8q-.086.13-.195.288c-.335.48-.83 1.12-1.465 1.755q-.247.248-.517.486z"/>
                                                    <path d="M11.297 9.176a3.5 3.5 0 0 0-4.474-4.474l.823.823a2.5 2.5 0 0 1 2.829 2.829zm-2.943 1.299.822.822a3.5 3.5 0 0 1-4.474-4.474l.823.823a2.5 2.5 0 0 0 2.829 2.829"/>
                                                    <path d="M3.35 5.47q-.27.24-.518.487A13 13 0 0 0 1.172 8l.195.288c.335.48.83 1.12 1.465 1.755C4.121 11.332 5.881 12.5 8 12.5c.716 0 1.39-.133 2.02-.36l.77.772A7 7 0 0 1 8 13.5C3 13.5 0 8 0 8s.939-1.721 2.641-3.238l.708.709zm10.296 8.884-12-12 .708-.708 12 12z"/>
                                                </svg>
                                            </button>
                                            <div class="invalid-feedback">Las contraseñas no coinciden.</div>
                                        </div>
                                    </div>
                                </div>

                                <asp:Label ID="lblError" runat="server" CssClass="text-danger fw-bold d-block mb-2" Visible="false"></asp:Label>

                                <div class="d-grid mb-3">
                                    <asp:Button ID="btnRegistrar" runat="server" Text="Crear Cuenta »" CssClass="btn btn-primary fw-bold py-2 rounded-pill shadow-sm" OnClientClick="return validarRegistro();" OnClick="btnRegistrar_Click" />
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