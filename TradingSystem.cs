namespace TradeSys;

class TradingSystem
{

    User ActiveUser = null;
    int CurrentScreen = (int)Screen.Main;

    string NormalizedInput()
    {
        return Console.ReadLine().ToLower();
    }

    //Main loop, checks if active user is null, if so sets Current Screen to Login. Then throws the user into the relevant method depending on choice
    public void MainScreen()
    {
        Console.WriteLine("Select your choice:\n");
        string Input = Console.ReadLine().ToLower();

        switch (CurrentScreen)
        {
            case (int)Screen.Browse:
                break;

            case (int)Screen.Add:
                break;

            case (int)Screen.Send:
                break;

            case (int)Screen.History:
                break;

            case (int)Screen.Logout:
                ActiveUser = null;
                break;

            default:
                Console.WriteLine("Invalid Input");
                break;
        }
    }

    //Handles user login
    public void LoginScreen()
    {
        Console.WriteLine("Log in to your user account:");
        Console.WriteLine("Username:");
        string User = Console.ReadLine();
        Console.WriteLine("Password:");
        string Pass = Console.ReadLine();
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