INSERT INTO [Checkers].[dbo].[Contact]
	([Contact_Name],[Contact_UserName],[Contact_Password],[Contact_Type],[Contact_Phone],[Contact_Address],[Contact_Email],[Contact_OrganizationName],
     [Contact_OrganizationPhone],[Contact_OrganizationAddress],[Contact_Credit],[Contact_Status],[Contact_TimeStamp])
VALUES ('Administrator','Administrator','Checkers','Administrator',null,null,null,null,null,null,0,1,CURRENT_TIMESTAMP);

INSERT INTO [Checkers].[dbo].[Miscellaneous] ([Miscellaneous_Key],[Miscellaneous_Value]) VALUES('SalesSession','0');
INSERT INTO [Checkers].[dbo].[Miscellaneous] ([Miscellaneous_Key],[Miscellaneous_Value]) VALUES('UserLogged','0');