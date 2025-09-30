namespace TradeSys;

class TradingSystem
{
    List<User> Users = new List<User>();
    List<Item> Items = new List<Item>();
    List<Transaction> Transactions = new List<Transaction>();
    User ActiveUser = null;
    private int _CurrentScreen = (int)Screen.Main;
    public int CurrentScreen {get { return _CurrentScreen; }}

    string NormalizedInput()
    {
        return Console.ReadLine().ToLower();
    }

    //Main loop, checks if active user is null, if so sets Current Screen to Login. Then throws the user into the relevant method depending on choice
    public void MainScreen()
    {
        Console.WriteLine("Select your choice:\n\"browse\" to view available items\n\"add\" to add available item\n\"send\" to send a trade request\n\"history\" to view trade history\n\"pending\" to view active requests\n\"logout\" to logout");
        string Input = Console.ReadLine().ToLower();

        switch (Input)
        {
            case "browse": _CurrentScreen = (int)Screen.Browse; break;
            case "add": _CurrentScreen = (int)Screen.Add; break;
            case "send": _CurrentScreen = (int)Screen.Send; break;
            case "history": _CurrentScreen = (int)Screen.History; break;
            case "pending": _CurrentScreen = (int)Screen.Pending; break;
            case "logout": _CurrentScreen = (int)Screen.Logout; break;

            default: Console.WriteLine("Invalid Input"); break;
        }
    }

    public void BrowseScreen()
    {
        DisplayItems(false);
        Console.WriteLine("Type in the name of the item you would like to trade for:");
    }

    private void DisplayItems(bool DisplayOwned)
    {
        Console.WriteLine("Items:");
        foreach (Item item in Items)
        {
            if (!DisplayOwned && item.MatchOwned(ActiveUser.GetName()))
            {
                Console.WriteLine(item.Name + ": " + item.Description);
            }
        }
    }

    public void AddScreen()
    {
        Console.WriteLine("Write the Name of the item you would like to add:");
        string Name = Console.ReadLine();
        Console.WriteLine("Write the Description of the item you would like to add:");
        string Description = Console.ReadLine();
        Items.Add(new Item(Name, Description, ActiveUser.GetName()));
    }
    
    public void SendScreen()
    {

    }
    
    public void HistoryScreen()
    {

    }
    
    //Handle Pending trade requests
    public void PendingScreen()
    {

    }
    
    public void LogoutScreen()
    {

    }

    public void CreateScreen()
    {
        Console.Clear();
        Console.WriteLine("Username:");
        string Name = Console.ReadLine();
        Console.WriteLine("Password:");
        string Pass = Console.ReadLine();
        ActiveUser = new User(Name, Pass);
        Users.Add(ActiveUser);
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
        }
    }

    private User FindUser(string Name, string Pass)
    {
        foreach (User user in Users) {
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

    private void StoreItems()
    {
        List<string> output = new List<string>();
        foreach (Item item in Items)
        {
            output.AddRange(item.GetFields());
            output.Add("\n");
        }
        File.WriteAllLines("items.csv", output);
    }

    private void LoadItems()
    {
        String[] items_csv = File.ReadAllLines("items.csv");

        foreach (string data in items_csv)
        {
            string[] split_data = data.Split(",");
            Items.Add(new Item(split_data[0], split_data[1], split_data[2]));
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