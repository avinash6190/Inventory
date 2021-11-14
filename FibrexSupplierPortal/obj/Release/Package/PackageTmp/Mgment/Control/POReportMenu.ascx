<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="POReportMenu.ascx.cs" Inherits="FibrexSupplierPortal.Mgment.Control.POReportMenu" %>

<ul class="nav" id="side-menu">
    <li id="SideMenuReceivingDetailReport" runat="server">
        <a href="frmPOReceivingDetailReport"><i class="fa fa-table fa-fw"></i>PO Receiving details</a>
    </li>
    <li id="SideMenu360Degree" visible="false" runat="server">
        <a href="frmPO360DegreeReport"><i class="fa fa-edit fa-fw"></i>PO 360 degree</a>
    </li>
</ul>