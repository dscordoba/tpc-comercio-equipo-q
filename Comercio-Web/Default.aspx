<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container-fluid py-3 py-md-4 px-2 px-md-3">
        <!-- Bienvenida -->
        <div class="row mb-3 mb-md-4">
            <div class="col-12">
                <h1 class="fw-bold text-dark fs-2 fs-md-1">Panel de Control</h1>
                <p class="text-muted mb-0">Bienvenido al sistema de gestión. Selecciona una acción para comenzar.</p>
            </div>
        </div>

        <!-- ACCESOS RÁPIDOS -->
        <div class="row g-3 g-md-4 mb-4 mb-md-5">
            <!-- Nueva Venta -->
            <div class="col-12 col-md-6">
                <div class="card h-100 border-0 shadow-sm bg-primary text-white p-3 p-md-4 position-relative overflow-hidden">
                    <div class="card-body d-flex flex-column justify-content-between p-0">
                        <div>
                            <div class="mb-2">
                                <svg xmlns="http://www.w3.org/2000/svg" width="38" height="38" fill="currentColor" class="bi bi-cash-coin" viewBox="0 0 16 16" aria-hidden="true">
                                    <path fill-rule="evenodd" d="M11 15a4 4 0 1 0 0-8 4 4 0 0 0 0 8m5-4a5 5 0 1 1-10 0 5 5 0 0 1 10 0"/>
                                    <path d="M9.438 11.944c.047.596.518 1.06 1.363 1.116v.44h.375v-.443c.875-.061 1.386-.529 1.386-1.207 0-.618-.39-.936-1.09-1.1l-.296-.07v-1.2c.376.043.614.248.671.532h.658c-.047-.575-.472-1.024-1.329-1.073V8.5h-.375v.45c-.747.073-1.255.522-1.255 1.158 0 .562.378.92 1.007 1.066l.248.061v1.272c-.384-.058-.639-.27-.696-.563h-.668zm1.36-1.354c-.369-.085-.569-.26-.569-.522 0-.294.216-.514.572-.578v1.1h-.003zm.432.746c.449.104.655.272.655.569 0 .339-.257.571-.709.614v-1.195zM1 0a1 1 0 0 0-1 1v8a1 1 0 0 0 1 1h4.083q.088-.517.258-1H1V2h13v2.214q.54.2 1 .518V1a1 1 0 0 0-1-1z"/>
                                </svg>
                            </div>
                            <h3 class="card-title fw-bold">Nueva Venta</h3>
                            <p class="card-text text-white-50">Registra una salida de mercancía y genera comprobantes a clientes.</p>
                        </div>
                        <div class="mt-3 mt-md-4">
                            <a href="~/Mod_Ventas/NuevaVenta.aspx" runat="server" class="btn btn-light fw-bold px-4 py-2 rounded-pill shadow-sm">Iniciar Venta &raquo;</a>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Nueva Compra -->
            <div class="col-12 col-md-6">
                <div class="card h-100 border-0 shadow-sm bg-dark text-white p-3 p-md-4">
                    <div class="card-body d-flex flex-column justify-content-between p-0">
                        <div>
                            <div class="mb-2">
                                <svg xmlns="http://www.w3.org/2000/svg" width="38" height="38" fill="currentColor" class="bi bi-box-seam" viewBox="0 0 16 16" aria-hidden="true">
                                    <path d="M8.186 1.113a.5.5 0 0 0-.372 0L1.846 3.5l2.404.961L10.404 2zm3.564 1.426L5.596 5 8 5.961 14.154 3.5zm3.25 1.7-6.5 2.6v7.922l6.5-2.6V4.24zM7.5 14.762V6.838L1 4.239v7.923zM7.443.184a1.5 1.5 0 0 1 1.114 0l7.129 2.852A.5.5 0 0 1 16 3.5v8.662a1 1 0 0 1-.629.928l-7.185 2.874a.5.5 0 0 1-.372 0L.63 13.09a1 1 0 0 1-.63-.928V3.5a.5.5 0 0 1 .314-.464z"/>
                                </svg>
                            </div>
                            <h3 class="card-title fw-bold">Nueva Compra</h3>
                            <p class="card-text text-white-50">Ingresa stock al inventario y actualiza costos.</p>
                        </div>
                        <div class="mt-3 mt-md-4">
                            <a href="~/Mod_Compras/NuevaCompra.aspx" runat="server" class="btn btn-outline-light fw-bold px-4 py-2 rounded-pill">Registrar Compra &raquo;</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- INFORMACIÓN -->
        <div class="row g-3 g-md-4">
            <!-- Resumen del día -->
            <div class="col-12 col-md-4">
                <div class="card h-100 border-0 shadow-sm p-3">
                    <div class="card-body p-0">
                        <h5 class="fw-bold text-secondary mb-3 d-flex align-items-center">
                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-graph-up-arrow me-2 text-primary" viewBox="0 0 16 16" aria-hidden="true">
                                <path fill-rule="evenodd" d="M0 0h1v15h15v1H0zm10 3.5a.5.5 0 0 1 .5-.5h4a.5.5 0 0 1 .5.5v4a.5.5 0 0 1-1 0V4.9l-3.613 4.417a.5.5 0 0 1-.74.037L7.06 6.767l-3.656 5.027a.5.5 0 0 1-.808-.588l4-5.5a.5.5 0 0 1 .758-.06l2.609 2.61L13.445 4H10.5a.5.5 0 0 1-.5-.5"/>
                            </svg>
                            Resumen del Día
                        </h5>
                        <ul class="list-group list-group-flush small">
                            <li class="list-group-item d-flex justify-content-between align-items-center px-0">
                                Ventas concretadas
                                <span class="badge bg-success rounded-pill">12</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center px-0">
                                Compras ingresadas
                                <span class="badge bg-dark rounded-pill">3</span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

            <!-- Alertas de Inventario -->
            <div class="col-12 col-md-4">
                <div class="card h-100 border-0 shadow-sm p-3">
                    <div class="card-body p-0 d-flex flex-column justify-content-between">
                        <div>
                            <h5 class="fw-bold text-secondary mb-3 d-flex align-items-center">
                                <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-exclamation-triangle-fill me-2 text-warning" viewBox="0 0 16 16" aria-hidden="true">
                                    <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5m.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2"/>
                                </svg>
                                Alertas de Stock
                            </h5>
                            <p class="card-text text-muted small">Hay productos que alcanzaron su stock mínimo de reposición.</p>
                        </div>
                        <div class="mt-2">
                            <a href="~/Mod_Inventario/Productos.aspx" runat="server" class="btn btn-link text-danger p-0 fw-bold small text-decoration-none">Ver productos faltantes &raquo;</a>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Accesos de Administración -->
            <div class="col-12 col-md-4">
                <div class="card h-100 border-0 shadow-sm p-3">
                    <div class="card-body p-0">
                        <h5 class="fw-bold text-secondary mb-3 d-flex align-items-center">
                            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor" class="bi bi-gear-fill me-2 text-secondary" viewBox="0 0 16 16" aria-hidden="true">
                                <path d="M9.405 1.05c-.413-1.4-2.397-1.4-2.81 0l-.1.34a1.464 1.464 0 0 1-2.105.872l-.31-.17c-1.283-.698-2.686.705-1.987 1.987l.169.311c.446.82.023 1.841-.872 2.105l-.34.1c-1.4.413-1.4 2.397 0 2.81l.34.1a1.464 1.464 0 0 1 .872 2.105l-.17.31c-.698 1.283.705 2.686 1.987 1.987l.311-.169a1.464 1.464 0 0 1 2.105.872l.1.34c.413 1.4 2.397 1.4 2.81 0l.1-.34a1.464 1.464 0 0 1 2.105-.872l.31.17c1.283.698 2.686-.705 1.987-1.987l-.169-.311a1.464 1.464 0 0 1 .872-2.105l.34-.1c1.4-.413 1.4-2.397 0-2.81l-.34-.1a1.464 1.464 0 0 1-.872-2.105l.17-.31c.698-1.283-.705-2.686-1.987-1.987l-.311.169a1.464 1.464 0 0 1-2.105-.872zM8 10.93a2.929 2.929 0 1 1 0-5.858 2.929 2.929 0 0 1 0 5.858z"/>
                            </svg>
                            Configuración Rápida
                        </h5>
                        <div class="d-grid gap-2">
                            <a href="~/Mod_Admin/Usuarios.aspx" runat="server" class="btn btn-outline-secondary btn-sm text-start">Gestión de Usuarios</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>