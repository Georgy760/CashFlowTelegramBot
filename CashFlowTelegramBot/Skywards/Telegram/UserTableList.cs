using System.Diagnostics;
using CashFlowTelegramBot.Skywards.Web;

namespace CashFlowTelegramBot.Skywards.Telegram;

public class UserTableList
{
    public int id { get; set; }
    public long userID { get; set; }
    public int? table_ID_copper = null;
    public Table.TableRole? copperTableRole;
    public int? table_ID_bronze = null;
    public Table.TableRole? bronzeTableRole;
    public int? table_ID_silver = null;
    public Table.TableRole? silverTableRole;
    public int? table_ID_gold = null;
    public Table.TableRole? goldTableRole;
    public int? table_ID_platinum = null;
    public Table.TableRole? platinumTableRole;
    public int? table_ID_diamond = null;
    public Table.TableRole? diamondTableRole;
    public UserTableList(){}

    public void PrintUserTableList()
    {
        Trace.Write("\n-----------------------------------------------------------------" + 
                          "\n--------------------------UserTableList--------------------------" +
                          "\n-----------------------------------------------------------------");
        Trace.Write("\nID: " + id + "\nuserID: " + userID + "\ntable_ID_copper: " + table_ID_copper + "\ncopperTableRole: " +
                          copperTableRole + "\ntable_ID_bronze: " + table_ID_bronze + "\nbronzeTableRole: " + bronzeTableRole + "\ntable_ID_silver: " + table_ID_silver +
                          "\nsilverTableRole: " + silverTableRole + "\ntable_ID_gold: " + table_ID_gold + "\ngoldTableRole: " + goldTableRole + "\ntable_ID_platinum: " + table_ID_platinum
                          + "\nplatinumTableRole: " + platinumTableRole + "\ntable_ID_diamond: " + table_ID_diamond);
    }
}