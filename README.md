David Maxson
scnerd@gmail.com
10/7/2013

==========
EasyBudget
==========

Windows tray app for easily tracking with your budget

Compiled under C# .NET 4.5, but there's no reason it couldn't be built under older frameworks.

SQLite framework provided by devart:
http://www.devart.com/dotconnect/sqlite/

Note that only the x64, .NET 4.5 version of their dll's is included. Versions are available for both x86 and x64, and for .NET 2.0, 3.0, 3.5, 4.0, and 4.5, so this project could probably be compiled as far back as .NET 2.0.

==========
RUN INSTRUCTIONS
==========

When run, an icon will appear in your system tray (lower right corner). Right-clicking on it will give you a few options by default.

Categories:
	You can add as many budget categories as you like
	Each category has a maximum amount of money you want to spend on it
	This is set at creation, and currently can't be modified.

Spending:
	When you spend money within one of the categories, click on it to record the spending
	Note that records are never deleted, but after 30 days they will no longer appear in the context menu
	Old records can still be recovered by clicking on the CSV button, and dumping the whole database
	
CSV:
	Data contained by the program can be dumped as a CSV file, which is best viewed in Excel or a similar program
	Note that the CSV does not represent how this program stores data, it just contains all relevant information stored
	
==========
CODE NOTES
==========

My apologies for the code looking so terrible... forms and controls use default names, variables are named haphazardly, etc. I whipped this all up in just a few hours, so I haven't cleaned it up yet :|

==========
TODO
==========

Clean up code and naming schemes

Enable editing/deletion of categories

Enable editing/deletion of spendings

Provide help and instructions (perhaps by clicking on the popup baloon?)

Provide better input/output methods.
