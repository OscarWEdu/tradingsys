using TradeSys;

int CurrentScreen = (int) Screen.Login;

bool is_running = true;

Console.Clear();
while (is_running)
{
    switch (CurrentScreen)
    {
        case (int)Screen.Login:
            return;
    }

    Console.WriteLine("Press Any Key to Continue");
    string Input = Console.ReadLine().ToLower();

    is_running = false;
}

void LoginScreen()
{
    Console.WriteLine("Log in to your user account:");
    Console.WriteLine("Username:");
    string User = Console.ReadLine();
    Console.WriteLine("Password:");
    string Pass = Console.ReadLine();
}