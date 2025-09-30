namespace TradeSys;

class TradingSystem
{
    List<User> Users = new List<User>();
    List<Item> Items = new List<Item>();
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
        
    }
    
    public void AddScreen()
    {

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

    }

    //Handles user login
    public void LoginScreen()
    {
        Console.WriteLine("Log in to your user account:");
        Console.WriteLine("Username:");
        string Name = Console.ReadLine();
        Console.WriteLine("Password:");
        string Pass = Console.ReadLine();
        ActiveUser = FindUser(Name, Pass);
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

    public bool IsLoggedIn()
    {
        if (ActiveUser == null)
        {
            return false;
        }
        return true;
    }
}