using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace CashFlowTelegramBot.Skywards.Telegram;

public partial class Languages
{
    public abstract class Tables
    {
        //------RU------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerRU = InlineKeyboardButton.WithCallbackData("Связаться с банкиром", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableRU = InlineKeyboardButton.WithCallbackData("Назад", "ChooseTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableRU = InlineKeyboardButton.WithCallbackData("Выйти со стола", "LeaveTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataRU = InlineKeyboardButton.WithCallbackData("Банкир", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataRU = InlineKeyboardButton.WithCallbackData("Менеджер-1", "GetManagerAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataRU = InlineKeyboardButton.WithCallbackData("Менеджер-2", "GetManagerBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataRU = InlineKeyboardButton.WithCallbackData("Даритель-1", "GetGiverAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataRU = InlineKeyboardButton.WithCallbackData("Даритель-2", "GetGiverBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataRU = InlineKeyboardButton.WithCallbackData("Даритель-3", "GetGiverDData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataRU = InlineKeyboardButton.WithCallbackData("Даритель-4", "GetGiverCData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamRU = InlineKeyboardButton.WithCallbackData("Показать команду списком", "ShowListTeam");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerARU = InlineKeyboardButton.WithCallbackData("Удалить со стола", "RemoveFromTableManagerA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerBRU = InlineKeyboardButton.WithCallbackData("Удалить со стола", "RemoveFromTableManagerB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverARU = InlineKeyboardButton.WithCallbackData("Удалить со стола", "RemoveFromTableGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBRU = InlineKeyboardButton.WithCallbackData("Удалить со стола", "RemoveFromTableGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCRU = InlineKeyboardButton.WithCallbackData("Удалить со стола", "RemoveFromTableGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDRU = InlineKeyboardButton.WithCallbackData("Удалить со стола", "RemoveFromTableGiverD");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverARU = InlineKeyboardButton.WithCallbackData("Подтвердить", "VerfGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBRU = InlineKeyboardButton.WithCallbackData("Подтвердить", "VerfGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCRU = InlineKeyboardButton.WithCallbackData("Подтвердить", "VerfGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDRU = InlineKeyboardButton.WithCallbackData("Подтвердить", "VerfGiverD");
        //------ENG------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableENG = InlineKeyboardButton.WithCallbackData("Back", "ChooseTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerENG = InlineKeyboardButton.WithCallbackData("Contact a banker", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableENG = InlineKeyboardButton.WithCallbackData("Exit the table", "LeaveTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataENG = InlineKeyboardButton.WithCallbackData("Banker", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataENG = InlineKeyboardButton.WithCallbackData("Manager-1", "GetManagerAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataENG = InlineKeyboardButton.WithCallbackData("Manager-2", "GetManagerBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataENG = InlineKeyboardButton.WithCallbackData("Giver-1", "GetGiverAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataENG = InlineKeyboardButton.WithCallbackData("Giver-2", "GetGiverBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataENG = InlineKeyboardButton.WithCallbackData("Giver-3", "GetGiverDData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataENG = InlineKeyboardButton.WithCallbackData("Giver-4", "GetGiverCData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamENG = InlineKeyboardButton.WithCallbackData("Show command in list", "ShowListTeam");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerAENG = InlineKeyboardButton.WithCallbackData("Delete from the table", "RemoveFromTableManagerA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerBENG = InlineKeyboardButton.WithCallbackData("Delete from the table", "RemoveFromTableManagerB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverAENG = InlineKeyboardButton.WithCallbackData("Delete from the table", "RemoveFromTableGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBENG = InlineKeyboardButton.WithCallbackData("Delete from the table", "RemoveFromTableGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCENG = InlineKeyboardButton.WithCallbackData("Delete from the table", "RemoveFromTableGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDENG = InlineKeyboardButton.WithCallbackData("Delete from the table", "RemoveFromTableGiverD");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverAENG = InlineKeyboardButton.WithCallbackData("Confirm", "VerfGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBENG = InlineKeyboardButton.WithCallbackData("Confirm", "VerfGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCENG = InlineKeyboardButton.WithCallbackData("Confirm", "VerfGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDENG = InlineKeyboardButton.WithCallbackData("Confirm", "VerfGiverD");
        //------FR------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableFR = InlineKeyboardButton.WithCallbackData("Retour", "ChooseTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerFR = InlineKeyboardButton.WithCallbackData("Contactez un banquier", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableFR = InlineKeyboardButton.WithCallbackData("Sortir du tableau", "LeaveTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataFR = InlineKeyboardButton.WithCallbackData("Banquier", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataFR = InlineKeyboardButton.WithCallbackData("Gestionnaire-1", "GetManagerAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataFR = InlineKeyboardButton.WithCallbackData("Gestionnaire-2", "GetManagerBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataFR = InlineKeyboardButton.WithCallbackData("Donateur-1", "GetGiverAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataFR = InlineKeyboardButton.WithCallbackData("Donateur-2", "GetGiverBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataFR = InlineKeyboardButton.WithCallbackData("Donateur-3", "GetGiverDData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataFR = InlineKeyboardButton.WithCallbackData("Donateur-4", "GetGiverCData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamFR = InlineKeyboardButton.WithCallbackData("Afficher la commande dans la liste", "ShowListTeam");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerAFR = InlineKeyboardButton.WithCallbackData("Supprimer du tableau", "RemoveFromTableManagerA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerBFR = InlineKeyboardButton.WithCallbackData("Supprimer du tableau", "RemoveFromTableManagerB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverAFR = InlineKeyboardButton.WithCallbackData("Supprimer du tableau", "RemoveFromTableGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBFR = InlineKeyboardButton.WithCallbackData("Supprimer du tableau", "RemoveFromTableGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCFR = InlineKeyboardButton.WithCallbackData("Supprimer du tableau", "RemoveFromTableGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDFR = InlineKeyboardButton.WithCallbackData("Supprimer du tableau", "RemoveFromTableGiverD");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverAFR = InlineKeyboardButton.WithCallbackData("Confirmer", "VerfGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBFR = InlineKeyboardButton.WithCallbackData("Confirmer", "VerfGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCFR = InlineKeyboardButton.WithCallbackData("Confirmer", "VerfGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDFR = InlineKeyboardButton.WithCallbackData("Confirmer", "VerfGiverD");
        //------DE------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableDE = InlineKeyboardButton.WithCallbackData("Der Rücken", "ChooseTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerDE = InlineKeyboardButton.WithCallbackData("Sie sich an einen Banker", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableDE = InlineKeyboardButton.WithCallbackData("Der Rücken Verlasse den Tisch", "LeaveTable");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataDE = InlineKeyboardButton.WithCallbackData("Banker", "GetBankerData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataDE = InlineKeyboardButton.WithCallbackData("Manager-1", "GetManagerAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataDE = InlineKeyboardButton.WithCallbackData("Manager-2", "GetManagerBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataDE = InlineKeyboardButton.WithCallbackData("Geber-1", "GetGiverAData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataDE = InlineKeyboardButton.WithCallbackData("Geber-2", "GetGiverBData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataDE = InlineKeyboardButton.WithCallbackData("Geber-3", "GetGiverDData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataDE = InlineKeyboardButton.WithCallbackData("Geber-4", "GetGiverCData");
        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamDE = InlineKeyboardButton.WithCallbackData("Befehl in Liste anzeigen", "ShowListTeam");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerADE = InlineKeyboardButton.WithCallbackData("Aus der Tabelle löschen", "RemoveFromTableManagerA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableManagerBDE = InlineKeyboardButton.WithCallbackData("Aus der Tabelle löschen", "RemoveFromTableManagerB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverADE = InlineKeyboardButton.WithCallbackData("Aus der Tabelle löschen", "RemoveFromTableGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBDE = InlineKeyboardButton.WithCallbackData("Aus der Tabelle löschen", "RemoveFromTableGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCDE = InlineKeyboardButton.WithCallbackData("Aus der Tabelle löschen", "RemoveFromTableGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDDE = InlineKeyboardButton.WithCallbackData("Aus der Tabelle löschen", "RemoveFromTableGiverD");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverADE = InlineKeyboardButton.WithCallbackData("Bestätigen", "VerfGiverA");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBDE = InlineKeyboardButton.WithCallbackData("Bestätigen", "VerfGiverB");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCDE = InlineKeyboardButton.WithCallbackData("Bestätigen", "VerfGiverC");
        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDDE = InlineKeyboardButton.WithCallbackData("Bestätigen", "VerfGiverD");
        public static async void Giver(ITelegramBotClient botClient, long chatId, Table.TableType tableType, UserData userData)
        {
            string typeOfTable = "typeOfTable";
            int giftSum = 0;
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType.ToString() + ".png";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType.ToString() + ".png";
            }

            InlineKeyboardMarkup? inlineKeyboard;
            switch (userData.playerData.lang)
            {
                case "ru":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 медный стол";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 бронзовый стол";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 серебряный стол";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 золотой стол";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 платиновый стол";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 алмазный стол";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonContactBankerRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonLeaveTableRU
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Добро пожаловать на {typeOfTable}" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\nВы дарите денежный финансовый подарок в размере {giftSum}$ на игровом столе! " + 
                            $"Связаться с \"Банкиром\" можно через чат Телеграм, нажав кнопку \"Связаться с Банкиром\". " +
                            $"Узнайте реквизиты и сделайте финансовый подарок игроку. " +
                            $"После того как Вы выполнили условия, \"Банкир\" Подтверждает Вас на столе, тем самым активирует Вас как \"Дарителя\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Добро пожаловать на {typeOfTable}" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\nВы дарите денежный финансовый подарок в размере {giftSum}$ на игровом столе! " + 
                            $"Связаться с \"Банкиром\" можно через чат Телеграм, нажав кнопку \"Связаться с Банкиром\". " +
                            $"Узнайте реквизиты и сделайте финансовый подарок игроку. " +
                            $"После того как Вы выполнили условия, \"Банкир\" Подтверждает Вас на столе, тем самым активирует Вас как \"Дарителя\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "eng":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 copper table";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze table";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 silver table";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 gold table";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platinum table";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamond table";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonContactBankerENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonLeaveTableENG
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Welcome to {typeOfTable}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table! " + 
                            $"You can contact the \"Banker\" through the Telegram chat by clicking the \"Contact the Banker\" button. " +
                            $"Find out the details and make a financial gift to the player. " +
                            $"After you have fulfilled the conditions, the \"Banker\" Confirms you on the table, thereby activating you as a \"Giver\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Welcome to {typeOfTable}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table! " + 
                            $"You can contact the \"Banker\" through the Telegram chat by clicking the \"Contact the Banker\" button. " +
                            $"Find out the details and make a financial gift to the player. " +
                            $"After you have fulfilled the conditions, the \"Banker\" Confirms you on the table, thereby activating you as a \"Giver\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "fr":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 copper table";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze table";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 d'Argent table";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 d'or table";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platine table";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamant table";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonContactBankerFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonLeaveTableFR
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Bienvenue à {typeOfTable}" +
                            $"\nIdentifiant du tableau: {tableData.tableData.tableID}" +
                            $"\nVous faites un don financier en espèces d'un montant de {giftSum}$ sur la table de jeu! " + 
                            $"Vous pouvez contacter le \"banquier\" via le chat Telegram en cliquant sur le bouton \"Contacter le banquier\". " +
                            $"Découvrez les détails et faites un don financier au joueur. " +
                            $"Une fois que vous avez rempli les conditions, le \"banquier\" vous confirme sur la table, vous activant ainsi en tant que \"donneur\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Bienvenue à {typeOfTable}" +
                            $"\nIdentifiant du tableau: {tableData.tableData.tableID}" +
                            $"\nVous faites un don financier en espèces d'un montant de {giftSum}$ sur la table de jeu! " + 
                            $"Vous pouvez contacter le \"banquier\" via le chat Telegram en cliquant sur le bouton \"Contacter le banquier\". " +
                            $"Découvrez les détails et faites un don financier au joueur. " +
                            $"Une fois que vous avez rempli les conditions, le \"banquier\" vous confirme sur la table, vous activant ainsi en tant que \"donneur\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "de":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 kupfer Tisch";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze Tisch";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 silberner Tisch";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 goldener Tisch";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platin Tisch";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamant Tisch";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonContactBankerDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonLeaveTableDE
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Willkommen bei {typeOfTable}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\nSie leisten eine Barzuwendung in Höhe von {giftSum}$ auf dem Spieltisch! " + 
                            $"Sie können den \"Banker\" über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche \"Banker kontaktieren\" klicken. " +
                            $"Finden Sie die Details heraus und machen Sie dem Spieler ein finanzielles Geschenk. " +
                            $"Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der \"Banker\" auf dem Tisch und aktiviert Sie dadurch als \"Geber\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Willkommen bei {typeOfTable}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\nSie leisten eine Barzuwendung in Höhe von {giftSum}$ auf dem Spieltisch! " + 
                            $"Sie können den \"Banker\" über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche \"Banker kontaktieren\" klicken. " +
                            $"Finden Sie die Details heraus und machen Sie dem Spieler ein finanzielles Geschenk. " +
                            $"Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der \"Banker\" auf dem Tisch und aktiviert Sie dadurch als \"Geber\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
            }
        }
        public static async void Manager(ITelegramBotClient botClient, long chatId, Table.TableType tableType, UserData userData)
        {
            string typeOfTable = "typeOfTable";
            int giftSum = 0;
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType.ToString() + ".png";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType.ToString() + ".png";
            }

            InlineKeyboardMarkup? inlineKeyboard;
            switch (userData.playerData.lang)
            {
                case "ru":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 медный стол";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 бронзовый стол";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 серебряный стол";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 золотой стол";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 платиновый стол";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 алмазный стол";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataRU,
                                InlineKeyboardButtonGetManagerBDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataRU,
                                InlineKeyboardButtonGetGiverBDataRU,
                                InlineKeyboardButtonGetGiverDDataRU,
                                InlineKeyboardButtonGetGiverCDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamRU 
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU //back
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Добро пожаловать на {typeOfTable}" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\nВы дарите денежный финансовый подарок в размере {giftSum}$ на игровом столе! " + 
                            $"Связаться с \"Банкиром\" можно через чат Телеграм, нажав кнопку \"Связаться с Банкиром\". " +
                            $"Узнайте реквизиты и сделайте финансовый подарок игроку. " +
                            $"После того как Вы выполнили условия, \"Банкир\" Подтверждает Вас на столе, тем самым активирует Вас как \"Дарителя\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Добро пожаловать на {typeOfTable}" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\nВы дарите денежный финансовый подарок в размере {giftSum}$ на игровом столе! " + 
                            $"Связаться с \"Банкиром\" можно через чат Телеграм, нажав кнопку \"Связаться с Банкиром\". " +
                            $"Узнайте реквизиты и сделайте финансовый подарок игроку. " +
                            $"После того как Вы выполнили условия, \"Банкир\" Подтверждает Вас на столе, тем самым активирует Вас как \"Дарителя\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "eng":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 copper table";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze table";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 silver table";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 gold table";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platinum table";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamond table";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataENG,
                                InlineKeyboardButtonGetManagerBDataENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataENG,
                                InlineKeyboardButtonGetGiverBDataENG,
                                InlineKeyboardButtonGetGiverDDataENG,
                                InlineKeyboardButtonGetGiverCDataENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableENG
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Welcome to {typeOfTable}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table! " + 
                            $"You can contact the \"Banker\" through the Telegram chat by clicking the \"Contact the Banker\" button. " +
                            $"Find out the details and make a financial gift to the player. " +
                            $"After you have fulfilled the conditions, the \"Banker\" Confirms you on the table, thereby activating you as a \"Giver\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Welcome to {typeOfTable}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table! " + 
                            $"You can contact the \"Banker\" through the Telegram chat by clicking the \"Contact the Banker\" button. " +
                            $"Find out the details and make a financial gift to the player. " +
                            $"After you have fulfilled the conditions, the \"Banker\" Confirms you on the table, thereby activating you as a \"Giver\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "fr":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 copper table";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze table";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 d'Argent table";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 d'or table";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platine table";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamant table";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataFR,
                                InlineKeyboardButtonGetManagerBDataFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataFR,
                                InlineKeyboardButtonGetGiverBDataFR,
                                InlineKeyboardButtonGetGiverDDataFR,
                                InlineKeyboardButtonGetGiverCDataFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Bienvenue à {typeOfTable}" +
                            $"\nIdentifiant du tableau: {tableData.tableData.tableID}" +
                            $"\nVous faites un don financier en espèces d'un montant de {giftSum}$ sur la table de jeu! " + 
                            $"Vous pouvez contacter le \"banquier\" via le chat Telegram en cliquant sur le bouton \"Contacter le banquier\". " +
                            $"Découvrez les détails et faites un don financier au joueur. " +
                            $"Une fois que vous avez rempli les conditions, le \"banquier\" vous confirme sur la table, vous activant ainsi en tant que \"donneur\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Bienvenue à {typeOfTable}" +
                            $"\nIdentifiant du tableau: {tableData.tableData.tableID}" +
                            $"\nVous faites un don financier en espèces d'un montant de {giftSum}$ sur la table de jeu! " + 
                            $"Vous pouvez contacter le \"banquier\" via le chat Telegram en cliquant sur le bouton \"Contacter le banquier\". " +
                            $"Découvrez les détails et faites un don financier au joueur. " +
                            $"Une fois que vous avez rempli les conditions, le \"banquier\" vous confirme sur la table, vous activant ainsi en tant que \"donneur\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "de":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 kupfer Tisch";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze Tisch";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 silberner Tisch";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 goldener Tisch";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platin Tisch";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamant Tisch";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataDE,
                                InlineKeyboardButtonGetManagerBDataDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataDE,
                                InlineKeyboardButtonGetGiverBDataDE,
                                InlineKeyboardButtonGetGiverDDataDE,
                                InlineKeyboardButtonGetGiverCDataDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Willkommen bei {typeOfTable}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\nSie leisten eine Barzuwendung in Höhe von {giftSum}$ auf dem Spieltisch! " + 
                            $"Sie können den \"Banker\" über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche \"Banker kontaktieren\" klicken. " +
                            $"Finden Sie die Details heraus und machen Sie dem Spieler ein finanzielles Geschenk. " +
                            $"Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der \"Banker\" auf dem Tisch und aktiviert Sie dadurch als \"Geber\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Willkommen bei {typeOfTable}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\nSie leisten eine Barzuwendung in Höhe von {giftSum}$ auf dem Spieltisch! " + 
                            $"Sie können den \"Banker\" über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche \"Banker kontaktieren\" klicken. " +
                            $"Finden Sie die Details heraus und machen Sie dem Spieler ein finanzielles Geschenk. " +
                            $"Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der \"Banker\" auf dem Tisch und aktiviert Sie dadurch als \"Geber\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
            }
        }
        public static async void Banker(ITelegramBotClient botClient, long chatId, Table.TableType tableType, UserData userData)
        {
            string typeOfTable = "typeOfTable";
            int giftSum = 0;
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType.ToString() + ".png";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType.ToString() + ".png";
            }

            InlineKeyboardMarkup? inlineKeyboard;
            switch (userData.playerData.lang)
            {
                case "ru":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 медный стол";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 бронзовый стол";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 серебряный стол";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 золотой стол";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 платиновый стол";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 алмазный стол";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataRU,
                                InlineKeyboardButtonGetManagerBDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataRU,
                                InlineKeyboardButtonGetGiverBDataRU,
                                InlineKeyboardButtonGetGiverDDataRU,
                                InlineKeyboardButtonGetGiverCDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Добро пожаловать на {typeOfTable}" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\nВы дарите денежный финансовый подарок в размере {giftSum}$ на игровом столе! " + 
                            $"Связаться с \"Банкиром\" можно через чат Телеграм, нажав кнопку \"Связаться с Банкиром\". " +
                            $"Узнайте реквизиты и сделайте финансовый подарок игроку. " +
                            $"После того как Вы выполнили условия, \"Банкир\" Подтверждает Вас на столе, тем самым активирует Вас как \"Дарителя\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Добро пожаловать на {typeOfTable}" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\nВы дарите денежный финансовый подарок в размере {giftSum}$ на игровом столе! " + 
                            $"Связаться с \"Банкиром\" можно через чат Телеграм, нажав кнопку \"Связаться с Банкиром\". " +
                            $"Узнайте реквизиты и сделайте финансовый подарок игроку. " +
                            $"После того как Вы выполнили условия, \"Банкир\" Подтверждает Вас на столе, тем самым активирует Вас как \"Дарителя\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "eng":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 copper table";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze table";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 silver table";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 gold table";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platinum table";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamond table";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataENG,
                                InlineKeyboardButtonGetManagerBDataENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataENG,
                                InlineKeyboardButtonGetGiverBDataENG,
                                InlineKeyboardButtonGetGiverDDataENG,
                                InlineKeyboardButtonGetGiverCDataENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamENG
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableENG
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Welcome to {typeOfTable}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table! " + 
                            $"You can contact the \"Banker\" through the Telegram chat by clicking the \"Contact the Banker\" button. " +
                            $"Find out the details and make a financial gift to the player. " +
                            $"After you have fulfilled the conditions, the \"Banker\" Confirms you on the table, thereby activating you as a \"Giver\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Welcome to {typeOfTable}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table! " + 
                            $"You can contact the \"Banker\" through the Telegram chat by clicking the \"Contact the Banker\" button. " +
                            $"Find out the details and make a financial gift to the player. " +
                            $"After you have fulfilled the conditions, the \"Banker\" Confirms you on the table, thereby activating you as a \"Giver\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "fr":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 copper table";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze table";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 d'Argent table";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 d'or table";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platine table";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamant table";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataFR,
                                InlineKeyboardButtonGetManagerBDataFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataFR,
                                InlineKeyboardButtonGetGiverBDataFR,
                                InlineKeyboardButtonGetGiverDDataFR,
                                InlineKeyboardButtonGetGiverCDataFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamFR
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Bienvenue à {typeOfTable}" +
                            $"\nIdentifiant du tableau: {tableData.tableData.tableID}" +
                            $"\nVous faites un don financier en espèces d'un montant de {giftSum}$ sur la table de jeu! " + 
                            $"Vous pouvez contacter le \"banquier\" via le chat Telegram en cliquant sur le bouton \"Contacter le banquier\". " +
                            $"Découvrez les détails et faites un don financier au joueur. " +
                            $"Une fois que vous avez rempli les conditions, le \"banquier\" vous confirme sur la table, vous activant ainsi en tant que \"donneur\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Bienvenue à {typeOfTable}" +
                            $"\nIdentifiant du tableau: {tableData.tableData.tableID}" +
                            $"\nVous faites un don financier en espèces d'un montant de {giftSum}$ sur la table de jeu! " + 
                            $"Vous pouvez contacter le \"banquier\" via le chat Telegram en cliquant sur le bouton \"Contacter le banquier\". " +
                            $"Découvrez les détails et faites un don financier au joueur. " +
                            $"Une fois que vous avez rempli les conditions, le \"banquier\" vous confirme sur la table, vous activant ainsi en tant que \"donneur\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
                case "de":
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                            typeOfTable = "🎗 kupfer Tisch";
                            giftSum = 100;
                            break;
                        case Table.TableType.bronze:
                            typeOfTable = "🎗 bronze Tisch";
                            giftSum = 400;
                            break;
                        case Table.TableType.silver:
                            typeOfTable = "🎗 silberner Tisch";
                            giftSum = 1000;
                            break;
                        case Table.TableType.gold:
                            typeOfTable = "🎗 goldener Tisch";
                            giftSum = 2500;
                            break;
                        case Table.TableType.platinum:
                            typeOfTable = "🎗 platin Tisch";
                            giftSum = 5000;
                            break;
                        case Table.TableType.diamond:
                            typeOfTable = "🎗 diamant Tisch";
                            giftSum = 10000;
                            break;
                    }
                        
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonGetBankerDataDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetManagerADataDE,
                                InlineKeyboardButtonGetManagerBDataDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverADataDE,
                                InlineKeyboardButtonGetGiverBDataDE,
                                InlineKeyboardButtonGetGiverDDataDE,
                                InlineKeyboardButtonGetGiverCDataDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamDE
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });


                    if (path != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Willkommen bei {typeOfTable}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\nSie leisten eine Barzuwendung in Höhe von {giftSum}$ auf dem Spieltisch! " + 
                            $"Sie können den \"Banker\" über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche \"Banker kontaktieren\" klicken. " +
                            $"Finden Sie die Details heraus und machen Sie dem Spieler ein finanzielles Geschenk. " +
                            $"Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der \"Banker\" auf dem Tisch und aktiviert Sie dadurch als \"Geber\".",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Willkommen bei {typeOfTable}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\nSie leisten eine Barzuwendung in Höhe von {giftSum}$ auf dem Spieltisch! " + 
                            $"Sie können den \"Banker\" über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche \"Banker kontaktieren\" klicken. " +
                            $"Finden Sie die Details heraus und machen Sie dem Spieler ein finanzielles Geschenk. " +
                            $"Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der \"Banker\" auf dem Tisch und aktiviert Sie dadurch als \"Geber\".",
                            replyMarkup: inlineKeyboard);
                    }
                    break;
            }
        }

        
    }
}