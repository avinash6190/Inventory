using ClosedXML.Excel;
using DevExpress.Spreadsheet;
using DevExpress.Web;
using DocumentFormat.OpenXml.Spreadsheet;
using FSPBAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FibrexSupplierPortal.Mgment
{
    public partial class frmPO360DegreeReport : System.Web.UI.Page
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
            }
        }
        protected void LoadControl()
        {
            ddlStatus.DataSource = db.SS_ALNDomains.Where(x => x.DomainName == "POSTATUS");
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, "Select");
        }
        protected void txtOrganization_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                HIDOrganizationCode.Value = "";
                txtProjectCode.Text = "";
                if (txtOrganization.Text != "")
                {
                    string OrgCode = Proj.ValidateOrganization(txtOrganization.Text);
                    if (OrgCode != "")
                    {
                        string orgname = string.Empty;
                        string[] CusOrgCode = OrgCode.Split(';', ' ');
                        HIDOrganizationCode.Value = CusOrgCode[0];

                        for (int i = 1; i < CusOrgCode.Count(); i++)
                        {
                            if (CusOrgCode[i] != "")
                            {
                                orgname += CusOrgCode[i] + " ";
                            }
                        }
                        txtOrganization.CssClass = "form-control";
                        txtOrganization.Text = orgname;
                        txtProjectCode.Focus();
                    }
                    else
                    {
                        lblError.Text = smsg.getMsgDetail(1075);
                        divError.Visible = true;
                        divError.Attributes["class"] = smsg.GetMessageBg(1075);
                        txtOrganization.Attributes["css"] = "boxshow";
                        txtOrganization.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
            }
        }

        protected void txtProjectCode_TextChanged(object sender, EventArgs e)
        {
            HidProjectCode.Value = "";
            if (txtProjectCode.Text != "" && HIDOrganizationCode.Value != "")
            {
                string OrgCode = Proj.ValidateUsingProjectCode(txtProjectCode.Text, HIDOrganizationCode.Value);
                if (OrgCode != "")
                {
                    string[] Org = OrgCode.Split(new string[] { ";;" }, StringSplitOptions.None);
                    HidProjectCode.Value = Org[1];
                    txtProjectCode.Text = Org[0];

                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1074);
                    divError.Visible = true;
                    divError.Attributes["class"] = smsg.GetMessageBg(1074);
                    txtProjectCode.Attributes["CssClass"] = "boxshow";
                }
                txtProjectCode.Focus();
            }
        }

        protected void txtBuyers_TextChanged(object sender, EventArgs e)
        {
            HidBuyersID.Value = "";
            if (txtBuyers.Text != "")
            {
                string BuyerID = Proj.ValidateBuyerUserID(int.Parse(txtBuyers.Text));
                if (BuyerID != "")
                {
                    if (BuyerID.Contains("Exception"))
                    {
                        lblError.Text = smsg.getMsgDetail(1076) + " " + BuyerID;
                        divError.Visible = true;
                        divError.Attributes["class"] = smsg.GetMessageBg(1076);
                        txtBuyers.CssClass += " boxshow";
                        txtBuyers.Focus();
                    }
                    else
                    {
                        HidBuyersID.Value = txtBuyers.Text;
                        txtBuyers.Text = BuyerID;
                        txtBuyers.CssClass = "form-control";
                    }
                }
                else
                {
                    lblError.Text = smsg.getMsgDetail(1076);
                    divError.Visible = true;
                    divError.Attributes["class"] = smsg.GetMessageBg(1076);
                    txtBuyers.Attributes["CssClass"] = "boxshow";
                }
                txtBuyers.Focus();
            }
        }
        public void LoadOrganization()
        {
            try
            {
                lblError.Text = "";
                divError.Visible = true;
                gvOrganization.DataSource = db.FIRMS_GetAllOrgs();
                gvOrganization.DataBind();
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
                    gvProjectLists.DataSource = db.FIRMS_GetAllProjects(int.Parse(OrgCode));
                    gvProjectLists.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        protected void btnSelectProject_Click(object sender, EventArgs e)
        {
            try
            {
                if (HIDOrganizationCode.Value != "")
                {
                    gvProjectLists.FilterExpression = string.Empty;
                    LoadProject(HIDOrganizationCode.Value);
                    popupProject.ShowOnPageLoad = true;
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

        protected void btnSearchClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtBuyers.Text = "";
                txtCompanyName.Text = "";
                txtRequisition.Text = "";
                txtOrganization.Text = "";
                txtProjectCode.Text = "";
                txtOrderDatefrom.Text = "";
                txtOrderDateTo.Text = "";
                ddlStatus.Text = "Select";
                lblError.Text = "";
                divError.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

        protected void gvOrganization_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadOrganization();
        }

        protected void gvOrganization_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadOrganization();
        }

        protected void gvOrganization_PageIndexChanged(object sender, EventArgs e)
        {
            LoadOrganization();
        }

        protected void gvOrganization_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string org_Code = grid.GetRowValuesByKeyValue(id, "org_code").ToString();
            HIDOrganizationCode.Value = org_Code;
            string org_name = grid.GetRowValuesByKeyValue(id, "org_name").ToString();
            txtOrganization.Text = org_name;
            txtProjectCode.Text = "";
            LoadProject(org_Code);
        }

        protected void gvProjectLists_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadProject(HIDOrganizationCode.Value);
        }

        protected void gvProjectLists_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadProject(HIDOrganizationCode.Value);
        }

        protected void gvProjectLists_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string org_Code = grid.GetRowValuesByKeyValue(id, "depm_code").ToString();
            HidProjectCode.Value = org_Code;
            string org_name = grid.GetRowValuesByKeyValue(id, "depm_desc").ToString();
            txtProjectCode.Text = org_name;
            popupProject.ShowOnPageLoad = false;
        }

        protected void gvSupplierLIst_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadAllSupplier();
        }

        protected void gvSupplierLIst_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadAllSupplier();
        }

        protected void gvSupplierLIst_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string Value = grid.GetRowValuesByKeyValue(id, "SupplierID").ToString();
            hidCompanyID.Value = Value;
            string SupplierName = grid.GetRowValuesByKeyValue(id, "SupplierName").ToString();
            txtCompanyName.Text = SupplierName;
        }
        protected void LoadAllSupplier()
        {
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                DSSupplierList.SelectCommand = @" Select * from ViewAllSuppliers";
                gvSupplierLIst.DataSource = DSSupplierList;
                gvSupplierLIst.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }
        protected void gvUserList_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadPopupControl();
        }

        protected void gvUserList_AfterPerformCallback(object sender, DevExpress.Web.ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadPopupControl();
        }

        protected void gvUserList_RowCommand(object sender, DevExpress.Web.ASPxGridViewRowCommandEventArgs e)
        {
            lblError.Text = "";
            divError.Visible = false;
            ASPxGridView grid = (ASPxGridView)sender;
            object id = e.KeyValue;
            string UserID = grid.GetRowValuesByKeyValue(id, "emp_code").ToString();
            HidBuyersID.Value = UserID;
            string emp_name = grid.GetRowValuesByKeyValue(id, "emp_name").ToString();
            txtBuyers.Text = emp_name;
        }
        protected void LoadPopupControl()
        {
            lblError.Text = "";
            divError.Visible = false;
            try
            {
                DSUserList.SelectCommand = "FIRMS_GetAllEmployee";
                gvUserList.DataSource = DSUserList;
                gvUserList.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

        protected void btnSearchPO360_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                bool VerifyOrderDate = ValidateDates(txtOrderDatefrom.Text, txtOrderDateTo.Text, "Order Date");
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
                string ExcelTitle = string.Empty;
                string Query = " select convert(nvarchar(10),a.PONUM) as PONUM,a.POREVISION,a.ORGNAME as DIVISION, a.PROJECTCODE as PROJECTCODE, a.PROJECTNAME as PROJECTNAME, a.VENDORNAME as VENDORNAME," +
                    "b.BUYERNAME as BUYERNAME,s.Description as STATUS,b.ORDERDATE,a.ITEMCODE,c.prm_item_desc as ITEMDESCRIPTION,RECEIPTSTATUS as RECEIPTSTATUS," +

                    " a.TOTALCOST as TOTALCOST, RECEIVINGTOTALCOST as TOTALRECEIVINGCOST,RECEIVEDTOTALCOST as TOTALRECEIVEDCOST,INVO as INVOICED," +
                    " INPROG as PAINPROGRESS, WAITFA as PAWAITINGFORFUNDALLOC, PAID as PAPAID, PCV as PCV,REV as REV, UNKNOWN as PAID " +
                    //"CONVERT(VARCHAR,CAST(a.TOTALCOST as money),1) as TOTALCOST,CONVERT(VARCHAR,CAST(RECEIVINGTOTALCOST as money),1) as TOTALRECEIVINGCOST," +
                    //"CONVERT(VARCHAR,CAST(RECEIVEDTOTALCOST as money),1) as TOTALRECEIVEDCOST,CONVERT(VARCHAR,CAST(INVO as money),1)" +
                    //"as INVOICED,CONVERT(VARCHAR,CAST(INPROG as money),1) as PAINPROGRESS,CONVERT(VARCHAR,CAST(WAITFA as money),1) as PAWAITINGFORFUNDALLOC," +
                    //"CONVERT(VARCHAR,CAST(PAID as money),1) as PAPAID, CONVERT(VARCHAR,CAST(PCV as money),1) as PCV,CONVERT(VARCHAR,CAST(REV as money),1) as REV,CONVERT(VARCHAR,CAST(UNKNOWN as money),1) as PAID FROM " +

                    " from " + dbTemp.Connection.Database + "..VW_PO360 as a " +
                    //" (SELECT     ORGCODE,(SELECT org_name FROM securityPRJ.dbo.ORG_MASTER WHERE (org_code = A.ORGCODE)) AS ORGNAME, PROJECTCODE, PROJECTNAME, VENDORID," +
                    //" (SELECT     SupplierName FROM " + db.Connection.Database + "..Supplier AS supplier_1 WHERE (SupplierID = A.VENDORID)) AS VENDORNAME, PONUM, TOTALCOST, RECEIVINGTOTALCOST, RECEIVEDTOTALCOST, RECEIPTSTATUS, INVO, INPROG," +
                    //" WAITFA, PAID, PCV, REV, UNKNOWN, MRNUM, POREVISION, ITEMCODE" +
                    //" FROM         (SELECT     CASE WHEN ORGCODE IS NULL THEN VORGCODE ELSE ORGCODE END AS ORGCODE, CASE WHEN POPROJECTCODE IS NULL " +
                    //" THEN PROJECTCODE ELSE POPROJECTCODE END AS PROJECTCODE, CASE WHEN POPROJECTNAME IS NULL " +
                    //" THEN PROJECTNAME ELSE POPROJECTNAME END AS PROJECTNAME, CASE WHEN VENDORID IS NULL " +
                    //" THEN SUPPLIERID ELSE VENDORID END AS VENDORID, PORECEIVING_1.PONUM, PORECEIVING_1.TOTALCOST, " +
                    //" PORECEIVING_1.RECEIVINGTOTALCOST, PORECEIVING_1.RECEIVEDTOTALCOST, PORECEIVING_1.RECEIPTSTATUS, " +
                    //" CASE WHEN POINVOICING.INVO IS NULL THEN 0.0 ELSE POINVOICING.INVO END AS INVO, CASE WHEN POINVOICING.INPROG IS NULL " +
                    //" THEN 0.0 ELSE POINVOICING.INPROG END AS INPROG, CASE WHEN POINVOICING.WAITFA IS NULL THEN 0.0 ELSE POINVOICING.WAITFA END AS WAITFA," +
                    //" CASE WHEN POINVOICING.PAID IS NULL THEN 0.0 ELSE POINVOICING.PAID END AS PAID, CASE WHEN POINVOICING.PCV IS NULL " +
                    //" THEN 0.0 ELSE POINVOICING.PCV END AS PCV, CASE WHEN POINVOICING.REV IS NULL THEN 0.0 ELSE POINVOICING.REV END AS REV, " +
                    //" CASE WHEN POINVOICING.UNKNOWN IS NULL THEN 0.0 ELSE POINVOICING.UNKNOWN END AS UNKNOWN, PORECEIVING_1.MRNUM, " +
                    //" PORECEIVING_1.POREVISION, PORECEIVING_1.ITEMCODE" +
                    //" FROM (SELECT     PO.ORGCODE, PO.PROJECTCODE AS POPROJECTCODE, PO.PROJECTNAME AS POPROJECTNAME, PO.VENDORID, PO.VENDORNAME, " +
                    //" PO.PONUM, PO.TOTALCOST, PO.MRNUM, PORECEIVING.RECEIVINGTOTALCOST, PORECEIVING.RECEIVEDTOTALCOST, " +
                    //" PORECEIVING.RECEIPTSTATUS, PORECEIVING.POREVISION, PORECEIVING.ITEMCODE FROM " + db.Connection.Database + "..VW_PO AS PO INNER JOIN" +
                    //" (SELECT (SELECT     ORGCODE  FROM  " + db.Connection.Database + "..VW_PO AS A" +
                    //" WHERE (PONUM = B.PONUM) AND (POREVISION = B.POREVISION)) AS ORGCODE," +
                    //" (SELECT     PROJECTCODE FROM " + db.Connection.Database + "..VW_PO AS A" +
                    //" WHERE      (PONUM = B.PONUM) AND (POREVISION = B.POREVISION)) AS PROJECTCODE," +
                    //" (SELECT     VENDORID FROM  " + db.Connection.Database + "..VW_PO AS A" +
                    //" WHERE (PONUM = B.PONUM) AND (POREVISION = B.POREVISION)) AS VENDORCODE, PONUM, POREVISION, PRETAXRECEIVINGTOTALCOST, " +
                    //" RECEIVINGTOTALCOST, RECEIVEDTOTALCOST,CASE WHEN B.RECEIVEDTOTALCOST >= B.RECEIVINGTOTALCOST THEN 'COMPLETE' WHEN B.RECEIVEDTOTALCOST > 0 AND " +
                    //" B.RECEIVEDTOTALCOST < B.RECEIVINGTOTALCOST THEN 'PARTIAL' WHEN B.RECEIVEDTOTALCOST = 0 THEN 'NONE' END AS RECEIPTSTATUS, ITEMCODE" +
                    //" FROM         (SELECT     PONUM, POREVISION, CAST(SUM(LINECOST) AS DECIMAL(10, 2)) AS PRETAXRECEIVINGTOTALCOST, CAST(SUM(LINECOST + TAXTOTAL) AS DECIMAL(10, " +
                    //" 2)) AS RECEIVINGTOTALCOST, CAST(SUM(RECEIVEDTOTALCOST) AS DECIMAL(10, 2)) AS RECEIVEDTOTALCOST, ITEMCODE" +
                    //" FROM " + dbTemp.Connection.Database + "..VW_POLINERECEIVING GROUP BY PONUM, POREVISION, ITEMCODE) AS B) AS PORECEIVING  " +
                    //" ON PO.PONUM = PORECEIVING.PONUM AND PO.POREVISION = PORECEIVING.POREVISION" +
                    //" WHERE      (PO.STATUS = 'APRV') AND (PO.HISTORYFLAG <> '1')) AS PORECEIVING_1 FULL OUTER JOIN" +
                    //" " + dbTemp.Connection.Database + "..VW_SUPPYMTSMRY AS POINVOICING ON PORECEIVING_1.VENDORID = POINVOICING.SUPPLIERID AND CONVERT(varchar, PORECEIVING_1.PONUM)" +
                    //" = CONVERT(varchar, POINVOICING.PONUM) AND PORECEIVING_1.ORGCODE = POINVOICING.VORGCODE) AS A) as a " +

                    " INNER JOIN " + db.Connection.Database + "..PO as b on a.PONUM=b.PONUM and a.POREVISION=b.POREVISION " +
                    "INNER JOIN " + db.Connection.Database + "..SS_ALNDomain as s on b.STATUS = s.Value and DomainName='POSTATUS' " +
                    "LEFT JOIN " + dbTemp.Connection.Database + "..VW_PRODUCT_MASTER as c on a.ITEMCODE=c.prm_item_code and b.ORGCODE=c.orgCode";
                if (rbdetail.Checked == true)
                {
                    ExcelTitle = "PO_360_raw_data";
                    if (Request.QueryString["Status"] != null)
                    {
                        ddlStatus.Text = Request.QueryString["Status"].ToString();
                    }
                    if (ddlStatus.Text != "Select")
                    {
                        where += " AND b.STATUS='" + ddlStatus.SelectedValue + "'";
                    }
                    if (txtOrganization.Text != "")
                    {
                        if (txtOrganization.Text.Contains('%'))
                        {
                            where += " AND a.ORGNAME like '" + txtOrganization.Text + "'";
                        }
                        else
                        {
                            where += " AND a.ORGCODE = '" + HIDOrganizationCode.Value + "'";
                        }
                    }
                    if (txtProjectCode.Text != "")
                    {
                        if (txtProjectCode.Text.Contains('%'))
                        {
                            where += " AND a.PROJECTNAME like '" + txtProjectCode.Text + "'";
                        }
                        else
                        {
                            where += " AND a.PROJECTCODE = '" + HidProjectCode.Value + "'";
                        }
                    }
                    if (txtCompanyName.Text != "")
                    {
                        if (txtCompanyName.Text.Contains('%'))
                        {
                            where += " AND a.VENDORNAME like '" + txtCompanyName.Text + "'";
                        }
                        else
                        {
                            where += " AND a.VENDORID = '" + hidCompanyID.Value + "'";
                        }
                    }
                    if (txtBuyers.Text != "")
                    {
                        if (txtBuyers.Text.Contains('%'))
                        {
                            where += " AND b.BUYERNAME like '" + txtBuyers.Text + "'";
                        }
                        else
                        {
                            where += " AND b.BUYERCODE = '" + HidBuyersID.Value + "'";
                        }
                    }
                    if (txtOrderDatefrom.Text != "")
                    {
                        where += " AND b.ORDERDATE >='" + DateTime.Parse(txtOrderDatefrom.Text) + "'";
                    }
                    if (txtOrderDateTo.Text != "")
                    {
                        where += " AND b.ORDERDATE <='" + DateTime.Parse(txtOrderDateTo.Text).AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
                    }


                    if (txtRequisition.Text != "")
                    {
                        if (txtRequisition.Text.Contains('%'))
                        {
                            where += " AND b.MRNUM like '" + txtRequisition.Text + "'";
                        }
                        else
                        {
                            where += " AND b.MRNUM = '" + txtRequisition.Text + "'";
                        }
                    }
                    if (where != "")
                    {
                        where = where.Remove(0, 4);
                        Query += " where " + where;
                    }
                    Query += " order by a.PONUM asc";
                }
                else if (rbsummary.Checked == true)
                {
                    ExcelTitle = "PO360_Degree_summary_data";
                    Query = string.Empty;
                    where = string.Empty;

                    Query = " select a.PONUM,a.POREVISION,a.ORGNAME as DIVISION, a.PROJECTCODE as PROJECTCODE, a.PROJECTNAME as PROJECTNAME, a.VENDORNAME as VENDORNAME," +
                    "b.BUYERNAME as BUYERNAME,s.Description as STATUS,b.ORDERDATE,a.ITEMCODE,c.prm_item_desc as ITEMDESCRIPTION,RECEIPTSTATUS as RECEIPTSTATUS," +

                    //"REPLACE(CONVERT(varchar(30),sum(a.TOTALCOST), 0), '.', ',') as TOTALCOST,REPLACE(CONVERT(varchar(30), sum(RECEIVINGTOTALCOST), 0), '.', ',') as TOTALRECEIVINGCOST,REPLACE(CONVERT(varchar(30), sum(RECEIVEDTOTALCOST), 0), '.', ',') as TOTALRECEIVEDCOST,REPLACE(CONVERT(varchar(30), sum(INVO), 0), '.', ',') as INVOICED," +
                    //"REPLACE(CONVERT(varchar(30), sum(INPROG), 0), '.', ',') as PAINPROGRESS,REPLACE(CONVERT(varchar(30), sum(WAITFA), 0), '.', ',') as PAWAITINGFORFUNDALLOC,REPLACE(CONVERT(varchar(30), sum(PAID), 0), '.', ',') as PAPAID,REPLACE(CONVERT(varchar(30), sum(PCV), 0), '.', ',') as PCV,REPLACE(CONVERT(varchar(30), sum(REV), 0), '.', ',') as REV,REPLACE(CONVERT(varchar(30), sum(UNKNOWN), 0), '.', ',') as PAID FROM " +
                    "CONVERT(VARCHAR,CAST(sum(a.TOTALCOST) as money),1) as TOTALCOST,CONVERT(VARCHAR,CAST(sum(RECEIVINGTOTALCOST) as money),1) as TOTALRECEIVINGCOST," +
                    "CONVERT(VARCHAR,CAST(sum(RECEIVEDTOTALCOST) as money),1) as TOTALRECEIVEDCOST,CONVERT(VARCHAR,CAST(sum(INVO) as money),1)" +
                    "as INVOICED,CONVERT(VARCHAR,CAST(sum(INPROG) as money),1) as PAINPROGRESS,CONVERT(VARCHAR,CAST(sum(WAITFA) as money),1) as PAWAITINGFORFUNDALLOC," +
                    "CONVERT(VARCHAR,CAST(sum(PAID) as money),1) as PAPAID, CONVERT(VARCHAR,CAST(sum(PCV) as money),1) as PCV,CONVERT(VARCHAR,CAST(sum(REV) as money),1) as REV,CONVERT(VARCHAR,CAST(sum(UNKNOWN) as money),1) as PAID " +

                    " from " + dbTemp.Connection.Database + "..VW_PO360 as a " +
                    //" (SELECT     ORGCODE,(SELECT org_name FROM securityPRJ.dbo.ORG_MASTER WHERE (org_code = A.ORGCODE)) AS ORGNAME, PROJECTCODE, PROJECTNAME, VENDORID," +
                    //" (SELECT     SupplierName FROM " + db.Connection.Database + "..Supplier AS supplier_1 WHERE (SupplierID = A.VENDORID)) AS VENDORNAME, PONUM, TOTALCOST, RECEIVINGTOTALCOST, RECEIVEDTOTALCOST, RECEIPTSTATUS, INVO, INPROG," +
                    //" WAITFA, PAID, PCV, REV, UNKNOWN, MRNUM, POREVISION, ITEMCODE" +
                    //" FROM         (SELECT     CASE WHEN ORGCODE IS NULL THEN VORGCODE ELSE ORGCODE END AS ORGCODE, CASE WHEN POPROJECTCODE IS NULL " +
                    //" THEN PROJECTCODE ELSE POPROJECTCODE END AS PROJECTCODE, CASE WHEN POPROJECTNAME IS NULL " +
                    //" THEN PROJECTNAME ELSE POPROJECTNAME END AS PROJECTNAME, CASE WHEN VENDORID IS NULL " +
                    //" THEN SUPPLIERID ELSE VENDORID END AS VENDORID, PORECEIVING_1.PONUM, PORECEIVING_1.TOTALCOST, " +
                    //" PORECEIVING_1.RECEIVINGTOTALCOST, PORECEIVING_1.RECEIVEDTOTALCOST, PORECEIVING_1.RECEIPTSTATUS, " +
                    //" CASE WHEN POINVOICING.INVO IS NULL THEN 0.0 ELSE POINVOICING.INVO END AS INVO, CASE WHEN POINVOICING.INPROG IS NULL " +
                    //" THEN 0.0 ELSE POINVOICING.INPROG END AS INPROG, CASE WHEN POINVOICING.WAITFA IS NULL THEN 0.0 ELSE POINVOICING.WAITFA END AS WAITFA," +
                    //" CASE WHEN POINVOICING.PAID IS NULL THEN 0.0 ELSE POINVOICING.PAID END AS PAID, CASE WHEN POINVOICING.PCV IS NULL " +
                    //" THEN 0.0 ELSE POINVOICING.PCV END AS PCV, CASE WHEN POINVOICING.REV IS NULL THEN 0.0 ELSE POINVOICING.REV END AS REV, " +
                    //" CASE WHEN POINVOICING.UNKNOWN IS NULL THEN 0.0 ELSE POINVOICING.UNKNOWN END AS UNKNOWN, PORECEIVING_1.MRNUM, " +
                    //" PORECEIVING_1.POREVISION, PORECEIVING_1.ITEMCODE" +
                    //" FROM (SELECT     PO.ORGCODE, PO.PROJECTCODE AS POPROJECTCODE, PO.PROJECTNAME AS POPROJECTNAME, PO.VENDORID, PO.VENDORNAME, " +
                    //" PO.PONUM, PO.TOTALCOST, PO.MRNUM, PORECEIVING.RECEIVINGTOTALCOST, PORECEIVING.RECEIVEDTOTALCOST, " +
                    //" PORECEIVING.RECEIPTSTATUS, PORECEIVING.POREVISION, PORECEIVING.ITEMCODE FROM " + db.Connection.Database + "..VW_PO AS PO INNER JOIN" +
                    //" (SELECT (SELECT     ORGCODE  FROM  " + db.Connection.Database + "..VW_PO AS A" +
                    //" WHERE (PONUM = B.PONUM) AND (POREVISION = B.POREVISION)) AS ORGCODE," +
                    //" (SELECT     PROJECTCODE FROM " + db.Connection.Database + "..VW_PO AS A" +
                    //" WHERE      (PONUM = B.PONUM) AND (POREVISION = B.POREVISION)) AS PROJECTCODE," +
                    //" (SELECT     VENDORID FROM  " + db.Connection.Database + "..VW_PO AS A" +
                    //" WHERE (PONUM = B.PONUM) AND (POREVISION = B.POREVISION)) AS VENDORCODE, PONUM, POREVISION, PRETAXRECEIVINGTOTALCOST, " +
                    //" RECEIVINGTOTALCOST, RECEIVEDTOTALCOST,CASE WHEN B.RECEIVEDTOTALCOST >= B.RECEIVINGTOTALCOST THEN 'COMPLETE' WHEN B.RECEIVEDTOTALCOST > 0 AND " +
                    //" B.RECEIVEDTOTALCOST < B.RECEIVINGTOTALCOST THEN 'PARTIAL' WHEN B.RECEIVEDTOTALCOST = 0 THEN 'NONE' END AS RECEIPTSTATUS, ITEMCODE" +
                    //" FROM         (SELECT     PONUM, POREVISION, CAST(SUM(LINECOST) AS DECIMAL(10, 2)) AS PRETAXRECEIVINGTOTALCOST, CAST(SUM(LINECOST + TAXTOTAL) AS DECIMAL(10, " +
                    //" 2)) AS RECEIVINGTOTALCOST, CAST(SUM(RECEIVEDTOTALCOST) AS DECIMAL(10, 2)) AS RECEIVEDTOTALCOST, ITEMCODE" +
                    //" FROM " + dbTemp.Connection.Database + "..VW_POLINERECEIVING GROUP BY PONUM, POREVISION, ITEMCODE) AS B) AS PORECEIVING  " +
                    //" ON PO.PONUM = PORECEIVING.PONUM AND PO.POREVISION = PORECEIVING.POREVISION" +
                    //" WHERE      (PO.STATUS = 'APRV') AND (PO.HISTORYFLAG <> '1')) AS PORECEIVING_1 FULL OUTER JOIN" +
                    //" " + dbTemp.Connection.Database + "..VW_SUPPYMTSMRY AS POINVOICING ON PORECEIVING_1.VENDORID = POINVOICING.SUPPLIERID AND CONVERT(varchar, PORECEIVING_1.PONUM)" +
                    //" = CONVERT(varchar, POINVOICING.PONUM) AND PORECEIVING_1.ORGCODE = POINVOICING.VORGCODE) AS A) as a " +

                    "INNER JOIN " + db.Connection.Database + "..PO as b on a.PONUM=b.PONUM and a.POREVISION=b.POREVISION " +
                    "INNER JOIN " + db.Connection.Database + "..SS_ALNDomain as s on b.STATUS = s.Value and DomainName='POSTATUS' " +
                    "LEFT JOIN " + dbTemp.Connection.Database + "..VW_PRODUCT_MASTER as c on a.ITEMCODE=c.prm_item_code and b.ORGCODE=c.orgCode";
                    if (txtOrganization.Text != "")
                    {
                        if (txtOrganization.Text.Contains('%'))
                        {
                            where += " AND a.ORGNAME like '" + txtOrganization.Text + "'";
                        }
                        else
                        {
                            where += " AND a.ORGCODE = '" + HIDOrganizationCode.Value + "'";
                        }
                    }
                    if (txtProjectCode.Text != "")
                    {
                        if (txtProjectCode.Text.Contains('%'))
                        {
                            where += " AND a.PROJECTNAME like '" + txtProjectCode.Text + "'";
                        }
                        else
                        {
                            where += " AND a.PROJECTCODE = '" + HidProjectCode.Value + "'";
                        }
                    }
                    if (txtCompanyName.Text != "")
                    {
                        if (txtCompanyName.Text.Contains('%'))
                        {
                            where += " AND a.VENDORNAME like '" + txtCompanyName.Text + "'";
                        }
                        else
                        {
                            where += " AND a.VENDORID = '" + hidCompanyID.Value + "'";
                        }
                    }
                    if (txtBuyers.Text != "")
                    {
                        if (txtBuyers.Text.Contains('%'))
                        {
                            where += " AND b.BUYERNAME like '" + txtBuyers.Text + "'";
                        }
                        else
                        {
                            where += " AND b.BUYERCODE = '" + HidBuyersID.Value + "'";
                        }
                    }
                    if (Request.QueryString["Status"] != null)
                    {
                        ddlStatus.Text = Request.QueryString["Status"].ToString();
                    }
                    if (ddlStatus.Text != "Select")
                    {
                        where += " AND b.STATUS='" + ddlStatus.SelectedValue + "'";
                    }
                    if (txtOrderDatefrom.Text != "")
                    {
                        where += " AND b.ORDERDATE >='" + DateTime.Parse(txtOrderDatefrom.Text) + "'";
                    }
                    if (txtOrderDateTo.Text != "")
                    {
                        where += " AND b.ORDERDATE <='" + DateTime.Parse(txtOrderDateTo.Text).AddHours(23).AddMinutes(59).AddSeconds(59) + "'";
                    }
                    if (where != "")
                    {
                        where = where.Remove(0, 4);
                        Query += " where " + where;
                    }
                    Query += " group by a.PONUM,a.POREVISION,a.ORGNAME,a.PROJECTCODE,a.PROJECTNAME,a.VENDORNAME,b.BUYERNAME,s.Description,b.ORDERDATE,a.ITEMCODE,c.prm_item_desc,RECEIPTSTATUS order by a.PONUM asc";
                }

                SqlConnection Con = new SqlConnection(App_Code.HostSettings.CS);
                SqlCommand cmd = new SqlCommand(Query);
                cmd.Connection = Con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (ExcelTitle == "PO_360_raw_data")
                        {
                            var sheet = wb.Worksheets.Add(ExcelTitle);
                            var table = sheet.Cell(1, 1).InsertTable(dt, ExcelTitle, true);
                            var ptSheet = wb.Worksheets.Add("PO_360_rpt");
                            var pt = ptSheet.PivotTables.Add("PO_360_rpt", ptSheet.Cell(1, 1), table.AsRange());
                            pt.RowLabels.Add("DIVISION");
                            pt.RowLabels.Add("PROJECTNAME");
                            pt.RowLabels.Add("PROJECTCODE");
                            pt.RowLabels.Add("VENDORNAME");
                            pt.RowLabels.Add("PONUM");
                            pt.Values.Add("TOTALCOST").SummaryFormula=XLPivotSummary.Sum;
                            pt.Values.Add("TOTALRECEIVINGCOST").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("TOTALRECEIVEDCOST").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("INVOICED").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("PAINPROGRESS").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("PAWAITINGFORFUNDALLOC").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("PAPAID").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("PCV").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("REV").SummaryFormula = XLPivotSummary.Sum;
                            pt.Values.Add("PAID").SummaryFormula = XLPivotSummary.Sum;

                            wb.Worksheet(1).Columns().AdjustToContents();
                            wb.Worksheet(2).Columns().AdjustToContents();
                            wb.Worksheet(2).Columns("1").Width = 70;
                            wb.Worksheet(2).Columns("2").Width = 12;
                            wb.Worksheet(2).Columns("3").Width = 22;
                            wb.Worksheet(2).Columns("4").Width = 18;
                            wb.Worksheet(2).Columns("5").Width = 12;
                            wb.Worksheet(2).Columns("6").Width = 12;
                            wb.Worksheet(2).Columns("7").Width = 25;
                            wb.Worksheet(2).Columns("8").Width = 12;
                            wb.Worksheet(2).Columns("9").Width = 12;
                            wb.Worksheet(2).Columns("10").Width = 12;
                            wb.Worksheet(2).Columns("11").Width = 12;

                            wb.Worksheet(2).Columns("2").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("3").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("4").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("5").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("6").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("7").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("8").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("9").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("10").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(2).Columns("11").Style.NumberFormat.Format = "#,##0.00";

                            wb.Worksheet(1).Columns("13").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("14").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("15").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("16").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("17").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("18").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("19").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("20").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("21").Style.NumberFormat.Format = "#,##0.00";
                            wb.Worksheet(1).Columns("22").Style.NumberFormat.Format = "#,##0.00";

                            wb.Worksheet(2).SetTabActive();
                            IXLSheetView view = wb.Worksheet(1).SheetView;
                            view.ZoomScale = 90;
                            IXLSheetView viewPivot = wb.Worksheet(2).SheetView;
                            viewPivot.ZoomScale = 90;
                        }
                        else
                        {
                            wb.Worksheets.Add(dt, ExcelTitle);
                            IXLSheetView view = wb.Worksheet(1).SheetView;
                            view.ZoomScale = 90;
                        }
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=PO360Degree.xlsx");
                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                    else
                    {
                        lblError.Text = "There is no records to display";
                        divError.Visible = true;
                        divError.Attributes["class"] = "alert alert-danger alert-dismissable";
                    }
                }

                selected_tab.Value = Request.Form[selected_tab.UniqueID];
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                divError.Visible = true;
                divError.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
        }

    }
}