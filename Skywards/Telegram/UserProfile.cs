using Telegram.Bot;
using Telegram.Bot.Types;
using System;
using System.Linq;

using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
namespace CashFlowTelegramBot.Skywards.Telegram;


public class UserProfile
{
    public int id { get; set; }
    public string? username { get; set; } = null;
    public int? refId { get; set; }
    public string? invitedBy { get; set; } = null;
    public string? lang { get; set; } = null;
    public int? table_id { get; set; } = null;

    public string level_tableType { get; set; }
    public string tableRole { get; set; }

    public int invited = 0;

    public UserProfile() {}

    public UserProfile(long id)
    {
        this.id = (int)id;
    }

    public UserProfile(long id, string username)
    {
        this.id = (int)id;
        this.username = username;
    }
    public UserProfile(long id, int refId, string username)
    {
        this.id = (int)id;
        this.refId = refId;
        this.username = username;
    }
    public UserProfile(long id, string username, string lang)
    {
        this.id = (int)id;
        this.username = username;
        this.lang = lang;
    }
    public UserProfile(long id, int refId, string username, string lang)
    {
        this.id = (int)id;
        this.refId = refId;
        this.username = username;
        this.lang = lang;
    }

    public UserProfile(int id, int table_id)
    {
        this.id = id;
        this.table_id = table_id;
    }
    public void AddLang(string lang)
    {
        this.lang = lang;
    }

    public void AddTableData(Table.TableType tableType, Table.TableRole tableRole)
    {
        this.tableRole = tableRole.ToString();
        this.level_tableType = tableType.ToString();
    }
    public void PrintUserProfile()
    {
        Console.WriteLine("\nID: " + id + "\nUsername: " + username + "\nRefId: " + refId + "\nInvitedBy: "+ invitedBy + "\nLang: " + lang +
                          "\nTable_ID: " + table_id + "\nlevel_tableType: " + level_tableType + "\ntableRole: " + tableRole + "\nInvited: " + invited);
    }
}
    
