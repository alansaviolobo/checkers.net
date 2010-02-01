# start sales button / end sales button
to ensure that the billing is done for the respective day.
start sales records the date of sales
end sales closes the date of sales (no more sales allowed for the same date)
*****************
> create a page with one button
> before the order page, show this page
> change the button caption to <start sales>
> on click - insert an entry in the database table Misc (start_session => current_timestamp)
> provide an option on the order page, admin page
> click on the option - reset the (start_session) entry to 0
*****************

# open menu item
allow to enter any menu item on the fly, without recording it in the menu item list, along with the price

# remark for cancellation of OT
provide remark when the appropriate OT referring to the order is cancelled

# reports
category wise
item wise
bill wise (bifurcate for payment modes)

# table transfer and table clubbing
transfer - when the person places the order and moves to another table
clubbing - when two tables are to be billed under on table (eg. when a family comes with their driver, the family is on one table and the driver on another table. the OT are printed respective to the table. the bill is printed to either of the tables [master table, slave table])

# bill viewing
allow viewing of bills when searched with the bill_number
*****************
> create a new section in the administrator section
> ask to enter the bill number
> click on the search button
> fill the following details
	>> bill details
	>> items ordered
	>> payment mode
*****************

# preview of the reports
allow to preview of the reports and allow the option to print
*****************
> use crystal reports
*****************

# receipt / kot / bill numbers
provide numbers for bills, receipts, OT
*****************
> print numbers on bills, receipts, OT
*****************

# events
define and event
associate the person with the event
define packages
individual items are billed independently
advance payment option

++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

Order System
--------------------------------------------------------

Navigation Items:
	
	Menu : new, edit, delete
	Ingredient : new, edit, delete, converter, purchase
	Contact : new, edit, delete, credit
	User : new, edit, delete, password
	Petty Account : PettyCash, PettyExpense		
	Event : event, package
	Database : backup, restore
	
--------------------------------------------------------

reporting
offer
event

Status Codes -

Sales:
0 - closed
1 - open
2 - cancelled

Source:
0 - closed
1 - open
2 - billed

Invoice:
0 - closed : InvoiceClosed
1 - open : InvoiceNew
2 - cancelled : InvoiceDelete	

for new orders -
new sales : status 1 : source source_id
new source: status 1

for the bill -
new invoice : type bill : status 1
edit source : status 2

for bill close -
edit invoice : status 0
edit source : status 0
edit sales : status 0

for bill cancellation -
edit invoice : status 2
edit sales : status 0
edit source : status 0	

invoice -

cash : invoice status 0
credit : invoice status 0
close the bill marked credit
copy the payable_amount to balance_amount
store the record of the client and the bill

credit card : invoice status 0
zero billing : invoice status 0, tax 0, discount 100%