<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutputCache.aspx.cs" Inherits="WebCaching.OutputCache" %>
<%--<%@ OutputCache VaryByParam="none" Duration="60" Location="Server" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<h1>OutputCache</h1>    
		<h2><%= DateTime.Now %></h2>
    </div>
    </form>
</body>
</html>
