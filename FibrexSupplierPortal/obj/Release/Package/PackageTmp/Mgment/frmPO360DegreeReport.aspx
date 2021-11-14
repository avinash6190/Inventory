<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Mgment/mainMaster.Master" CodeBehind="frmPO360DegreeReport.aspx.cs" Inherits="FibrexSupplierPortal.Mgment.frmPO360DegreeReport" %>

<%@ Register Assembly="DevExpress.XtraReports.v16.1.Web, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<%@ Register Src="~/Mgment/Control/POReportMenu.ascx" TagPrefix="uc1" TagName="POReportMenu" %>
<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowOrganization() {
            gvOrganization.ClearFilter();
            popupOrganization.Show();
        }
        function ClickPOLProjectEvent() {
            $('#ContentPlaceHolder1_btnSelectProject').click();
        }
        function ShowSupplierList() {
            popupSupplier.Show();
        }
        function ShowUserList() {
            popupUsers.Show();
        }
        function OnSelectCloseSupplierPopup(s, e) {
            popupSupplier.Hide();
        }
        function OnRefundProjectPanelEndCallback(s, e) {
            popupProject.Hide();
        }
        function OnRefundPanelEndCallback(s, e) {
            popupOrganization.Hide();
        }
        function OnSelectCloseUserPopupPOL(s, e) {
            popupUsers.Hide();
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMenu" runat="server">
    <uc1:POReportMenu runat="server" ID="POReportMenu" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajax:ToolkitScriptManager ID="ssc2" runat="server"></ajax:ToolkitScriptManager>
    <div class="row">
        <div class="RPTheadingName">
            Search PO 360 degree
          <div class="row" style="float: right; margin-top: -2px; margin-right: -5px;"></div>
        </div>
    </div>
    <div style="padding-top: 5px;">&nbsp;</div>
    <asp:HiddenField ID="selected_tab" runat="server" />
    <div class="row">
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
                                            <asp:TextBox ID="txtOrganization" runat="server" OnTextChanged="txtOrganization_TextChanged"  CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            <asp:HiddenField ID="HIDOrganizationCode" runat="server" />
                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ShowOrganization();" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Project</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtProjectCode" runat="server" OnTextChanged="txtProjectCode_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                            <asp:HiddenField ID="HidProjectCode" runat="server"></asp:HiddenField>
                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img id="imgPOLProject" src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ClickPOLProjectEvent();" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Vendor</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:HiddenField ID="hidCompanyID" runat="server"></asp:HiddenField>

                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ShowSupplierList();" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Buyer</label>
                                        <div class="col-sm-7">
                                            <asp:TextBox ID="txtBuyers" runat="server" AutoPostBack="true" OnTextChanged="txtBuyers_TextChanged" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="HidBuyersID" runat="server" />
                                        </div>
                                        <div style="float: left; margin-left: -12px;" class="col-sm-1">
                                            <img src="../images/search-icon.png" class="SearchImg imgPopup" onclick="return ShowUserList();" />
                                        </div>
                                    </div>
                                      
                                   </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                            Status</label>
                                        <div class="col-sm-7">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" DataValueField="Value" DataTextField="Description"></asp:DropDownList>
                                        </div>
                                        <div style="width: 2%; float: left;">
                                        </div>
                                    </div>
                                      <div class="form-group">
                                                        <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                                            Order Date From 
                                                        </label>
                                                        <div class="col-sm-3">
                                                            <asp:TextBox ID="txtOrderDatefrom" runat="server" CssClass="form-control"> </asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="imgpopCalender1OrderFrom" TargetControlID="txtOrderDatefrom" Format="dd-MMM-yyyy"></ajax:CalendarExtender>
                                                        </div>
                                                        <label class="col-sm-1 Pdringtop CalenderImg" for="inputName">
                                                            <img src="../images/rsz_calendar-icon-png-4.png" id="imgpopCalender1OrderFrom" runat="server" class="SearchImg" />
                                                        </label>
                                                        <div style="float: left; margin-left: -12px;margin-top:5px" class="col-sm-1">
                                                                <label for="inputName">To</label>
                                                        </div>
                                                        <div class="col-sm-3 Pdringtop" style="float:left;margin-left: -12px;">
                                                            <asp:TextBox ID="txtOrderDateTo" runat="server" CssClass="form-control"> </asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="imgpopCalender1OrderTo" TargetControlID="txtOrderDateTo" Format="dd-MMM-yyyy"></ajax:CalendarExtender>
                                                        </div>
                                                        <label class="col-sm-1 Pdringtop CalenderImg" for="inputName">
                                                            <img src="../images/rsz_calendar-icon-png-4.png" id="imgpopCalender1OrderTo" runat="server" class="SearchImg" />
                                                        </label>
                                      </div>
                                      
                                         <div class="form-group">
                                                    <label class="control-label col-sm-3 Pdringtop" for="inputName">
                                                     Requisition Ref No.
                                                    </label>
                                                    <div class="col-sm-7">
                                                        <asp:TextBox ID="txtRequisition" runat="server" CssClass="form-control"> </asp:TextBox>
                                                    </div>
                                                </div>

                                    <div class="form-group">
                                                      <div class="col-sm-2" style="margin-left:130px">
                                        <asp:RadioButton id="rbdetail" runat="server" Checked="true" Text="Detail" GroupName="rbldetailsummary"></asp:RadioButton>
                                                       </div>
                                                          <div style="margin-left:0px">
                                        <asp:RadioButton id="rbsummary" runat="server" Text="Summary" GroupName="rbldetailsummary"></asp:RadioButton>
                                                          </div>
                                    </div>
                                <div class="form-group">
                                    <div class="col-sm-5 col-sm-offset-5" style="text-align: right;">
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnSearchPO360" runat="server" CssClass="btn btn-primary" Text="Run Report" OnClick="btnSearchPO360_Click" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="btn btn-primary" Text="Clear" OnClick="btnSearchClear_Click" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnSelectProject" runat="server" Style="display: none;" Text="Select" OnClick="btnSelectProject_Click" />
                                    </div>
                               </div>

                                </div>
                            </div>
                        </div>
                    </div>
                 </div>
             </div>
         </div>


        <%--Organization--%>
        <dx:ASPxPopupControl ID="popupOrganization" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupOrganization"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Division List" Width="400px" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Division from the list</p>
                    <br />
                    <dx:ASPxGridView ID="gvOrganization" ClientInstanceName="gvOrganization" runat="server" OnAfterPerformCallback="gvOrganization_AfterPerformCallback" OnBeforeColumnSortingGrouping="gvOrganization_BeforeColumnSortingGrouping" KeyFieldName="org_code" OnPageIndexChanged="gvOrganization_PageIndexChanged" OnRowCommand="gvOrganization_RowCommand" AutoGenerateColumns="False" Width="100%" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True" >
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
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <%--Projects--%>
        <dx:ASPxPopupControl ID="popupProject" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupProject"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Projects" Width="450px" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Projects from the list</p>
                    <br />
                    <dx:ASPxGridView ID="gvProjectLists" runat="server" OnBeforeColumnSortingGrouping="gvProjectLists_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvProjectLists_AfterPerformCallback" ClientInstanceName="gvProjectLists" AutoGenerateColumns="False" Width="100%" KeyFieldName="depm_code;depm_desc" Settings-ShowFilterRow="True" OnRowCommand="gvProjectLists_RowCommand">
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
        <dx:ASPxPopupControl ID="popupSupplier" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupSupplier"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" Width="750px" HeaderText="Suppliers List" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Supplier</p>
                    <br />
                    <dx:ASPxGridView ID="gvSupplierLIst" runat="server" OnBeforeColumnSortingGrouping="gvSupplierLIst_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvSupplierLIst_AfterPerformCallback" ClientInstanceName="gvSupplierLIst" KeyFieldName="SupplierID;SupplierName" AutoGenerateColumns="False" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True" OnRowCommand="gvSupplierLIst_RowCommand" >
                        <Settings ShowFilterRow="True" ShowFilterRowMenu="false" AutoFilterCondition="Contains"></Settings>
                        <Columns>
                            <dx:GridViewDataColumn Caption="Select" Name="Select" VisibleIndex="0" Width="60px">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lnkSelectUser" runat="server" Text="Select" OnClientClick="return OnSelectCloseSupplierPopup();"></asp:LinkButton>
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

        <%--Users--%>
        <dx:ASPxPopupControl ID="popupUsers" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True" ClientInstanceName="popupUsers"
            PopupHorizontalAlign="WindowCenter" AllowDragging="true" PopupVerticalAlign="WindowCenter" HeaderText="Users List" Width="700px" PopupAnimationType="None" EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server">
                    <p>Select Users from the list</p>
                    <br />
                    <dx:ASPxGridView ID="gvUserList" runat="server" OnBeforeColumnSortingGrouping="gvUserList_BeforeColumnSortingGrouping" OnAfterPerformCallback="gvUserList_AfterPerformCallback" ClientInstanceName="gvUserList" OnRowCommand="gvUserList_RowCommand" AutoGenerateColumns="False" Width="100%" KeyFieldName="emp_code;emp_name" Settings-ShowFilterBar="Hidden" Settings-ShowFilterRow="True">
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

    </div>
</asp:Content>