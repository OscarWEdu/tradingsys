using System.IO;
namespace TradeSys;

class TradingSystem
{
    List<User> Users = new List<User>();
    List<Item> Items = new List<Item>();
    List<Transaction> Transactions = new List<Transaction>();
    private User? ActiveUser = null;
    private Screen _CurrentScreen = Screen.Main;
    public Screen CurrentScreen { get { return _CurrentScreen; } }

    // //Creates some test data
    // public TradingSystem()
    // {
    //     Users.Add(new User("1", "1"));
    //     Users.Add(new User("2", "2"));
    //     Items.Add(new Item("Boat", "A full size boat, should definitely not be traded for a pencil", "1"));
    //     Items.Add(new Item("Pencil", "A pencil", "2"));
    //     Items.Add(new Item("Man", "A living human man", "1"));
    //     Items.Add(new Item("Lizard", "Some crawly gecko", "2"));
    //     List<Item?> ItemsIn1 = new List<Item?>();
    //     List<Item?> ItemsOut1 = new List<Item?>();
    //     List<Item?> ItemsIn2 = new List<Item?>();
    //     List<Item?> ItemsOut2 = new List<Item?>();
    //     ItemsIn1.Add(GetItem("Boat"));
    //     ItemsIn2.Add(GetItem("Lizard"));
    //     ItemsOut1.Add(GetItem("Pencil"));
    //     ItemsOut2.Add(GetItem("Man"));

    //     Transactions.Add(new Transaction(ItemsIn1, ItemsOut1));
    //     Transactions.Add(new Transaction(ItemsIn2, ItemsOut2));
    // }

    //Main loop, checks if active user is null, if so sets Current Screen to Login. Then throws the user into the relevant method depending on choice
    public void MainScreen()
    {
        Console.WriteLine("Select your choice:\n\"browse\" to view available items\n\"add\" to add available item\n\"send\" to send a trade request\n\"history\" to view trade history\n\"pending\" to view active requests\n\"logout\" to logout");
        string Input = NormalizedInput();

        switch (Input)
        {
            case "browse": _CurrentScreen = Screen.Browse; break;
            case "add": _CurrentScreen = Screen.Add; break;
            case "send": _CurrentScreen = Screen.Send; break;
            case "history": _CurrentScreen = Screen.History; break;
            case "pending": _CurrentScreen = Screen.Pending; break;
            case "logout": _CurrentScreen = Screen.Logout; break;

            default: Console.WriteLine("Invalid Input"); break;
        }
    }

    //TODO: Include tests to make sure input isn't empty
    string NormalizedInput()
    {
        return Console.ReadLine().ToLower();
    }

    //Sets _CurrentScreen to Main, this is then handled by the main program loop.
    private void ReturnToMain() { _CurrentScreen = Screen.Main; }

    //Displays all owned items
    public void BrowseScreen()
    {
        DisplayItems(true);
        ReturnToMain();
    }

    //Displays pending transactions, and then prompts the user to create a transaction for a selected item.
    public void SendScreen()
    {
        DisplayItems(false);
        Console.WriteLine("Type in the name of the item you would like to trade for:");
        string ItemName = Console.ReadLine();
        List<Item?> ItemsIn = new List<Item?>();
        ItemsIn.Add(GetItem(ItemName));
        AddItems(ItemsIn);
        Console.WriteLine("Type in the name of the owned item you are offering:");
        DisplayItems(true);
        ItemName = Console.ReadLine();
        List<Item?> ItemsOut = new List<Item?>();
        ItemsOut.Add(GetItem(ItemName));
        AddItems(ItemsOut);
        if (ItemsIn[0] != null && ItemsOut[0] != null) { Transactions.Add(new Transaction(ItemsOut, ItemsIn)); }
        ReturnToMain();
    }

    //Allows the user to keep adding Items to a given list
    private void AddItems(List<Item?> ItemList)
    {
        string UserInput = "";
        bool KeepTrading = true;
        while (KeepTrading) //Oh yeah these two loops could absolutely be refactore to be a method, I'm well aware lol
        {
            KeepTrading = false;
            Console.WriteLine("Would you like to trade another item? (Y/N)");
            UserInput = NormalizedInput();
            if (UserInput == "y") { KeepTrading = true; break; }
            Console.WriteLine("Type in the name of the item you would like to trade:");
            UserInput = Console.ReadLine();
            ItemList.Add(GetItem(UserInput));
        }
    }

