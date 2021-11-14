<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/Mgment/mainMaster.Master" CodeBehind="frmSearchPOLine.aspx.cs" Inherits="FibrexSupplierPortal.Mgment.frmSearchPOLine" ValidateRequest="false" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/Mgment/Control/PurchaseOrderSideMenu.ascx" TagPrefix="uc1" TagName="DashboardLeftSideMenu" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Content/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../Scripts/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../Scripts/Gerenal.js"></script>

    <script type="text/javascript">
        function ShowPOLOrganization() {
            gvPOLOrganization.ClearFilter();
            popupPOLOrganization.Show();
        }
        function toggle() {
            var ele = document.getElementById("toggleText");
            var text = document.getElementById("displayText");
            if (ele.style.display == "block") {
                ele.style.display = "none";
                text.innerHTML = "+ Show more search options";
            }
            else {
                ele.style.display = "block";
                text.innerHTML = "- Hide more options";
            }
        }
        function ShowCreateAccountWindow() {
            popupPOLProject.Show();
        }
        function ShowPOLSupplierList() {
            popupPOLSupplier.Show();
        }
        function ShowUserListPOL() {
            popupPOLUsers.Show();
        }
        function getProjectID(element, ID) {
            $('#ContentPlaceHolder1_txtPOLProjectCode').val(ID);
            popupPOLProject.Hide();
        }
        function getOrganizationID(element, ID) {
            $('#ContentPlaceHolder1_txtPOLOrganization').val(ID);
            popupPOLOrganization.Hide();
        }
        function getBuyerID(element, ID) {
            $('#ContentPlaceHolder1_txtPOLBuyers').val(ID);
            popupPOLUsers.Hide();
        }
        function getSupplierID(element, ID) {
            $('#ContentPlaceHolder1_txtPOLCompanyID').val(ID);
            popupPOLSupplier.Hide();
        }
        function ShowPOLITEMCODE() {
            gvITEMCODE.ClearFilter();
            popupPOLITEMCODE.Show();
        }
        function showPOLCostCode() {
            gvPOLCostCode.ClearFilter();
            popupPOLCostCode.Show();
        }
        function OnRefundPanelEndCallback(s, e) {
            popupPOLOrganization.Hide();
        }
        function OnRefundProjectPanelEndCallback(s, e) {
            popupPOLProject.Hide();
        }
        function OnSelectCloseUserPopupPOL(s, e) {
            popupPOLUsers.Hide();
        }
        function OnSelectCloseSupplierPopupPOL(s, e) {
            popupPOLSupplier.Hide();
        }
         function onSelectCloseCostCodePopupPOL(s, e) {
            isDirtyselectLastName = true;
            popupPOLCostCode.Hide();
        }
        function OnSelectCloseITEMCODEPopupPOL(s, e) {
            isDirtyselectLastName = true;
            popupPOLITEMCODE.Hide();
        }
        function OnSelectCloseRequestorPopup(s, e) {
            isDirtyselectLastName = true;
            popupRequestor.Hide();
        }
        function ShowREQUESTOR() {
            gvRequestor.ClearFilter();
            popupRequestor.Show();
        }
        function ClickPOLProjectEvent() {
            $('#ContentPlaceHolder1_btnSelectPOLProject').click();
        }
        $(document).keypress(function (e) {
            if (e.which == 13) {
                $("#ContentPlaceHolder1_btnSearchTemplates").click();
            }
        });
        $(document).on('keypress',function(e) {
           if(e.which == 13) {
                $("#ContentPlaceHolder1_btnSearchTemplates").click();
            }
         });
        $(document).ready(function () {
            localStorage.removeItem('activeTab');
          <%--  $('#<%= txtPurchaseOrderNumber.ClientID %>').keydown(function (e) {
                // Allow: backspace, delete, tab, escape, enter and .
                if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 165]) !== -1 ||
                    // Allow: Ctrl+A, Command+A
                    (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                    (e.keyCode == 67 && e.ctrlKey === true) ||
                    // Allow: Ctrl+X
                    (e.keyCode == 88 && e.ctrlKey === true) ||
                    // Allow: home, end, left, right, down, up
                    (e.keyCode >= 35 && e.keyCode <= 40)) {
                    // let it happen, don't do anything
                    return;
                }
                // Ensure that it is a number and stop the keypress !e.shiftKey ||
                if ((e.keyCode < 48 || e.keyCode > 57) && (e.keyCode < 96 || e.keyCode > 105)) {
                    e.preventDefault();
                }

            });--%>

        });
        function removeActiveTab() {
            localStorage.removeItem('activeTab');
        }
        window.onbeforeunload = function () {
            localStorage.removeItem('activeTab');
        };
    </script>

    <style>
        /*.row {
            margin-right: 0px;
            margin-left: -0px;
        }*/
        .clschk {
            padding: 5px;
        }

            .clschk label {
                padding-left: 5px;
                position: absolute;
                margin-top: 1px !important;
            }
    </style>

    <script src="../Scripts/SupexpendText.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajax:ToolkitScriptManager ID="ssc2" runat="server"></ajax:ToolkitScriptManager>
    <div class="row">
        <div class="RPTheadingName">
            Search Purchase Order Line
          <div class="row" style="float: right; /*width: 16%; */ margin-top: -2px; margin-right: -5px;"></div>
        </div>
    </div>
    <div style="padding-top: 5px;">&nbsp;</div>
    <asp:HiddenField ID="selected_tab" runat="server" />
    <div class="row">
        <%--  <asp:UpdatePanel ID="upPoDetail" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
        <div lass="alert alert-danger alert-dismissable" id="divError" runat="server" visible="false" style="margin-bottom: 10px;">
            <asp:Label ID="lblError" runat="server"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
        </div>
        <div class="panel-group" id="accordion">
            <div class="panel panel-default">
                <div class="panel-collapse">
                    <div class="panel-body bg">
                        <div class="form-horizontal">
                            <p>
                                To perform a multiple character wildcard search, use the percent sign (%) symbol . Fields are case insensitive. Leave fields blank for a list of all values.
                            </p>
                            <div class="row">&nbsp;</div>
                            <div class="col-lg-12">
                                  <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Division</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtPOLOrganization" runat="server" OnTextChanged="txtPOLOrganization_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            <asp:HiddenField ID="HIDPOLOrganizationCode" runat="server" />
                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ShowPOLOrganization();" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Project</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtPOLProjectCode" runat="server" OnTextChanged="txtPOLProjectCode_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            <asp:HiddenField ID="HidPOLProjectCode" runat="server"></asp:HiddenField>
                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img id="imgPOLProject" src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ClickPOLProjectEvent();" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Vendor</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtPOLCompanyName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="hidPOLCompanyID" runat="server"></asp:HiddenField>

                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ShowPOLSupplierList();" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Buyer</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtPOLBuyers" OnTextChanged="txtPOLBuyers_TextChanged1" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            <asp:HiddenField ID="HidPOLBuyersID" runat="server" />
                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ShowUserListPOL();" />
                                        </div>
                                    </div>
                                      <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Status</label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlPOLStatus" runat="server" CssClass="form-control" DataValueField="Value" DataTextField="Description"></asp:DropDownList>
                                        </div>
                                        <div style="width: 2%; float: left;">
                                        </div>
                                    </div>

                                                   <div class="form-group">
                                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                                            Order Date From 
                                                        </label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtPOLOrderDatefrom" runat="server" CssClass="form-control"> </asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgpopCalender1OrderFrom" TargetControlID="txtPOLOrderDatefrom" Format="dd-MMM-yyyy"></ajax:CalendarExtender>
                                                        </div>
                                                        <label class="col-sm-1 Pdringtop CalenderImg" for="inputName">
                                                            <img src="../images/rsz_calendar-icon-png-4.png" id="imgpopCalender1OrderFrom" runat="server" class="SearchImg" />
                                                        </label>
                                                        <div style="float: left; margin-left: -12px;margin-top:5px" class="col-sm-1">
                                                                <label for="inputName">To</label>
                                                        </div>
                                                        <div class="col-sm-3 Pdringtop" style="float:left;margin-left: -12px;">
                                                            <asp:TextBox ID="txtPOLOrderDateTo" runat="server" CssClass="form-control"> </asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgpopCalender1OrderTo" TargetControlID="txtPOLOrderDateTo" Format="dd-MMM-yyyy"></ajax:CalendarExtender>
                                                        </div>
                                                        <label class="col-sm-1 Pdringtop CalenderImg" for="inputName">
                                                            <img src="../images/rsz_calendar-icon-png-4.png" id="imgpopCalender1OrderTo" runat="server" class="SearchImg" />
                                                        </label>
                                                    </div>
                                </div>
                                <div class="col-lg-6">                                    
                                            <div class="form-group">
                                                 <label class="control-label col-sm-2" for="inputName">
                                                Item Code</label>
                                                <div class="col-sm-3">
                                                        <asp:TextBox ID="txtPOLItemCode" runat="server" CssClass="form-control" OnTextChanged="txtPOLItemCode_TextChanged" ValidationGroup="Equip" AutoPostBack="true" onkeydown="ShowToolTip(event)"></asp:TextBox>
                                                    </div>
                                                    <div style="float: left; margin-left: -12px" class="col-sm-1">
                                                        <asp:ImageButton ID="imgPOLItemCode" runat="server" ImageUrl="~/images/search-icon.png" CssClass="SearchImg imgPopup" style="margin-top: 5px;" Visible="true" OnClick="imgPOLItemCode_Click" />
                                                    </div>
                                                   <div style="float: left; margin-left: -10px;padding-top:5px"  class="col-sm-6">
                                                       <asp:Label ID="lblItemdescription" runat="server" Text="Label" Visible="false"></asp:Label>
                                                   </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-sm-2 Pdringtop" for="inputName">
                                                     Description
                                                    </label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtPOLDescription" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                        <label class="control-label col-sm-2 Pdringtop" for="inputName">
                                            Model</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtPOLModel" runat="server"  MaxLength="250" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                        <label class="control-label col-sm-2 Pdringtop" for="inputName">
                                            Manufacturer</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtPOLManufacturer" runat="server"  MaxLength="250" CssClass="form-control"> </asp:TextBox>
                                        </div>
                                                        </div>
                                      <div class="form-group">
                                        <label class="control-label col-sm-2 Pdringtop" for="inputName">
                                            Supp Ref No</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtPOLSupplierRefNo" runat="server"  MaxLength="250" CssClass="form-control"> </asp:TextBox>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                                 <label class="control-label col-sm-2" for="inputName">
                                                Cost Code</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtPOLCostCode" runat="server" CssClass="form-control" ValidationGroup="Equip" AutoPostBack="true" onkeydown="ShowToolTip(event)"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnPOLtxtCostCode" runat="server" />
                                                </div>
                                                <div style="float: left; margin-left: -12px" class="col-sm-1">
                                                        <asp:ImageButton ID="imgPOLCostCode" runat="server" ImageUrl="~/images/search-icon.png" OnClick="imgPOLCostCode_Click" CssClass="SearchImg imgPopup" style="margin-top: 5px;" Visible="true" />
                                                    </div>
                                                </div>

                                    <div class="form-group">
                                <div class="control-label col-sm-9 Pdringtop">
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnSearchPOL" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearchPOL_Click" />
                                    <asp:Button ID="btnSearchClearPOL" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="btnSearchClearPOL_Click" />
                                    <asp:Button ID="btnSelectPOLProject" runat="server" Style="display: none;" Text="Select" OnClick="btnSelectPOLProject_Click" />
                                </div>
                            </div>

                                </div>
                            </div>
                        </div>
                    </div>


                     <div class="form-group" style="background-color: #AFC8D7; padding: 5px 5px; color: black">
                        Search Result
                         <asp:ImageButton ID="exportExcel" runat="server" ImageUrl="../images/excel.png" width="22" height="17" style="float:right;cursor:pointer;margin-right: 10px;" Visible="true" OnClick="exportExcel_Click" ToolTip="Export to Excel" />
                    </div>
                    <div class="table-responsive" style="overflow: auto;">
                        <dx:ASPxGridView ID="gvSearchPurchaseOrder" runat="server" OnPageIndexChanged="gvSearchPurchaseOrder_PageIndexChanged" OnBeforeColumnSortingGrouping="gvSearchPurchaseOrder_BeforeColumnSortingGrouping" KeyFieldName="PONUM" AutoGenerateColumns="False" Width="100%" EnableCallBacks="False">
                            <SettingsCommandButton>
                                <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>
                                
                                <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
                            </SettingsCommandButton>
                            <Settings HorizontalScrollBarMode="Auto" /> 
                            <Columns>
                                <dx:GridViewDataColumn Caption="PO NUM" VisibleIndex="0" FieldName="PONUM" Width="55" FixedStyle="Left">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                    <DataItemTemplate>
                                        <a onclick='removeActiveTab();' href="<%# string.Format("../Mgment/frmUpdatePurchaseOrder?ID={0}&revision={1}", FSPBAL.Security.URLEncrypt(Eval("PONUM").ToString()), FSPBAL.Security.URLEncrypt(Eval("POREVISION").ToString())) %>">
                                            <%# Eval("PONUM")%>
                                        </a>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataColumn>

                                <dx:GridViewDataTextColumn FieldName="POREVISION" VisibleIndex="1" Caption="Rev" Width="35" FixedStyle="Left">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataDateColumn FieldName="POLINENUM" VisibleIndex="1" Caption="PO Line Num" Width="80" FixedStyle="Left">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>

                                
                                
                                <dx:GridViewDataTextColumn FieldName="ORGNAME" VisibleIndex="2" Caption="Division" FixedStyle="Left">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="PROJECTNAME" VisibleIndex="3" Caption="Project" Width="250" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="VENDORNAME" VisibleIndex="4" Caption="Vendor" Width="250" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="BUYERNAME" VisibleIndex="5" Caption="Buyer" Width="180" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="Status" VisibleIndex="6" Caption="Status" FixedStyle="Left">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="ITEMNUM" VisibleIndex="7" Caption="Item Code" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="DESCRIPTION" VisibleIndex="8" Caption="Description" Width="250" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="SPECIFICATION" VisibleIndex="8" Caption="Additional Specification" Width="180" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="MODELNUM" VisibleIndex="9" Caption="Model">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="MANUFACUTRER" VisibleIndex="9" Caption="Manufacturer" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="ORDERUNIT" VisibleIndex="9" Caption="Order Unit" Width="70" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="UNITCOST" VisibleIndex="10" Caption="Unit Rate" Width="75" FixedStyle="None">
                                    <%--<SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>--%>
                                    <PropertiesTextEdit DisplayFormatString="n2"></PropertiesTextEdit>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataDateColumn FieldName="CATALOGCODE" VisibleIndex="11" Caption="Supp Ref. No." FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataDateColumn FieldName="COSTCODE" VisibleIndex="12" Caption="Cost Code" FixedStyle="None">
                                    <SettingsHeaderFilter>
                                        <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                    </SettingsHeaderFilter>
                                </dx:GridViewDataDateColumn>
                            </Columns>

                            <Styles>
                                <Header CssClass="gridHeader">
                                </Header>
                                <Row CssClass="gridRowOdd">
                                </Row>
                                <AlternatingRow CssClass="gridRowEven">
                                </AlternatingRow>
                                <Footer CssClass="GridFooter">
                                </Footer>

                                <Cell Wrap="False"></Cell>
                            </Styles>

                            <Border BorderStyle="None"></Border>
                        </dx:ASPxGridView>
                        <asp:SqlDataSource runat="server" ID="DSSearchPurchaseOrder" ConnectionString='<%$ ConnectionStrings:CS %>' SelectCommand="SELECT * FROM [ViewAllPurchaseOrder]"></asp:SqlDataSource>
                        <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" GridViewID="gvSearchPurchaseOrder" runat="server"></dx:ASPxGridViewExporter>
                    </div>



                </div>
            </div>
        </div>

        <%--Organization--%>
        <dx:ASPxPopupControl ID="popupPOLOrganization" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupPOLOrganization"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Division List" Width="400px" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Division from the list</p>
                    <br />
                    <%--   <dx:ASPxCallbackPanel ID="RefundCallbackPanel" ClientInstanceName="RefundPanel" runat="server" OnCallback="RefundCallbackPanel_Callback">
                                                <PanelCollection>
                                                    <dx:PanelContent>--%>
                    <dx:ASPxGridView ID="gvPOLOrganization" ClientInstanceName="gvPOLOrganization" runat="server" OnAfterPerformCallback="gvPOLOrganization_AfterPerformCallback" OnBeforeColumnSortingGrouping="gvPOLOrganization_BeforeColumnSortingGrouping" KeyFieldName="org_code" OnPageIndexChanged="gvPOLOrganization_PageIndexChanged" OnRowCommand="gvPOLOrganization_RowCommand" AutoGenerateColumns="False" Width="100%" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True" >
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="false" AutoFilterCondition="Contains"></Settings>
                        <Columns>
                            <dx:GridViewDataColumn Caption="Select" Name="Select" VisibleIndex="0" Width="60px">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lnkOrgSelect" runat="server" Text="Select" OnClientClick="OnRefundPanelEndCallback();"></asp:LinkButton>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="org_code" VisibleIndex="5" Caption="Code" Width="60px">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="org_abbr_name" VisibleIndex="5" Caption="Abbr Name" Width="60px">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="org_name" VisibleIndex="6" Caption="Division Name">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <%-- </dx:PanelContent>
                                                </PanelCollection>
                                            </dx:ASPxCallbackPanel>--%>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--Item Code--%>
        <dx:ASPxPopupControl ID="popupPOLITEMCODE" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupPOLITEMCODE"
                                PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Item Master" Width="700px" PopupAnimationType="None" EnableViewState="False">
                                <ContentCollection>
                                    <dx:PopupControlContentControl runat="server">
                                        <p>Select ITEM CODES from the list</p>
                                        <br />
                                        <dx:ASPxGridView ID="gvPOLITEMCODE" runat="server" ClientInstanceName="gvPOLITEMCODE" OnRowCommand="gvPOLITEMCODE_RowCommand" OnBeforeColumnSortingGrouping="gvPOLITEMCODE_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvPOLITEMCODE_AfterPerformCallback" AutoGenerateColumns="False" Width="100%" KeyFieldName="prm_item_code;prm_item_desc;" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True">
                                            <Settings ShowFilterRow="True" ShowFilterRowMenu="true" AutoFilterCondition="Contains" ShowFilterRowMenuLikeItem="true"></Settings>
                                            <Columns>
                                                <dx:GridViewDataColumn Caption="Select" Name="Select" VisibleIndex="0" Width="60px">
                                                    <DataItemTemplate>
                                                        <asp:LinkButton ID="lnkSelectITEMCODE" runat="server" Text="Select" OnClientClick="return OnSelectCloseITEMCODEPopupPOL();"></asp:LinkButton>
                                                    </DataItemTemplate>
                                                    <CellStyle HorizontalAlign="Left">
                                                    </CellStyle>
                                                </dx:GridViewDataColumn>
                                           <dx:GridViewDataTextColumn FieldName="orgCode" ReadOnly="True" Visible="false" Caption="Org Code" Width="60px">
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="orgName" VisibleIndex="0" Caption="Org Name" Width="60px">
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="prm_item_code" VisibleIndex="1" Caption="Item Code" Width="60px">
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="prm_item_desc" VisibleIndex="2" Caption="Item Description" Width="250px">
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="prm_uom_code"  Caption="UOM Code" Visible="false">
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="uom_desc"  VisibleIndex="3" Caption="Order Unit" Width="60px">
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </dx:PopupControlContentControl>
                                </ContentCollection>
                            </dx:ASPxPopupControl>

        <%--Cost Code Code--%>
        <dx:ASPxPopupControl ID="popupPOLCostCode" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupPOLCostCode"
                            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Cost Code's List" Width="700px" PopupAnimationType="None" EnableViewState="False">
                            <ContentCollection>
                                <dx:PopupControlContentControl runat="server">
                                    <p>Select Cost Code from the list</p>
                                    <br />
                                    <dx:ASPxGridView ID="gvPOLCostCode" runat="server" OnBeforeColumnSortingGrouping="gvPOLCostCode_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvPOLCostCode_AfterPerformCallback" ClientInstanceName="gvPOLCostCode" OnRowCommand="gvPOLCostCode_RowCommand" AutoGenerateColumns="False" Width="100%" KeyFieldName="ccm_cost_code;ccm_desc" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True">
                                        <Settings ShowFilterRow="True" ShowFilterRowMenu="true" AutoFilterCondition="Contains"></Settings>
                                        <Columns>
                                            <dx:GridViewDataColumn Caption="Select" Name="Select" VisibleIndex="0" Width="60px">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lnkSelectCostCode" runat="server" Text="Select" OnClientClick="return onSelectCloseCostCodePopupPOL();"></asp:LinkButton>
                                                </DataItemTemplate>
                                                <CellStyle HorizontalAlign="Left">
                                                </CellStyle>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataTextColumn FieldName="orgCode" ReadOnly="True" VisibleIndex="0" Visible="false">
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="orgName" VisibleIndex="1">
                                                <HeaderTemplate>Division</HeaderTemplate>
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ccm_cost_code" VisibleIndex="2">
                                                <HeaderTemplate>Cost Code</HeaderTemplate>
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ccm_desc" VisibleIndex="3">
                                                <HeaderTemplate>Cost Description</HeaderTemplate>
                                                <SettingsHeaderFilter>
                                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                                </SettingsHeaderFilter>
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                    </dx:ASPxGridView>
                                    <asp:SqlDataSource runat="server" ID="tmpFibConso" ConnectionString='<%$ ConnectionStrings:DS %>' SelectCommand="SELECT * FROM dbo.cost_code_master"></asp:SqlDataSource>
                                </dx:PopupControlContentControl>
                            </ContentCollection>
                        </dx:ASPxPopupControl>

        <%--Users--%>
        <dx:ASPxPopupControl ID="popupPOLUsers" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupPOLUsers"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Users List" Width="700px" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Users from the list</p>
                    <br />
                    <dx:ASPxGridView ID="gvPOLUserList" runat="server" OnBeforeColumnSortingGrouping="gvPOLUserList_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvPOLUserList_AfterPerformCallback" ClientInstanceName="gvPOLUserList" OnRowCommand="gvPOLUserList_RowCommand" AutoGenerateColumns="False" Width="100%" KeyFieldName="emp_code;emp_name" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True">
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="false" AutoFilterCondition="Contains"></Settings>
                        <Columns>
                            <dx:GridViewDataColumn Caption="Select" Name="Select" VisibleIndex="0" Width="60px">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lnkSelectUser" runat="server" Text="Select" OnClientClick="return OnSelectCloseUserPopupPOL();"></asp:LinkButton>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="emp_code" ReadOnly="True" VisibleIndex="0" Width="70px">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="emp_name" VisibleIndex="1">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="dgt_desig_name" VisibleIndex="2">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource runat="server" ID="DSUserList" ConnectionString='<%$ ConnectionStrings:CS %>' SelectCommand="FIRMS_GetAllEmployee" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <%--Projects--%>

        <dx:ASPxPopupControl ID="popupPOLProject" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupPOLProject"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Projects" Width="450px" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Projects from the list</p>
                    <br />
                    <dx:ASPxGridView ID="gvPOLProjectLists" runat="server" OnBeforeColumnSortingGrouping="gvPOLProjectLists_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvPOLProjectLists_AfterPerformCallback" ClientInstanceName="gvPOLProjectLists" AutoGenerateColumns="False" Width="100%" KeyFieldName="depm_code;depm_desc" Settings-ShowFilterRow="True" OnRowCommand="gvPOLProjectLists_RowCommand">
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="false" AutoFilterCondition="Contains"></Settings>

                        <Columns>

                            <dx:GridViewDataColumn Caption="Select" Name="Select" VisibleIndex="0" Width="60px">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" Text="Select" OnClientClick="return OnRefundProjectPanelEndCallback();"></asp:LinkButton>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="depm_code" ReadOnly="True" Width="90px" VisibleIndex="2" Caption="Dept Code">
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>

                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="depm_desc" VisibleIndex="3" Caption="Dept Name">
                                <Settings AllowAutoFilter="True" AutoFilterCondition="Contains"></Settings>

                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource runat="server" ID="DSProjects" ConnectionString='<%$ ConnectionStrings:CS %>' SelectCommand="SELECT [ProjectID], [ProjectCode], [ProjectDesc] FROM [Projects]"></asp:SqlDataSource>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


        <%--Supplier--%>
        <dx:ASPxPopupControl ID="popupPOLSupplier" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupPOLSupplier"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" Width="750px" HeaderText="Suppliers List" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Supplier</p>
                    <br />
                    <dx:ASPxGridView ID="gvPOLSupplierLIst" runat="server" OnBeforeColumnSortingGrouping="gvPOLSupplierLIst_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvPOLSupplierLIst_AfterPerformCallback" ClientInstanceName="gvPOLSupplierLIst" KeyFieldName="SupplierID;SupplierName" AutoGenerateColumns="False" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True" OnRowCommand="gvPOLSupplierLIst_RowCommand" >
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="false" AutoFilterCondition="Contains"></Settings>
                        <Columns>
                            <dx:GridViewDataColumn Caption="Select" Name="Select" VisibleIndex="0" Width="60px">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lnkSelectUser" runat="server" Text="Select" OnClientClick="return OnSelectCloseSupplierPopupPOL();"></asp:LinkButton>
                                </DataItemTemplate>
                                <CellStyle HorizontalAlign="Left">
                                </CellStyle>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataTextColumn FieldName="SupplierID" ReadOnly="True" Caption="Supplier ID" VisibleIndex="0" Width="75px">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>

                                <EditFormSettings Visible="False"></EditFormSettings>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SupplierName" VisibleIndex="1">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="SupplierType" VisibleIndex="3" Width="100px">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RegDocID" VisibleIndex="6" Width="100px">
                                <SettingsHeaderFilter>
                                    <DateRangePickerSettings EditFormatString=""></DateRangePickerSettings>
                                </SettingsHeaderFilter>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <asp:SqlDataSource runat="server" ID="DSSupplierList" ConnectionString='<%$ ConnectionStrings:CS %>' SelectCommand="SELECT     Supplier.SupplierID, Supplier.SupplierName, Supplier.OfficialEmail,  FROM         Supplier "></asp:SqlDataSource>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>

</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="ContentMenu" runat="server">
    <uc1:DashboardLeftSideMenu runat="server" ID="DashboardLeftSideMenu" />
</asp:Content>
