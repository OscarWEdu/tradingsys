namespace TradeSys;

class Transaction
{
    private bool Request = true;
    private DateTime TransDate;
    private Item ItemSent;
    private Item ItemRecieved;

    public Transaction(Item itemSent)
    {
        ItemSent = itemSent;
    }

    public void CompleteTransaction(Item itemRecieved)
    {
        Request = false;
        ItemRecieved = itemRecieved;
    }
}