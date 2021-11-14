using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FSPBAL;

namespace FibrexSupplierPortal.Mgment.Control
{
	public partial class POReportMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            PageAccess();

        }
        protected void PageAccess()
        {
            bool sideMenuPOR = UserPermissions.SS_SecurityGroupPermission.SearchPermissionWithPermissionID(98);
            if (sideMenuPOR)
            {
                SideMenuReceivingDetailReport.Visible = true;
            }
            else
            {
                SideMenuReceivingDetailReport.Visible = false;
            }
            bool sideMenuPO360 = UserPermissions.SS_SecurityGroupPermission.SearchPermissionWithPermissionID(99);
            if (sideMenuPO360)
            {
                //SideMenu360Degree.Visible = true;
                SideMenu360Degree.Visible = false;
            }
            else
            {
                SideMenu360Degree.Visible = false;
            }
        }
	}
}