    //Gets Item by Name
    private Item? GetItem(string ItemName)
    {
        foreach (Item item in Items)
        {
            if (item.Name == ItemName)
            {
                return item;
            }
        }
        return null;
    }

    //Displays either all Items belonging to the User or all non-owned Items
    private void DisplayItems(bool DisplayOwned)
    {
        Console.WriteLine("Items:");
        foreach (Item item in Items)
        {
            if (!DisplayOwned && !item.MatchOwned(ActiveUser.GetName()))
            {
                Console.WriteLine(item.Name + ": " + item.Description);
            }

            else if (DisplayOwned && item.MatchOwned(ActiveUser.GetName()))
            {
                Console.WriteLine(item.Name + ": " + item.Description);
            }
        }
    }

    //Prompts the User to create a new Item and adds it to the Items list
    public void AddScreen()
    {
        Console.WriteLine("Write the Name of the item you would like to add:");
        string Name = Console.ReadLine();
        Console.WriteLine("Write the Description of the item you would like to add:");
        string Description = Console.ReadLine();
        Items.Add(new Item(Name, Description, ActiveUser.GetName()));
        ReturnToMain();
    }

    private void CompleteTransaction(Transaction transaction)
    {
        TradeItem(transaction.ItemsSent, transaction.ItemsRecieved);
        transaction.CompleteTransaction();
    }

    //Swaps the Owner variable of 2 items
    private void TradeItem(List<Item> ItemsSent, List<Item> ItemsRecieved)
    {
        foreach (Item ItemRecieved in ItemsRecieved)
        {
            string Recipient = ItemRecieved.Owner;
            ItemRecieved.Owner = ItemsSent[ItemsRecieved.IndexOf(ItemRecieved)].Owner;
            ItemsSent[ItemsRecieved.IndexOf(ItemRecieved)].Owner = Recipient;
        }
    }

    //Displays all completed transactions
    public void HistoryScreen()
    {
        Console.WriteLine("Completed Transactions:  (Trades with more than 1 item are concatenated to only show 1 of the items traded)");
        foreach (Transaction transaction in Transactions)
        {
            if (!transaction.IsPending())
            {
                transaction.Print();
            }
        }
        ReturnToMain();
    }

    //Displays and allows the user to either accept or remove Pending trade requests in the Transactions list
    public void PendingScreen()
    {
        Console.Clear();
        Console.WriteLine("Pending Transactions:");
        foreach (Transaction transaction in Transactions)
        {
            if (transaction.ItemsRecieved != null)
            {
                if (transaction.IsPending() && transaction.IsRecipient(ActiveUser.GetName()))
                {
                    transaction.Print();
                }
            }
        }
        Console.WriteLine("Type the name of the item in the trade you would like to accept\nType clr followed by the name to reject the trade\nOr type anything else to return to main menu");
        string Name = Console.ReadLine();
        string[] NameArray = Name.Split(' ');
        bool ExitPending = true;
        foreach (Transaction transaction in Transactions)
        {
            if (transaction.IsRecipient(ActiveUser.GetName()))
            {
                if (NameArray.Length > 1)
                {
                    if (NameArray[0] == "clr" && transaction.ItemsRecieved[0].Name == NameArray[1]) { Transactions.Remove(transaction); ExitPending = false; break; }
                }
                else if (transaction.ItemsRecieved[0].Name == Name) { CompleteTransaction(transaction); ExitPending = false; }
            }
        }
        if (ExitPending) { ReturnToMain(); }
    }

    public void LogoutScreen()
    {
        ActiveUser = null;
        _CurrentScreen = Screen.Login; //TODO: Never gets checked
    }

    //Allows the user to create a new User and adds it to the list
    public void CreateScreen()
    {
        Console.Clear();
        Console.WriteLine("Username:");
        string Name = Console.ReadLine();
        Console.WriteLine("Password:");
        string Pass = Console.ReadLine();
        ActiveUser = new User(Name, Pass);
        Users.Add(ActiveUser);
        ReturnToMain();
    }

