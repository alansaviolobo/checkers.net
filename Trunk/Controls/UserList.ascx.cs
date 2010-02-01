using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_UserList : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        if (!Page.IsPostBack)
        {
            var User = Checkers.Contacts.Where(C => C.Contact_Status == 1 && C.Contact_Type != "Customer" && C.Contact_Name != "Administrator").Select(C => C);
            if (User.Count() > 0)
            {
                DgUser.Visible = true;
                DgUser.DataSource = User;
                DgUser.DataBind();
            }
            else
            {
                DgUser.Visible = false;
                LtrMessage.Text = "No Users found.";
            }
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 4;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var User = Checkers.Contacts.Where(C => C.Contact_Status == 1 && C.Contact_Name.Equals(TxtName.Text) && C.Contact_Type != "Customer" && C.Contact_Name != "Administrator").Select(C => C);
        if (User.Count() > 0)
        {
            DgUser.Visible = true;
            DgUser.DataSource = User;
            DgUser.DataBind();
        }
        else
        {
            DgUser.Visible = false;
            LtrMessage.Text = "No Users found.";
        }
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action cancelled by the user.";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.ContactDelete(int.Parse(HdnUserId.Value));
        LtrMessage.Text = Status == 1 ? "User item deleted successfully." : "Error occurred.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
        var User = Checkers.Contacts.Where(C => C.Contact_Status == 1 && C.Contact_Type != "Customer" && C.Contact_Name != "Administrator").Select(C => C);
        if (User.Count() > 0)
        {
            DgUser.Visible = true;
            DgUser.DataSource = User;
            DgUser.CurrentPageIndex = 0;
            DgUser.DataBind();
        }
        else
        {
            DgUser.Visible = false;
            LtrMessage.Text = "No Users found.";
        }
    }
    protected void DgUser_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        HdnUserId.Value = e.Item.Cells[0].Text;
        LtrMessage.Text = "Are you sure, you want to delete item " + e.Item.Cells[1].Text + " ?";
        BtnYes.Visible = true;
        BtnNo.Visible = true;
    }
    protected void DgUser_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        Response.Redirect("Operation.aspx?Section=User&Action=Edit&Id=" + e.Item.Cells[0].Text);
    }
}
