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
    public Hashtable GetMenuDetails(string MenuName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable MenuInformation = new Hashtable();

        var MenuItem = (from M in Checkers.Menus
                        where M.Menu_Name.Contains(MenuName) && M.Menu_Status.Equals(1)
                        select M).Single();

        MenuInformation.Add("Id", MenuItem.Menu_Id);
        MenuInformation.Add("Name", MenuItem.Menu_Name);
        MenuInformation.Add("Category", MenuItem.Menu_Category);
        MenuInformation.Add("TokenSection", MenuItem.Menu_TokenSection);
        MenuInformation.Add("SellingPrice", MenuItem.Menu_SellingPrice);

        return MenuInformation;
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
    public Hashtable GetInventoryDetails(string InventoryName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable InventoryInformation = new Hashtable();

        var InventoryItem = (from I in Checkers.Inventories
                             where I.Inventory_Name.Equals(InventoryName) && I.Inventory_Status.Equals(1)
                             select I).Single();

        InventoryInformation.Add("Id", InventoryItem.Inventory_Id);
        InventoryInformation.Add("Name", InventoryItem.Inventory_Name);
        InventoryInformation.Add("BuyingPrice", InventoryItem.Inventory_BuyingPrice);
        InventoryInformation.Add("Threshold", InventoryItem.Inventory_Threshold);
        InventoryInformation.Add("Quantity", InventoryItem.Inventory_Quantity);
        InventoryInformation.Add("PurchaseUnit", InventoryItem.Inventory_PurchaseUnit);
        InventoryInformation.Add("ConversionUnit", InventoryItem.Inventory_ConversionUnit);

        return InventoryInformation;
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
    public Hashtable GetPackageDetails(string PackageName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable PackageInformation = new Hashtable();

        var PackageItem = (from P in Checkers.Packages
                           where P.Package_Name.Equals(PackageName) && P.Package_Status.Equals(1)
                           select P).Single();

        PackageInformation.Add("Id", PackageItem.Package_Id);
        PackageInformation.Add("Name", PackageItem.Package_Name);
        PackageInformation.Add("Type", PackageItem.Package_Type);
        PackageInformation.Add("Comments", PackageItem.Package_Comments);

        return PackageInformation;
    }
    #endregion

    #region Event Methods
    [WebMethod]
    public string[] GetEventList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from E in Checkers.Events
                where E.Event_Name.StartsWith(prefixText) && E.Event_Status.Equals(1)
                select E.Event_Name + "-" + (Checkers.Contacts.Where(C => C.Contact_Id == E.Event_Organizer && C.Contact_Status.Equals(1)).Select(C => C.Contact_Name)).Single()).ToArray<string>();
    }

    [WebMethod]
    public Hashtable GetEventDetails(string EventName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable EventInformation = new Hashtable();

        string[] Name = EventName.Split('-');

        var EventItem = (from E in Checkers.Events
                         where E.Event_Name.Equals(Name[0]) && E.Event_Status.Equals(1)
                         select E).Single();

        EventInformation.Add("Id", EventItem.Event_Id);
        EventInformation.Add("Name", EventItem.Event_Name);
        EventInformation.Add("FromDate", EventItem.Event_FromTimeStamp.ToString().Substring(0, 10).Replace("-", "/"));
        EventInformation.Add("ToDate", EventItem.Event_ToTimeStamp.ToString().Substring(0, 10).Replace("-", "/"));
        EventInformation.Add("Organizer", EventItem.Event_Organizer);
        EventInformation.Add("Venue", EventItem.Event_Venue);

        return EventInformation;
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
    public Hashtable GetContactDetails(string UserName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable ContactInformation = new Hashtable();

        var ContactItem = (from P in Checkers.Contacts
                           where P.Contact_Name.Equals(UserName) && P.Contact_Status.Equals(1)
                           select P).Single();

        ContactInformation.Add("Id", ContactItem.Contact_Id);
        ContactInformation.Add("Name", ContactItem.Contact_Name);
        ContactInformation.Add("UserName", ContactItem.Contact_UserName);
        ContactInformation.Add("Type", ContactItem.Contact_Type);
        ContactInformation.Add("Phone", ContactItem.Contact_Phone);
        ContactInformation.Add("Address", ContactItem.Contact_Address);
        ContactInformation.Add("Email", ContactItem.Contact_Email);
        ContactInformation.Add("OrganizationName", ContactItem.Contact_OrganizationName);
        ContactInformation.Add("OrganizationPhone", ContactItem.Contact_OrganizationPhone);
        ContactInformation.Add("OrganizationAddress", ContactItem.Contact_OrganizationAddress);
        ContactInformation.Add("Credit", ContactItem.Contact_Credit);

        return ContactInformation;
    }

    [WebMethod]
    public Hashtable GetOrganizationDetails(string OrganizationName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable ContactInformation = new Hashtable();

        var ContactItem = (from P in Checkers.Contacts
                           where P.Contact_OrganizationName.Equals(OrganizationName)
                           select P).Single();

        ContactInformation.Add("Id", ContactItem.Contact_Id);
        ContactInformation.Add("Name", ContactItem.Contact_Name);
        ContactInformation.Add("UserName", ContactItem.Contact_UserName);
        ContactInformation.Add("Type", ContactItem.Contact_Type);
        ContactInformation.Add("Phone", ContactItem.Contact_Phone);
        ContactInformation.Add("Address", ContactItem.Contact_Address);
        ContactInformation.Add("Email", ContactItem.Contact_Email);
        ContactInformation.Add("OrganizationName", ContactItem.Contact_OrganizationName);
        ContactInformation.Add("OrganizationPhone", ContactItem.Contact_OrganizationPhone);
        ContactInformation.Add("OrganizationAddress", ContactItem.Contact_OrganizationAddress);
        ContactInformation.Add("Credit", ContactItem.Contact_Credit);

        return ContactInformation;
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
    public Hashtable GetClientDetails(string UserName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable ClientInformation = new Hashtable();

        string[] Name = UserName.Split('-');

        var ClientItem = (from P in Checkers.Contacts
                          where P.Contact_Name.Equals(Name[0])
                          select P).Single();

        ClientInformation.Add("Id", ClientItem.Contact_Id);
        ClientInformation.Add("Name", ClientItem.Contact_Name);
        ClientInformation.Add("UserName", ClientItem.Contact_UserName);
        ClientInformation.Add("Type", ClientItem.Contact_Type);
        ClientInformation.Add("Phone", ClientItem.Contact_Phone);
        ClientInformation.Add("Address", ClientItem.Contact_Address);
        ClientInformation.Add("Email", ClientItem.Contact_Email);
        ClientInformation.Add("OrganizationName", ClientItem.Contact_OrganizationName);
        ClientInformation.Add("OrganizationPhone", ClientItem.Contact_OrganizationPhone);
        ClientInformation.Add("OrganizationAddress", ClientItem.Contact_OrganizationAddress);
        ClientInformation.Add("Credit", ClientItem.Contact_Credit);

        return ClientInformation;
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

    #region Asset
    [WebMethod]
    public string[] GetAssesList(string prefixText, int count)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        return (from A in Checkers.Assets
                where A.Asset_Status == 1
                select A.Asset_Name).Distinct().ToArray<string>();
    }

    [WebMethod]
    public Hashtable GetAssetDetails(string AssetName)
    {
        CheckersDataContext Checkers = new CheckersDataContext();
        Hashtable AssetInformation = new Hashtable();

        var AssetItem = (from A in Checkers.Assets
                         where A.Asset_Name.Contains(AssetName) && A.Asset_Status.Equals(1)
                         select A).Single();

        AssetInformation.Add("Id", AssetItem.Asset_Id);
        AssetInformation.Add("Name", AssetItem.Asset_Name);
        AssetInformation.Add("Quantity", AssetItem.Asset_Quantity);
        AssetInformation.Add("PurchaseDate", AssetItem.Asset_PurchaseDate);

        return AssetInformation;
    }
    #endregion
}