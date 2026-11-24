<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Presentacion.Error" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Error</title>
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" runat="server" />
</head>
<body class="container mt-5">
    <form id="form1" runat="server">
        <div class="alert alert-danger">
            <h3>Hubo un problema</h3>
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        </div>
        <asp:HyperLink ID="lnkVolver" runat="server" CssClass="btn btn-primary">Volver al inicio</asp:HyperLink>
    </form>
</body>
</html>

