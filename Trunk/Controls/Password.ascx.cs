﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Password : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        LtrUserName.Text = (from U in Checkers.Contacts where U.Contact_Id == int.Parse(Session["UserId"].ToString()) select U.Contact_Name).Single().ToString();
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 3;
    }
    protected void BtnChangePassword_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.ChangePassword(int.Parse(Session["UserId"].ToString()), TxtOldPassword.Text, TxtNewPassword.Text);
        LtrMessage.Text = Status == 1 ? "Password Changed Successfully." : "User Name And Password Do Not Match.";
        if (Status == 1) Status = Checkers.ActivityNew("Password Changed For User " + Session["UserId"].ToString(), int.Parse(Session["UserId"].ToString()));
    }
}