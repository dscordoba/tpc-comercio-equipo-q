<%@ Page Title="Gestión de Categorías" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="Presentacion.Mod_Inventario.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!-- CONTENEDOR GENERAL -->
    <div class="container-fluid p-2 p-sm-3 bg-transparent">

        <!-- BREADCRUMB (ruta superior) -->
        <div class="row mb-3">
            <div class="col-12">
                <nav aria-label="breadcrumb">
                    <ol class="breadcrumb mb-0">
                        <li class="breadcrumb-item text-secondary">INVENTARIO</li>
                        <li class="breadcrumb-item active fw-bold text-dark" aria-current="page">Gestión de Categorias</li>
                    </ol>
                </nav>
            </div>
        </div>

        <!--PRINCIPAL -->
        <div class="row">

            <!-- COLUMNA IZQUIERDA: Listado -->
            <div class="col-12 col-lg-7 mb-3">
                <div class="card shadow-sm border-3 h-100">
                    <div class="card-body p-3 p-md-4">
                        <h3 class="text-center mb-3 mb-md-4">Listado de Categorias</h3>
                        <div class="table-responsive">
                            <asp:GridView ID="dgvCategorias" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover border align-middle mb-0">
                                <HeaderStyle CssClass="table-info" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" />

                                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />

                                    <asp:CheckBoxField DataField="Activo" HeaderText="Activo" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <!-- COLUMNA DERECHA: formulario de Carga -->
            <div class="col-12 col-lg-5 mb-3">
                <div class="card shadow-sm border-3">
                    <div class="card-body p-3 p-md-4">
                        <h3 class="text-center mb-3 mb-md-4">Categoría</h3>

                        <asp:HiddenField ID="hfCategoriaId" runat="server" />

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Id</span>
                            <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" Disabled="true"></asp:TextBox>
                        </div>

                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold" style="width: 110px;">Descripción</span>
                            <asp:Textbox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:Textbox>
                        </div>

                        <!-- RadioButton para Activo -->
                        <div class="input-group mb-3">
                            <span class="input-group-text fw-bold justify-content-center" style="width: 110px;">Activo</span>
                            <div class="form-control bg-white d-flex align-items-center justify-content-center gap-3">
                                <div class="form-check form-check-inline mb-0 d-flex align-items-center">

                                    <asp:RadioButton ID="rbActivoSI" runat="server" GroupName="rbGrupoActivoCat" Checked="true" />
                                    <label class="form-check-label ms-2" for="<%= rbActivoSI.ClientID %>">
                                        <span class="badge bg-success px-2 py-1" style="font-size: 0.75rem; font-weight: 600;">SI</span>
                                    </label>

                                </div>

                                <div class="form-check form-check-inline mb-0 d-flex align-items-center">
                                    <asp:RadioButton ID="rbActivoNO" runat="server" GroupName="rbGrupoActivoCat" />
                                    <label class="form-check-label ms-2" for="<%=rbActivoNO.ClientID %>">
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
                    </div>
                </div>
            </div>
        </div>


    </div>

</asp:Content>