    //Handles user login
    public void LoginScreen()
    {
        Console.WriteLine("Enter your username to login to your account, or type \"new\" to create a new account:");
        string Name = Console.ReadLine();
        if (string.Equals(Name, "new")) { CreateScreen(); }
        else
        {
            Console.WriteLine("Password:");
            string Pass = Console.ReadLine();
            ActiveUser = FindUser(Name, Pass);
            ReturnToMain();
        }
    }

    //Fetches a User with a given username and password
    private User? FindUser(string Name, string Pass)
    {
        foreach (User user in Users)
        {
            if (user.MatchName(Name))
            {
                if (user.MatchPass(Pass))
                {
                    return user;
                }
            }
        }
        return null;
    }

    //Stores all variables of objects to a csv file 
    public void StoreData()
    {
        if (Users.Count != 0)
        {
            List<string> output = new List<string>();
            foreach (User user in Users)
            {
                output.AddRange(user.GetFields());
                output.Add(Convert.ToChar(0).ToString());
            }
            output.Add(Convert.ToChar(128).ToString());
            foreach (Item item in Items)
            {
                output.AddRange(item.GetFields());
                output.Add(Convert.ToChar(0).ToString());
            }
            output.Add(Convert.ToChar(128).ToString());
            foreach (Transaction transaction in Transactions)
            {
                output.AddRange(transaction.WriteAsString());
                output.Add(Convert.ToChar(0).ToString());
            }
            File.WriteAllLines("backup.csv", output);
        }
    }

    //Loads data from csv file and populates the list with created objects
    public void LoadData()
    {
        string path = "backup.csv";
        if (File.Exists(path))
        {
            string csv_data = File.ReadAllText(path);
            string[] DataFields = csv_data.Split("\n" + Convert.ToChar(128).ToString());
            LoadUsers(DataFields[0]);
            LoadItems(DataFields[1]);
            LoadTransactions(DataFields[2]);
        }
    }

    //Creates Users based on an input string representing the data of 1 or more users, then add them to the Users list
    private void LoadUsers(string UserData)
    {
        string[] SplitData = UserData.Split("\n" + Convert.ToChar(0).ToString(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (string Data in SplitData)
        {
            string[] DataField = Data.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Users.Add(new User(DataField[0], DataField[1]));
        }
    }

    //Creates Items based on an input string representing the data of 1 or more items, then add them to the Items list
    private void LoadItems(string ItemData)
    {
        string[] SplitData = ItemData.Split("\n" + Convert.ToChar(0).ToString(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (string Data in SplitData)
        {
            string[] DataField = Data.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            Items.Add(new Item(DataField[0], DataField[1], DataField[2]));
        }
    }

    //Creates Transactions based on an input string representing the data of 1 or more transactions, fetches the items matching the Item data stored, then add all Transaction to the Transactions list
    private void LoadTransactions(string TransactionData)
    {
        int ItemVariables = 3; //The number of variables contained in the Item object
        string[] SplitData = TransactionData.Split("\n" + Convert.ToChar(0).ToString(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        foreach (string Data in SplitData)
        {
            string[] DataField = Data.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            List<Item?> ItemsIn = new List<Item?>();
            List<Item?> ItemsOut = new List<Item?>();
            for (int i = 0; i < int.Parse(DataField[2]); i++)
            {
                ItemsOut.Add(GetItem(DataField[4 + (i * ItemVariables )]));
            }
            for (int i = 0; i < int.Parse(DataField[3]); i++)
            {
                ItemsIn.Add(GetItem(DataField[4 + i + (int.Parse(DataField[2]) * ItemVariables)]));
            }
            Transaction LoadedTrans = new Transaction(ItemsOut, ItemsIn);
            LoadedTrans.LoadTransactionData(bool.Parse(DataField[0]), DateTime.Parse(DataField[1])); //Todo: use tryparse
            // if (ItemsIn.Any() && ItemsOut.Any()) { Transactions.Add(LoadedTrans); }
            Transactions.Add(LoadedTrans);
        }
    }

    public bool IsLoggedIn()
    {
        if (ActiveUser == null)
        {
            return false;
        }
        return true;
    }
}