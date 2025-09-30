namespace TradeSys;

class User
{
    private string Username;
    private string Password;

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public bool MatchName(string username)
    {
        if (Username == username)
        {
            return true;
        }
        return false;
    }
    public bool MatchPass(string password)
    {
        if (Password == password)
        {
            return true;
        }
        return false;
    }
}