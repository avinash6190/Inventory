<%@ Page Title="" Language="C#" MasterPageFile="~/Mgment/Blankmaster.Master" AutoEventWireup="true" CodeBehind="frmAddAttachment.aspx.cs" Inherits="FibrexSupplierPortal.Mgment.frmAddAttachment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


            <div class="form-horizontal">
                <div class="alert alert-danger alert-dismissable" id="divError" runat="server" visible="false">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                </div>
                <p>
                    Please usethe below fields to attach your files; after browsing and specifying the file please write the Document Name and brief descrpition of the file.
                </p>

                <div class="form-group">
                    <label class="control-label col-sm-3 Pdringtop" for="inputName">
                        Specify a File
                    </label>
                    <div class="col-sm-8">
                        <asp:FileUpload ID="FilePopupAdded" runat="server" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3 Pdringtop" for="inputName">
                        File Title
                    </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtPopupFileTitle" runat="server" CssClass="form-control" ValidationGroup="Popup" MaxLength="128"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-sm-3 Pdringtop" for="inputName">
                        File Description
                    </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtPopupFileDescription" runat="server" TextMode="MultiLine" Height="75px" CssClass="form-control" ValidationGroup="Popup" MaxLength="256"></asp:TextBox>
                    </div>
                </div>
            </div>
    

    <div class="reg-panel panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Attachments</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group" style="margin: 10px;">
                            <label class="btn btn-default" onclick="return ShowPop();" style="cursor: pointer;">
                                <span>Add Attachment</span>
                            </label>
                            <br />
                            <br />
                            <strong style="font-size: 13px;">Attachments</strong>
                            <br />
                            <br />
                            <div class="form-group">
                                <asp:GridView ID="gvShowSeletSupplierAttachment" runat="server" AllowPaging="true" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No search results" AutoGenerateColumns="false" OnRowDataBound="gvShowSeletSupplierAttachment_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Title">
                                            <ItemTemplate>
                                                <a href="<%#Eval("FileURL") %>" target="_blank">
                                                    <asp:Label ID="lblSupplierAttachmentTitle" runat="server" Text='<%#Eval("Title") %>'></asp:Label>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierAttachmentDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modified By">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierLastModifiedBy" runat="server" Text='<%#Eval("LastModifiedBy") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Modified Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierLastModifiedDate" runat="server" Text='<%#Eval("LastModifiedDate","{0:dd-MMM-yyyy hh:mm:ss tt}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierAttachmentFileName" runat="server" Text='<%#Eval("FileName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="File URL" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSupplierAttachmentFileURL" runat="server" Text='<%#Eval("FileURL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkDelete" runat="server" ImageUrl="~/images/DeleteRed.png" Width="16px" Height="16px" OnClick="lnkDelete_Click" OnClientClick="return ConfirmDelete();" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Mode="NumericFirstLast" />
                                    <PagerStyle CssClass="GridFooterStyle" />
                                </asp:GridView>
                            </div>
                            <div class="col-sm-4">
                                <ul class="b">
                                    <li>Valid Trade/Comercial License.*</li>
                                    <li>Valid Membership of Chamber of commerce.*</li>
                                    <li>Signature of the authorized signatory.*</li>
                                    <li>Listof major customers/Projects.*</li>
                                    <li>Recent Statement of work and Labours(for subcontractors).*</li>
                                    <li>Company Profile.</li>
                                </ul>
                                * Attachments are mandatory.
                            </div>
                            <div class="col-sm-4">
                                <ul class="b">
                                    <li>HSE Certificate.</li>
                                    <li>ISO Certificates.</li>
                                    <li>QA/QC Policy.</li>
                                    <li>Insurance Certificate (When Applicable).</li>
                                    <li>Any necessary certificate related to your materials.</li>
                                </ul>
                            </div>

                        </div>
                    </div>
                </div>
            </div>


</asp:Content>
