using Telegram.Bot;

namespace CashFlowTelegramBot.Skywards.Telegram;

public class UserProfile
{
    public int id { get; set; }
    public string? username { get; set; }
    public int? refId { get; set; }
    public string? invitedBy { get; set; }
    public string? lang { get; set; }

    public bool verf { get; set; }
    public UserTableList? UserTableList { get; set; }

    public Table.TableType level_tableType { get; set; }

    //public string tableRole { get; set; }
    public int invited = 0;
    public int team = 0;
    public int giftsReceived = 0;

    public UserProfile()
    {
        UserTableList = new UserTableList();
    }

    public UserProfile(long id)
    {
        this.id = (int) id;
    }

    public UserProfile(long id, string username)
    {
        this.id = (int) id;
        this.username = username;
    }
    public UserProfile(long id, Table.TableType tableType)
    {
        this.id = (int) id;
        this.level_tableType = tableType;
    }

    public UserProfile(long id, int refId, string username)
    {
        this.id = (int) id;
        this.refId = refId;
        this.username = username;
    }

    public UserProfile(long id, string username, string lang)
    {
        this.id = (int) id;
        this.username = username;
        this.lang = lang;
    }

    public UserProfile(long id, int refId, string username, string lang)
    {
        this.id = (int) id;
        this.refId = refId;
        this.username = username;
        this.lang = lang;
    }

    public void AddLang(string lang)
    {
        this.lang = lang;
    }

