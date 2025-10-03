namespace TradeSys;

class Transaction
{
    private bool Pending = true;
    private DateTime TransDate = new DateTime();
    public readonly List<Item> ItemsSent = new List<Item>();
    public readonly List<Item> ItemsRecieved = new List<Item>();

    //Sets a Transaction up as pending
    public Transaction(List<Item> itemsSent, List<Item> itemsRecieved)
    {
        ItemsSent = itemsSent;
        ItemsRecieved = itemsRecieved;
        TransDate = DateTime.Now;
    }

    //Sets the Transaction as completed and updates TransDate to show when it was completed rather than when the Transaction was sent
    public void CompleteTransaction()
    {
        Pending = false;
        TransDate = DateTime.Now;
    }

    public void LoadTransactionData(bool pending, DateTime transDate)
    {
        Pending = pending;
        TransDate = transDate;
    }

    //Outputs all variables as a string array, including the variables of the Item objects
    public List<string> WriteAsString()
    {
        List<string> Output = new List<string>();
        Output.Add(Pending.ToString());
        Output.Add(TransDate.ToString());
        Output.Add(ItemsSent.Count().ToString());
        Output.Add(ItemsRecieved.Count().ToString());
        foreach (Item item in ItemsSent) { if (item != null) { Output.AddRange(item.GetFields()); } }
        foreach (Item item in ItemsRecieved) { if (item != null) { Output.AddRange(item.GetFields()); } }

        return Output;
    }

    public void Print()
    {
        if (Pending) { PrintPending(); }
        else { PrintCompleted(); }
    }

    //Prints the output of WriteAsString in a legible format, formatted for completed transactions
    public void PrintCompleted()
    {
        List<string> DataList = WriteAsString();
        string Output = "Transaction:\n";
        Output += "From: " + DataList[6]; //UserSent
        Output += " to: " + DataList[9]; //UserRecieved
        Output += "\nAt date: " + DataList[1]; //TransDate
        Output += "\nTraded: " + DataList[4] + " for " + DataList[7] + "\n"; //ItemSent / Itemrecieved .Name
        Console.WriteLine(Output);
    }

    //Prints the output of WriteAsString in a legible format, formatted for pending transactions
    public void PrintPending()
    {
        int ItemVariables = 3; //The number of variables contained in the Item object

        List<string> DataList = WriteAsString();
        string Output = "Pending Transaction:\n";
        Output += "User: " + DataList[6]; //UserSent
        Output += " wants to trade: ";
        for (int i = 0; i < int.Parse(DataList[3]); i++) { Output += DataList[4 + int.Parse(DataList[2]) * ItemVariables + i * ItemVariables] + " "; }  //ItemRecieved.Name
        Output += " for: ";
        for (int i = 0; i < int.Parse(DataList[2]); i++) { Output += DataList[4 + i * ItemVariables] + " "; }  //ItemSent.Name
        Output += "\nThe request was sent at: " + DataList[1] + "\n"; //TransDate
        Console.WriteLine(Output);
    }

    public bool IsPending() { return Pending; }

    //Returns whether the provided User owns the recieved Item
    public bool IsRecipient(string Username)
    {
        if (ItemsRecieved[0].MatchOwned(Username))
        {
            return true;
        }
        return false;
    }
}