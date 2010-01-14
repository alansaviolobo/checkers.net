using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class CheckersWebService : System.Web.Services.WebService
{
    #region Menu Methods
    [WebMethod]
    public string[] GetMenuList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from M in Checkers.Menus
                where M.Menu_Name.StartsWith(prefixText) && M.Menu_Status.Equals(1)
                select M.Menu_Name).ToArray<string>();
    }

    [WebMethod]
    public int GetMenuId(string MenuName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();

        return (from M in Checkers.Menus
                where M.Menu_Name.Equals(MenuName)
                select M.Menu_Id).Single();
    }
    #endregion

    #region Inventory Methods
    [WebMethod]
    public string[] GetInventoryList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from M in Checkers.Inventories
                where M.Inventory_Name.StartsWith(prefixText) && M.Inventory_Status.Equals(1)
                select M.Inventory_Name).ToArray<string>();
    }

    [WebMethod]
    public int GetInventoryId(string InventoryName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();

        return (from M in Checkers.Inventories
                where M.Inventory_Name.Equals(InventoryName)
                select M.Inventory_Id).Single();
    }
    #endregion

    #region Package Methods
    [WebMethod]
    public string[] GetPackageList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from P in Checkers.Packages
                where P.Package_Name.StartsWith(prefixText) && P.Package_Status.Equals(1)
                select P.Package_Name).ToArray<string>();
    }

    [WebMethod]
    public int GetPackageId(string PackageName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();

        return (from P in Checkers.Packages
                where P.Package_Name.Equals(PackageName)
                select P.Package_Id).Single();
    }
    #endregion

    #region Event Methods
    [WebMethod]
    public string[] GetEventList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from E in Checkers.Events
                where E.Event_Name.StartsWith(prefixText) && E.Event_Status.Equals(1)
                select E.Event_Name).ToArray<string>();
    }

    [WebMethod]
    public int GetEventId(string EventName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();

        return (from E in Checkers.Events
                where E.Event_Name.Equals(EventName)
                select E.Event_Id).Single();
    }
    #endregion

    #region Contact Methods
    [WebMethod]
    public string[] GetUserList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from P in Checkers.Contacts
                where P.Contact_Name.StartsWith(prefixText) && P.Contact_Status.Equals(1)
                && P.Contact_Type != "Customer" && P.Contact_Name != "Administrator"
                select P.Contact_Name).Distinct().ToArray<string>();
    }

    [WebMethod]
    public string[] GetContactList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from P in Checkers.Contacts
                where P.Contact_Name.StartsWith(prefixText) && P.Contact_Status.Equals(1)
                && P.Contact_Type == "Customer"
                select P.Contact_Name).ToArray<string>();
    }

    [WebMethod]
    public string[] GetOrganizationList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from P in Checkers.Contacts
                where P.Contact_OrganizationName.StartsWith(prefixText) && P.Contact_Status.Equals(1)
                && P.Contact_Type == "Customer"
                select P.Contact_OrganizationName).Distinct().ToArray<string>();
    }

    [WebMethod]
    public int GetContactId(string UserName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable UserDetails = new Hashtable();

        return (from P in Checkers.Contacts
                where P.Contact_Name.Equals(UserName)
                select P.Contact_Id).Single();
    }

    [WebMethod]
    public int GetOrganizationId(string OrganizationName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable UserDetails = new Hashtable();

        return (from P in Checkers.Contacts
                where P.Contact_OrganizationName.Equals(OrganizationName)
                select P.Contact_Id).Single();
    }
    #endregion

    #region Converter Methods
    [WebMethod]
    public string[] GetConverterList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return ((from M in Checkers.Menus
                 from C in Checkers.Converters
                 where C.Converter_Status == 1 && M.Menu_Status == 1 && M.Menu_Id == C.Converter_Menu && M.Menu_Name.StartsWith(prefixText)
                 select M.Menu_Name).Distinct()).ToArray<string>();
    }

    [WebMethod]
    public string GetConverterDetails(string ConverterKeyName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        int ConverterKeyId = (from M in Checkers.Menus where M.Menu_Name == ConverterKeyName select M.Menu_Id).Single();

        return ConverterKeyId.ToString();
    }
    #endregion

    #region Client Methods
    [WebMethod]
    public string[] GetClientList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from P in Checkers.Contacts
                where (P.Contact_Name.Contains(prefixText) || P.Contact_OrganizationName.Contains(prefixText)) && P.Contact_Status.Equals(1)
                && P.Contact_Type == "Customer"
                select P.Contact_Name + "- " + P.Contact_OrganizationName).Distinct().ToArray<string>();
    }

    [WebMethod]
    public int GetClientId(string UserName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();

        string[] Name = UserName.Split('-');

        return (from P in Checkers.Contacts
                where P.Contact_Name.Equals(Name[0])
                select P.Contact_Id).Single();
    }
    #endregion

    #region Offer
    [WebMethod]
    public string[] GetOfferList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from O in Checkers.Offers
                where O.Offer_Status == 1
                select O.Offer_Name).Distinct().ToArray<string>();
    }
    #endregion
}