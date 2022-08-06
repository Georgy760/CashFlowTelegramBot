namespace CashFlowTelegramBot.Skywards.Telegram;

public class Table
{
    public int tableID { get; set; }
    public TableType tableType { get; set; }
    public int? bankerID { get; set; } = null;
    public int? managerA_ID { get; set; } = null;
    public int? managerB_ID { get; set; } = null;
    public int? giverA_ID { get; set; } = null;
    public bool verf_A { get; set; }
    public int? giverB_ID { get; set; } = null;
    public bool verf_B { get; set; }
    public int? giverC_ID { get; set; } = null;
    public bool verf_C { get; set; }
    public int? giverD_ID { get; set; } = null;
    public bool verf_D { get; set; }
    public enum TableType
    {
        copper,
        bronze,
        silver,
        gold,
        platinum,
        diamond
    }
    public enum TableRole
    {
        giver,
        manager,
        banker
    }
}

public class TableProfile : Table
{
    private TableType TableType { get; set; }
    private TableRole TableRole { get; set; }
    
    public TableProfile(){}
    public TableProfile(UserProfile userProfile)
    {
        this.TableType = TableType;
        this.TableRole = TableRole;
    }
    
    public void PrintTableProfile()
    {
        Console.WriteLine("\ntableID: " + tableID + "\nTableType: " + tableType + "\nBankerID: " + bankerID +
                          "\nManagerA_ID: " + managerA_ID + "\nManagerB_ID: " + managerB_ID +
                          "\nGiverA_ID: " + giverA_ID + "\nVerfA_ID: " + verf_A +
                          "\nGiverA_ID: " + giverB_ID + "\nVerfA_ID: " + verf_B +
                          "\nGiverA_ID: " + giverC_ID + "\nVerfA_ID: " + verf_C +
                          "\nGiverA_ID: " + giverD_ID + "\nVerfA_ID: " + verf_D);
    }
}
