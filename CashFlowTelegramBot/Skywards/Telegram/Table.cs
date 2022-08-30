using System.Diagnostics;

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
    public long? bankerID { get; set; } = null;
    public long? managerA_ID { get; set; } = null;
    public long? giverA_ID { get; set; } = null;
    public bool verf_A { get; set; }
    public long? giverB_ID { get; set; } = null;
    public bool verf_B { get; set; }
    public long? managerB_ID { get; set; } = null;
    public long? giverC_ID { get; set; } = null;
    public bool verf_C { get; set; }
    public long? giverD_ID { get; set; } = null;
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

    public TableProfile(int tableID, long? giverA_ID, long? giverB_ID, long? giverC_ID, long? giverD_ID)
    {
        this.giverA_ID = giverA_ID;
        this.giverB_ID = giverB_ID;
        this.giverC_ID = giverC_ID;
        this.giverD_ID = giverD_ID;
        this.tableID = tableID;
    }

    public TableProfile(int tableID)
    {
        this.tableID = tableID;
    }

    public TableProfile(int? tableID)
    {
        this.tableID = (int) tableID;
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

    public static TableType GetTableType(string data)
    {
        Table.TableType tableType = Enum.Parse<TableType>(data, true);
        return tableType;
    }
    public static string GetTableType(UserProfile user, TableType tableType)
    {
        var result = "";
        switch (user.lang)
        {
            case "ru":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Медный";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Бронзовый";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Серебрянный";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Золотой";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Платиновый";
                        break;
                    case Table.TableType.diamond:
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
                    case Table.TableType.copper:
                        result = "🎗 Copper";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Silver";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Gold";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platinum";
                        break;
                    case Table.TableType.diamond:
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
                    case Table.TableType.copper:
                        result = "🎗 Cuivre";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Argent";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Doré";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platine";
                        break;
                    case Table.TableType.diamond:
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
                    case Table.TableType.copper:
                        result = "🎗 Kupfer";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Silberner";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Goldener";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platin";
                        break;
                    case Table.TableType.diamond:
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
        Trace.Write("\n-----------------------------------------------------------------------" + 
                          $"\n--------------------------TableProfile of: {tableID}--------------------------" +
                          "\n-----------------------------------------------------------------------");
        Trace.Write("\ntableID: " + tableID + "\nTableType: " + tableType + "\nBankerID: " + bankerID +
                          "\nManagerA_ID: " + managerA_ID + "\nManagerB_ID: " + managerB_ID +
                          "\nGiverA_ID: " + giverA_ID + "\nVerfA_ID: " + verf_A +
                          "\nGiverB_ID: " + giverB_ID + "\nVerfB_ID: " + verf_B +
                          "\nGiverC_ID: " + giverC_ID + "\nVerfC_ID: " + verf_C +
                          "\nGiverD_ID: " + giverD_ID + "\nVerfD_ID: " + verf_D);
    }
}