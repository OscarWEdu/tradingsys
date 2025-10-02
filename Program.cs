using TradeSys;

TradingSystem tradingSystem = new TradingSystem();

bool is_running = true;

//Checks if a user is logged in, if not, send them to the login page, else use screenhandler to handle which page they are on
while (is_running)
{
    Console.Clear();
    if (!tradingSystem.IsLoggedIn()) { tradingSystem.LoginScreen(); }
    else { ScreenHandler(); }

    Console.WriteLine("Press Any Key to Continue, or press 0 to exit");
    string Input = Console.ReadLine();
    if (Input == "0")
    {
        Console.Clear();
        is_running = false;
    }

}

void ScreenHandler()
{
    int screen = tradingSystem.CurrentScreen;
    switch (screen)
    {
        case ((int)Screen.Main): tradingSystem.MainScreen(); break;
        case ((int)Screen.Browse): tradingSystem.BrowseScreen(); break;
        case ((int)Screen.Add): tradingSystem.AddScreen(); break;
        case ((int)Screen.Send): tradingSystem.SendScreen(); break;
        case ((int)Screen.History): tradingSystem.HistoryScreen(); break;
        case ((int)Screen.Pending): tradingSystem.PendingScreen(); break;
        case ((int)Screen.Logout): tradingSystem.LogoutScreen(); break;
        case ((int)Screen.Create): tradingSystem.CreateScreen(); break;
        default: break;
    }
}

