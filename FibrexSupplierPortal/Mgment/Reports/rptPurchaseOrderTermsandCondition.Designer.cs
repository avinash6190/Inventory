namespace FibrexSupplierPortal.Mgment.Reports
{
    partial class rptPurchaseOrderTermsandCondition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrCheckBox6 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine14 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine13 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel47 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel48 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox1 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrCheckBox2 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrCheckBox4 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrCheckBox3 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrLabel51 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel52 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrCheckBox5 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrCheckBox10 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrCheckBox9 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrCheckBox8 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.xrCheckBox7 = new DevExpress.XtraReports.UI.XRCheckBox();
            this.OrgCode = new DevExpress.XtraReports.Parameters.Parameter();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.dsPoDefination1 = new FibrexSupplierPortal.Mgment.Reports.DS.dsPoDefination();
            this.pODefinationTableAdapter = new FibrexSupplierPortal.Mgment.Reports.DS.dsPoDefinationTableAdapters.PODefinationTableAdapter();
            this.chkboxMaterLbl = new DevExpress.XtraReports.UI.CalculatedField();
            this.chkquo = new DevExpress.XtraReports.UI.CalculatedField();
            this.chkaward = new DevExpress.XtraReports.UI.CalculatedField();
            this.chkOthers = new DevExpress.XtraReports.UI.CalculatedField();
            ((System.ComponentModel.ISupportInitialize)(this.dsPoDefination1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrCheckBox6,
            this.xrLabel1,
            this.xrLine14,
            this.xrLabel49,
            this.xrLine13,
            this.xrLabel47,
            this.xrLabel48,
            this.xrCheckBox1,
            this.xrCheckBox2,
            this.xrCheckBox4,
            this.xrCheckBox3,
            this.xrLabel51,
            this.xrLabel52,
            this.xrCheckBox5,
            this.xrCheckBox10,
            this.xrCheckBox9,
            this.xrCheckBox8,
            this.xrCheckBox7});
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 124.9584F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrCheckBox6
            // 
            this.xrCheckBox6.Dpi = 100F;
            this.xrCheckBox6.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox6.FormattingRules.Add(this.formattingRule1);
            this.xrCheckBox6.LocationFloat = new DevExpress.Utils.PointFloat(688.4284F, 104.3749F);
            this.xrCheckBox6.Name = "xrCheckBox6";
            this.xrCheckBox6.SizeF = new System.Drawing.SizeF(88.15497F, 15.99998F);
            this.xrCheckBox6.StylePriority.UseFont = false;
            this.xrCheckBox6.Text = "Others (Specify)";
            // 
            // formattingRule1
            // 
            this.formattingRule1.Condition = "Iif([Parameters.OrgCode] ==1,true,false)";
            // 
            // 
            // 
            this.formattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule1.Name = "formattingRule1";
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PODefination.SupplierNotes")});
            this.xrLabel1.Dpi = 100F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 6F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(409.8655F, 14.99996F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(396.6345F, 16.00002F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "xrLabel1";
            // 
            // xrLine14
            // 
            this.xrLine14.BorderColor = System.Drawing.Color.White;
            this.xrLine14.Dpi = 100F;
            this.xrLine14.ForeColor = System.Drawing.Color.LightGray;
            this.xrLine14.LocationFloat = new DevExpress.Utils.PointFloat(0.5000035F, 30.99999F);
            this.xrLine14.Name = "xrLine14";
            this.xrLine14.SizeF = new System.Drawing.SizeF(806F, 8F);
            this.xrLine14.StylePriority.UseBorderColor = false;
            this.xrLine14.StylePriority.UseForeColor = false;
            // 
            // xrLabel49
            // 
            this.xrLabel49.Dpi = 100F;
            this.xrLabel49.Font = new System.Drawing.Font("Arial", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(409.8655F, 0F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(232.968F, 15F);
            this.xrLabel49.StylePriority.UseFont = false;
            this.xrLabel49.StylePriority.UseTextAlignment = false;
            this.xrLabel49.Text = "IMPORTANT NOTE TO SUPPLIERS:";
            this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLine13
            // 
            this.xrLine13.BorderColor = System.Drawing.Color.White;
            this.xrLine13.Dpi = 100F;
            this.xrLine13.ForeColor = System.Drawing.Color.LightGray;
            this.xrLine13.LineDirection = DevExpress.XtraReports.UI.LineDirection.Vertical;
            this.xrLine13.LocationFloat = new DevExpress.Utils.PointFloat(401.8654F, 0F);
            this.xrLine13.Name = "xrLine13";
            this.xrLine13.SizeF = new System.Drawing.SizeF(7.999969F, 50.52564F);
            this.xrLine13.StylePriority.UseBorderColor = false;
            this.xrLine13.StylePriority.UseForeColor = false;
            // 
            // xrLabel47
            // 
            this.xrLabel47.Dpi = 100F;
            this.xrLabel47.Font = new System.Drawing.Font("Arial", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel47.LocationFloat = new DevExpress.Utils.PointFloat(0.5000035F, 0F);
            this.xrLabel47.Name = "xrLabel47";
            this.xrLabel47.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel47.SizeF = new System.Drawing.SizeF(117.343F, 15F);
            this.xrLabel47.StylePriority.UseFont = false;
            this.xrLabel47.StylePriority.UseTextAlignment = false;
            this.xrLabel47.Text = "FOR INTERNAL USE ONLY:";
            this.xrLabel47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel48
            // 
            this.xrLabel48.Dpi = 100F;
            this.xrLabel48.Font = new System.Drawing.Font("Arial", 6F);
            this.xrLabel48.LocationFloat = new DevExpress.Utils.PointFloat(117.843F, 0F);
            this.xrLabel48.Name = "xrLabel48";
            this.xrLabel48.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel48.SizeF = new System.Drawing.SizeF(105F, 15F);
            this.xrLabel48.StylePriority.UseFont = false;
            this.xrLabel48.StylePriority.UseTextAlignment = false;
            this.xrLabel48.Text = "Attachment Checklist:";
            this.xrLabel48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrCheckBox1
            // 
            this.xrCheckBox1.Dpi = 100F;
            this.xrCheckBox1.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox1.FormattingRules.Add(this.formattingRule1);
            this.xrCheckBox1.LocationFloat = new DevExpress.Utils.PointFloat(0.5000035F, 104.3749F);
            this.xrCheckBox1.Name = "xrCheckBox1";
            this.xrCheckBox1.SizeF = new System.Drawing.SizeF(148.3233F, 15.99998F);
            this.xrCheckBox1.StylePriority.UseFont = false;
            this.xrCheckBox1.Text = "Mill/Test Certificate";
            // 
            // xrCheckBox2
            // 
            this.xrCheckBox2.Dpi = 100F;
            this.xrCheckBox2.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox2.FormattingRules.Add(this.formattingRule1);
            this.xrCheckBox2.LocationFloat = new DevExpress.Utils.PointFloat(270.4899F, 104.3749F);
            this.xrCheckBox2.Name = "xrCheckBox2";
            this.xrCheckBox2.SizeF = new System.Drawing.SizeF(133.3653F, 15.99998F);
            this.xrCheckBox2.StylePriority.UseFont = false;
            this.xrCheckBox2.Text = "Certificate of origin";
            // 
            // xrCheckBox4
            // 
            this.xrCheckBox4.Dpi = 100F;
            this.xrCheckBox4.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox4.FormattingRules.Add(this.formattingRule1);
            this.xrCheckBox4.LocationFloat = new DevExpress.Utils.PointFloat(148.8233F, 104.3749F);
            this.xrCheckBox4.Name = "xrCheckBox4";
            this.xrCheckBox4.SizeF = new System.Drawing.SizeF(121.6666F, 15.99998F);
            this.xrCheckBox4.StylePriority.UseFont = false;
            this.xrCheckBox4.Text = "Delivery Notes/Tickets";
            // 
            // xrCheckBox3
            // 
            this.xrCheckBox3.Dpi = 100F;
            this.xrCheckBox3.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox3.FormattingRules.Add(this.formattingRule1);
            this.xrCheckBox3.LocationFloat = new DevExpress.Utils.PointFloat(403.8552F, 104.3749F);
            this.xrCheckBox3.Name = "xrCheckBox3";
            this.xrCheckBox3.SizeF = new System.Drawing.SizeF(148.3233F, 15.99997F);
            this.xrCheckBox3.StylePriority.UseFont = false;
            this.xrCheckBox3.Text = "Operation & maintenance Manual";
            // 
            // xrLabel51
            // 
            this.xrLabel51.Dpi = 100F;
            this.xrLabel51.Font = new System.Drawing.Font("Arial", 6F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.xrLabel51.LocationFloat = new DevExpress.Utils.PointFloat(0F, 38.99998F);
            this.xrLabel51.Name = "xrLabel51";
            this.xrLabel51.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel51.SizeF = new System.Drawing.SizeF(232.968F, 15F);
            this.xrLabel51.StylePriority.UseFont = false;
            this.xrLabel51.StylePriority.UseTextAlignment = false;
            this.xrLabel51.Text = "CONDITIONS:";
            this.xrLabel51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel52
            // 
            this.xrLabel52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "PODefination.DefinationContent")});
            this.xrLabel52.Dpi = 100F;
            this.xrLabel52.Font = new System.Drawing.Font("Arial", 7F);
            this.xrLabel52.LocationFloat = new DevExpress.Utils.PointFloat(0.5000035F, 53.99998F);
            this.xrLabel52.Multiline = true;
            this.xrLabel52.Name = "xrLabel52";
            this.xrLabel52.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel52.SizeF = new System.Drawing.SizeF(796.5F, 50.37498F);
            this.xrLabel52.StylePriority.UseFont = false;
            this.xrLabel52.StylePriority.UseTextAlignment = false;
            this.xrLabel52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrCheckBox5
            // 
            this.xrCheckBox5.Dpi = 100F;
            this.xrCheckBox5.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox5.FormattingRules.Add(this.formattingRule1);
            this.xrCheckBox5.LocationFloat = new DevExpress.Utils.PointFloat(566.7618F, 104.3749F);
            this.xrCheckBox5.Name = "xrCheckBox5";
            this.xrCheckBox5.SizeF = new System.Drawing.SizeF(121.6666F, 15.99998F);
            this.xrCheckBox5.StylePriority.UseFont = false;
            this.xrCheckBox5.Text = "Bill of Lading";
            // 
            // xrCheckBox10
            // 
            this.xrCheckBox10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chkquo")});
            this.xrCheckBox10.Dpi = 100F;
            this.xrCheckBox10.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox10.LocationFloat = new DevExpress.Utils.PointFloat(97.5F, 14.99996F);
            this.xrCheckBox10.Name = "xrCheckBox10";
            this.xrCheckBox10.SizeF = new System.Drawing.SizeF(110.3654F, 16F);
            this.xrCheckBox10.StylePriority.UseFont = false;
            // 
            // xrCheckBox9
            // 
            this.xrCheckBox9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chkaward")});
            this.xrCheckBox9.Dpi = 100F;
            this.xrCheckBox9.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox9.LocationFloat = new DevExpress.Utils.PointFloat(207.8654F, 14.99999F);
            this.xrCheckBox9.Name = "xrCheckBox9";
            this.xrCheckBox9.SizeF = new System.Drawing.SizeF(97F, 16F);
            this.xrCheckBox9.StylePriority.UseFont = false;
            // 
            // xrCheckBox8
            // 
            this.xrCheckBox8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chkOthers")});
            this.xrCheckBox8.Dpi = 100F;
            this.xrCheckBox8.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox8.LocationFloat = new DevExpress.Utils.PointFloat(304.8654F, 14.99996F);
            this.xrCheckBox8.Name = "xrCheckBox8";
            this.xrCheckBox8.SizeF = new System.Drawing.SizeF(97F, 16F);
            this.xrCheckBox8.StylePriority.UseFont = false;
            // 
            // xrCheckBox7
            // 
            this.xrCheckBox7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "chkboxMaterLbl")});
            this.xrCheckBox7.Dpi = 100F;
            this.xrCheckBox7.Font = new System.Drawing.Font("Arial", 6F);
            this.xrCheckBox7.LocationFloat = new DevExpress.Utils.PointFloat(0.5000035F, 14.99999F);
            this.xrCheckBox7.Name = "xrCheckBox7";
            this.xrCheckBox7.SizeF = new System.Drawing.SizeF(97F, 16F);
            this.xrCheckBox7.StylePriority.UseFont = false;
            // 
            // OrgCode
            // 
            this.OrgCode.Description = "OrgCode";
            this.OrgCode.Name = "OrgCode";
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 10F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // dsPoDefination1
            // 
            this.dsPoDefination1.DataSetName = "dsPoDefination";
            this.dsPoDefination1.EnforceConstraints = false;
            this.dsPoDefination1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pODefinationTableAdapter
            // 
            this.pODefinationTableAdapter.ClearBeforeFill = true;
            // 
            // chkboxMaterLbl
            // 
            this.chkboxMaterLbl.Expression = "Iif([Parameters.OrgCode] == 1,\'Mill/Test Certificate\',\'Material Requisition\')";
            this.chkboxMaterLbl.Name = "chkboxMaterLbl";
            // 
            // chkquo
            // 
            this.chkquo.Expression = "Iif([Parameters.OrgCode] == 1,\'Delivery Notes/Tickets\',\'Quotation\')";
            this.chkquo.Name = "chkquo";
            // 
            // chkaward
            // 
            this.chkaward.Expression = "Iif([Parameters.OrgCode] == 1,\'Certificate of Origin\',\'Award Approval Form\')";
            this.chkaward.Name = "chkaward";
            // 
            // chkOthers
            // 
            this.chkOthers.Expression = "Iif([Parameters.OrgCode] == 1,\'Bill of Lading\',\'Others(Specify)\')";
            this.chkOthers.Name = "chkOthers";
            // 
            // rptPurchaseOrderTermsandCondition
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.chkboxMaterLbl,
            this.chkquo,
            this.chkaward,
            this.chkOthers});
            this.DataAdapter = this.pODefinationTableAdapter;
            this.DataMember = "PODefination";
            this.DataSource = this.dsPoDefination1;
            this.FilterString = "[OrgCode] = ?OrgCode";
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.OrgCode});
            this.Version = "16.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rptPurchaseOrderTermsandCondition_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.dsPoDefination1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLine xrLine14;
        private DevExpress.XtraReports.UI.XRLabel xrLabel49;
        private DevExpress.XtraReports.UI.XRLine xrLine13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel47;
        private DevExpress.XtraReports.UI.XRLabel xrLabel48;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox1;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox2;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel51;
        private DevExpress.XtraReports.UI.XRLabel xrLabel52;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox5;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox10;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox9;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox8;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox7;
        private DS.dsPoDefination dsPoDefination1;
        private DS.dsPoDefinationTableAdapters.PODefinationTableAdapter pODefinationTableAdapter;
        private DevExpress.XtraReports.Parameters.Parameter OrgCode;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox6;
        private DevExpress.XtraReports.UI.XRCheckBox xrCheckBox4;
        private DevExpress.XtraReports.UI.CalculatedField chkboxMaterLbl;
        private DevExpress.XtraReports.UI.CalculatedField chkquo;
        private DevExpress.XtraReports.UI.CalculatedField chkaward;
        private DevExpress.XtraReports.UI.CalculatedField chkOthers;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
    }
}
