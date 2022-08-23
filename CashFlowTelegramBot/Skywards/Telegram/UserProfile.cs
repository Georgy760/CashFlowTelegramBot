namespace CashFlowTelegramBot.Skywards.Telegram;

public class UserProfile
{
    public int invited = 0;

    public UserProfile()
    {
    }

    public UserProfile(long id)
    {
        this.id = (int) id;
    }

    public UserProfile(Table.TableRole tableRole)
    {
        this.tableRole = tableRole.ToString();
    }

    public UserProfile(long id, string username)
    {
        this.id = (int) id;
        this.username = username;
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

    public UserProfile(int id, int table_id)
    {
        this.id = id;
        this.table_id = table_id;
    }

    public int id { get; set; }
    public string? username { get; set; }
    public int? refId { get; set; }
    public string? invitedBy { get; set; } = null;
    public string? lang { get; set; }
    public int? table_id { get; set; }

    public string level_tableType { get; set; }
    public string tableRole { get; set; }

    public void AddLang(string lang)
    {
        this.lang = lang;
    }

    public void AddTableData(Table.TableType tableType, Table.TableRole tableRole)
    {
        this.tableRole = tableRole.ToString();
        level_tableType = tableType.ToString();
    }

    public string GetTableType()
    {
        var result = "";
        switch (lang)
        {
            case "ru":
                switch (level_tableType)
                {
                    case "copper":
                        result = "🎗 Медный";
                        break;
                    case "bronze":
                        result = "🥉 Бронзовый";
                        break;
                    case "silver":
                        result = "🥈 Серебрянный";
                        break;
                    case "gold":
                        result = "🥇 Золотой";
                        break;
                    case "platinum":
                        result = "🎖 Платиновый";
                        break;
                    case "diamond":
                        result = "💎 Алмазный";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "eng":
                switch (level_tableType)
                {
                    case "copper":
                        result = "🎗 Copper";
                        break;
                    case "bronze":
                        result = "🥉 Bronze";
                        break;
                    case "silver":
                        result = "🥈 Silver";
                        break;
                    case "gold":
                        result = "🥇 Gold";
                        break;
                    case "platinum":
                        result = "🎖 Platinum";
                        break;
                    case "diamond":
                        result = "💎 Diamond";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "fr":
                switch (level_tableType)
                {
                    case "copper":
                        result = "🎗 Cuivre";
                        break;
                    case "bronze":
                        result = "🥉 bronze";
                        break;
                    case "silver":
                        result = "🥈 Argent";
                        break;
                    case "gold":
                        result = "🥇 Doré";
                        break;
                    case "platinum":
                        result = "🎖 Platine";
                        break;
                    case "diamond":
                        result = "💎 Diamant";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "de":
                switch (level_tableType)
                {
                    case "copper":
                        result = "🎗 Kupfer";
                        break;
                    case "bronze":
                        result = "🥉 Bronze";
                        break;
                    case "silver":
                        result = "🥈 Silberner";
                        break;
                    case "gold":
                        result = "🥇 Goldener";
                        break;
                    case "platinum":
                        result = "🎖 Platin";
                        break;
                    case "diamond":
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

    public string GetTableRole(string lang)
    {
        var result = "";
        switch (lang)
        {
            case "ru":
                switch (tableRole)
                {
                    case "giver":
                        result = "🎁 Даритель";
                        break;
                    case "manager":
                        result = "👤 Менеджер";
                        break;
                    case "banker":
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
                    case "giver":
                        result = "🎁 Giver";
                        break;
                    case "manager":
                        result = "👤 Manager";
                        break;
                    case "banker":
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
                    case "giver":
                        result = "🎁 Donateur";
                        break;
                    case "manager":
                        result = "👤 Gestionnaire";
                        break;
                    case "banker":
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
                    case "giver":
                        result = "🎁 Geber";
                        break;
                    case "manager":
                        result = "👤 Manager";
                        break;
                    case "banker":
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

    public string UserInfo(string lang, bool IsItYou)
    {
        var result = "";
        switch (lang)
        {
            case "ru":
                result = $"<b>Роль: {GetTableRole(lang)}</b>";
                if (IsItYou) result += " 🔘";
                result += $"\n<b>Ник:</b> @{username}" +
                          $"\n<b>Имя пользователя:</b> " + //TODO Add to db field with first & last names
                          $"\n<b>Лично приглашённых:</b> {invited}" +
                          $"\n<b>Пригласил:</b> @{invitedBy}\n\n";
                break;
            case "eng":
                result = $"<b>Role: {GetTableRole(lang)}</b>";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Nickname:</b> @{username}" +
                          $"\n<b>Username:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                          $"\n<b>Personally invited:</b> {invited}" +
                          $"\n<b>Invited:</b> @{invitedBy}\n\n";
                break;
            case "fr":
                result = $"<b>Rôle: {GetTableRole(lang)}</b>";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Pseudonyme: @{username}</b>" +
                          $"\n<b>Nom d'utilisateur:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                          $"\n<b>Personnellement invité:</b> {invited}" +
                          $"\n<b>Invité:</b> @{invitedBy}\n\n";
                break;
            case "de":
                result = $"<b>Rolle: {GetTableRole(lang)}</b>";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Spitzname:</b> @{username}" +
                          $"\n<b>Benutzername:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                          $"\n<b>Persönlich eingeladen:</b> {invited}" +
                          $"\n<b>Eingeladen:</b> @{invitedBy}\n\n";
                break;
            default:
                result = "empty";
                break;
        }

        return result;
    }
    public string UserInfo(string lang, bool IsItYou, bool Verf, int num)
    {
        var result = "";
        switch (lang)
        {
            case "ru":
                result = $"<b>Роль: {GetTableRole(lang)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Ник:</b> @{username}" +
                          $"\n<b>Имя пользователя:</b> " + //TODO Add to db field with first & last names
                          $"\n<b>Лично приглашённых:</b> {invited}" +
                          $"\n<b>Пригласил:</b> @{invitedBy}\n\n";
                          
                break;
            case "eng":
                result = $"<b>Role: {GetTableRole(lang)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Nickname:</b> @{username}" +
                          $"\n<b>Username:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                          $"\n<b>Personally invited:</b> {invited}" +
                          $"\n<b>Invited:</b> @{invitedBy}\n\n";
                break;
            case "fr":
                result = $"<b>Rôle: {GetTableRole(lang)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Pseudonyme: @{username}</b>" +
                          $"\n<b>Nom d'utilisateur:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                          $"\n<b>Personnellement invité:</b> {invited}" +
                          $"\n<b>Invité:</b> @{invitedBy}\n\n";
                break;
            case "de":
                result = $"<b>Rolle: {GetTableRole(lang)}-{num}</b>";
                if (Verf) result += " ✅";
                else result += " ❌";
                if (IsItYou) result += "🔘";
                result += $"\n<b>Spitzname:</b> @{username}" +
                          $"\n<b>Benutzername:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
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
        var user = new UserProfile(tableRole);
        switch (lang)
        {
            case "ru":
                result = $"<b>Роль: {user.GetTableRole(lang)}</b>" +
                         "\n<b>Место вакантно...</b>";
                break;
            case "eng":
                result = $"<b>Role: {user.GetTableRole(lang)}</b>" +
                         "\n<b>Place is vacant...</b>";
                break;
            case "fr":
                result = $"<b>Rôle: {user.GetTableRole(lang)}</b>" +
                         "\n<b>La place est vacante...</b>";
                break;
            case "de":
                result = $"<b>Rolle: {user.GetTableRole(lang)}</b>" +
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
        Console.WriteLine("\nID: " + id + "\nUsername: " + username + "\nRefId: " + refId + "\nInvitedBy: " +
                          invitedBy + "\nLang: " + lang +
                          "\nTable_ID: " + table_id + "\nlevel_tableType: " + level_tableType + "\ntableRole: " +
                          tableRole + "\nInvited: " + invited);
    }
}