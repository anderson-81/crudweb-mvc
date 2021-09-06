using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crudweb_mvc.Reports
{
    [Authorize]
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Report");
            }

            if (!IsPostBack)
            {
                int opt = 0;

                try
                {
                    opt = int.Parse(Request.QueryString["opt"]);
                }
                catch
                {
                    Response.Redirect("~/Report");
                }

                //Salary Range:
                if (opt == 1)
                {
                    bool flag = false;

                    decimal sal1 = 0;
                    decimal sal2 = 0;

                    try
                    {
                        sal1 = decimal.Parse(Request.QueryString["InitialSal"]);
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Invalid value for starting salary.');", true);
                    }

                    try
                    {
                        sal2 = decimal.Parse(Request.QueryString["FinalSal"]);
                        flag = true;
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Invalid value for final salary.');", true);
                    }

                    if (flag == true)
                    {
                        if (sal1 <= sal2)
                        {
                            try
                            {
                                ReportViewer1.LocalReport.ReportEmbeddedResource = "crudweb_mvc.Reports.RpSalaryRange.rdlc";
                                ReportViewer1.LocalReport.ReportPath = @"Reports\RpSalaryRange.rdlc";
                                ObjectDataSource1.SelectParameters.Clear();
                                ObjectDataSource1.SelectParameters.Add("sal1", System.Data.DbType.Decimal, sal1.ToString());
                                ObjectDataSource1.SelectParameters.Add("sal2", System.Data.DbType.Decimal, sal2.ToString());
                                ObjectDataSource1.SelectMethod = "GetPhysicalPerson_BySalaryRange";
                            }
                            catch (Exception)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Error when generating report by salary range.')", true); ;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Salary range is invalid for report.')", true); ;
                        }
                    }
                }

                //All Physical Persons
                if (opt == 2)
                {
                    try
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "crudweb_mvc.Reports.RpPhysicalPerson.rdlc";
                        ReportViewer1.LocalReport.ReportPath = @"Reports\RpPhysicalPerson.rdlc";
                        ObjectDataSource1.SelectMethod = "GetPhysicalPerson";
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Error occurred while generating Physical Person's report.')", true); ;
                    }
                }

                //Average
                if (opt == 3)
                {
                    try
                    {
                        int opc_avg = int.Parse(Request.QueryString["opc_avg"]);

                        ReportViewer1.LocalReport.ReportEmbeddedResource = "crudweb_mvc.Reports.RpAverageWaze.rdlc";
                        ReportViewer1.LocalReport.ReportPath = @"Reports\RpAverageWaze.rdlc";

                        if (opc_avg == 1)
                        {
                            ObjectDataSource1.SelectMethod = "GetPhysicalPerson_SalaryAboveAVG";
                        }

                        if (opc_avg == 2)
                        {
                            ObjectDataSource1.SelectMethod = "GetPhysicalPerson_SalaryEqualAVG";
                        }

                        if (opc_avg == 3)
                        {
                            ObjectDataSource1.SelectMethod = "GetPhysicalPerson_SalaryUnderAVG";
                        }
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Error occurred while generating Average Wage's report.')", true); ;
                    }
                }

                //By Month
                if (opt == 4)
                {
                    try
                    {
                        int month = int.Parse(Request.QueryString["month"]);

                        ReportViewer1.LocalReport.ReportEmbeddedResource = "crudweb_mvc.Reports.RpByMonth.rdlc";
                        ReportViewer1.LocalReport.ReportPath = @"Reports\RpByMonth.rdlc";
                        ObjectDataSource1.SelectParameters.Clear();
                        ObjectDataSource1.SelectParameters.Add("month", System.Data.DbType.Int32, month.ToString());
                        ObjectDataSource1.SelectMethod = "GetPhysicalPerson_ByBirthMonth";
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Error occurred while generating month born's report.')", true); ;
                    }
                }

                //By Salary
                if (opt == 5)
                {
                    try
                    {
                        int opc_sal = int.Parse(Request.QueryString["opc_sal"]);

                        ReportViewer1.LocalReport.ReportEmbeddedResource = "crudweb_mvc.Reports.RpBySalary.rdlc";
                        ReportViewer1.LocalReport.ReportPath = @"Reports\RpBySalary.rdlc";

                        if (opc_sal == 1)
                        {
                            ObjectDataSource1.SelectMethod = "GetPhysicalPerson_HigherSalary";
                        }

                        if (opc_sal == 2)
                        {
                            ObjectDataSource1.SelectMethod = "GetPhysicalPerson_LowerSalary";
                        }
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Error occurred while generating salary's report.')", true); ;
                    }
                }

                //By Genre
                if (opt == 6)
                {
                    try
                    {
                        ReportViewer1.LocalReport.ReportEmbeddedResource = "crudweb_mvc.Reports.RpByGenre.rdlc";
                        ReportViewer1.LocalReport.ReportPath = @"Reports\RpByGenre.rdlc";
                        ObjectDataSource1.SelectMethod = "GetCountPhysicalPerson_ByGenre";
                    }
                    catch (Exception)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Error", "alert('Error occurred while generating by gender's report.')", true);
                    }
                }
            }
        }
    }
}