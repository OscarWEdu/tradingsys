using TradeSys;

TradingSystem tradingSystem = new TradingSystem();

tradingSystem.LoadData();

bool is_running = true;
//Checks if a user is logged in, if not, send them to the login page, else use screenhandler to handle which page they are on
while (is_running)
{
    // Console.Clear();
    if (!tradingSystem.IsLoggedIn()) { tradingSystem.LoginScreen(); }
    else { ScreenHandler(); }

    Console.WriteLine("Press Any Key to Continue, or press 0 to exit");
    string Input = Console.ReadLine();
    if (Input == "0")
    {
        Console.Clear();
        is_running = false;
    }
    tradingSystem.StoreData();
}

//Executes the *Screen Method corresponding with the CurrentScreen variable
void ScreenHandler()
{
    Screen screen = tradingSystem.CurrentScreen;
    switch (screen)
    {
        case (Screen.Main): tradingSystem.MainScreen(); break;
        case (Screen.Browse): tradingSystem.BrowseScreen(); break;
        case (Screen.Add): tradingSystem.AddScreen(); break;
        case (Screen.Send): tradingSystem.SendScreen(); break;
        case (Screen.History): tradingSystem.HistoryScreen(); break;
        case (Screen.Pending): tradingSystem.PendingScreen(); break;
        case (Screen.Login): tradingSystem.LoginScreen(); break;
        case (Screen.Logout): tradingSystem.LogoutScreen(); break;
        case (Screen.Create): tradingSystem.CreateScreen(); break;
        default: break;
    }
}

