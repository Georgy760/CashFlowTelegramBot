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
            InlineKeyboardButton.WithCallbackData("✅ Активировать", "VerfGiverA");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverBRU =
            InlineKeyboardButton.WithCallbackData("✅ Активировать", "VerfGiverB");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverCRU =
            InlineKeyboardButton.WithCallbackData("✅ Активировать", "VerfGiverC");

        public static readonly InlineKeyboardButton InlineKeyboardButtonVerfGiverDRU =
            InlineKeyboardButton.WithCallbackData("✅ Активировать", "VerfGiverD");

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
            UserData? tableData = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_copper), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.bronze:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_bronze), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.silver:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_silver), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.gold:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_gold), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.platinum:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_platinum), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.diamond:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_diamond), WebManager.RequestType.GetTableData);
                    break;
            }
            
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
                    if (tableData.tableData.giverA_ID == userData.playerData.id)
                    {
                        inlineKeyboardButtonGiverAInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅ 🔘", "GetGiverAData|" + tableType);
                    }
                    else
                    {
                        inlineKeyboardButtonGiverAInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverAData|" + tableType);
                    }
                }
                else
                {
                    inlineKeyboardButtonGiverAInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverAData|" + tableType);
                }
                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-1", "GetGiverAData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-1", "GetGiverAData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-1", "GetGiverAData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverAInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData|" + tableType);
                        break;
                }
            }
            if (tableData.tableData.giverB_ID != null)
            {
                
                if (tableData.tableData.giverB_ID == userData.playerData.id && tableData.tableData.verf_B)
                {
                    num = 2;
                    ThisUserIsVerfed = true;
                }
                giverInfo = await WebManager.SendData(new UserProfile((int) tableData.tableData.giverB_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_B)
                {
                    if (tableData.tableData.giverB_ID == userData.playerData.id)
                    {
                        inlineKeyboardButtonGiverBInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅ 🔘", "GetGiverBData|" + tableType);
                    }
                    else
                    {
                        inlineKeyboardButtonGiverBInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverBData|" + tableType);
                    }
                }
                else
                {
                    inlineKeyboardButtonGiverBInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverBData|" + tableType);
                }
                giverCount++;
            }
            else 
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-2", "GetGiverBData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-2", "GetGiverBData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-2", "GetGiverBData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverBInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData|" + tableType);
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
                    if (tableData.tableData.giverC_ID == userData.playerData.id)
                    {
                        inlineKeyboardButtonGiverCInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅ 🔘", "GetGiverCData|" + tableType);
                    }
                    else
                    {
                        inlineKeyboardButtonGiverCInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverCData|" + tableType);
                    }
                }
                else
                {
                    inlineKeyboardButtonGiverCInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverCData|" + tableType);
                }
                giverCount++;
            }
            else 
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-3", "GetGiverCData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-3", "GetGiverCData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-3", "GetGiverCData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverCInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData|" + tableType);
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
                    if (tableData.tableData.giverD_ID == userData.playerData.id)
                    {
                        inlineKeyboardButtonGiverDInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅ 🔘", "GetGiverDData|" + tableType);
                    }
                    else
                    {
                        inlineKeyboardButtonGiverDInfo = 
                            InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅", "GetGiverDData|" + tableType);
                    }
                }
                else
                {
                    inlineKeyboardButtonGiverDInfo = 
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌", "GetGiverDData|" + tableType);
                }
                giverCount++;
            }
            else 
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Даритель-4", "GetGiverDData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Donneur-4", "GetGiverDData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Geber-4", "GetGiverDData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverDInfo = InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData|" + tableType);
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
                                    InlineKeyboardButton.WithCallbackData("🏦 Банкир", "GetBankerData|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("👤 Менеджер-1", "GetManagerAData|" + tableType),
                                    InlineKeyboardButton.WithCallbackData("👤 Менеджер-2", "GetManagerBData|" + tableType)
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
                                    InlineKeyboardButton.WithCallbackData("📝 Показать команду списком", "ShowListTeam|" + tableType),
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Показать стол картинкой", "TableImage|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU,
                                    InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                }
                            });

                        
                        if (giversVerfed)
                            verf = @"✅ Все дарители подтверждены\!";
                        else verf = @"❌ Не все дарители подтверждены\!";
                        caption = "*Добро пожаловать на*" + "\n" +
                                  $@"*{TableProfile.GetTableType(userData.playerData,tableType)} стол\!*" + "\n" +
                                  $"\n*ID стола:* {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Всего дарителей на столе:* {giverCount} из 4" + "\n" +
                                  $@"*Ваша роль:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}\-{num}" +
                                  "\n\nВыберите игрока для получения информации:";

                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🏦 Banker", "GetBankerData|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData|" + tableType),
                                    InlineKeyboardButton.WithCallbackData("👤Manager-2", "GetManagerBData|" + tableType)
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
                                    InlineKeyboardButton.WithCallbackData("📝 Show command in list", "ShowListTeam|" + tableType),
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Show table image", "TableImage|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableRU,
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });
                        if (giversVerfed)
                            verf = @"✅ All Givers are confirmed\!";
                        else verf = @"❌ Not all Givers are verified\!";
                        caption = "*Welcome to*" + "\n" +
                                  $@"*{TableProfile.GetTableType(userData.playerData,tableType)} table\!*" + "\n" +
                                  $"*Table ID: * {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Total givers on the table:* {giverCount} of 4" + "\n" +
                                  $@"*Your Role:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}\-{num}" +
                                  "\n\nSelect a player to view info:";
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🏦 Banquier", "GetBankerData|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("👤 Gestionnaire-1", "GetManagerAData|" + tableType),
                                    InlineKeyboardButton.WithCallbackData("👤Gestionnaire-2", "GetManagerBData|" + tableType)
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
                                    InlineKeyboardButton.WithCallbackData("📝Afficher la commande dans la liste", "ShowListTeam|" + tableType),
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Afficher l'image du tableau", "TableImage|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableFR,
                                    InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                }
                            });
                        if (giversVerfed)
                            verf = @"✅ Tous les Donneurs sont confirmés\!";
                        else verf = @"❌ Tous les Donneurs ne sont pas vérifiés\!";
                        caption = "*Bienvenue à table*" + "\n" +
                                  $@"*{TableProfile.GetTableType(userData.playerData,tableType)}\!*" + "\n" +
                                  $"\n*ID de table: * {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Total des donateurs sur la table:* {giverCount} sur 4" + "\n" +
                                  $@"*Votre rôle:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}\-{num}" +
                                  "\n\nSélectionnez un joueur pour afficher les informations:";
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🏦 Banker", "GetBankerData|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData|" + tableType),
                                    InlineKeyboardButton.WithCallbackData("👤 Manager-2", "GetManagerBData|" + tableType)
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
                                    InlineKeyboardButton.WithCallbackData("📝 Befehl in Liste anzeigen", "ShowListTeam|" + tableType),
                                },
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🖼 Tabellenbild anzeigen", "TableImage|" + tableType)
                                },
                                new[]
                                {
                                    InlineKeyboardButtonChooseTableDE,
                                    InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                }
                            });
                        if (giversVerfed)
                            verf = @"✅ Alle Geber sind bestätigt\!";
                        else verf = @"❌ Nicht alle Geber sind verifiziert\!";
                        caption = "*Willkommen am*" + "\n" +
                                  $@"*{TableProfile.GetTableType(userData.playerData,tableType)} tisch\!*" + "\n" +
                                  $@"*Tabellen\-ID: * {tableData.tableData.tableID}" +
                                  $"\n\n*{verf}*" +
                                  $"\n\n*Gesamtzahl der Geber auf dem Tisch:* {giverCount} von 4" + "\n" +
                                  $@"*Ihre Rolle:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}\-{num}" +
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
                        caption = $"*Добро пожаловать на {TableProfile.GetTableType(userData.playerData,tableType)}*" +
                                  $"\n\n*ID стола:* {tableData.tableData.tableID}" +
                                  "\n\n*Фиксированный курс:*\n" +
                                  @"📈  1$ \= 0\.98€ \= 62₽" + 
                                  "\n\n" +
                                  $@"Вы дарите  финансовый подарок в размере {giftSum}$ на игровом столе\! " + "\n" +
                                  @"Узнайте реквизиты и сделайте финансовый подарок игроку\. 🎁 " +
                                  "\n\n" +
                                  @"Связаться с Банкиром можно через чат Telegram, нажав кнопку «*Связаться с Банкиром*»\. 📨" +
                                  "\n\n👇 Теперь просто нажмите на текст ниже  и отправьте его Банкиру: " + "\n" +
                                  $@"«`Привет\! 👋 Хочу подарить тебе подарок ${giftSum} 💸`»" +
                                  "\n\n\n" +
                                  @"_После того, как Вы выполнили условия, Банкир подтверждает Вас на столе, тем самым активирует Вас, как Дарителя_\.";
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
                        caption = $"*Welcome to {TableProfile.GetTableType(userData.playerData,tableType)}*" +
                                  $"\n\n*Table ID:* {tableData.tableData.tableID}" +
                                  "\n\n*Fixed exchange rate:*" + "\n" +
                                  @"📈  1$ \= 0\.98€ \= 62₽" +
                                  "\n\n" +
                                  $@"You give a cash financial gift in the amount of {giftSum}$ on the gaming table\!" + "\n" +
                                  @"Find out the details of the banker and make a financial gift to the player\. 🎁" +
                                  "\n\n" +
                                  @"You can contact the Banker via the Telegram chat by clicking the «*Contact the Banker*» button\. 📨" +
                                  "\n\n👇 Now just click on the text below and send it to the Banker:" + "\n" +
                                  $@"«`Hi\! 👋 I want to give you ${giftSum} as a gift 💸`»" +
                                  "\n\n\n" +
                                  @"_After you have fulfilled the conditions, the Banker confirms you on the table, thereby activating you as a Giver_\.";
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
                        caption = $"*Bienvenue à {TableProfile.GetTableType(userData.playerData,tableType)}*" +
                                  $"\n\n*Identifiant du tableau:* {tableData.tableData.tableID}" +
                                  "\n\n*Taux de change fixe:*" + "\n" +
                                  @"📈  1$ \= 0\.98€ \= 62₽" +
                                  "\n\n" +
                                  $@"Vous offrez un cadeau financier en espèces d'un montant de {giftSum}$ sur la table de jeu\!" + "\n" +
                                  @"Découvrez les coordonnées du banquier et faites un don financier au joueur\. 🎁" +
                                  "\n\n" +
                                  @"Vous pouvez contacter le banquier via le chat Telegram en cliquant sur le bouton «*Contacter le banquier*»\. 📨" +
                                  "\n\n" +
                                  @"👇 Maintenant, cliquez simplement sur le texte ci\-dessous et envoyez\-le au banquier:" + "\n" +
                                  $@"«`Salut\! 👋 Je veux vous offrir ${giftSum} en cadeau 💸`»" +
                                  "\n\n\n" +
                                  @"_Une fois que vous avez rempli les conditions, le banquier vous confirme sur la table, vous activant ainsi en tant que donneur_\.";
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
                        
                        caption = $"*Willkommen bei {TableProfile.GetTableType(userData.playerData,tableType)}*" +
                                  "\n\n" +
                                  $@"*Tabellen\-ID:* {tableData.tableData.tableID}" +
                                  "\n\n*Fester Wechselkurs:*" + "\n" +
                                  @"📈  1$ \= 0\.98€ \= 62₽" +
                                  "\n\n\n" +
                                  $@"Du verschenkst ein Geldgeschenk in Höhe von {giftSum}$ auf dem Spieltisch\!" + "\n" +
                                  @"Finden Sie die Details des Bankiers heraus und machen Sie dem Spieler ein finanzielles Geschenk\. 🎁" + 
                                  "\n\n" +
                                  @"Sie können den Banker über den Telegramm\-Chat kontaktieren, indem Sie auf die Schaltfläche «*Banker kontaktieren*» klicken\. 📨" +
                                  "\n\n👇 Klicken Sie jetzt einfach auf den folgenden Text und senden Sie ihn an den Banker:" + "\n" +
                                  $@"«`Hi\! 👋 Ich möchte dir ${giftSum} schenken 💸`»" +
                                  "\n\n\n" +
                                  @"_Nachdem Sie die Bedingungen erfüllt haben, bestätigt Sie der Banker auf dem Tisch und aktiviert Sie dadurch als Geber_\.";
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
                ParseMode.MarkdownV2,
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
            UserData? tableData = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_copper), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.bronze:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_bronze), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.silver:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_silver), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.gold:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_gold), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.platinum:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_platinum), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.diamond:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_diamond), WebManager.RequestType.GetTableData);
                    break;
            }
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
            InlineKeyboardButton inlineKeyboardButtonManagerAInfo;
            InlineKeyboardButton inlineKeyboardButtonManagerBInfo;
            if (tableData.tableData.managerA_ID == userData.playerData.id)
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonManagerAInfo = 
                            InlineKeyboardButton.WithCallbackData($"👤 Менеджер-1 🔘", "GetManagerAData");
                        inlineKeyboardButtonManagerBInfo = InlineKeyboardButton.WithCallbackData("👤 Менеджер-2", "GetManagerBData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonManagerAInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Manager-1 🔘", "GetManagerAData");
                        inlineKeyboardButtonManagerBInfo = InlineKeyboardButton.WithCallbackData("👤Manager-2", "GetManagerBData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonManagerAInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Gestionnaire-1 🔘", "GetManagerAData");
                        inlineKeyboardButtonManagerBInfo = InlineKeyboardButton.WithCallbackData("👤Gestionnaire-2", "GetManagerBData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonManagerAInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Manager-1 🔘", "GetManagerAData");
                        inlineKeyboardButtonManagerBInfo = InlineKeyboardButton.WithCallbackData("👤 Manager-2", "GetManagerBData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonManagerAInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Manager-1 🔘", "GetManagerAData");
                        inlineKeyboardButtonManagerBInfo = InlineKeyboardButton.WithCallbackData("👤Manager-2", "GetManagerBData|" + tableType);
                        break;
                }
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonManagerBInfo = 
                            InlineKeyboardButton.WithCallbackData($"👤 Менеджер-2 🔘", "GetManagerBData");
                        inlineKeyboardButtonManagerAInfo = InlineKeyboardButton.WithCallbackData("👤 Менеджер-1", "GetManagerAData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonManagerBInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Manager-2 🔘", "GetManagerBData");
                        inlineKeyboardButtonManagerAInfo = InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonManagerBInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Gestionnaire-2 🔘", "GetManagerBData");
                        inlineKeyboardButtonManagerAInfo = InlineKeyboardButton.WithCallbackData("👤 Gestionnaire-1", "GetManagerAData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonManagerBInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Manager-2 🔘", "GetManagerBData");
                        inlineKeyboardButtonManagerAInfo = InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonManagerBInfo =
                            InlineKeyboardButton.WithCallbackData($"👤 Manager-2 🔘", "GetManagerBData");
                        inlineKeyboardButtonManagerAInfo = InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData|" + tableType);
                        break;
                }
            }
            if (tableData.tableData.giverA_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverA_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_A)
                {
                    inlineKeyboardButtonGiverAInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅",
                            "GetGiverAData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverAInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverAData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-1", "GetGiverAData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-1", "GetGiverAData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-1", "GetGiverAData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData|" + tableType);
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
                            "GetGiverBData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverBInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverBData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-2", "GetGiverBData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-2", "GetGiverBData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-2", "GetGiverBData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData|" + tableType);
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
                            "GetGiverCData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverCInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverCData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-3", "GetGiverCData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-3", "GetGiverCData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-3", "GetGiverCData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData|" + tableType);
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
                            "GetGiverDData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverDInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverDData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-4", "GetGiverDData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-4", "GetGiverDData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-4", "GetGiverDData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData|" + tableType);
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
                                InlineKeyboardButton.WithCallbackData("🏦 Банкир", "GetBankerData|" + tableType)
                            },
                            new[]
                            {
                                inlineKeyboardButtonManagerAInfo,
                                inlineKeyboardButtonManagerBInfo
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
                                InlineKeyboardButton.WithCallbackData("📝 Показать команду списком", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Показать стол картинкой", "TableImage|" + tableType)
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
                              $"\n*{TableProfile.GetTableType(userData.playerData,tableType)} стол!*" +
                              $"\n*ID стола: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Всего дарителей на столе:* {giverCount} из 4" +
                              $"\n*Ваша роль:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
                              "\n\nВыберите игрока для получения информации:";

                    break;
                case "eng":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🏦 Banker", "GetBankerData|" + tableType)
                            },
                            new[]
                            {
                                inlineKeyboardButtonManagerAInfo,
                                inlineKeyboardButtonManagerBInfo
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
                                InlineKeyboardButton.WithCallbackData("📝 Show command in list", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Show table image", "TableImage|" + tableType)
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
                              $"\n*{TableProfile.GetTableType(userData.playerData,tableType)} table!*" +
                              $"*Table ID: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Total givers on the table:* {giverCount} of 4" +
                              $"\n*Your Role:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
                              "\n\nSelect a player to view info:";
                    break;
                case "fr":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🏦 Banquier", "GetBankerData|" + tableType)
                            },
                            new[]
                            {
                                inlineKeyboardButtonManagerAInfo,
                                inlineKeyboardButtonManagerBInfo
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
                                InlineKeyboardButton.WithCallbackData("📝Afficher la commande dans la liste", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Afficher l'image du tableau", "TableImage|" + tableType)
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
                              $"\n*{TableProfile.GetTableType(userData.playerData,tableType)}!*" +
                              $"\n*ID de table: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Total des donateurs sur la table:* {giverCount} sur 4" +
                              $"\n*Votre rôle:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
                              "\n\nSélectionnez un joueur pour afficher les informations:";
                    break;
                case "de":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🏦 Banker", "GetBankerData|" + tableType)
                            },
                            new[]
                            {
                                inlineKeyboardButtonManagerAInfo,
                                inlineKeyboardButtonManagerBInfo
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
                                InlineKeyboardButton.WithCallbackData("📝 Befehl in Liste anzeigen", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Tabellenbild anzeigen", "TableImage|" + tableType)
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
                              $"\n*{TableProfile.GetTableType(userData.playerData,tableType)} tisch!*" +
                              $"*Tabellen-ID: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Gesamtzahl der Geber auf dem Tisch:* {giverCount} von 4" +
                              $"\n*Ihre Rolle:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
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
        public static async void Banker(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            Table.TableType tableType,
            UserData userData)
        {
            UserData? tableData = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_copper), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.bronze:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_bronze), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.silver:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_silver), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.gold:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_gold), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.platinum:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_platinum), WebManager.RequestType.GetTableData);
                    break;
                case Table.TableType.diamond:
                    tableData = await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_diamond), WebManager.RequestType.GetTableData);
                    break;
            }
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
            InlineKeyboardButton inlineKeyboardButtonBankerInfo = null;
            if (tableData.tableData.bankerID == userData.playerData.id)
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonBankerInfo = 
                            InlineKeyboardButton.WithCallbackData($"🏦 Банкир 🔘", "GetBankerData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonBankerInfo =
                            InlineKeyboardButton.WithCallbackData($"🏦 Banker 🔘", "GetBankerData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonBankerInfo =
                            InlineKeyboardButton.WithCallbackData($"🏦 Banquier 🔘", "GetBankerData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonBankerInfo =
                            InlineKeyboardButton.WithCallbackData($"🏦 Banker 🔘", "GetBankerData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonBankerInfo =
                            InlineKeyboardButton.WithCallbackData($"🏦 Banker 🔘", "GetBankerData|" + tableType);
                        break;
                }
            }
            if (tableData.tableData.giverA_ID != null)
            {
                giverInfo = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverA_ID),
                    WebManager.RequestType.GetUserData);
                if (tableData.tableData.verf_A)
                {
                    inlineKeyboardButtonGiverAInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ✅",
                            "GetGiverAData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverAInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverAData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-1", "GetGiverAData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-1", "GetGiverAData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-1", "GetGiverAData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverAInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-1", "GetGiverAData|" + tableType);
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
                            "GetGiverBData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverBInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverBData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-2", "GetGiverBData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-2", "GetGiverBData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-2", "GetGiverBData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverBInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-2", "GetGiverBData|" + tableType);
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
                            "GetGiverCData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverCInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverCData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-3", "GetGiverCData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-3", "GetGiverCData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-3", "GetGiverCData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverCInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-3", "GetGiverCData|" + tableType);
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
                            "GetGiverDData|" + tableType);
                }
                else
                {
                    inlineKeyboardButtonGiverDInfo =
                        InlineKeyboardButton.WithCallbackData($"🎁 @{giverInfo.playerData.username} ❌",
                            "GetGiverDData|" + tableType);
                }

                giverCount++;
            }
            else
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Даритель-4", "GetGiverDData|" + tableType);
                        break;
                    case "eng":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData|" + tableType);
                        break;
                    case "fr":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Donneur-4", "GetGiverDData|" + tableType);
                        break;
                    case "de":
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Geber-4", "GetGiverDData|" + tableType);
                        break;
                    default:
                        inlineKeyboardButtonGiverDInfo =
                            InlineKeyboardButton.WithCallbackData($"🎁 Giver-4", "GetGiverDData|" + tableType);
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
                                inlineKeyboardButtonBankerInfo
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("👤 Менеджер-1", "GetManagerAData|" + tableType),
                                InlineKeyboardButton.WithCallbackData("👤 Менеджер-2", "GetManagerBData|" + tableType)
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
                                InlineKeyboardButton.WithCallbackData("📝 Показать команду списком", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Показать стол картинкой", "TableImage|" + tableType)
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU,
                                InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                            }
                        }!);


                    if (giversVerfed)
                        verf = "✅ Все дарители подтверждены!";
                    else verf = "❌ Не все дарители подтверждены!";
                    caption = "*Добро пожаловать на*" +
                              $"\n*{TableProfile.GetTableType(userData.playerData, tableType)} стол!*" +
                              $"\n*ID стола: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Всего дарителей на столе:* {giverCount} из 4" +
                              $"\n*Ваша роль:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
                              "\n\nВыберите игрока для получения информации:";

                    break;
                case "eng":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                inlineKeyboardButtonBankerInfo
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData|" + tableType),
                                InlineKeyboardButton.WithCallbackData("👤Manager-2", "GetManagerBData|" + tableType)
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
                                InlineKeyboardButton.WithCallbackData("📝 Show command in list", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Show table image", "TableImage|" + tableType)
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableENG,
                                InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                            }
                        }!);
                    if (giversVerfed)
                        verf = "✅ All Givers are confirmed!";
                    else verf = "❌ Not all Givers are verified!";
                    caption = "*Welcome to*" +
                              $"\n*{TableProfile.GetTableType(userData.playerData,tableType)} table!*" +
                              $"*Table ID: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Total givers on the table:* {giverCount} of 4" +
                              $"\n*Your Role:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
                              "\n\nSelect a player to view info:";
                    break;
                case "fr":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                inlineKeyboardButtonBankerInfo
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("👤 Gestionnaire-1", "GetManagerAData|" + tableType),
                                InlineKeyboardButton.WithCallbackData("👤Gestionnaire-2", "GetManagerBData|" + tableType)
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
                                InlineKeyboardButton.WithCallbackData("📝Afficher la commande dans la liste", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Afficher l'image du tableau", "TableImage|" + tableType)
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR,
                                InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                            }
                        }!);
                    if (giversVerfed)
                        verf = "✅ Tous les Donneurs sont confirmés!";
                    else verf = "❌ Tous les Donneurs ne sont pas vérifiés!";
                    caption = "*Bienvenue à table*" +
                              $"\n*{TableProfile.GetTableType(userData.playerData,tableType)}!*" +
                              $"\n*ID de table: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Total des donateurs sur la table:* {giverCount} sur 4" +
                              $"\n*Votre rôle:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
                              "\n\nSélectionnez un joueur pour afficher les informations:";
                    break;
                case "de":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                inlineKeyboardButtonBankerInfo
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("👤 Manager-1", "GetManagerAData|" + tableType),
                                InlineKeyboardButton.WithCallbackData("👤 Manager-2", "GetManagerBData|" + tableType)
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
                                InlineKeyboardButton.WithCallbackData("📝 Befehl in Liste anzeigen", "ShowListTeam|" + tableType),
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🖼 Tabellenbild anzeigen", "TableImage|" + tableType)
                            },
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE,
                                InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                            }
                        }!);
                    if (giversVerfed)
                        verf = "✅ Alle Geber sind bestätigt!";
                    else verf = "❌ Nicht alle Geber sind verifiziert!";
                    caption = "*Willkommen am*" +
                              $"\n*{TableProfile.GetTableType(userData.playerData,tableType)} tisch!*" +
                              $"*Tabellen-ID: * {tableData.tableData.tableID}" +
                              $"\n\n*{verf}*" +
                              $"\n\n*Gesamtzahl der Geber auf dem Tisch:* {giverCount} von 4" +
                              $"\n*Ihre Rolle:* {userData.playerData.GetTableRole(userData.playerData.lang, tableType)}" +
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

        private static async void RoleSelection(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData,
            Table.TableType tableType)
        {
            userData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData);
            switch (tableType)
            {
                case Table.TableType.copper:
                    switch (userData.playerData.UserTableList.copperTableRole)
                    {
                        case Table.TableRole.giver:
                            Giver(botClient, chatId, callbackData, Table.TableType.copper, userData);
                            break;
                        case Table.TableRole.manager:
                            Manager(botClient, chatId, callbackData, Table.TableType.copper, userData);
                            break;
                        case Table.TableRole.banker:
                            Banker(botClient, chatId, callbackData, Table.TableType.copper, userData);
                            break;
                    }
                    break;
                case Table.TableType.bronze:
                    switch (userData.playerData.UserTableList.bronzeTableRole)
                    {
                        case Table.TableRole.giver:
                            Giver(botClient, chatId, callbackData, Table.TableType.bronze, userData);
                            break;
                        case Table.TableRole.manager:
                            Manager(botClient, chatId, callbackData, Table.TableType.bronze, userData);
                            break;
                        case Table.TableRole.banker:
                            Banker(botClient, chatId, callbackData, Table.TableType.bronze, userData);
                            break;
                    }
                    break;
                case Table.TableType.silver:
                    switch (userData.playerData.UserTableList.silverTableRole)
                    {
                        case Table.TableRole.giver:
                            Giver(botClient, chatId, callbackData, Table.TableType.silver, userData);
                            break;
                        case Table.TableRole.manager:
                            Manager(botClient, chatId, callbackData, Table.TableType.silver, userData);
                            break;
                        case Table.TableRole.banker:
                            Banker(botClient, chatId, callbackData, Table.TableType.silver, userData);
                            break;
                    }
                    break;
                case Table.TableType.gold:
                    switch (userData.playerData.UserTableList.goldTableRole)
                    {
                        case Table.TableRole.giver:
                            Giver(botClient, chatId, callbackData, Table.TableType.gold, userData);
                            break;
                        case Table.TableRole.manager:
                            Manager(botClient, chatId, callbackData, Table.TableType.gold, userData);
                            break;
                        case Table.TableRole.banker:
                            Banker(botClient, chatId, callbackData, Table.TableType.gold, userData);
                            break;
                    }
                    break;
                case Table.TableType.platinum:
                    switch (userData.playerData.UserTableList.platinumTableRole)
                    {
                        case Table.TableRole.giver:
                            Giver(botClient, chatId, callbackData, Table.TableType.platinum, userData);
                            break;
                        case Table.TableRole.manager:
                            Manager(botClient, chatId, callbackData, Table.TableType.platinum, userData);
                            break;
                        case Table.TableRole.banker:
                            Banker(botClient, chatId, callbackData, Table.TableType.platinum, userData);
                            break;
                    }
                    break;
                case Table.TableType.diamond:
                    switch (userData.playerData.UserTableList.diamondTableRole)
                    {
                        case Table.TableRole.giver:
                            Giver(botClient, chatId, callbackData, Table.TableType.diamond, userData);
                            break;
                        case Table.TableRole.manager:
                            Manager(botClient, chatId, callbackData, Table.TableType.diamond, userData);
                            break;
                        case Table.TableRole.banker:
                            Banker(botClient, chatId, callbackData, Table.TableType.diamond, userData);
                            break;
                    }
                    break;
            }
            
        }

        public static async void Copper(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData) 
        {
            userData.playerData.level_tableType = Table.TableType.copper;
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
            userData.playerData.level_tableType = Table.TableType.bronze;
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
            userData.playerData.level_tableType = Table.TableType.silver;
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
            userData.playerData.level_tableType = Table.TableType.gold;
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
            userData.playerData.level_tableType = Table.TableType.platinum;
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
            userData.playerData.level_tableType = Table.TableType.diamond;
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