    public string GetTableRole(string lang, Table.TableType tableType)
    {
        var result = "";
        switch (lang)
        {
            case "ru":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        switch (UserTableList.copperTableRole)
                        {
                            case Table.TableRole.giver:
                                result = "🎁 Даритель";
                                break;
                            case Table.TableRole.manager:
                                result = "👤 Менеджер";
                                break;
                            case Table.TableRole.banker:
                                result = "🏦 Банкир";
                                break;
                            default:
                                result = "empty";
                                break;
                        }

                        break;
                    case Table.TableType.bronze:
                        switch (UserTableList.bronzeTableRole)
                        {
                            case Table.TableRole.giver:
                                result = "🎁 Даритель";
                                break;
                            case Table.TableRole.manager:
                                result = "👤 Менеджер";
                                break;
                            case Table.TableRole.banker:
                                result = "🏦 Банкир";
                                break;
                            default:
                                result = "empty";
                                break;
                        }

                        break;
                    case Table.TableType.silver:
                        switch (UserTableList.silverTableRole)
                        {
                            case Table.TableRole.giver:
                                result = "🎁 Даритель";
                                break;
                            case Table.TableRole.manager:
                                result = "👤 Менеджер";
                                break;
                            case Table.TableRole.banker:
                                result = "🏦 Банкир";
                                break;
                            default:
                                result = "empty";
                                break;
                        }

                        break;
                    case Table.TableType.gold:
                        switch (UserTableList.goldTableRole)
                        {
                            case Table.TableRole.giver:
                                result = "🎁 Даритель";
                                break;
                            case Table.TableRole.manager:
                                result = "👤 Менеджер";
                                break;
                            case Table.TableRole.banker:
                                result = "🏦 Банкир";
                                break;
                            default:
                                result = "empty";
                                break;
                        }

                        break;
                    case Table.TableType.platinum:
                        switch (UserTableList.platinumTableRole)
                        {
                            case Table.TableRole.giver:
                                result = "🎁 Даритель";
                                break;
                            case Table.TableRole.manager:
                                result = "👤 Менеджер";
                                break;
                            case Table.TableRole.banker:
                                result = "🏦 Банкир";
                                break;
                            default:
                                result = "empty";
                                break;
                        }

                        break;
                    case Table.TableType.diamond:
                        switch (UserTableList.diamondTableRole)
                        {
                            case Table.TableRole.giver:
                                result = "🎁 Даритель";
                                break;
                            case Table.TableRole.manager:
                                result = "👤 Менеджер";
                                break;
                            case Table.TableRole.banker:
                                result = "🏦 Банкир";
                                break;
                            default:
                                result = "empty";
                                break;
                        }

                        break;
                }


                break;
            case "eng":
                switch (UserTableList.copperTableRole)
                {
                    case Table.TableRole.giver:
                        result = "🎁 Giver";
                        break;
                    case Table.TableRole.manager:
                        result = "👤 Manager";
                        break;
                    case Table.TableRole.banker:
                        result = "🏦 Banker";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "fr":
                switch (UserTableList.copperTableRole)
                {
                    case Table.TableRole.giver:
                        result = "🎁 Donateur";
                        break;
                    case Table.TableRole.manager:
                        result = "👤 Gestionnaire";
                        break;
                    case Table.TableRole.banker:
                        result = "🏦 Banquier";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "de":
                switch (UserTableList.copperTableRole)
                {
                    case Table.TableRole.giver:
                        result = "🎁 Geber";
                        break;
                    case Table.TableRole.manager:
                        result = "👤 Manager";
                        break;
                    case Table.TableRole.banker:
                        result = "🏦 Banker";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
        }

        return result;
    }

    public string GetTableRole(Table.TableRole tableRole)
    {
        var result = "";
        switch (lang)
        {
            case "ru":
                switch (tableRole)
                {
                    case Table.TableRole.giver:
                        result = "🎁 Даритель";
                        break;
                    case Table.TableRole.manager:
                        result = "👤 Менеджер";
                        break;
                    case Table.TableRole.banker:
                        result = "🏦 Банкир";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "eng":
                switch (tableRole)
                {
                    case Table.TableRole.giver:
                        result = "🎁 Giver";
                        break;
                    case Table.TableRole.manager:
                        result = "👤 Manager";
                        break;
                    case Table.TableRole.banker:
                        result = "🏦 Banker";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "fr":
                switch (tableRole)
                {
                    case Table.TableRole.giver:
                        result = "🎁 Donateur";
                        break;
                    case Table.TableRole.manager:
                        result = "👤 Gestionnaire";
                        break;
                    case Table.TableRole.banker:
                        result = "🏦 Banquier";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "de":
                switch (tableRole)
                {
                    case Table.TableRole.giver:
                        result = "🎁 Geber";
                        break;
                    case Table.TableRole.manager:
                        result = "👤 Manager";
                        break;
                    case Table.TableRole.banker:
                        result = "🏦 Banker";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
        }

        return result;
    }

    public string UserInfo(ITelegramBotClient botClient, string lang, TableProfile tableData, bool IsItYou)
    {
        var result = "";
        string? firstName = "";
        string? lastName = "";
        try
        {
            firstName = botClient.GetChatAsync(id).Result.FirstName;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        try
        {
            lastName = botClient.GetChatAsync(id).Result.LastName;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        switch (lang)
        {
            case "ru":
                result = $"<b>Роль: {GetTableRole(lang, Table.TableType.copper)}</b>";
                if (IsItYou) result += " 🔘";
                result += $"\n<b>Ник:</b> @{username}" +
                          $"\n<b>Имя пользователя:</b> {firstName + lastName}" + 
                          $"\n<b>Лично приглашённых:</b> {invited}" +
                          $"\n<b>Пригласил:</b> @{invitedBy}\n\n";
                break;
            case "eng":
                result = $"<b>Role: {GetTableRole(lang, Table.TableType.copper)}</b>";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Nickname:</b> @{username}" +
                          $"\n<b>Username:</b> {firstName + lastName}" + 
                          $"\n<b>Personally invited:</b> {invited}" +
                          $"\n<b>Invited:</b> @{invitedBy}\n\n";
                break;
            case "fr":
                result = $"<b>Rôle: {GetTableRole(lang, Table.TableType.copper)}</b>";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Pseudonyme: @{username}</b>" +
                          $"\n<b>Nom d'utilisateur:</b> {firstName + lastName}" + 
                          $"\n<b>Personnellement invité:</b> {invited}" +
                          $"\n<b>Invité:</b> @{invitedBy}\n\n";
                break;
            case "de":
                result = $"<b>Rolle: {GetTableRole(lang, Table.TableType.copper)}</b>";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Spitzname:</b> @{username}" +
                          $"\n<b>Benutzername:</b> {firstName + lastName}" + 
                          $"\n<b>Persönlich eingeladen:</b> {invited}" +
                          $"\n<b>Eingeladen:</b> @{invitedBy}\n\n";
                break;
            default:
                result = "empty";
                break;
        }

        return result;
    }

    public string UserInfo(ITelegramBotClient botClient, string lang, TableProfile tableData, bool IsItYou, bool Verf,
        int num)
    {
        var result = "";
        string? firstName = "";
        string? lastName = "";
        try
        {
            firstName = botClient.GetChatAsync(id).Result.FirstName;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        try
        {
            lastName = botClient.GetChatAsync(id).Result.LastName;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        switch (lang)
        {
            case "ru":
                result = $"<b>Роль: {GetTableRole(lang, Table.TableType.copper)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Ник:</b> @{username}" +
                          $"\n<b>Имя пользователя:</b> {firstName + lastName}" +
                          $"\n<b>Лично приглашённых:</b> {invited}" +
                          $"\n<b>Пригласил:</b> @{invitedBy}\n\n";

                break;
            case "eng":
                result = $"<b>Role: {GetTableRole(lang, Table.TableType.copper)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Nickname:</b> @{username}" +
                          $"\n<b>Username:</b> {firstName + lastName}" + 
                          $"\n<b>Personally invited:</b> {invited}" +
                          $"\n<b>Invited:</b> @{invitedBy}\n\n";
                break;
            case "fr":
                result = $"<b>Rôle: {GetTableRole(lang, Table.TableType.copper)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Pseudonyme: @{username}</b>" +
                          $"\n<b>Nom d'utilisateur:</b> {firstName + lastName}" + 
                          $"\n<b>Personnellement invité:</b> {invited}" +
                          $"\n<b>Invité:</b> @{invitedBy}\n\n";
                break;
            case "de":
                result = $"<b>Rolle: {GetTableRole(lang, Table.TableType.copper)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Spitzname:</b> @{username}" +
                          $"\n<b>Benutzername:</b> {firstName + lastName}" + 
                          $"\n<b>Persönlich eingeladen:</b> {invited}" +
                          $"\n<b>Eingeladen:</b> @{invitedBy}\n\n";
                break;
            default:
                result = "empty";
                break;
        }

        return result;
    }

    public string UserInfo(Table.TableRole tableRole)
    {
        var result = "";
        switch (lang)
        {
            case "ru":
                result = $"\n<b>Роль: {GetTableRole(tableRole)}</b>" +
                         "\n<b>Место вакантно...</b>\n\n";
                break;
            case "eng":
                result = $"<b>Role: {GetTableRole(tableRole)}</b>" +
                         "\n<b>Place is vacant...</b>";
                break;
            case "fr":
                result = $"<b>Rôle: {GetTableRole(tableRole)}</b>" +
                         "\n<b>La place est vacante...</b>";
                break;
            case "de":
                result = $"<b>Rolle: {GetTableRole(tableRole)}</b>" +
                         "\n<b>Platz ist frei...</b>";
                break;
            default:
                result = "empty";
                break;
        }

        return result;
    }

    public void PrintUserProfile()
    {
        Console.WriteLine("\n---------------------------------------------------------------" +
                          "\n--------------------------UserProfile--------------------------" +
                          "\n---------------------------------------------------------------");
        Console.WriteLine("\nID: " + id + "\nUsername: " + username + "\nRefId: " + refId + "\nInvitedBy: " +
                          invitedBy + "\nLang: " + lang + "\nlevel_tableType: " + level_tableType + "\ntableRole: " +
                          "\nInvited: " + invited);
    }
}