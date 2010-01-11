using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;

public partial class Controls_DatabaseBackup : System.Web.UI.UserControl
{
    public static string FileName;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 7;
    }
    protected void BtnBackup_Click(object sender, EventArgs e)
    {
        ServerConnection Con = new ServerConnection();
        Con.LoginSecure = false;
        Con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CheckersConnectionString"].ConnectionString;
        Server svr = new Server(Con);
        Backup bkp = new Backup();
        bkp.Devices.AddDevice(Server.MapPath("~") + @"\Backup\Checkers " + DateTime.Today.Date.ToLongDateString() + ".bak", DeviceType.File);
        bkp.Database = "Checkers";
        bkp.Action = BackupActionType.Database;
        bkp.Initialize = true;
        try
        {
            bkp.SqlBackup(svr);
            LtrMessage.Text = "Database Backup Successful";

            Response.AddHeader("Content-Disposition", "attachment; filename=Checkers " + DateTime.Today.Date.ToLongDateString() + ".bak");
            Response.ContentType = "application/octet-stream";
            string FilePath = Server.MapPath("~") + @"\Backup\Checkers " + DateTime.Today.Date.ToLongDateString() + ".bak";
            Response.WriteFile(FilePath);
            Response.End();

            File.Delete(Server.MapPath("~") + @"\Backup\Checkers " + DateTime.Today.Date.ToLongDateString() + ".bak");
        }
        catch (Exception ea)
        {
            LtrMessage.Text = ea.Message;
        }
    }
}
