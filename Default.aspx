<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tot">
    
        <div id="panelExamen">

            <asp:Button ID="ButtonAltaUsuarios" runat="server" Text="1. Alta usuarios" OnClick="ButtonAltaUsuarios_Click" />
            <asp:Button ID="ButtonAltaRoles" runat="server" Text="2. Alta roles" OnClick="ButtonAltaRoles_Click" />
            <asp:Button ID="ButtonModificarUsuarios" runat="server" Text="3. Modificar usuarios" OnClick="ButtonModificarUsuarios_Click" />
            <asp:Button ID="ButtonModificarRoles" runat="server" Text="4. Modificar roles" OnClick="ButtonModificarRoles_Click" />
            <asp:Button ID="ButtonBorrarUsuarios" runat="server" Text="5. Borrar usuarios" OnClick="ButtonBorrarUsuarios_Click" />
            <asp:Button ID="ButtonBorrarRoles" runat="server" Text="6. Borrar roles" OnClick="ButtonBorrarRoles_Click" />

        </div>

        <div id="PanelGrids">

            <asp:label ID="labelErrores" runat="server"></asp:label>

            <asp:GridView ID="GridViewMostrarUsuaris" runat="server" AutoGenerateColumns="false">
                <Columns>

                    <asp:BoundField DataField="Id" HeaderText="Identificador"></asp:BoundField>
                    <asp:BoundField DataField="UserName" HeaderText="Nom Usuari"></asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Correu Electrònic"></asp:BoundField>
                    <asp:BoundField DataField="PhoneNumber" HeaderText="Telèfon"></asp:BoundField>

                </Columns>

            </asp:GridView>

            <asp:GridView ID="GridViewMostrarRoles" runat="server" AutoGenerateColumns="False">

                <Columns>

                    <asp:BoundField DataField="Id" HeaderText="Identificador" />
                    <asp:BoundField DataField="Name" HeaderText="Nom Rol" />

                </Columns>

            </asp:GridView>

        </div>

    </div>
    </form>
</body>
</html>
