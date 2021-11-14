using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Linq;

namespace FibrexSupplierPortal.Mgment.Reports
{
    public partial class rptSubPoLines : DevExpress.XtraReports.UI.XtraReport
    {
        string CurrencyCode = string.Empty;
       
        
        public rptSubPoLines()
        {
            InitializeComponent();
            //Session["MyVariable"] = 5;
          
        }

        private void rptSubPoLines_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string PoNum = Parameters[0].Value.ToString();
            string Revision = Parameters[1].Value.ToString();
            string Status = Parameters[2].Value.ToString();            
            ShowHideControl(decimal.Parse(PoNum), short.Parse(Revision));
            var totalCostWords = string.Empty;
            if (GetCurrentColumnValue("CURRENCYCODE") != null)
            {
                CurrencyCode = GetCurrentColumnValue("CURRENCYCODE").ToString();
                //lblGrandTotal.Text = ConvertToWords(totalCostWords);
            }
            SqlConnection con = new SqlConnection(FibrexSupplierPortal.App_Code.HostSettings.CS);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            DataSet dsCurrencyDescription = new DataSet();
            cmd.Connection = con;
            // cmd.CommandText = string.Format("SELECT * FROM POLINE where PONUM= '{0}' AND POREVISION={1} AND DESCRIPTION='{2}' AND REMARK='{3}'", PoNum, Revision, xrDescription.Text, xrTableCell2.Text);
            cmd.CommandText = string.Format("select Description from SS_NUMDOMAIN  where Value in(select DesValue from SS_LOOKUPMAP where SourceValue='{0}') and DomainName='Subcurrency'", CurrencyCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            string subCurrencyCode = string.Empty;

            foreach (DataRow drnew in ds.Tables[0].Rows)
            {
                subCurrencyCode = drnew["Description"].ToString();
            }
            cmd.CommandText = string.Format("SELECT * FROM SS_ALNDOMAIN where DomainName= '{0}' AND Value='{1}' ", "Currency", CurrencyCode);
            SqlDataAdapter daCurrencyDescription = new SqlDataAdapter(cmd);
            daCurrencyDescription.Fill(dsCurrencyDescription);
            string CurrencyDescription = string.Empty;

            foreach (DataRow drCurrency in dsCurrencyDescription.Tables[0].Rows)
            {
                CurrencyDescription = drCurrency["Description"].ToString();
            }
            if (GetCurrentColumnValue("TOTALCOST") != null)
            {
                totalCostWords = Convert.ToDecimal(GetCurrentColumnValue("TOTALCOST").ToString()).ToString("#,##0.00");
                lblGrandTotal.Text = ConvertToWords(totalCostWords, CurrencyDescription, subCurrencyCode);
            }

           

            //if (Status == "APRV")
            //{ xrtblCostCodeValue.Visible = false;
            //xrTableCell7.Visible = false;
            //}           
        }
        public void ShowHideControl(decimal PoNum, short Revision)
        {
            SqlConnection con = new SqlConnection(FibrexSupplierPortal.App_Code.HostSettings.CS);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            cmd.Connection = con;
            cmd.CommandText = string.Format("SELECT * FROM POLINE where PONUM= '{0}' AND POREVISION={1}", PoNum, Revision);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            dr.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                xrPanel1.Visible = false;
                xrPanel2.Visible = false;
                xrTable1.Visible = false;
                xrTable2.Visible = false;
                //xrLabel1.Visible = false; 
            }
            //SqlCommand cmd1 = new SqlCommand();
            //cmd1.Connection = con;
            //cmd1.CommandText = string.Format("SELECT * FROM PO where PONUM= '{0}' AND POREVISION={1}", PoNum, Revision);
            //SqlDataAdapter dr1 = new SqlDataAdapter(cmd1);
            //dr1.Fill(ds1);
            //if (ds1.Tables[0].Rows.Count != 0)
            //{
            //    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //    {
            //        if (ds1.Tables[0].Rows[i]["EXTNOTE"].ToString() != "")
            //        {
            //            xrExternalNotes.Text = ds1.Tables[0].Rows[i]["EXTNOTE"].ToString();
            //        }
            //        else
            //        {
            //            xrExternalNotes.Visible = false;
            //        }
            //    }
            //}
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //double TotalSubCost=0;
            //double TotalPreTax=0;
            //double TotalTax=0;
            //if (xrTableCell121.Text != "")
            //{
            //    TotalSubCost = double.Parse(xrTableCell121.Text);
            //}
            //if (xrPreTax.Text != "")
            //{
            //    TotalPreTax = double.Parse(xrPreTax.Text);
            //}
            //if (xrTax.Text != "")
            //{
            //    TotalTax = double.Parse(xrTax.Text);
            //}
            //double totalCost = TotalPreTax + TotalTax;
            //xrLabel8.Text = totalCost.ToString();
        }

