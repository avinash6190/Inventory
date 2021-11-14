using FSPBAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FibrexSupplierPortal.Mgment
{
    public partial class frmRptPuchaseOrder : System.Web.UI.Page
    {
        FSPBAL.FSPDataAccessModelDataContext db = new FSPBAL.FSPDataAccessModelDataContext(App_Code.HostSettings.CS);
        string UserName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = Security.DecryptText(HttpContext.Current.User.Identity.Name);
            LoadReports();
        }

        public void LoadReports()
        {
            try
            {
                string PORef = string.Empty;
                if (Request.QueryString["rptID"] != "")
                {
                    string rptID = Security.URLDecrypt(Request.QueryString["rptID"].ToString());
                    string revision = Security.URLDecrypt(Request.QueryString["revision"].ToString());
                    string Query = "Select * from ViewAllPurchaseOrder where PoNum='" + rptID + "' AND POREVISION='" + revision + "'";
                    PO ObjPo = db.POs.SingleOrDefault(x => x.PONUM == int.Parse(rptID) && x.POREVISION == short.Parse(revision));
                    SqlConnection Con = new SqlConnection(App_Code.HostSettings.CS);

                    Reports.DS.dsViewAllPurchaseOrder dsPO = new Reports.DS.dsViewAllPurchaseOrder();
                    dsPO.Clear();
                    dsPO.EnforceConstraints = false;
                    SqlDataAdapter da = new SqlDataAdapter(Query, Con);

                    string CompanyQuery = "";
                    if (ObjPo.ORGCODE == "26")
                    {
                        CompanyQuery = "select org_long_name,org_trade_name+' - P.O.Box:'+org_pob+' - '+org_address as org_address,org_pob,'Tel:' + org_telephone + ' - Fax:' + org_fax + ' - www.fibrex.ae' as org_telephone,org_fax,org_trade_name from securityPRJ.dbo.ORG_MASTER where org_code= '" + ObjPo.ORGCODE + "'";
                    }
                    else
                    {
                        CompanyQuery = "select org_long_name,org_trade_name+' - P.O.Box:'+org_pob+' - '+org_address as org_address,org_pob,'Tel:' + org_telephone + ' - Fax:' + org_fax + ' - www.fibrex.ae' as org_telephone,org_fax,org_trade_name from securityPRJ.dbo.ORG_MASTER where org_code = 101";
                    }
                    SqlDataAdapter comsqlda = new SqlDataAdapter(CompanyQuery, Con);
                    comsqlda.Fill(dsPO.CompanyDetail);

                    string BillToQuery = "";
                    if (ObjPo.CLASSIFICATION == "LPO" || ObjPo.CLASSIFICATION == null)
                    {
                        if (ObjPo.ORGCODE == "26")
                        {
                            BillToQuery = "select org_trade_name from securityPRJ.dbo.ORG_MASTER where org_code= '" + ObjPo.ORGCODE + "'";
                        }
                        else
                        {
                            BillToQuery = "select org_trade_name from securityPRJ.dbo.ORG_MASTER where org_code=101";
                        }
                    }
                    else
                    {
                        BillToQuery = "select org_trade_name from securityPRJ.dbo.ORG_MASTER where org_code= '" + ObjPo.ORGCODE+"'";
                    }
                    SqlDataAdapter billsqlda = new SqlDataAdapter(BillToQuery, Con);
                    billsqlda.Fill(dsPO.BillTo);

                    da.SelectCommand.CommandType = CommandType.Text;
                    da.Fill(dsPO.ViewAllPurchaseOrder);

                    if (dsPO.Tables[0].Rows.Count > 0)
                    {
                      /*  if (ObjPo.STATUS == "APRV")
                        {
                            Reports.rptPrintApprovedPurchaseOrder rpt = new Reports.rptPrintApprovedPurchaseOrder() { DataSource = dsPO };
                            rptViewer.Report = rpt;
                        }
                        else
                        {*/
                        if (ObjPo.STATUS == "APRV" || ObjPo.STATUS == "WAPPR" || ObjPo.STATUS == "REOPEN")
                        {
                            PORef = ObjPo.POREF;
                        }
                        else
                        {
                            PORef = ObjPo.PONUM.ToString();
                        }

                            Reports.rptPrintDraftPurchaseOrder rpt = new Reports.rptPrintDraftPurchaseOrder();// { DataSource = dsPO };
                            rpt.Parameters["POStatusValue"].Value = ObjPo.STATUS;
                            rpt.Parameters["POStatusValue"].Visible = false;//
                            rpt.Parameters["CusPORefNo"].Value = PORef;//CUSPORefNo
                            rpt.Parameters["CusPORefNo"].Visible = false;//CUSPORefNo
                            rpt.DataSource = dsPO;
                            rptViewer.Report = rpt;
                        //}
                        Con.Close();
                    }
                    else
                    {
                        rptViewer.Visible = false;
                    }
                }
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