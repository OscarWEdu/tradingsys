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
    public string GetName() { return Username; }
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

    //Returns all variables in an array
    public string[] GetFields()
    {
        string[] Fields = new string[2];
        Fields[0] = Username;
        Fields[1] = Password;

        return Fields;
    }
}