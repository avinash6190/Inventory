using DevExpress.Web;
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
using ClosedXML.Excel;
using DevExpress.Spreadsheet;

namespace FibrexSupplierPortal.Mgment
{
    public partial class frmPOReceivingDetailReport : System.Web.UI.Page
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

        protected void gvProjectLists_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadProject(HIDOrganizationCode.Value);
        }

        protected void gvProjectLists_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadProject(HIDOrganizationCode.Value);
        }

        protected void gvProjectLists_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
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

        protected void gvSupplierLIst_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadAllSupplier();
        }

        protected void gvSupplierLIst_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadAllSupplier();
        }

        protected void gvSupplierLIst_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
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

        protected void gvUserList_BeforeColumnSortingGrouping(object sender, ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
        {
            LoadPopupControl();
        }

        protected void gvUserList_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            LoadPopupControl();
        }

        protected void gvUserList_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
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
        protected void btnSearchPOReceive_Click(object sender, EventArgs e)
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
                string Query = " select a.PONUM, a.POREVISION, POLINENUM, " +
                    "b.ORGNAME as DIVISION,b.PROJECTCODE as PROJECTCODE" +
                    ",b.PROJECTNAME as PROJECTNAME,b.VENDORID as VENDORCODE,b.VENDORNAME as VENDORNAME,b.BUYERNAME as BUYERNAME,d.Description as STATUS," +
                    "b.ORDERDATE as ORDERDATE,b.MRNUM as REQUISTIONREFNO,a.ITEMCODE as ITEMCODE,c.prm_item_desc as ITEMDESCRIPTION,a.SPECIFICATION as ITEMSPECIFICATION,a.ORDERUNIT as ORDERUNIT," +

                    "CONVERT(VARCHAR,CAST(ORDERQTY as money),1) as ORDERQTY,CONVERT(VARCHAR,CAST(UNITCOST as money),1) as UNITCOST,CONVERT(VARCHAR,CAST(LINECOST as money),1) as LINECOST," +
                    "CONVERT(VARCHAR,CAST(TAXTOTAL as money),1) as TAXTOTAL, CONVERT(VARCHAR,CAST(RECEIVEDQTY as money),1) as RECEIVEDQTY, CONVERT(VARCHAR,CAST(REMAININGQTY as money),1) as REMAININGQTY," +
                    "CONVERT(VARCHAR,CAST(RECEIVEDTOTALCOST as money),1) as RECEIVEDTOTALCOST" +

                    //" from (SELECT PONUM, POREVISION, POLINENUM, POLINEID, ORDERQTY, UNITCOST, LINECOST, TAXTOTAL, RECEIVEDQTY, " +
                    //    "CASE WHEN ORDERQTY - RECEIVEDQTY < 0 THEN 0 ELSE ORDERQTY - RECEIVEDQTY END AS REMAININGQTY, RECEIPTTOLERANCE," +
                    //    "ORDERQTY * RECEIPTTOLERANCE / 100.0 AS RECEIPTTOLQTY, LINECOST * RECEIPTTOLERANCE / 100.0 AS RECEIPTTOLAMT, (LINECOST + TAXTOTAL)" +
                    //    "* RECEIVEDQTY / ORDERQTY AS RECEIVEDTOTALCOST, CASE WHEN B.RECEIVEDQTY >= B.ORDERQTY THEN 'COMPLETE' WHEN B.RECEIVEDQTY > 0 AND " +
                    //    "B.RECEIVEDQTY < B.ORDERQTY THEN 'PARTIAL' WHEN B.RECEIVEDQTY = 0 THEN 'NONE' END AS RECEIPTSTATUS, ITEMCODE, ORDERUNIT" +
                    //    " FROM (SELECT     PONUM, POREVISION, POLINENUM, POLINEID, ORDERQTY, UNITCOST, LINECOST, RECEIPTTOLERANCE, CASE WHEN TAXTOTAL IS NULL" +
                    //    " THEN 0 ELSE TAXTOTAL END AS TAXTOTAL, ISNULL ((SELECT     SUM(mrvd_rcvd_order_unit) AS Expr1 FROM " + dbTemp.Connection.Database + "..VW_MATRECTRANS" +
                    //    " WHERE     (mrvm_po_no = A.PONUM) AND (mrvd_po_line_no = A.POLINENUM) AND (mrvm_po_rev <= A.POREVISION)), 0) AS RECEIVEDQTY, " +
                    //    " ITEMCODE, ORDERUNIT FROM " + db.Connection.Database + "..VW_POLINE AS A WHERE (LINETYPE = 'ITEM')) AS B) as a " +
                    " from " + dbTemp.Connection.Database + "..VW_POLINERECEIVING as a " +

                    "INNER JOIN " + db.Connection.Database + "..PO as b on a.PONUM=b.PONUM and a.POREVISION=b.POREVISION" +
                    " LEFT JOIN " + dbTemp.Connection.Database + "..VW_PRODUCT_MASTER as c on a.ITEMCODE=c.prm_item_code and b.ORGCODE=c.orgCode " +
                     " INNER JOIN " + db.Connection.Database + "..SS_ALNDomain as d on b.STATUS=d.Value and d.DomainName='POSTATUS' ";
                if (rbdetail.Checked == true)
                {
                    ExcelTitle = "PO_recv_raw_data";
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
                            where += " AND b.ORGNAME like '" + txtOrganization.Text + "'";
                        }
                        else
                        {
                            where += " AND b.ORGCODE = '" + HIDOrganizationCode.Value + "'";
                        }
                    }
                    if (txtProjectCode.Text != "")
                    {
                        if (txtProjectCode.Text.Contains('%'))
                        {
                            where += " AND b.PROJECTNAME like '" + txtProjectCode.Text + "'";
                        }
                        else
                        {
                            where += " AND b.PROJECTCODE = '" + HidProjectCode.Value + "'";
                        }
                    }
                    if (txtCompanyName.Text != "")
                    {
                        if (txtCompanyName.Text.Contains('%'))
                        {
                            where += " AND b.VENDORNAME like '" + txtCompanyName.Text + "'";
                        }
                        else
                        {
                            where += " AND b.VENDORID = '" + hidCompanyID.Value + "'";
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
                    ExcelTitle = "PO_Receiving_summary_data";
                    Query = string.Empty;
                    where = string.Empty;
                    Query = " select a.PONUM, a.POREVISION, POLINENUM,b.ORGNAME as DIVISION,b.PROJECTCODE as PROJECTCODE, " +
                        "b.PROJECTNAME as PROJECTNAME,b.VENDORID as VENDORCODE,b.VENDORNAME as VENDORNAME,b.BUYERNAME as BUYERNAME,s.Description as STATUS," +
                     "b.ORDERDATE as ORDERDATE,b.MRNUM as REQUISTIONREFNO,a.ITEMCODE as ITEMCODE,c.prm_item_desc as ITEMDESCRIPTION,a.SPECIFICATION as ITEMSPECIFICATION," +

                     //"REPLACE(CONVERT(varchar(30), sum(ORDERQTY), 0), '.', ',') as ORDERQTY, REPLACE(CONVERT(varchar(30), sum(UNITCOST), 0), '.', ',') as UNITCOST," +
                     //"REPLACE(CONVERT(varchar(30), sum(LINECOST), 0), '.', ',') as LINECOST, REPLACE(CONVERT(varchar(30), sum(TAXTOTAL), 0), '.', ',') as TAXTOTAL ," +
                     //"REPLACE(CONVERT(varchar(30), sum(RECEIVEDQTY), 0), '.', ',') as RECEIVEDQTY," +
                     //"REPLACE(CONVERT(varchar(30), sum(RECEIVEDTOTALCOST), 0), '.', ',') as RECEIVEDTOTALCOST " +
                     "CONVERT(VARCHAR,CAST(sum(ORDERQTY) as money),1) as ORDERQTY,CONVERT(VARCHAR,CAST(sum(UNITCOST) as money),1) as UNITCOST,CONVERT(VARCHAR,CAST(sum(LINECOST) as money),1) as LINECOST," +
                     "CONVERT(VARCHAR,CAST(sum(TAXTOTAL) as money),1) as TAXTOTAL, CONVERT(VARCHAR,CAST(sum(RECEIVEDQTY) as money),1) as RECEIVEDQTY, CONVERT(VARCHAR,CAST(sum(REMAININGQTY) as money),1) as REMAININGQTY," +
                     "CONVERT(VARCHAR,CAST(sum(RECEIVEDTOTALCOST) as money),1) as RECEIVEDTOTALCOST" +
                        //" from " + dbTemp.Connection.Database + "..VW_POLINERECEIVING as a " +

                        //" from (SELECT PONUM, POREVISION, POLINENUM, POLINEID, ORDERQTY, UNITCOST, LINECOST, TAXTOTAL, RECEIVEDQTY, " +
                        //"CASE WHEN ORDERQTY - RECEIVEDQTY < 0 THEN 0 ELSE ORDERQTY - RECEIVEDQTY END AS REMAININGQTY, RECEIPTTOLERANCE," +
                        //"ORDERQTY * RECEIPTTOLERANCE / 100.0 AS RECEIPTTOLQTY, LINECOST * RECEIPTTOLERANCE / 100.0 AS RECEIPTTOLAMT, (LINECOST + TAXTOTAL)" +
                        //"* RECEIVEDQTY / ORDERQTY AS RECEIVEDTOTALCOST, CASE WHEN B.RECEIVEDQTY >= B.ORDERQTY THEN 'COMPLETE' WHEN B.RECEIVEDQTY > 0 AND " +
                        //"B.RECEIVEDQTY < B.ORDERQTY THEN 'PARTIAL' WHEN B.RECEIVEDQTY = 0 THEN 'NONE' END AS RECEIPTSTATUS, ITEMCODE, ORDERUNIT" +
                        //" FROM (SELECT     PONUM, POREVISION, POLINENUM, POLINEID, ORDERQTY, UNITCOST, LINECOST, RECEIPTTOLERANCE, CASE WHEN TAXTOTAL IS NULL" +
                        //" THEN 0 ELSE TAXTOTAL END AS TAXTOTAL, ISNULL ((SELECT     SUM(mrvd_rcvd_order_unit) AS Expr1 FROM " + dbTemp.Connection.Database + "..VW_MATRECTRANS" +
                        //" WHERE     (mrvm_po_no = A.PONUM) AND (mrvd_po_line_no = A.POLINENUM) AND (mrvm_po_rev <= A.POREVISION)), 0) AS RECEIVEDQTY, " +
                        //" ITEMCODE, ORDERUNIT FROM " + db.Connection.Database + "..VW_POLINE AS A WHERE (LINETYPE = 'ITEM')) AS B) as a " +
                        " from " + dbTemp.Connection.Database + "..VW_POLINERECEIVING as a " +

                        "INNER JOIN " + db.Connection.Database + "..PO as b on a.PONUM=b.PONUM and a.POREVISION=b.POREVISION INNER JOIN " + db.Connection.Database + "..SS_ALNDomain as s" +
                        " on b.STATUS = s.Value and DomainName='POSTATUS'" +
                    " LEFT JOIN " + dbTemp.Connection.Database + "..VW_PRODUCT_MASTER as c on a.ITEMCODE=c.prm_item_code and b.ORGCODE=c.orgCode ";
                    if (txtOrganization.Text != "")
                    {
                        if (txtOrganization.Text.Contains('%'))
                        {
                            where += " AND b.ORGNAME like '" + txtOrganization.Text + "'";
                        }
                        else
                        {
                            where += " AND b.ORGCODE = '" + HIDOrganizationCode.Value + "'";
                        }
                    }
                    if (txtProjectCode.Text != "")
                    {
                        if (txtProjectCode.Text.Contains('%'))
                        {
                            where += " AND b.PROJECTNAME like '" + txtProjectCode.Text + "'";
                        }
                        else
                        {
                            where += " AND b.PROJECTCODE = '" + HidProjectCode.Value + "'";
                        }
                    }
                    if (txtCompanyName.Text != "")
                    {
                        if (txtCompanyName.Text.Contains('%'))
                        {
                            where += " AND b.VENDORNAME like '" + txtCompanyName.Text + "'";
                        }
                        else
                        {
                            where += " AND b.VENDORID = '" + hidCompanyID.Value + "'";
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
                    Query += " group by a.PONUM, a.POREVISION, POLINENUM,b.ORGNAME,b.PROJECTCODE,b.PROJECTNAME,b.VENDORID,b.VENDORNAME,b.BUYERNAME,s.Description,b.ORDERDATE,a.ITEMCODE,c.prm_item_desc,a.SPECIFICATION,b.MRNUM order by a.PONUM asc";
                }

                SqlConnection Con = new SqlConnection(App_Code.HostSettings.CS);
                SqlCommand cmd = new SqlCommand(Query);
                cmd.Connection = Con;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                using (XLWorkbook wb = new XLWorkbook())
                {
                    if ( dt != null && dt.Rows.Count>0)
                    { 
                        wb.Worksheets.Add(dt, ExcelTitle);
                        IXLSheetView view = wb.Worksheet(1).SheetView;
                        view.ZoomScale = 90;
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=POReceiving.xlsx");
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