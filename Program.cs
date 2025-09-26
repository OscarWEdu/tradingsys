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
        is_running = false;
    }

}

void ScreenHandler()
{
    int screen = tradingSystem.CurrentScreen;
    switch (screen)
    {
        case ((int)Screen.Browse):
            break;
            
        case ((int)Screen.Add):
            break;
            
        case ((int)Screen.Send):
            break;
            
        case ((int)Screen.History):
            break;
            
        case ((int)Screen.Logout):
            break;
        
        
    }
}

