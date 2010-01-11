using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Report : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 8;
    }
    protected void BtnCreate_Click(object sender, EventArgs e)
    {

    }
}
