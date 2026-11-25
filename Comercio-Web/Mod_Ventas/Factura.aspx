<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Factura.aspx.cs" Inherits="Presentacion.Mod_Ventas.Factura" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Factura</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 30px; }
        @media print { .no-imprimir { display: none; } }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div class="no-imprimir">
            <button type="button" onclick="window.print();">Imprimir</button>
            <a href="NuevaVenta.aspx">Volver a Nueva Venta</a>
            <hr />
        </div>

        <h2>FACTURA</h2>

        <p>Nro. de factura: <asp:Label ID="lblNumero" runat="server"></asp:Label></p>
        <p>Fecha: <asp:Label ID="lblFecha" runat="server"></asp:Label></p>
        <p>Cliente: <asp:Label ID="lblCliente" runat="server"></asp:Label></p>

        <asp:GridView ID="dgvLineas" runat="server" AutoGenerateColumns="False" GridLines="Both" CellPadding="5">
            <Columns>
                <asp:TemplateField HeaderText="Producto">
                    <ItemTemplate>
                        <%# Eval("ProductoAsociado.Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio unitario" DataFormatString="{0:C}" />
                <asp:TemplateField HeaderText="Subtotal">
                    <ItemTemplate>
                        <%# (Convert.ToInt32(Eval("Cantidad")) * Convert.ToDecimal(Eval("PrecioUnitario"))).ToString("C") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <h3>Total: <asp:Label ID="lblTotal" runat="server"></asp:Label></h3>

    </form>
</body>
</html>