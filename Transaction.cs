namespace TradeSys;

class Transaction
{
    private bool Pending = true;
    private DateTime TransDate = new DateTime();
    public readonly Item ItemSent;
    public readonly Item ItemRecieved;
    public Transaction(Item itemSent, Item itemRecieved)
    {
        ItemSent = itemSent;
        ItemRecieved = itemRecieved;
        TransDate = DateTime.Now;
    }

    public void CompleteTransaction()
    {
        Pending = false;
        TransDate = DateTime.Now;
    }

    public List<string> WriteAsString()
    {
        List<string> Output = new List<string>();
        Output.Add(Pending.ToString());
        Output.Add(TransDate.ToString());
        Output.AddRange(ItemSent.GetFields());
        Output.AddRange(ItemRecieved.GetFields());

        return Output;
    }

    public void Print()
    {
        if (Pending) { PrintPending(); }
        else { PrintCompleted(); }
    }

    public void PrintCompleted()
    {
        List<string> DataList = WriteAsString();
        string Output = "Transaction:\n";
        Output += "From: " + DataList[4]; //UserSent
        Output += " to: " + DataList[7]; //UserRecieved
        Output += "\nAt date: " + DataList[1]; //TransDate
        Output += "\nTraded: " + DataList[2] + " for " + DataList[5] + "\n"; //ItemSent / Itemrecieved .Name
        Console.WriteLine(Output);
    }

    public void PrintPending()
    {
        List<string> DataList = WriteAsString();
        string Output = "";
        Output += "User: " + DataList[4]; //UserSent
        Output += " wants to trade: " + DataList[5]; //ItemRecieved.Name
        Output += " for: " + DataList[2]; //ItemSent.Name
        Output += "\nThe request was sent at: " + DataList[1] + "\n"; //TransDate
        Console.WriteLine(Output);
    }

    public bool IsPending() { return Pending; }

    public bool IsRecipient(string Username)
    {
        if (ItemRecieved.MatchOwned(Username))
        {
            return true;
        }
        return false;
    }
}