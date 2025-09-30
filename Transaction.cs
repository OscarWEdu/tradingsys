namespace TradeSys;

class Transaction
{
    private bool Request = true;
    private DateTime TransDate;
    private Item ItemSent;
    private Item ItemRecieved;

    public Transaction(Item itemSent, Item itemRecieved)
    {
        ItemSent = itemSent;
    }

    public void CompleteTransaction()
    {
        Request = false;
    }
}