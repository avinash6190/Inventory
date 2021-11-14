using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSPBAL;

namespace FibrexSupplierPortal.Mgment.Control
{
    public partial class PurchaseOrderReportMenu : System.Web.UI.UserControl
    {
        FSPDataAccessModelDataContext db = new FSPDataAccessModelDataContext(App_Code.HostSettings.CS);
        protected void Page_Load(object sender, EventArgs e)
        {
            PageAccess();
        }

        protected void PageAccess()
        {
           
        }
    }
}