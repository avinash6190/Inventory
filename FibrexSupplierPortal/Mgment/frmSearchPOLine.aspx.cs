using DevExpress.Web;
using FSPBAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace FibrexSupplierPortal.Mgment
{
    public partial class frmSearchPOLine : System.Web.UI.Page
    {
        FSPBAL.FSPDataAccessModelDataContext db = new FSPBAL.FSPDataAccessModelDataContext(App_Code.HostSettings.CS);
        FSPBAL.tmpFibConsoDataContext dbTemp = new tmpFibConsoDataContext(App_Code.HostSettings.DS);
        SS_Message smsg = new SS_Message();
        Supplier Sup = new Supplier();
        Project Proj = new Project();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadControl();
                LoadOrganization();
                LoadAllSupplier();
                LoadPopupControl();
                LoadSearchRecords();
            }
        }
        protected void LoadPopupControl()
        {
            lblError.Text = "";
            divError.Visible = false;
            try
            {
                DSUserList.SelectCommand = "FIRMS_GetAllEmployee";
                gvPOLUserList.DataSource = DSUserList;
                gvPOLUserList.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        protected void LoadControl()
        {
            ddlPOLStatus.DataSource = db.SS_ALNDomains.Where(x => x.DomainName == "POSTATUS");
            ddlPOLStatus.DataBind();
            ddlPOLStatus.Items.Insert(0, "Select");
        }
        public void LoadOrganization()
        {
            try
            {
                lblError.Text = "";
                divError.Visible = true;
                gvPOLOrganization.DataSource = db.FIRMS_GetAllOrgs();
                gvPOLOrganization.DataBind();
            }
            catch (SqlException ex)
            {
                lblError.Text = ex.Message + " ErrorCode: " + ex.ErrorCode;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        public void LoadProject(string OrgCode)
        {
            lblError.Text = "";
            divError.Visible = false;
            try
            {
                if (OrgCode != "")
                {
                    gvPOLProjectLists.DataSource = db.FIRMS_GetAllProjects(int.Parse(OrgCode));
                    gvPOLProjectLists.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        protected void LoadAllSupplier()
        {
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                DSSupplierList.SelectCommand = @" Select * from ViewAllSuppliers";
                gvPOLSupplierLIst.DataSource = DSSupplierList;
                gvPOLSupplierLIst.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        protected void gvPOLOrganization_PageIndexChanged(object sender, EventArgs e)
        {
            LoadOrganization();
        }
        protected void gvPOLOrganization_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string org_Code = grid.GetRowValuesByKeyValue(id, "org_code").ToString();
            HIDPOLOrganizationCode.Value = org_Code;
            string org_name = grid.GetRowValuesByKeyValue(id, "org_name").ToString();
            txtPOLOrganization.Text = org_name;
            txtPOLProjectCode.Text = "";
            LoadProject(org_Code);
        }

        protected void txtPOLProjectCode_TextChanged(object sender, EventArgs e)
        {
            HidPOLProjectCode.Value = "";
            if (txtPOLProjectCode.Text != "" && HIDPOLOrganizationCode.Value != "")
            {
                string OrgCode = Proj.ValidateUsingProjectCode(txtPOLProjectCode.Text, HIDPOLOrganizationCode.Value);
                if (OrgCode != "")
                {
                    string[] Org = OrgCode.Split(new string[] { ";;" }, StringSplitOptions.None);
                    HidPOLProjectCode.Value = Org[1];
                    txtPOLProjectCode.Text = Org[0];

                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1074);
                    divError.Visible = true;
                    divError.Attributes["class"] = smsg.GetMessageBg(1074);
                    txtPOLProjectCode.Attributes["CssClass"] = "boxshow";
                }
                txtPOLProjectCode.Focus();
            }
        }

        protected void gvPOLProjectLists_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string org_Code = grid.GetRowValuesByKeyValue(id, "depm_code").ToString();
            HidPOLProjectCode.Value = org_Code;
            string org_name = grid.GetRowValuesByKeyValue(id, "depm_desc").ToString();
            txtPOLProjectCode.Text = org_name;
            popupPOLProject.ShowOnPageLoad = false;
        }
        protected void btnSelectPOLProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (HIDPOLOrganizationCode.Value != "")
                {
                    gvPOLProjectLists.FilterExpression = string.Empty;
                    LoadProject(HIDPOLOrganizationCode.Value);
                    popupPOLProject.ShowOnPageLoad = true;
                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1082);
                    divError.Attributes["class"] = smsg.GetMessageBg(1082);
                    divError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

        protected void gvPOLSupplierLIst_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string Value = grid.GetRowValuesByKeyValue(id, "SupplierID").ToString();
            hidPOLCompanyID.Value = Value;
            string SupplierName = grid.GetRowValuesByKeyValue(id, "SupplierName").ToString();
            txtPOLCompanyName.Text = SupplierName;
        }

        protected void gvPOLUserList_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string UserID = grid.GetRowValuesByKeyValue(id, "emp_code").ToString();
            HidPOLBuyersID.Value = UserID;
            string emp_name = grid.GetRowValuesByKeyValue(id, "emp_name").ToString();
            txtPOLBuyers.Text = emp_name;
        }

        protected void txtPOLBuyers_TextChanged(object sender, EventArgs e)
        {
            HidPOLBuyersID.Value = "";
            if (txtPOLBuyers.Text != "")
            {
                string BuyerID = Proj.ValidateBuyerUserName(txtPOLBuyers.Text);
                if (BuyerID != "")
                {
                    HidPOLBuyersID.Value = BuyerID;
                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1076);
                    divError.Visible = true;
                    divError.Attributes["class"] = smsg.GetMessageBg(1076);
                    txtPOLBuyers.Attributes["CssClass"] = "boxshow";
                }
                txtPOLBuyers.Focus();
            }
        }

        protected void txtPOLOrganization_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                HIDPOLOrganizationCode.Value = "";
                txtPOLProjectCode.Text = "";
                if (txtPOLOrganization.Text != "")
                {
                    string OrgCode = Proj.ValidateOrganization(txtPOLOrganization.Text);
                    if (OrgCode != "")
                    {
                        string orgname = string.Empty;
                        string[] CusOrgCode = OrgCode.Split(';', ' ');
                        HIDPOLOrganizationCode.Value = CusOrgCode[0];

                        for (int i = 1; i < CusOrgCode.Count(); i++)
                        {
                            if (CusOrgCode[i] != "")
                            {
                                orgname += CusOrgCode[i] + " ";
                            }
                        }
                        txtPOLOrganization.CssClass = "form-control";
                        txtPOLOrganization.Text = orgname;
                        txtPOLProjectCode.Focus();
                    }
                    else
                    {
                        lblError.Text = smsg.getMsgDetail(1075);
                        divError.Visible = true;
                        divError.Attributes["class"] = smsg.GetMessageBg(1075);
                        txtPOLOrganization.Attributes["css"] = "boxshow";
                        txtPOLOrganization.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
            }
        }

        protected void imgPOLItemCode_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (HIDPOLOrganizationCode.Value != "")
                {
                    gvPOLITEMCODE.FilterExpression = string.Empty;
                    LoadITEMCODE(HIDPOLOrganizationCode.Value);
                    popupPOLITEMCODE.ShowOnPageLoad = true;
                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1159);
                    divError.Attributes["class"] = smsg.GetMessageBg(1159);
                    divError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        public void LoadITEMCODE(string orgCode)
        {
            try
            {
                gvPOLITEMCODE.DataSource = dbTemp.VW_PRODUCT_MASTERs.Where(x => x.orgCode == orgCode).ToList();
                gvPOLITEMCODE.DataBind();
            }
            catch (SqlException ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

        protected void gvPOLITEMCODE_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            ASPxGridView grid = (ASPxGridView)sender;
            string MODELNUM = string.Empty;
            string MANUFACUTRER = string.Empty;
            object id = e.KeyValue;
            string ITEMCODE = grid.GetRowValuesByKeyValue(id, "prm_item_code").ToString();
            string ITEMDESC = grid.GetRowValuesByKeyValue(id, "prm_item_desc").ToString();
            string UNIT = grid.GetRowValuesByKeyValue(id, "uom_desc").ToString();
            txtPOLItemCode.Text = ITEMCODE;
            lblItemdescription.Visible = true;
            lblItemdescription.Text = ITEMDESC;
            txtPOLItemCode.CssClass = "form-control";
            popupPOLProject.ShowOnPageLoad = false;
        }

        protected void imgPOLCostCode_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (HIDPOLOrganizationCode.Value != "")
                {
                    gvPOLCostCode.FilterExpression = string.Empty;
                    loadCostCode(HIDPOLOrganizationCode.Value);
                    popupPOLCostCode.ShowOnPageLoad = true;
                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1158);
                    divError.Attributes["class"] = smsg.GetMessageBg(1158);
                    divError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        public void loadCostCode(string orgCode)
        {
            try
            {
                try
                {
                    tmpFibConso.SelectCommand = "Select * from VW_COST_CODE_MASTER  where orgCode ='" + orgCode + "'   order By ccm_desc";
                    gvPOLCostCode.DataSource = tmpFibConso;
                    gvPOLCostCode.DataBind();
                    popupPOLCostCode.ShowOnPageLoad = true;
                }
                catch (SqlException ex)
                {
                    lblError.Text = ex.Message;
                    divError.Visible = true;
                    divError.Attributes["class"] = "alert alert-danger alert-dismissable";
                }
            }
            catch (SqlException ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

        protected void gvPOLCostCode_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            txtPOLCostCode.Text = "";
            hdnPOLtxtCostCode.Value = "";
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string currentCostCode = grid.GetRowValuesByKeyValue(id, "ccm_cost_code").ToString();
            hdnPOLtxtCostCode.Value = currentCostCode;
            string currentCostCodeDescription = grid.GetRowValuesByKeyValue(id, "ccm_desc").ToString();
            txtPOLCostCode.Text = currentCostCode;
        }

        protected void btnSearchClearPOL_Click(object sender, EventArgs e)
        {
            try
            {
                txtPOLCostCode.Text = "";
                txtPOLBuyers.Text = "";
                txtPOLCompanyName.Text = "";
                txtPOLSupplierRefNo.Text = "";
                txtPOLCostCode.Text = "";
                txtPOLModel.Text = "";
                txtPOLManufacturer.Text = "";
                txtPOLDescription.Text = "";
                txtPOLOrganization.Text = "";
                txtPOLProjectCode.Text = "";
                txtPOLItemCode.Text = "";
                txtPOLOrderDatefrom.Text = "";
                txtPOLOrderDateTo.Text = "";
                lblItemdescription.Visible = false;
                lblItemdescription.Text = "";
                ddlPOLStatus.Text = "Select";
                LoadSearchRecords();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        protected void LoadSearchRecords()
        {
            string where = string.Empty;
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                string CostCode = string.Empty;
                string RequestedBy = string.Empty;
                string AdditionalSpecification = string.Empty;
                //string Query = "Select * from ViewAllPurchaseOrder ";
                string Query = " select a.PONUM,a.POREVISION,a.ORGNAME,a.PROJECTNAME,a.VENDORNAME,a.BUYERNAME,a.StatusDescription as Status,b.ITEMNUM,b.DESCRIPTION,b.ORDERUNIT,b.MODELNUM,b.MANUFACUTRER,b.CATALOGCODE,b.COSTCODE,b.POLINENUM,b.UNITCOST,b.SPECIFICATION from ViewAllPurchaseOrder as a inner join POLINE as b on a.PONUM = b.PONUM and a.POREVISION=b.POREVISION";
                if (Request.QueryString["Status"] != null)
                {
                    ddlPOLStatus.Text = Request.QueryString["Status"].ToString();
                }
                if (ddlPOLStatus.Text != "Select")
                {
                    where += " AND STATUS='" + ddlPOLStatus.SelectedValue + "'";
                }
                if (txtPOLOrganization.Text != "")
                {
                    if (txtPOLOrganization.Text.Contains('%'))
                    {
                        where += " AND ORGNAME like '" + txtPOLOrganization.Text + "'";
                    }
                    else
                    {
                        where += " AND ORGNAME = '" + txtPOLOrganization.Text + "'";
                    }
                }
                if (txtPOLProjectCode.Text != "")
                {
                    if (txtPOLProjectCode.Text.Contains('%'))
                    {
                        where += " AND PROJECTNAME like '" + txtPOLProjectCode.Text + "'";
                    }
                    else
                    {
                        where += " AND PROJECTNAME = '" + txtPOLProjectCode.Text + "'";
                    }
                }
                if (txtPOLCompanyName.Text != "")
                {
                    if (txtPOLCompanyName.Text.Contains('%'))
                    {
                        where += " AND VENDORNAME like '" + txtPOLCompanyName.Text + "'";
                    }
                    else
                    {
                        where += " AND VENDORNAME = '" + txtPOLCompanyName.Text + "'";
                    }
                }
                if (txtPOLBuyers.Text != "")
                {
                    if (txtPOLBuyers.Text.Contains('%'))
                    {
                        where += " AND BUYERNAME like '" + txtPOLBuyers.Text + "'";
                    }
                    else
                    {
                        where += " AND BUYERNAME = '" + txtPOLBuyers.Text + "'";
                    }
                }
                if (txtPOLOrderDatefrom.Text != "")
                {
                    where += " AND ORDERDATE >='" + DateTime.Parse(txtPOLOrderDatefrom.Text) + "'";
                }
                if (txtPOLOrderDateTo.Text != "")
                {
                    where += " AND ORDERDATE <='" + DateTime.Parse(txtPOLOrderDateTo.Text).AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
                }

                if (txtPOLItemCode.Text != "")
                {
                    if (txtPOLItemCode.Text.Contains('%'))
                    {
                        where += " AND ITEMNUM like '" + txtPOLItemCode.Text + "'";
                    }
                    else
                    {
                        where += " AND ITEMNUM = '" + txtPOLItemCode.Text + "'";
                    }
                }
                if (txtPOLDescription.Text != "")
                {
                    if (txtPOLDescription.Text.Contains('%'))
                    {
                        where += " AND b.DESCRIPTION like '" + txtPOLDescription.Text + "'";
                    }
                    else
                    {
                        where += " AND b.DESCRIPTION = '" + txtPOLDescription.Text + "'";
                    }
                }
                if (txtPOLModel.Text != "")
                {
                    if (txtPOLModel.Text.Contains('%'))
                    {
                        where += " AND MODELNUM like '" + txtPOLModel.Text + "'";
                    }
                    else
                    {
                        where += " AND MODELNUM = '" + txtPOLModel.Text + "'";
                    }
                }
                if (txtPOLManufacturer.Text != "")
                {
                    if (txtPOLManufacturer.Text.Contains('%'))
                    {
                        where += " AND MANUFACUTRER like '" + txtPOLManufacturer.Text + "'";
                    }
                    else
                    {
                        where += " AND MANUFACUTRER = '" + txtPOLManufacturer.Text + "'";
                    }
                }
                if (txtPOLSupplierRefNo.Text != "")
                {
                    if (txtPOLSupplierRefNo.Text.Contains('%'))
                    {
                        where += " AND CATALOGCODE like '" + txtPOLSupplierRefNo.Text + "'";
                    }
                    else
                    {
                        where += " AND CATALOGCODE = '" + txtPOLSupplierRefNo.Text + "'";
                    }
                }

                if (txtPOLCostCode.Text != "")
                {
                    if (txtPOLCostCode.Text.Contains('%'))
                    {
                        where += " AND COSTCODE like '" + txtPOLCostCode.Text + "'";
                    }
                    else
                    {
                        where += " AND COSTCODE = '" + txtPOLCostCode.Text + "'";
                    }
                }
                where += " AND HISTORYFLAG = '" + false + "' AND a.StatusDescription!='Cancelled'";
                if (where != "")
                {
                    where = where.Remove(0, 4);
                    Query += " where " + where;
                }
                DSSearchPurchaseOrder.SelectCommand = Query + " ORDER BY PONUM Desc";
                gvSearchPurchaseOrder.DataSource = DSSearchPurchaseOrder;
                gvSearchPurchaseOrder.DataBind();

                selected_tab.Value = Request.Form[selected_tab.UniqueID];
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

        protected void btnSearchPOL_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                bool VerifyOrderDate = ValidateDates(txtPOLOrderDatefrom.Text, txtPOLOrderDateTo.Text, "Order Date");
                if (!VerifyOrderDate)
                {
                    return;
                }
                LoadSearchRecords();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        protected bool ValidateDates(string DateFrom, string DateTo, string FieldName)
        {
            try
            {
                if (DateFrom != "")
                {
                    if (DateFrom != null)
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(DateFrom);
                            lblError.Text = "";
                            divError.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = smsg.getMsgDetail(1033).Replace("{0}", FieldName);
                            divError.Visible = true;
                            divError.Attributes["class"] = smsg.GetMessageBg(1033);
                            return false;
                        }
                    }
                }
                if (DateTo != "")
                {
                    if (DateTo != null)
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(DateTo);
                            lblError.Text = "";
                            divError.Visible = false;
                        }
                        catch (Exception ex)
                        {
                            lblError.Text = smsg.getMsgDetail(1033).Replace("{0}", FieldName);
                            divError.Visible = true;
                            divError.Attributes["class"] = smsg.GetMessageBg(1033);
                            return false;
                        }
                    }
                }
                if (DateFrom != "" && DateTo != "")
                {
                    if (DateTime.Parse(DateTo) < DateTime.Parse(DateFrom))
                    {
                        lblError.Text = smsg.getMsgDetail(1034).Replace("{0}", FieldName);
                        divError.Visible = true;
                        divError.Attributes["class"] = smsg.GetMessageBg(1034);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
                return false;
            }
            return true;
        }
        protected void gvSearchPurchaseOrder_PageIndexChanged(object sender, EventArgs e)
        {
            LoadSearchRecords();
            var view = sender as ASPxGridView;
            if (view == null) return;
            var pageIndex = view.PageIndex;
            gvSearchPurchaseOrder.PageIndex = pageIndex;
        }
        protected void gvSearchPurchaseOrder_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadSearchRecords();
        }

        protected void gvPOLITEMCODE_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadITEMCODE(HIDPOLOrganizationCode.Value);
        }

        protected void gvPOLITEMCODE_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadITEMCODE(HIDPOLOrganizationCode.Value);
        }

        protected void gvPOLOrganization_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadOrganization();
        }

        protected void gvPOLOrganization_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadOrganization();
        }

        protected void gvPOLCostCode_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            loadCostCode(HIDPOLOrganizationCode.Value);
        }

        protected void gvPOLCostCode_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            loadCostCode(HIDPOLOrganizationCode.Value);
        }

        protected void gvPOLUserList_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadPopupControl();
        }

        protected void gvPOLUserList_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadPopupControl();
        }

        protected void gvPOLProjectLists_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadProject(HIDPOLOrganizationCode.Value);
        }

        protected void gvPOLProjectLists_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadProject(HIDPOLOrganizationCode.Value);
        }

        protected void gvPOLSupplierLIst_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadAllSupplier();
        }

        protected void gvPOLSupplierLIst_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadAllSupplier();
        }
        protected void exportExcel_Click(object sender, ImageClickEventArgs e)
        {
            LoadSearchRecords();
            ASPxGridViewExporter1.WriteXlsxToResponse("PO-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
        }

        protected void txtPOLItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (HIDPOLOrganizationCode.Value != string.Empty)
                {
                    if (txtPOLItemCode.Text != string.Empty)
                    {
                        var itemDesc = dbTemp.VW_PRODUCT_MASTERs.ToList().Where(x => x.orgCode == HIDPOLOrganizationCode.Value && x.prm_item_code == txtPOLItemCode.Text).FirstOrDefault();
                        if (itemDesc != null)
                        {
                            lblItemdescription.Text = itemDesc.prm_item_desc;
                            lblItemdescription.Visible = true;
                        }
                        else
                        {
                            lblItemdescription.Visible = false;
                        }
                    }
                    else
                    {
                        lblItemdescription.Visible = false;
                    }
                }
                else
                {
                    lblItemdescription.Visible = true;
                    lblItemdescription.Text = "Please select division then provide item code to search";
                    txtPOLItemCode.Text = "";
                }
            }
             catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

        protected void txtPOLBuyers_TextChanged1(object sender, EventArgs e)
        {
            HidPOLBuyersID.Value = "";
            if (txtPOLBuyers.Text != "")
            {
                string BuyerID = Proj.ValidateBuyerUserID(int.Parse(txtPOLBuyers.Text));
                if (BuyerID != "")
                {
                    if (BuyerID.Contains("Exception"))
                    {
                        lblError.Text = smsg.getMsgDetail(1076) + " " + BuyerID;
                        divError.Visible = true;
                        divError.Attributes["class"] = smsg.GetMessageBg(1076);
                        txtPOLBuyers.CssClass += " boxshow";
                        txtPOLBuyers.Focus();
                    }
                    else
                    {
                        HidPOLBuyersID.Value = txtPOLBuyers.Text;
                        txtPOLBuyers.Text = BuyerID;
                        txtPOLBuyers.CssClass = "form-control";
                    }
                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1076);
                    divError.Visible = true;
                    divError.Attributes["class"] = smsg.GetMessageBg(1076);
                    txtPOLBuyers.Attributes["CssClass"] = "boxshow";
                }
                txtPOLBuyers.Focus();
            }
        }
    }
}