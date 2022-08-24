using CashFlowTelegramBot.Skywards.Web;

namespace CashFlowTelegramBot.Skywards.Telegram;

public class UserTableList
{
    public int id { get; set; }
    public int userID { get; set; }
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
}