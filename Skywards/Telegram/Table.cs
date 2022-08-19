namespace CashFlowTelegramBot.Skywards.Telegram;

public class Table
{
    public enum TableRole
    {
        giver,
        manager,
        banker
    }

    public enum TableType
    {
        copper,
        bronze,
        silver,
        gold,
        platinum,
        diamond
    }

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
}

public class TableProfile : Table
{
    public TableProfile()
    {
    }

    public TableProfile(UserProfile userProfile)
    {
        TableType = TableType;
        TableRole = TableRole;
    }

    private TableType TableType { get; }
    private TableRole TableRole { get; }

    public static string GiverVerification(bool verf)
    {
        string verfToString = null;
        if (verf)
            verfToString = "✅";
        else verfToString = "❌";
        return verfToString;
    }

    public string GetTableType(UserProfile user)
    {
        var lang = user.lang;
        var result = "";
        switch (lang)
        {
            case "ru":
                switch (tableType)
                {
                    case TableType.copper:
                        result = "🎗 Медный";
                        break;
                    case TableType.bronze:
                        result = "🥉 Бронзовый";
                        break;
                    case TableType.silver:
                        result = "🥈 Серебрянный";
                        break;
                    case TableType.gold:
                        result = "🥇 Золотой";
                        break;
                    case TableType.platinum:
                        result = "🎖 Платиновый";
                        break;
                    case TableType.diamond:
                        result = "💎 Алмазный";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "eng":
                switch (tableType)
                {
                    case TableType.copper:
                        result = "🎗 Copper";
                        break;
                    case TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case TableType.silver:
                        result = "🥈 Silver";
                        break;
                    case TableType.gold:
                        result = "🥇 Gold";
                        break;
                    case TableType.platinum:
                        result = "🎖 Platinum";
                        break;
                    case TableType.diamond:
                        result = "💎 Diamond";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "fr":
                switch (tableType)
                {
                    case TableType.copper:
                        result = "🎗 Cuivre";
                        break;
                    case TableType.bronze:
                        result = "🥉 bronze";
                        break;
                    case TableType.silver:
                        result = "🥈 Argent";
                        break;
                    case TableType.gold:
                        result = "🥇 Doré";
                        break;
                    case TableType.platinum:
                        result = "🎖 Platine";
                        break;
                    case TableType.diamond:
                        result = "💎 Diamant";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "de":
                switch (tableType)
                {
                    case TableType.copper:
                        result = "🎗 Kupfer";
                        break;
                    case TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case TableType.silver:
                        result = "🥈 Silberner";
                        break;
                    case TableType.gold:
                        result = "🥇 Goldener";
                        break;
                    case TableType.platinum:
                        result = "🎖 Platin";
                        break;
                    case TableType.diamond:
                        result = "💎 Diamant";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
        }

        return result;
    }

    public void PrintTableProfile()
    {
        Console.WriteLine("\ntableID: " + tableID + "\nTableType: " + tableType + "\nBankerID: " + bankerID +
                          "\nManagerA_ID: " + managerA_ID + "\nManagerB_ID: " + managerB_ID +
                          "\nGiverA_ID: " + giverA_ID + "\nVerfA_ID: " + verf_A +
                          "\nGiverB_ID: " + giverB_ID + "\nVerfB_ID: " + verf_B +
                          "\nGiverC_ID: " + giverC_ID + "\nVerfC_ID: " + verf_C +
                          "\nGiverD_ID: " + giverD_ID + "\nVerfD_ID: " + verf_D);
    }
}