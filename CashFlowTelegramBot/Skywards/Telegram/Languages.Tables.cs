using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace CashFlowTelegramBot.Skywards.Telegram;

public partial class Languages
{
    public abstract class Tables
    {
        //------RU------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerRU =
            InlineKeyboardButton.WithCallbackData("📨 Связаться с банкиром", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableRU =
            InlineKeyboardButton.WithCallbackData("🔙 Назад", "ChooseTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableRU =
            InlineKeyboardButton.WithCallbackData("❌ Выйти со стола", "LeaveTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataRU =
            InlineKeyboardButton.WithCallbackData("🏦 Банкир", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataRU =
            InlineKeyboardButton.WithCallbackData("👤 Менеджер-1", "GetManagerAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataRU =
            InlineKeyboardButton.WithCallbackData("👤 Менеджер-2", "GetManagerBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataRU =
            InlineKeyboardButton.WithCallbackData("🎁 Даритель-1", "GetGiverAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataRU =
            InlineKeyboardButton.WithCallbackData("🎁 Даритель-2", "GetGiverBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataRU =
            InlineKeyboardButton.WithCallbackData("🎁 Даритель-3", "GetGiverCData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataRU =
            InlineKeyboardButton.WithCallbackData("🎁 Даритель-4", "GetGiverDData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamRU =
            InlineKeyboardButton.WithCallbackData("📝 Показать команду списком", "ShowListTeam");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverARU =
            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола", "RemoveFromTableGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBRU =
            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола", "RemoveFromTableGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCRU =
            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола", "RemoveFromTableGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDRU =
            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола", "RemoveFromTableGiverD");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverARU =
            InlineKeyboardButton.WithCallbackData("✅ Подтвердить", "VerfGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBRU =
            InlineKeyboardButton.WithCallbackData("✅ Подтвердить", "VerfGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCRU =
            InlineKeyboardButton.WithCallbackData("✅ Подтвердить", "VerfGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDRU =
            InlineKeyboardButton.WithCallbackData("✅ Подтвердить", "VerfGiverD");

        //------ENG------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableENG =
            InlineKeyboardButton.WithCallbackData("🔙 Back", "ChooseTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerENG =
            InlineKeyboardButton.WithCallbackData("📨 Contact a banker", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableENG =
            InlineKeyboardButton.WithCallbackData("❌ Exit the table", "LeaveTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataENG =
            InlineKeyboardButton.WithCallbackData("🏦 Banker", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataENG =
            InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataENG =
            InlineKeyboardButton.WithCallbackData("👤Manager-2", "GetManagerBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataENG =
            InlineKeyboardButton.WithCallbackData("🎁 Giver-1", "GetGiverAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataENG =
            InlineKeyboardButton.WithCallbackData("🎁 Giver-2", "GetGiverBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataENG =
            InlineKeyboardButton.WithCallbackData("🎁 Giver-3", "GetGiverDData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataENG =
            InlineKeyboardButton.WithCallbackData("🎁 Giver-4", "GetGiverCData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamENG =
            InlineKeyboardButton.WithCallbackData("📝 Show command in list", "ShowListTeam");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverAENG =
            InlineKeyboardButton.WithCallbackData("❌ Delete from the table", "RemoveFromTableGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBENG =
            InlineKeyboardButton.WithCallbackData("❌ Delete from the table", "RemoveFromTableGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCENG =
            InlineKeyboardButton.WithCallbackData("❌ Delete from the table", "RemoveFromTableGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDENG =
            InlineKeyboardButton.WithCallbackData("❌ Delete from the table", "RemoveFromTableGiverD");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverAENG =
            InlineKeyboardButton.WithCallbackData("✅ Confirm", "VerfGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBENG =
            InlineKeyboardButton.WithCallbackData("✅ Confirm", "VerfGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCENG =
            InlineKeyboardButton.WithCallbackData("✅ Confirm", "VerfGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDENG =
            InlineKeyboardButton.WithCallbackData("✅ Confirm", "VerfGiverD");

        //------FR------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableFR =
            InlineKeyboardButton.WithCallbackData("🔙 Retour", "ChooseTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerFR =
            InlineKeyboardButton.WithCallbackData("📨 Contactez un banquier", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableFR =
            InlineKeyboardButton.WithCallbackData("❌ Sortir du tableau", "LeaveTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataFR =
            InlineKeyboardButton.WithCallbackData("🏦 Banquier", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataFR =
            InlineKeyboardButton.WithCallbackData("👤 Gestionnaire-1", "GetManagerAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataFR =
            InlineKeyboardButton.WithCallbackData("👤Gestionnaire-2", "GetManagerBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataFR =
            InlineKeyboardButton.WithCallbackData("🎁Donateur-1", "GetGiverAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataFR =
            InlineKeyboardButton.WithCallbackData("🎁Donateur-2", "GetGiverBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataFR =
            InlineKeyboardButton.WithCallbackData("🎁Donateur-3", "GetGiverDData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataFR =
            InlineKeyboardButton.WithCallbackData("🎁Donateur-4", "GetGiverCData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamFR =
            InlineKeyboardButton.WithCallbackData("📝Afficher la commande dans la liste", "ShowListTeam");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverAFR =
            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau", "RemoveFromTableGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBFR =
            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau", "RemoveFromTableGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCFR =
            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau", "RemoveFromTableGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDFR =
            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau", "RemoveFromTableGiverD");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverAFR =
            InlineKeyboardButton.WithCallbackData("✅ Confirmer", "VerfGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBFR =
            InlineKeyboardButton.WithCallbackData("✅ Confirmer", "VerfGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCFR =
            InlineKeyboardButton.WithCallbackData("✅ Confirmer", "VerfGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDFR =
            InlineKeyboardButton.WithCallbackData("✅ Confirmer", "VerfGiverD");

        //------DE------\\
        public static readonly InlineKeyboardButton InlineKeyboardButtonChooseTableDE =
            InlineKeyboardButton.WithCallbackData("🔙Der Rücken", "ChooseTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonContactBankerDE =
            InlineKeyboardButton.WithCallbackData("📨Sie sich an einen Banker", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonLeaveTableDE =
            InlineKeyboardButton.WithCallbackData("❌Der Rücken Verlasse den Tisch", "LeaveTable");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetBankerDataDE =
            InlineKeyboardButton.WithCallbackData("🏦 Banker", "GetBankerData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerADataDE =
            InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetManagerBDataDE =
            InlineKeyboardButton.WithCallbackData("👤 Manager-2", "GetManagerBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverADataDE =
            InlineKeyboardButton.WithCallbackData("🎁 Geber-1", "GetGiverAData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverBDataDE =
            InlineKeyboardButton.WithCallbackData("🎁 Geber-2", "GetGiverBData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverDDataDE =
            InlineKeyboardButton.WithCallbackData("🎁 Geber-3", "GetGiverDData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonGetGiverCDataDE =
            InlineKeyboardButton.WithCallbackData("🎁 Geber-4", "GetGiverCData");

        public static readonly InlineKeyboardButton InlineKeyboardButtonShowListTeamDE =
            InlineKeyboardButton.WithCallbackData("📝 Befehl in Liste anzeigen", "ShowListTeam");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverADE =
            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen", "RemoveFromTableGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverBDE =
            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen", "RemoveFromTableGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverCDE =
            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen", "RemoveFromTableGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonRemoveFromTableGiverDDE =
            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen", "RemoveFromTableGiverD");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverADE =
            InlineKeyboardButton.WithCallbackData("✅ Bestätigen", "VerfGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBDE =
            InlineKeyboardButton.WithCallbackData("✅ Bestätigen", "VerfGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCDE =
            InlineKeyboardButton.WithCallbackData("✅ Bestätigen", "VerfGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDDE =
            InlineKeyboardButton.WithCallbackData("✅ Bestätigen", "VerfGiverD");

        /// <summary>
        /// Creates an table menu with giver view
        /// </summary>
        public static async void Giver(ITelegramBotClient botClient, long chatId, Table.TableType tableType,
            UserData userData)
        {
            var giftSum = 0;
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType + ".png";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType + ".png";
            }

            InlineKeyboardMarkup? inlineKeyboard;
            Message sentPhoto;
            var giverCount = 0;
            var giversVerfed = true;
            var verf = "";
            var ThisUserIsVerfed = false;
            if (tableData.tableData.giverA_ID != null)
            {
                if (tableData.tableData.giverA_ID == userData.playerData.id && tableData.tableData.verf_A) ThisUserIsVerfed = true;
                if (!tableData.tableData.verf_A) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverB_ID != null)
            {
                if (tableData.tableData.giverB_ID == userData.playerData.id && tableData.tableData.verf_B) ThisUserIsVerfed = true;
                if (!tableData.tableData.verf_B) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverC_ID != null)
            {
                if (tableData.tableData.giverC_ID == userData.playerData.id && tableData.tableData.verf_C) ThisUserIsVerfed = true;
                if (!tableData.tableData.verf_C) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverD_ID != null)
            {
                if (tableData.tableData.giverD_ID == userData.playerData.id && tableData.tableData.verf_D) ThisUserIsVerfed = true;
                if (!tableData.tableData.verf_D) giversVerfed = false;
                giverCount++;
            }

            switch (tableType)
            {
                case Table.TableType.copper:
                    giftSum = 100;
                    break;
                case Table.TableType.bronze:
                    giftSum = 400;
                    break;
                case Table.TableType.silver:
                    giftSum = 1000;
                    break;
                case Table.TableType.gold:
                    giftSum = 2500;
                    break;
                case Table.TableType.platinum:
                    giftSum = 5000;
                    break;
                case Table.TableType.diamond:
                    giftSum = 10000;
                    break;
            }

            if (ThisUserIsVerfed)
                switch (userData.playerData.lang)
                {
                    case "ru":
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
                                    InlineKeyboardButtonGetGiverBDataRU
                                },
                                new[]
                                {
                                    InlineKeyboardButtonGetGiverDDataRU,
                                    InlineKeyboardButtonGetGiverCDataRU
                                },
                                new[]
                                {
                                    InlineKeyboardButtonShowListTeamRU //Показать команду списком
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU //back
                                }
                            });

                        
                        if (giversVerfed)
                            verf = "✅ Все дарители подтверждены!";
                        else verf = "❌ Не все дарители подтверждены!";

                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "Добро пожаловать на" +
                            $"\n{userData.playerData.GetTableType()} стол" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\n\n{verf}" +
                            $"\nВсего дарителей на столе: {giverCount} из 4" +
                            $"\nВаша роль: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                            "\n\nВыберите игрока для просмотра информации:",
                            replyMarkup: inlineKeyboard);

                        break;
                    case "eng":
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
                                    InlineKeyboardButtonGetGiverBDataENG
                                },
                                new[]
                                {
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
                        if (giversVerfed)
                            verf = "✅ All Givers are confirmed!";
                        else verf = "❌ Not all Givers are verified!";
                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Welcome to {userData.playerData.GetTableType()}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\n\n{verf}" +
                            $"\nTotal givers on the table: {giverCount} of 4" +
                            $"\nYour Role: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                            "\n\nSelect a player to view info:",
                            replyMarkup: inlineKeyboard);

                        break;
                    case "fr":
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
                                    InlineKeyboardButtonGetGiverBDataFR
                                },
                                new[]
                                {
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
                        if (giversVerfed)
                            verf = "✅ Tous les Donneurs sont confirmés!";
                        else verf = "❌ Tous les Donneurs ne sont pas vérifiés!";
                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Bienvenue sur {userData.playerData.GetTableType()}" +
                            $"\nID de table: {tableData.tableData.tableID}" +
                            $"\n\n{verf}" +
                            $"\nTotal des donateurs sur la table: {giverCount} sur 4" +
                            $"\nVotre rôle: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                            "\n\nSélectionnez un joueur pour afficher les informations:",
                            replyMarkup: inlineKeyboard);

                        break;
                    case "de":

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
                                    InlineKeyboardButtonGetGiverBDataDE
                                },
                                new[]
                                {
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
                        if (giversVerfed)
                            verf = "✅ Alle Geber sind bestätigt!";
                        else verf = "❌ Nicht alle Geber sind verifiziert!";
                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Willkommen bei {userData.playerData.GetTableType()}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\n\n{verf}" +
                            $"\nGesamtzahl der Geber auf dem Tisch: {giverCount} von 4" +
                            $"\nIhre Rolle: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                            "\nWählen Sie einen Spieler aus, um Informationen anzuzeigen:",
                            replyMarkup: inlineKeyboard);
                        break;
                }
            else
                switch (userData.playerData.lang)
                {
                    case "ru":

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

                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Добро пожаловать на {userData.playerData.GetTableType()}" +
                            $"\nID стола: {tableData.tableData.tableID}" +
                            $"\n\nВаша роль{userData.playerData.tableRole}" +
                            $"\nВы дарите  финансовый подарок в размере {giftSum}$ на игровом столе! " +
                            "\nУзнайте реквизиты и сделайте финансовый подарок игроку. 🎁 " +
                            "\n\n" +
                            "\nСвязаться с \"Банкиром\" можно через чат Telegram, нажав кнопку \"Связаться с Банкиром 📨\". " +
                            "\nТеперь просто напишите Банкиру:" +
                            $"\nПривет, хочу подаритье тебе подарок {giftSum}" +
                            "После выполнения условия, \"Банкир\" подтверждает Вас на столе, тем самым происходит активация аккаунтас на столе на роль  \"Дарителя\".",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
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


                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Welcome to {userData.playerData.GetTableType()}" +
                            $"\nTable ID: {tableData.tableData.tableID}" +
                            $"\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table!" +
                            "\nYou can contact the \"Banker\" through the Telegram chat by clicking the \"Contact the Banker\" button." +
                            "\nFind out the details and make a financial gift to the player.🎁️ " +
                            "\nAfter you have fulfilled the conditions, the \"Banker\" Confirms you on the table, thereby activating you as a \"Giver\".",
                            replyMarkup: inlineKeyboard);


                        break;
                    case "fr":
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


                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Bienvenue à {userData.playerData.GetTableType()}" +
                            $"\nIdentifiant du tableau: {tableData.tableData.tableID}" +
                            $"\nVous faites un don financier en espèces d'un montant de {giftSum}$ sur la table de jeu! " +
                            "\nVous pouvez contacter le \"banquier\" via le chat Telegram en cliquant sur le bouton \"Contacter le banquier\". " +
                            "\nDécouvrez les détails et faites un don financier au joueur.🎁️ " +
                            "\nUne fois que vous avez rempli les conditions, le \"banquier\" vous confirme sur la table, vous activant ainsi en tant que \"donneur\".",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
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

                        sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"Willkommen bei {userData.playerData.GetTableType()}" +
                            $"\nTabellen-ID: {tableData.tableData.tableID}" +
                            $"\nSie leisten eine Barzuwendung in Höhe von {giftSum}$ auf dem Spieltisch! " +
                            "\nSie können den \"Banker\" über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche \"Banker kontaktieren\" klicken. " +
                            "\nFinden Sie die Details heraus und machen Sie dem Spieler ein finanzielles Geschenk.🎁️" +
                            "\nNachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der \"Banker\" auf dem Tisch und aktiviert Sie dadurch als \"Geber\".",
                            replyMarkup: inlineKeyboard);


                        break;
                }
        }

        /// <summary>
        /// Creates an table menu with manager view
        /// </summary>
        public static async void Manager(ITelegramBotClient botClient, long chatId, Table.TableType tableType,
            UserData userData)
        {
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType + ".png";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType + ".png";
            }

            InlineKeyboardMarkup? inlineKeyboard;
            Message sentPhoto;
            var giverCount = 0;
            var giversVerfed = true;
            var verf = "";
            if (tableData.tableData.giverA_ID != null)
            {
                if (!tableData.tableData.verf_A) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverB_ID != null)
            {
                if (!tableData.tableData.verf_B) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverC_ID != null)
            {
                if (!tableData.tableData.verf_C) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverD_ID != null)
            {
                if (!tableData.tableData.verf_D) giversVerfed = false;
                giverCount++;
            }

            switch (userData.playerData.lang)
            {
                case "ru":
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
                                InlineKeyboardButtonGetGiverBDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonGetGiverDDataRU,
                                InlineKeyboardButtonGetGiverCDataRU
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamRU //Показать команду списком
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU //back
                            }
                        });


                    if (giversVerfed)
                        verf = "✅ Все дарители подтверждены!";
                    else verf = "❌ Не все дарители подтверждены!";

                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Добро пожаловать на {userData.playerData.GetTableType()}" +
                        $"\nID стола: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"Всего дарителей на столе:{giverCount} из 4" +
                        $"\nВаша роль: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nВыберете игрока для просмотра информации:",
                        replyMarkup: inlineKeyboard);

                    break;
                case "eng":
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
                                InlineKeyboardButtonGetGiverBDataENG
                            },
                            new[]
                            {
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
                    if (giversVerfed)
                        verf = "✅ All Givers are confirmed!";
                    else verf = "❌ Not all Givers are verified!";
                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Welcome to {userData.playerData.GetTableType()}" +
                        $"\nTable ID: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"Total givers on the table: {giverCount} of 4" +
                        $"\nYour Role: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nSelect a player to view info:",
                        replyMarkup: inlineKeyboard);

                    break;
                case "fr":
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
                                InlineKeyboardButtonGetGiverBDataFR
                            },
                            new[]
                            {
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
                    if (giversVerfed)
                        verf = "✅ Tous les Donneurs sont confirmés!";
                    else verf = "❌ Tous les Donneurs ne sont pas vérifiés!";
                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Bienvenue sur {userData.playerData.GetTableType()}" +
                        $"\nID de table: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"Total des donateurs sur la table: {giverCount} sur 4" +
                        $"\nVotre rôle: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nSélectionnez un joueur pour afficher les informations:",
                        replyMarkup: inlineKeyboard);

                    break;
                case "de":

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
                                InlineKeyboardButtonGetGiverBDataDE
                            },
                            new[]
                            {
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
                    if (giversVerfed)
                        verf = "✅ Alle Geber sind bestätigt!";
                    else verf = "❌ Nicht alle Geber sind verifiziert!";
                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Willkommen bei {userData.playerData.GetTableType()}" +
                        $"\nTabellen-ID: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"Gesamtzahl der Geber auf dem Tisch: {giverCount} von 4" +
                        $"\nIhre Rolle: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nWählen Sie einen Spieler aus, um Informationen anzuzeigen:",
                        replyMarkup: inlineKeyboard);
                    break;
            }
        }

        /// <summary>
        /// Creates an table menu with banker view
        /// </summary>
        public static async void Banker(ITelegramBotClient botClient, long chatId, Table.TableType tableType,
            UserData userData)
        {
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType + ".png";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType + ".png";
            }

            InlineKeyboardMarkup? inlineKeyboard;
            Message sentPhoto;
            var giverCount = 0;
            var giversVerfed = true;
            var verf = "";
            if (tableData.tableData.giverA_ID != null)
            {
                if (!tableData.tableData.verf_A) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverB_ID != null)
            {
                if (!tableData.tableData.verf_B) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverC_ID != null)
            {
                if (!tableData.tableData.verf_C) giversVerfed = false;
                giverCount++;
            }

            if (tableData.tableData.giverD_ID != null)
            {
                if (!tableData.tableData.verf_D) giversVerfed = false;
                giverCount++;
            }

            switch (userData.playerData.lang)
            {
                case "ru":
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
                                InlineKeyboardButtonGetGiverBDataRU
                            },
                            new[]
                            {
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

                    if (giversVerfed)
                        verf = "✅ Все дарители подтверждены!";
                    else verf = "❌ Не все дарители подтверждены!";

                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Добро пожаловать на {userData.playerData.GetTableType()}" +
                        $"\nID стола: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"\nВсего дарителей на столе: {giverCount} из 4" +
                        $"\nВаша роль: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nВыберете игрока для просмотра информации:",
                        replyMarkup: inlineKeyboard);
                    break;
                case "eng":
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
                                InlineKeyboardButtonGetGiverBDataENG
                            },
                            new[]
                            {
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
                    if (giversVerfed)
                        verf = "✅ All Givers are confirmed!";
                    else verf = "❌ Not all Givers are verified!";
                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Welcome to {userData.playerData.GetTableType()}" +
                        $"\nTable ID: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"\nTotal givers on the table: {giverCount} of 4" +
                        $"\nYour Role: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nSelect a player to view info:",
                        replyMarkup: inlineKeyboard);

                    break;
                case "fr":
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
                                InlineKeyboardButtonGetGiverBDataFR
                            },
                            new[]
                            {
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
                    if (giversVerfed)
                        verf = "✅ Tous les Donneurs sont confirmés!";
                    else verf = "❌ Tous les Donneurs ne sont pas vérifiés!";
                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Bienvenue sur {userData.playerData.GetTableType()}" +
                        $"\nID de table: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"\nTotal des donateurs sur la table: {giverCount} sur 4" +
                        $"\nVotre rôle: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nSélectionnez un joueur pour afficher les informations:",
                        replyMarkup: inlineKeyboard);
                    break;
                case "de":
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
                                InlineKeyboardButtonGetGiverBDataDE
                            },
                            new[]
                            {
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
                    if (giversVerfed)
                        verf = "✅ Alle Geber sind bestätigt!";
                    else verf = "❌ Nicht alle Geber sind verifiziert!";
                    sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"Willkommen bei {userData.playerData.GetTableType()}" +
                        $"\nTabellen-ID: {tableData.tableData.tableID}" +
                        $"\n\n{verf}" +
                        $"\nGesamtzahl der Geber auf dem Tisch: {giverCount} von 4" +
                        $"\nIhre Rolle: {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                        "\nWählen Sie einen Spieler aus, um Informationen anzuzeigen:",
                        replyMarkup: inlineKeyboard);
                    break;
            }
        }

        private static void RoleSelection(ITelegramBotClient botClient, long chatId, UserData userData,
            Table.TableType tableType)
        {
            switch (userData.playerData.tableRole)
            {
                case "giver":
                    Giver(botClient, chatId, tableType, userData);
                    break;
                case "manager":
                    Manager(botClient, chatId, tableType, userData);
                    break;
                case "banker":
                    Banker(botClient, chatId, tableType, userData);
                    break;
            }
        }

        public static async void Copper(ITelegramBotClient botClient, long chatId, UserData userData)
        {
            userData.playerData.level_tableType = "copper";
            var tableType = Table.TableType.copper;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, userData, tableType);
            }
            else
            {
                Console.WriteLine("ERROR");
                InlineKeyboardMarkup inlineKeyboard;
                Message sentMessage;
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "К сожалению таких столов пока что нет, попробуйте позже",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Unfortunately, there are no such tables yet, please try again later",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                            replyMarkup: inlineKeyboard);
                        break;
                }
            }
        }

        public static async void Bronze(ITelegramBotClient botClient, long chatId, UserData userData)
        {
            userData.playerData.level_tableType = "bronze";
            var tableType = Table.TableType.bronze;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, userData, tableType);
            }
            else
            {
                Console.WriteLine("ERROR");
                InlineKeyboardMarkup inlineKeyboard;
                Message sentMessage;
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "К сожалению таких столов пока что нет, попробуйте позже",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Unfortunately, there are no such tables yet, please try again later",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                            replyMarkup: inlineKeyboard);
                        break;
                }
            }
        }

        public static async void Silver(ITelegramBotClient botClient, long chatId, UserData userData)
        {
            userData.playerData.level_tableType = "silver";
            var tableType = Table.TableType.silver;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, userData, tableType);
            }
            else
            {
                Console.WriteLine("ERROR");
                InlineKeyboardMarkup inlineKeyboard;
                Message sentMessage;
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "К сожалению таких столов пока что нет, попробуйте позже",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Unfortunately, there are no such tables yet, please try again later",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                            replyMarkup: inlineKeyboard);
                        break;
                }
            }
        }

        public static async void Gold(ITelegramBotClient botClient, long chatId, UserData userData)
        {
            userData.playerData.level_tableType = "gold";
            var tableType = Table.TableType.gold;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, userData, tableType);
            }
            else
            {
                Console.WriteLine("ERROR");
                InlineKeyboardMarkup inlineKeyboard;
                Message sentMessage;
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "К сожалению таких столов пока что нет, попробуйте позже",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Unfortunately, there are no such tables yet, please try again later",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                            replyMarkup: inlineKeyboard);
                        break;
                }
            }
        }

        public static async void Platinum(ITelegramBotClient botClient, long chatId, UserData userData)
        {
            userData.playerData.level_tableType = "platinum";
            var tableType = Table.TableType.platinum;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, userData, tableType);
            }
            else
            {
                Console.WriteLine("ERROR");
                InlineKeyboardMarkup inlineKeyboard;
                Message sentMessage;
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "К сожалению таких столов пока что нет, попробуйте позже",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Unfortunately, there are no such tables yet, please try again later",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                            replyMarkup: inlineKeyboard);
                        break;
                }
            }
        }

        public static async void Diamond(ITelegramBotClient botClient, long chatId, UserData userData)
        {
            userData.playerData.level_tableType = "diamond";
            var tableType = Table.TableType.diamond;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, userData, tableType);
            }
            else
            {
                Console.WriteLine("ERROR");
                InlineKeyboardMarkup inlineKeyboard;
                Message sentMessage;
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "К сожалению таких столов пока что нет, попробуйте позже",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Unfortunately, there are no such tables yet, please try again later",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE
                                }
                            });

                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                            replyMarkup: inlineKeyboard);
                        break;
                }
            }
        }
    }
}