        private void xrDescription_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            string PoNum = Parameters[0].Value.ToString();
            string Revision = Parameters[1].Value.ToString();  
            SqlConnection con = new SqlConnection(FibrexSupplierPortal.App_Code.HostSettings.CS);
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            cmd.Connection = con;
           // cmd.CommandText = string.Format("SELECT * FROM POLINE where PONUM= '{0}' AND POREVISION={1} AND DESCRIPTION='{2}' AND REMARK='{3}'", PoNum, Revision, xrDescription.Text, xrTableCell2.Text);
            cmd.CommandText = string.Format("SELECT * FROM POLINE where PONUM= '{0}' AND POREVISION={1} AND POLINENUM='{2}'", PoNum, Revision, xrRecordID.Text);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            dr.Fill(ds);
           // if (ds.Tables[0].Rows.Count == 0)
            ///{
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string Description = string.Empty;
                    if (ds.Tables[0].Rows[i]["DESCRIPTION"].ToString() != "")
                    {
                        Description += " " + ds.Tables[0].Rows[0]["DESCRIPTION"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["SPECIFICATION"].ToString() != "")
                    {
                    // Description += Environment.NewLine + " " + ds.Tables[0].Rows[0]["SPECIFICATION"].ToString();
                    Description += "." + " " + ds.Tables[0].Rows[0]["SPECIFICATION"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["CATALOGCODE"].ToString() != "")
                    {
                        Description += "." + " Supplier Ref No.: " + ds.Tables[0].Rows[0]["CATALOGCODE"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["MODELNUM"].ToString() != "")
                    {
                        Description += "." + " Model: " + ds.Tables[0].Rows[0]["MODELNUM"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["MANUFACUTRER"].ToString() != "")
                    {
                        Description += "." + " Manufacturer : " + ds.Tables[0].Rows[0]["MANUFACUTRER"].ToString();
                    } if (ds.Tables[0].Rows[i]["REMARK"].ToString() != "")
                    {
                        Description += Environment.NewLine + " Remark : " + ds.Tables[0].Rows[0]["REMARK"].ToString();
                    }
                    xrDescription.Text = Description;
                }
           // }
        }

        public static String ones(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = "";
            switch (_Number)
            {

                case 1:
                    name = "one";
                    break;
                case 2:
                    name = "two";
                    break;
                case 3:
                    name = "three";
                    break;
                case 4:
                    name = "four";
                    break;
                case 5:
                    name = "five";
                    break;
                case 6:
                    name = "six";
                    break;
                case 7:
                    name = "seven";
                    break;
                case 8:
                    name = "eight";
                    break;
                case 9:
                    name = "nine";
                    break;
            }
            return name;
        }
        public static String tens(String Number)
        {
            int _Number = Convert.ToInt32(Number);
            String name = null;
            switch (_Number)
            {
                case 10:
                    name = "ten";
                    break;
                case 11:
                    name = "eleven";
                    break;
                case 12:
                    name = "twelve";
                    break;
                case 13:
                    name = "thirteen";
                    break;
                case 14:
                    name = "fourteen";
                    break;
                case 15:
                    name = "fifteen";
                    break;
                case 16:
                    name = "sixteen";
                    break;
                case 17:
                    name = "seventeen";
                    break;
                case 18:
                    name = "eighteen";
                    break;
                case 19:
                    name = "nineteen";
                    break;
                case 20:
                    name = "twenty";
                    break;
                case 30:
                    name = "thirty";
                    break;
                case 40:
                    name = "fourty";
                    break;
                case 50:
                    name = "fifty";
                    break;
                case 60:
                    name = "sixty";
                    break;
                case 70:
                    name = "seventy";
                    break;
                case 80:
                    name = "eighty";
                    break;
                case 90:
                    name = "ninety";
                    break;
                default:
                    if (_Number > 0)
                    {
                        name = tens(Number.Substring(0, 1) + "0") + " " + ones(Number.Substring(1));
                    }
                    break;
            }
            return name;
        }
        public static String ConvertWholeNumber(String Number)
        {
            string word = "";
            try
            {
                bool beginsZero = false;//tests for 0XX  
                bool isDone = false;//test if already translated  
                double dblAmt = (Convert.ToDouble(Number));
                //if ((dblAmt > 0) && number.StartsWith("0"))  
                if (dblAmt > 0)
                {//test for zero or digit zero in a nuemric  
                    beginsZero = Number.StartsWith("0");

                    int numDigits = dblAmt.ToString().Length;
                    int pos = 0;//store digit grouping  
                    String place = "";//digit grouping name:hundres,thousand,etc...  
                    switch (numDigits)
                    {
                        case 1://ones' range  

                            word = ones(dblAmt.ToString());
                            isDone = true;
                            break;
                        case 2://tens' range  
                            word = tens(dblAmt.ToString());
                            isDone = true;
                            break;
                        case 3://hundreds' range  
                            pos = (numDigits % 3) + 1;
                            place = " hundred ";
                            break;
                        case 4://thousands' range  
                        case 5:
                        case 6:
                            pos = (numDigits % 4) + 1;
                            place = " thousand ";
                            break;
                        case 7://millions' range  
                        case 8:
                        case 9:
                            pos = (numDigits % 7) + 1;
                            place = " million ";
                            break;
                        case 10://Billions's range  
                        case 11:
                        case 12:

                            pos = (numDigits % 10) + 1;
                            place = " billion ";
                            break;
                        //add extra case options for anything above Billion...  
                        default:
                            isDone = true;
                            break;
                    }
                    if (!isDone)
                    {//if transalation is not done, continue...(Recursion comes in now!!)  
                        if (dblAmt.ToString().Substring(0, pos) != "0" && dblAmt.ToString().Substring(pos) != "0")
                        {
                            try
                            {
                                word = ConvertWholeNumber(dblAmt.ToString().Substring(0, pos)) + place + ConvertWholeNumber(dblAmt.ToString().Substring(pos));
                            }
                            catch { }
                        }
                        else
                        {
                            word = ConvertWholeNumber(dblAmt.ToString().Substring(0, pos)) + ConvertWholeNumber(dblAmt.ToString().Substring(pos));
                        }

                        //check for trailing zeros  
                        //if (beginsZero) word = " and " + word.Trim();  
                    }
                    //ignore digit grouping names  
                    if (word.Trim().Equals(place.Trim())) word = "";
                }
            }
            catch { }
            return word.Trim();
        }

        public static String ConvertToWords(String numb, String currencyCode,String subCurrency)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = "only";
            String textField = "Grand Total :";
            string wholeNumberCustom = string.Empty;
            try
            {
                int decimalPlace = numb.IndexOf(".");
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = ", and";// just to separate whole numbers from points/cents  
                        endStr = subCurrency + " " + endStr;//Cents  
                        decimal decimalPoints = decimal.Parse(points);
                       // string decimalPointsTruncate = decimalPoints.ToString().TrimEnd('0');
                        pointStr = ConvertWholeNumber(decimalPoints.ToString());                      
                    }
                    string wholeNumber = ConvertWholeNumber(wholeNo).Trim();
                    wholeNumberCustom = wholeNumber.First().ToString().ToUpper() + String.Join("", wholeNumber.Skip(1));
                }
                val = String.Format("{0} {1} {2}{3} {4} {5}", textField, wholeNumberCustom, currencyCode, andStr, pointStr, endStr);
            }
            catch { }
            return val;
        }

        public static String ConvertDecimals(String number)
        {
            String cd = "", digit = "", engOne = "";
            for (int i = 0; i < number.Length; i++)
            {
                digit = number[i].ToString();
                if (digit.Equals("0"))
                {
                    engOne = "Zero";
                }
                else
                {
                    engOne = ones(digit);
                }
                cd += " " + engOne;
            }
            return cd;
        }

    }
}
