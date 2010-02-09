using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Content : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static int PackageId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        if (!Page.IsPostBack)
        {
            PackageId = Request.QueryString["PackageId"] != null ? int.Parse(Request.QueryString["PackageId"].ToString()) : 0;

            LtrPackageName.Text = Checkers.Packages.Where(P => P.Package_Id == PackageId).Select(P => P.Package_Name.ToString()).Single();

            if (Checkers.Contents.Where(C => C.Content_Package == PackageId).Select(C => C).Any() == true)
            {
                LtrMessage.Text = "Content Management For Package";
                FillData();
            }
            else
                LtrMessage.Text = "No Items Exists. Please Add Items.";
        }

        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 7;
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        //Status = Checkers.ContentNew(0, int.Parse(HdnMenuId.Value), decimal.Parse(TxtQuantity.Text), decimal.Parse(TxtSpecialUnitPrice.Text), (decimal.Parse(TxtQuantity.Text) * decimal.Parse(TxtSpecialUnitPrice.Text)), PackageId, 1, DateTime.Parse(Application["SalesSession"].ToString()));
        Status = Checkers.ContentNew(0, int.Parse(HdnMenuId.Value), decimal.Parse(TxtQuantity.Text), decimal.Parse(TxtSpecialUnitPrice.Text), PackageId, 1, DateTime.Parse(Application["SalesSession"].ToString()));
        LtrMessage.Text = Status == 1 ? "Content Associated With Package " + LtrPackageName.Text : "Error Occurred";

        ClearForm();
        FillData();
    }

    protected void DgContent_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.ContentDelete(int.Parse(e.Item.Cells[0].Text));
        LtrMessage.Text = Status == 1 ? "Item Deleted From Package" : "Error Occurred";

        FillData();
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();

        var PackageContents = from C in Checkers.Contents
                              from M in Checkers.Menus
                              where C.Content_Package == PackageId && C.Content_Status == 1 && C.Content_Menu == M.Menu_Id
                              select new { C.Content_Id, C.Content_Quantity, C.Content_Cost, M.Menu_Name, C.Content_UnitPrice };
        if (PackageContents.Count() > 0)
            DgContent.DataSource = PackageContents;
        else
            DgContent.DataSource = null;

        DgContent.DataBind();
    }

    public void ClearForm()
    {
        TxtName.Text = "";
        TxtQuantity.Text = "";
        TxtSpecialUnitPrice.Text = "";
    }
}
