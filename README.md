A command-line program which allows users to trade items between one another, while logging their transactions.

To run: Simply run the program via your command line, then type your command as prompted by the program to perform the desired action.

The program was implemented using C# objects to store data during runtime, which is loaded from file at program start. It does not quite adhere to the object oriented philosophy, but is intentionally kept modular, abstracting out a fair deal of the internal working of the program, but does not, for example, encapsulate most of the internal variables of the objects from being run from outside classes.

Known Issues:
There are currently no checks in place to ensure that user input is sane, nor that it does not overlap with existing data. There are currently no plans to address this.

For testing purposes github may or may not be able to handle storage of null values, which are used to separate values in the csv. A new one can be generated uncommenting the TradingSystem constructor.

Excessive implementation information:
After loading any stored data from the backup csv the program is ran via main loop, where an enum state is checked each loop, running a corresponding method for the state. This state is changed through command-line navigaiton by the user. Through this the user can intuitively be sent to a set commands they are allowed to give, and be provided with the relevant information by letting them select a "screen". One exception to this is the login method, which is additionally called via a method which sends the commandline user to it if no User object is loaded.

Trades are kept track of via three lists, one keeping track of the users, one all available items, and one all existing transactions. The user is simply a set of a username and a password, while items have unique names and descriptions, as well as a reference to the name of the user. Upon a trade being completed the owner value is simple swapped between the traded items. All real information of a trade is instead kept in the Traansaction objects, which stores which items are to be traded, as well as when it was performed. the Transaction object are also used to keep track of completed trades, which are done by setting a bool whether the transaction is, or has already been performed, this can then be read by the main program, which can call the relevant function for displaying the Transaction object correctly to the user.
(For both the transaction and item objects, in a real world scenario, any should reference to other objects should be kept track of via unique references or ids, rather than direct storage (or a simple name check, as is the case for Item ownership by Users)).
Although there is no communication implemented, multi-user functionality can be trialed by logging in to multiple User accounts.

At the end of each loop, all relevant object fields are then backed up to file, letting any changes which happened in the loop be saved.
