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
}