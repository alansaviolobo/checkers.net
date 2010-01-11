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