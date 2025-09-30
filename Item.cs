namespace TradeSys;

class Item
{
    private string Name;
    private string Description;
    private string Owner;

    public Item(string name, string description, string owner)
    {
        Name = name;
        Description = description;
        Owner = owner;
    }
    public string[] GetFields()
    {
        string[] Fields = new string[2];
        Fields[0] = Name;
        Fields[1] = Description;
        Fields[2] = Owner;

        return Fields;
    }
}