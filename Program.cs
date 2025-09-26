using TradeSys;

TradingSystem tradingSystem = new TradingSystem();

bool is_running = true;

while (is_running)
{
    Console.Clear();
    if (tradingSystem.IsLoggedIn())
    {
        tradingSystem.MainScreen();
    }
    else
    {
        tradingSystem.LoginScreen();
    }

    Console.WriteLine("Press Any Key to Continue, or press ");
    Console.ReadLine();
}

