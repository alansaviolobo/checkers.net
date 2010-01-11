using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;

public partial class Controls_DatabaseRestore : System.Web.UI.UserControl
{
    public static string FileName;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 7;
    }
    protected void BtnRestore_Click(object sender, EventArgs e)
    {
        if (FuBackup.HasFile)
        {
            LtrMessage.Text = "Database Restore Will Overwrite The Existing Database. Are You Sure You Want To Proceed ?";
            BtnYes.Visible = true;
            BtnNo.Visible = true;
            FileName = FuBackup.FileName;
            FuBackup.SaveAs(Server.MapPath("~") + @"\Backup\" + FuBackup.FileName);
        }
        else
            LtrMessage.Text = "You have not specified a file.";
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        try
        {
            ServerConnection Con = new ServerConnection();
            Con.LoginSecure = false;
            Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CheckersConnectionString"].ConnectionString;
            Server svr = new Server(Con);
            System.Data.SqlClient.SqlConnection.ClearAllPools();
            Restore res = new Restore();
            svr.KillAllProcesses("Checkers");
            res.Devices.AddDevice(Server.MapPath("~") + @"\Backup\" + FileName, DeviceType.File);
            res.Database = "Checkers";
            res.Action = RestoreActionType.Database;
            res.ReplaceDatabase = true;
            res.SqlRestore(svr);
            LtrMessage.Text = "Database Restored Successfully.";

            File.Delete(Server.MapPath("~") + @"\Backup\" + FileName);
            BtnYes.Visible = false;
            BtnNo.Visible = false;
        }
        catch (Exception ex)
        {
            BtnYes.Visible = false;
            BtnNo.Visible = false;
            LtrMessage.Text = ex.InnerException.ToString();
        }
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Operation Cancelled By The User.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
    }
}
