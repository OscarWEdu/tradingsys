namespace TradeSys;

class Item
{
    public readonly string Name;
    public readonly string Description;
    public string Owner { get; set; }

    public Item(string name, string description, string owner)
    {
        Name = name;
        Description = description;
        Owner = owner;
    }
    public string[] GetFields()
    {
        string[] Fields = new string[3];
        Fields[0] = Name;
        Fields[1] = Description;
        Fields[2] = Owner;

        return Fields;
    }
    public bool MatchOwned(string Username)
    {
        if (Username == Owner)
        {
            return true;
        }
        return false;
    }
}