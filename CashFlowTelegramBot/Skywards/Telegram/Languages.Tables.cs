using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
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
        public static async void Giver(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            Table.TableType tableType,
            UserData userData)
        {
            var giftSum = 0;
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            var bankerData = await WebManager.SendData(new UserProfile((int) tableData.tableData.bankerID),
                WebManager.RequestType.GetUserData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType + ".MP4";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType + ".MP4";
            }

            InlineKeyboardMarkup? inlineKeyboard = null;
            Message sentPhoto;
            string? caption = null;
            var giverCount = 0;
            var giversVerfed = false;
            var verf = "";
            var num = 0;
            var ThisUserIsVerfed = false;
            var giverInfo = new UserData();
            InlineKeyboardButton inlineKeyboardButtonGiverAInfo;
            InlineKeyboardButton inlineKeyboardButtonGiverBInfo;
            InlineKeyboardButton inlineKeyboardButtonGiverCInfo;
            InlineKeyboardButton inlineKeyboardButtonGiverDInfo;
            if (tableData.tableData.giverA_ID != null)
            {
                if (tableData.tableData.giverA_ID == userData.playerData.id && tableData.tableData.verf_A)
                {
                    num = 1;
                    ThisUserIsVerfed = true;
                }
                giverInfo = await WebManager.SendData(new UserProfile((int) tableData.tableData.giverA_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_A)
                {
                    inlineKeyboardButtonGiverAInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverAData");
                }
                else
                {
                    inlineKeyboardButtonGiverAInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverAData");
                }
                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-1", "GetGiverAData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-1", "GetGiverAData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-1", "GetGiverAData");
                        break;
                    default:
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData");
                        break;
                }
            }
            if (tableData.tableData.giverB_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int) tableData.tableData.giverB_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.giverB_ID == userData.playerData.id && tableData.tableData.verf_B)
                {
                    num = 2;
                    ThisUserIsVerfed = true;
                }
                if (tableData.tableData.verf_B)
                {
                    inlineKeyboardButtonGiverBInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverBData");
                }
                else
                {
                    inlineKeyboardButtonGiverBInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverBData");
                }
                giverCount++;
            }
            else 
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-2", "GetGiverBData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-2", "GetGiverBData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-2", "GetGiverBData");
                        break;
                    default:
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData");
                        break;
                }
            }
            if (tableData.tableData.giverC_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int) tableData.tableData.giverC_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.giverC_ID == userData.playerData.id && tableData.tableData.verf_C)
                {
                    num = 3;
                    ThisUserIsVerfed = true;
                }
                if (tableData.tableData.verf_C)
                {
                    inlineKeyboardButtonGiverCInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverCData");
                }
                else
                {
                    inlineKeyboardButtonGiverCInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverCData");
                }
                giverCount++;
            }
            else 
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-3", "GetGiverCData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-3", "GetGiverCData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-3", "GetGiverCData");
                        break;
                    default:
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData");
                        break;
                }
            }
            if (tableData.tableData.giverD_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int) tableData.tableData.giverD_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.giverD_ID == userData.playerData.id && tableData.tableData.verf_D)
                {
                    num = 4;
                    ThisUserIsVerfed = true;
                }
                if (tableData.tableData.verf_D)
                {
                    inlineKeyboardButtonGiverDInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverDData");
                }
                else
                {
                    inlineKeyboardButtonGiverDInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverDData");
                }
                giverCount++;
            }
            else 
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-4", "GetGiverDData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-4", "GetGiverDData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-4", "GetGiverDData");
                        break;
                    default:
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData");
                        break;
                }
            }

            if (tableData.tableData.verf_A && tableData.tableData.verf_B && 
                tableData.tableData.verf_C && tableData.tableData.verf_D)
                giversVerfed = true;
            else 
                giversVerfed = false;

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
                                    inlineKeyboardButtonGiverAInfo,
                                    inlineKeyboardButtonGiverBInfo
                                },
                                new[]
                                {
                                    inlineKeyboardButtonGiverCInfo,
                                    inlineKeyboardButtonGiverDInfo
                                },
                                new[]
                                {
                                    InlineKeyboardButtonShowListTeamRU
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Показать стол картинкой", "TableImage")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU,
                                    InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                }
                            });

                        
                        if (giversVerfed)
                            verf = "✅ Все дарители подтверждены!";
                        else verf = "❌ Не все дарители подтверждены!";
                        caption = "*Добро пожаловать на*" +
                                  $"\n*{userData.playerData.GetTableType()} стол!*" +
                                  $"*ID стола: * {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Всего дарителей на столе:* {giverCount} из 4" +
                                  $"\n*Ваша роль:* {userData.playerData.GetTableRole(userData.playerData.lang)}-{num}" +
                                  "\n\nВыберите игрока для получения информации:";

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
                                    inlineKeyboardButtonGiverAInfo,
                                    inlineKeyboardButtonGiverBInfo
                                },
                                new[]
                                {
                                    inlineKeyboardButtonGiverCInfo,
                                    inlineKeyboardButtonGiverDInfo
                                },
                                new[]
                                {
                                    InlineKeyboardButtonShowListTeamENG
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Show table image", "TableImage")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU,
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });
                        if (giversVerfed)
                            verf = "✅ All Givers are confirmed!";
                        else verf = "❌ Not all Givers are verified!";
                        caption = "*Welcome to*" +
                                  $"\n*{userData.playerData.GetTableType()} table!*" +
                                  $"*Table ID: * {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Total givers on the table:* {giverCount} of 4" +
                                  $"\n*Your Role:* {userData.playerData.GetTableRole(userData.playerData.lang)}-{num}" +
                                  "\n\nSelect a player to view info:";
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
                                    inlineKeyboardButtonGiverAInfo,
                                    inlineKeyboardButtonGiverBInfo
                                },
                                new[]
                                {
                                    inlineKeyboardButtonGiverCInfo,
                                    inlineKeyboardButtonGiverDInfo
                                },
                                new[]
                                {
                                    InlineKeyboardButtonShowListTeamFR
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Afficher l'image du tableau", "TableImage")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR,
                                    InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                }
                            });
                        if (giversVerfed)
                            verf = "✅ Tous les Donneurs sont confirmés!";
                        else verf = "❌ Tous les Donneurs ne sont pas vérifiés!";
                        caption = "*Bienvenue à table*" +
                                  $"\n*{userData.playerData.GetTableType()}!*" +
                                  $"*ID de table: * {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Total des donateurs sur la table:* {giverCount} sur 4" +
                                  $"\n*Votre rôle:* {userData.playerData.GetTableRole(userData.playerData.lang)}-{num}" +
                                  "\n\nSélectionnez un joueur pour afficher les informations:";
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
                                    inlineKeyboardButtonGiverAInfo,
                                    inlineKeyboardButtonGiverBInfo
                                },
                                new[]
                                {
                                    inlineKeyboardButtonGiverCInfo,
                                    inlineKeyboardButtonGiverDInfo
                                },
                                new[]
                                {
                                    InlineKeyboardButtonShowListTeamDE
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Tabellenbild anzeigen", "TableImage")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE,
                                    InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                }
                            });
                        if (giversVerfed)
                            verf = "✅ Alle Geber sind bestätigt!";
                        else verf = "❌ Nicht alle Geber sind verifiziert!";
                        caption = "*Willkommen am*" +
                                  $"\n*{userData.playerData.GetTableType()} tisch!*" +
                                  $"*Tabellen-ID: * {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Gesamtzahl der Geber auf dem Tisch:* {giverCount} von 4" +
                                  $"\n*Ihre Rolle:* {userData.playerData.GetTableRole(userData.playerData.lang)}-{num}" +
                                  "\n\nWählen Sie einen Spieler aus, um Informationen anzuzeigen:";
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
                                    InlineKeyboardButton.WithUrl("📨 Связаться с Банкиром", "https://t.me/" + bankerData.playerData.username), 
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🔔 Оповестить Банкира", "NotifyBanker")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonLeaveTableRU
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU,
                                    InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                }
                            });
                        caption = $"*Добро пожаловать на {userData.playerData.GetTableType()}*" +
                                  $"\n\n*ID стола:* {tableData.tableData.tableID}" +
                                  "\n\n*Фиксированный курс:*" +
                                  "\n📈  1$ = 0.98€ = 62₽" +
                                  $"\n\nВы дарите  финансовый подарок в размере {giftSum}$ на игровом столе! " +
                                  "\nУзнайте реквизиты и сделайте финансовый подарок игроку. 🎁 " +
                                  "\n\nСвязаться с Банкиром можно через чат Telegram, нажав кнопку «*Связаться с Банкиром*». 📨" +
                                  "\n\n👇 Теперь просто нажмите на текст ниже  и отправьте его Банкиру: " +
                                  $"\n«`Привет! 👋 Хочу подарить тебе подарок ${giftSum} 💸`»" +
                                  $"\n\n*Ваш Банкир:* @{bankerData.playerData.username}" +
                                  "\n\n_После того, как Вы выполнили условия, Банкир подтверждает Вас на столе, тем самым активирует Вас, как Дарителя._";
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Contact Banker", "https://t.me/" + bankerData.playerData.username), 
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🔔 Notify Banker", "NotifyBanker")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonLeaveTableENG
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG,
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });
                        caption = $"*Welcome to {userData.playerData.GetTableType()}*" +
                                  $"\n\n*Table ID:* {tableData.tableData.tableID}" +
                                  "\n\n*Fixed exchange rate:*" +
                                  "\n📈  1$ = 0.98€ = 62₽" +
                                  $"\n\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table!" +
                                  "\nFind out the details of the banker and make a financial gift to the player. 🎁" +
                                  "\n\nYou can contact the Banker via the Telegram chat by clicking the «*Contact the Banker*» button. 📨" +
                                  "\n\n👇 Now just click on the text below and send it to the Banker:" +
                                  $"\n«`Hi! 👋 I want to give you ${giftSum} as a gift 💸`»" +
                                  $"\n\n*Your banker:* @{bankerData.playerData.username}" +
                                  "\n\n_After you have fulfilled the conditions, the Banker confirms you on the table, thereby activating you as a Giver._";
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Contacter le banquier", "https://t.me/" + bankerData.playerData.username), 
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🔔 Avertir le banquier", "NotifyBanker")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonLeaveTableFR
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR,
                                    InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                }
                            });
                        caption = $"*Bienvenue à {userData.playerData.GetTableType()}*" +
                                  $"\n\n*Identifiant du tableau:* {tableData.tableData.tableID}" +
                                  "\n\n*Taux de change fixe:*" +
                                  "\n📈  1$ = 0.98€ = 62₽" +
                                  $"\n\nVous offrez un cadeau financier en espèces d'un montant de {giftSum}$ sur la table de jeu!" +
                                  "\nDécouvrez les coordonnées du banquier et faites un don financier au joueur. 🎁" +
                                  "\n\nVous pouvez contacter le banquier via le chat Telegram en cliquant sur le bouton «*Contacter le banquier*». 📨" +
                                  "\n\n👇 Maintenant, cliquez simplement sur le texte ci-dessous et envoyez-le au banquier :" +
                                  $"\n«`Salut! 👋 Je veux vous offrir ${giftSum} en cadeau 💸`»" +
                                  $"\n\n*Votre banquier:* @{bankerData.playerData.username}" +
                                  "\n\n_Une fois que vous avez rempli les conditions, le banquier vous confirme sur la table, vous activant ainsi en tant que donneur._";
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Banker kontaktieren", "https://t.me/" + bankerData.playerData.username), 
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🔔 Banker benachrichtigen", "NotifyBanker")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonLeaveTableDE
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE,
                                    InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
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
                        caption = $"*Willkommen bei {userData.playerData.GetTableType()}*" +
                                  $"\n\n*Tabellen-ID:* {tableData.tableData.tableID}" +
                                  "\n\n*Fester Wechselkurs:*" +
                                  "\n📈  1$ = 0.98€ = 62₽" +
                                  $"\n\nDu verschenkst ein Geldgeschenk in Höhe von {giftSum}$ auf dem Spieltisch!" +
                                  "\nFinden Sie die Details des Bankiers heraus und machen Sie dem Spieler ein finanzielles Geschenk. 🎁" +
                                  "\n\nSie können den Banker über den Telegramm-Chat kontaktieren, indem Sie auf die Schaltfläche «*Banker kontaktieren*» klicken. 📨" +
                                  "\n\n👇 Klicken Sie jetzt einfach auf den folgenden Text und senden Sie ihn an den Banker:" +
                                  $"\n«`Hi! 👋 Ich möchte dir ${giftSum} schenken 💸`»" +
                                  $"\n\n*Ihr Bankberater:* @{bankerData.playerData.username}" +
                                  "\n\n_Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der Banker auf dem Tisch und aktiviert Sie dadurch als Geber._";
                        break;
                    default:
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Contact Banker", "https://t.me/" + bankerData.playerData.username), 
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🔔 Notify Banker", "NotifyBanker")
                                },
                                new[]
                                {
                                    InlineKeyboardButtonLeaveTableENG
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableENG,
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });
                        caption = $"*Welcome to {userData.playerData.GetTableType()}*" +
                                  $"\n\n*Table ID:* {tableData.tableData.tableID}" +
                                  "\n\n*Fixed exchange rate:*" +
                                  "\n📈  1$ = 0.98€ = 62₽" +
                                  $"\n\nYou give a cash financial gift in the amount of {giftSum}$ on the gaming table!" +
                                  "\nFind out the details of the banker and make a financial gift to the player. 🎁" +
                                  "\n\nYou can contact the Banker via the Telegram chat by clicking the «*Contact the Banker*» button. 📨" +
                                  "\n\n👇 Now just click on the text below and send it to the Banker:" +
                                  $"\n«`Hi! 👋 I want to give you ${giftSum} as a gift 💸`»" +
                                  $"\n\n*Your banker:* @{bankerData.playerData.username}" +
                                  "\n\n_After you have fulfilled the conditions, the Banker confirms you on the table, thereby activating you as a Giver._";
                        break;
                }
            using (Stream
                   stream = System.IO.File.OpenRead(path)) 
                await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                    callbackData.Message.MessageId, 
                    media: new InputMediaVideo(new InputMedia(stream, "media"))
                );
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                caption, 
                ParseMode.Markdown, 
                null, 
                inlineKeyboard
            );
        }

        /// <summary>
        /// Creates an table menu with manager view
        /// </summary>
        public static async void Manager(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            Table.TableType tableType,
            UserData userData)
        {
            var tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
            string path = null;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += tableType + ".MP4";
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += tableType + ".MP4";
            }

            InlineKeyboardMarkup? inlineKeyboard = null;
            Message sentPhoto;
            string? caption = null;
            var giverCount = 0;
            var giversVerfed = false;
            var verf = "";
            var giverInfo = new UserData();
            InlineKeyboardButton inlineKeyboardButtonGiverAInfo;
            InlineKeyboardButton inlineKeyboardButtonGiverBInfo;
            InlineKeyboardButton inlineKeyboardButtonGiverCInfo;
            InlineKeyboardButton inlineKeyboardButtonGiverDInfo;
            if (tableData.tableData.giverA_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverA_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_A)
                {
                    inlineKeyboardButtonGiverAInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅",
                            "GetGiverAData");
                }
                else
                {
                    inlineKeyboardButtonGiverAInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverAData");
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-1", "GetGiverAData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-1", "GetGiverAData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-1", "GetGiverAData");
                        break;
                    default:
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData");
                        break;
                }
            }

            if (tableData.tableData.giverB_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverB_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_B)
                {
                    inlineKeyboardButtonGiverBInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅",
                            "GetGiverBData");
                }
                else
                {
                    inlineKeyboardButtonGiverBInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverBData");
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-2", "GetGiverBData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-2", "GetGiverBData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-2", "GetGiverBData");
                        break;
                    default:
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData");
                        break;
                }
            }

            if (tableData.tableData.giverC_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverC_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_C)
                {
                    inlineKeyboardButtonGiverCInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅",
                            "GetGiverCData");
                }
                else
                {
                    inlineKeyboardButtonGiverCInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverCData");
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-3", "GetGiverCData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-3", "GetGiverCData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-3", "GetGiverCData");
                        break;
                    default:
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData");
                        break;
                }
            }

            if (tableData.tableData.giverD_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverD_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_D)
                {
                    inlineKeyboardButtonGiverDInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅",
                            "GetGiverDData");
                }
                else
                {
                    inlineKeyboardButtonGiverDInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverDData");
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-4", "GetGiverDData");
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData");
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-4", "GetGiverDData");
                        break;
                    case "de":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-4", "GetGiverDData");
                        break;
                    default:
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData");
                        break;
                }
            }

            if (tableData.tableData.verf_A && tableData.tableData.verf_B &&
                tableData.tableData.verf_C && tableData.tableData.verf_D)
                giversVerfed = true;
            else
                giversVerfed = false;



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
                                inlineKeyboardButtonGiverAInfo,
                                inlineKeyboardButtonGiverBInfo
                            },
                            new[]
                            {
                                inlineKeyboardButtonGiverCInfo,
                                inlineKeyboardButtonGiverDInfo
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamRU
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Показать стол картинкой", "TableImage")
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU,
                                InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                            }
                        });


                    if (giversVerfed)
                        verf = "✅ Все дарители подтверждены!";
                    else verf = "❌ Не все дарители подтверждены!";
                    caption = "*Добро пожаловать на*" +
                              $"\n*{userData.playerData.GetTableType()} стол!*" +
                              $"*ID стола: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Всего дарителей на столе:* {giverCount} из 4" +
                              $"\n*Ваша роль:* {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                              "\n\nВыберите игрока для получения информации:";

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
                                inlineKeyboardButtonGiverAInfo,
                                inlineKeyboardButtonGiverBInfo
                            },
                            new[]
                            {
                                inlineKeyboardButtonGiverCInfo,
                                inlineKeyboardButtonGiverDInfo
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamENG
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Show table image", "TableImage")
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU,
                                InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                            }
                        });
                    if (giversVerfed)
                        verf = "✅ All Givers are confirmed!";
                    else verf = "❌ Not all Givers are verified!";
                    caption = "*Welcome to*" +
                              $"\n*{userData.playerData.GetTableType()} table!*" +
                              $"*Table ID: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Total givers on the table:* {giverCount} of 4" +
                              $"\n*Your Role:* {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                              "\n\nSelect a player to view info:";
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
                                inlineKeyboardButtonGiverAInfo,
                                inlineKeyboardButtonGiverBInfo
                            },
                            new[]
                            {
                                inlineKeyboardButtonGiverCInfo,
                                inlineKeyboardButtonGiverDInfo
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamFR
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Afficher l'image du tableau", "TableImage")
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR,
                                InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                            }
                        });
                    if (giversVerfed)
                        verf = "✅ Tous les Donneurs sont confirmés!";
                    else verf = "❌ Tous les Donneurs ne sont pas vérifiés!";
                    caption = "*Bienvenue à table*" +
                              $"\n*{userData.playerData.GetTableType()}!*" +
                              $"*ID de table: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Total des donateurs sur la table:* {giverCount} sur 4" +
                              $"\n*Votre rôle:* {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                              "\n\nSélectionnez un joueur pour afficher les informations:";
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
                                inlineKeyboardButtonGiverAInfo,
                                inlineKeyboardButtonGiverBInfo
                            },
                            new[]
                            {
                                inlineKeyboardButtonGiverCInfo,
                                inlineKeyboardButtonGiverDInfo
                            },
                            new[]
                            {
                                InlineKeyboardButtonShowListTeamDE
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Tabellenbild anzeigen", "TableImage")
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE,
                                InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                            }
                        });
                    if (giversVerfed)
                        verf = "✅ Alle Geber sind bestätigt!";
                    else verf = "❌ Nicht alle Geber sind verifiziert!";
                    caption = "*Willkommen am*" +
                              $"\n*{userData.playerData.GetTableType()} tisch!*" +
                              $"*Tabellen-ID: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Gesamtzahl der Geber auf dem Tisch:* {giverCount} von 4" +
                              $"\n*Ihre Rolle:* {userData.playerData.GetTableRole(userData.playerData.lang)}" +
                              "\n\nWählen Sie einen Spieler aus, um Informationen anzuzeigen:";
                    break;
            }
            using (Stream
                   stream = System.IO.File.OpenRead(path)) 
                await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                    callbackData.Message.MessageId, 
                    media: new InputMediaVideo(new InputMedia(stream, "media"))
                );
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                caption, 
                ParseMode.Markdown, 
                null, 
                inlineKeyboard
            );
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

        private static void RoleSelection(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData,
            Table.TableType tableType)
        {
            switch (userData.playerData.tableRole)
            {
                case "giver":
                    Giver(botClient, chatId, callbackData, tableType, userData);
                    break;
                case "manager":
                    Manager(botClient, chatId, callbackData, tableType, userData);
                    break;
                case "banker":
                    Banker(botClient, chatId, tableType, userData);
                    break;
            }
        }

        public static async void Copper(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData)
        {
            userData.playerData.level_tableType = "copper";
            var tableType = Table.TableType.copper;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, callbackData, userData, tableType);
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

        public static async void Bronze(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData)
        {
            userData.playerData.level_tableType = "bronze";
            var tableType = Table.TableType.bronze;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, callbackData, userData, tableType);
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

        public static async void Silver(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData)
        {
            userData.playerData.level_tableType = "silver";
            var tableType = Table.TableType.silver;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, callbackData, userData, tableType);
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

        public static async void Gold(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData)
        {
            userData.playerData.level_tableType = "gold";
            var tableType = Table.TableType.gold;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, callbackData, userData, tableType);
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

        public static async void Platinum(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData)
        {
            userData.playerData.level_tableType = "platinum";
            var tableType = Table.TableType.platinum;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, callbackData, userData, tableType);
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

        public static async void Diamond(ITelegramBotClient botClient, long chatId,
            CallbackQuery callbackData, UserData userData)
        {
            userData.playerData.level_tableType = "diamond";
            var tableType = Table.TableType.diamond;
            var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
            if (!data.error.isError)
            {
                RoleSelection(botClient, chatId, callbackData, userData, tableType);
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