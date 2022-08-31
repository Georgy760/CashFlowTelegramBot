﻿using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.ImageEditor;
using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace CashFlowTelegramBot.Skywards.Telegram;

public static class UpdateHandlers
{
    public static Task PollingErrorHandler(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Trace.Write(ErrorMessage);
        return Task.CompletedTask;
    }


    public static async Task StartButton(ITelegramBotClient botClient, KeyboardButton button)
    {
        new KeyboardButton("Start");
    }


    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        var handler = update.Type switch
        {
            // UpdateType.Unknown:  
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            // UpdateType.Poll:
            UpdateType.Message => BotOnMessageReceived(botClient, update.Message!),
            //UpdateType.EditedMessage      => BotOnMessageReceived(botClient, update.EditedMessage!),
            UpdateType.CallbackQuery => BotOnCallbackQueryReceived(botClient, update.CallbackQuery!),
            UpdateType.InlineQuery => BotOnInlineQueryReceived(botClient, update.InlineQuery!),
            UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(botClient, update.ChosenInlineResult!),
            _ => UnknownUpdateHandlerAsync(botClient, update)
        };

        try
        {
            await handler;
        }
#pragma warning disable CA1031
        catch (Exception exception)
#pragma warning restore CA1031
        {
            await PollingErrorHandler(botClient, exception, cancellationToken);
        }
    }

    private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message updateMessage)
    {
        Trace.Write($"\n{updateMessage.From.Id}");
        Trace.Write("MessageReceived: " + updateMessage.Text);
        if (updateMessage.Text.Contains("/chat"))
        {
            Trace.Write("ChatID: " + updateMessage.Chat.Id);
            var chatID = 237487193;
            Trace.Write("Data: " + botClient.GetChatAsync(chatID).Result.FirstName);
            var user = new UserProfile(updateMessage.From.Id, updateMessage.From.Username!);
            var userData = await WebManager.SendData(user, WebManager.RequestType.GetUserData, true);
        }

        if (updateMessage.From.Id ==  524338144 || updateMessage.From.Id ==  237487193 || updateMessage.From.Id == 5680187538) //ADMINS
        {
            if (updateMessage.Text.Contains("/update"))
            {
                //var test = await botClient.GetFileAsync(updateMessage.Photo[updateMessage.Photo.Count() - 1].FileId);

                //var image = Bitmap.FromStream(test.FileStream);

                //image.Save(@"C:\\Users\xxx\Desktop\test.png");
                var message = updateMessage.Text;
                message = message.Remove(0, 7);
                
                Trace.WriteLine("\n" + message);
                var data = await WebManager.SendData(new UserData(), WebManager.RequestType.GetAllUsersID, false);
                InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌", "MainMenu")
                        }
                    });
                if (data.updateData.Count > 0)
                {
                    //updateMessage.Video.
                    string path = null;
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            @"Images/MainMenu/mainMenu.png");

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                        path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                            @"Images\MainMenu\mainMenu.png");
                    foreach (var ID in data.updateData)
                    {
                        long chatID = ID;
                        try
                        {

                            var chat = botClient.GetChatAsync(chatID);
                            Trace.WriteLine($"\nID:[{chat.Result.Id}] username: [{chat.Result.Username}]");
                            chatID = chat.Result.Id;
                            if (chatID != null)
                            {
                                await botClient.SendPhotoAsync(
                                    chatID,
                                    File.OpenRead(path),
                                    message,
                                    ParseMode.Html,
                                    replyMarkup: inlineKeyboard);
                            }
                        }
                        catch(AggregateException aex)
                        {
                            try
                            {
                                var user = await WebManager.SendData(new UserProfile(chatID),
                                    WebManager.RequestType.GetUserData, false);
                                Trace.WriteLine($"\nCannot send message to - ID:[{user.playerData.id}] username: [@{user.playerData.username}]");
                            }
                            catch
                            {
                                Trace.WriteLine($"Someting wrong with ID: [{chatID}]");
                            }

                            //Trace.WriteLine("Handle Remaining Exceptions");
                            aex.Handle(ex => Exceptions.HandleException(ex));
                        }

                        
                    }
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("❌", "Close")
                            }
                        });
                     await botClient.SendPhotoAsync(
                         updateMessage.From.Id,
                         File.OpenRead(path),
                         "Update completed",
                         ParseMode.Html,
                         replyMarkup: inlineKeyboard);
                    Trace.WriteLine("\nUpdate Sent");
                }
            }
        }

        /*if (updateMessage.Text.Contains("/start"))
        {
            Languages.RegLanguageMenu(botClient, updateMessage.Chat.Id);
        }*/
        if (updateMessage.Text.Contains("/menu"))
        {
            var user = await WebManager.SendData(new UserProfile(updateMessage.From.Id), WebManager.RequestType.GetUserData, true);
            if (!user.error.isError)
            {
                Languages.MainMenu(botClient, updateMessage.Chat.Id, user.playerData.lang);
            }
        }
        if (updateMessage.Text.Contains("/start R"))
        {
            var splitMessage = updateMessage.Text.Split("R");
            var refId = int.Parse(splitMessage[1]);
            var user = new UserProfile(updateMessage.From.Id, refId, updateMessage.From.Username);
            switch (updateMessage.From.LanguageCode)
            {
                case "ru":
                    user.AddLang("ru");
                    break;
                case "en":
                    user.AddLang("eng");
                    break;
                case "fr":
                    user.AddLang("fr");
                    break;
                case "de":
                    user.AddLang("de");
                    break;
            }

            CallbackQuery callbackQuery = new CallbackQuery();
            callbackQuery.Data = "null";
            if (updateMessage.From.Username != null)
            {
                var error = await WebManager.SendData(user, WebManager.RequestType.RegisterWithRef, true);
                await botClient.DeleteMessageAsync(updateMessage.Chat.Id, updateMessage.MessageId);
                if (error.error.errorText != "RefLink invalid")
                {
                    Languages.RegLanguageMenu(botClient, updateMessage.Chat.Id);
                }
                else
                {
                    Languages.Warning(botClient, updateMessage.Chat.Id, callbackQuery, user, Error.RefLinkInvalid, null);
                }
            }
            else
            {
                Languages.Warning(botClient, updateMessage.Chat.Id, callbackQuery, user, Error.UserWithoutUsername, null);
            }
        }
    }

    // Process Inline Keyboard callback data
    private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        Trace.Write("\nCallbackQuery.from.Username : " + callbackQuery.From.Username);
        Trace.Write($"\nCallbackQuery.from.Id : [{ callbackQuery.From.Id}]" + callbackQuery.From.Id);
        Trace.Write("\nCallbackQuery.Data : " + callbackQuery.Data);
        var user = new UserProfile(callbackQuery.From.Id, callbackQuery.From.Username!);
        var userData = await WebManager.SendData(user, WebManager.RequestType.GetUserData, true);
        if (userData.playerData.UserTableList != null)
        {
            if (userData.playerData.UserTableList.table_ID_copper != null)
            {
                var data = await WebManager.SendData(
                    new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                    WebManager.RequestType.GetTableData, false);
                if (data.notification.isNotify)
                {
                    Notifications.Notify(botClient, user.id, data.notification);
                }
            }
            if (userData.playerData.UserTableList.table_ID_bronze != null)
            {
                var data = await WebManager.SendData(
                    new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                    WebManager.RequestType.GetTableData, false);
                if (data.notification.isNotify)
                {
                    Notifications.Notify(botClient, user.id, data.notification);
                }
            }
            if (userData.playerData.UserTableList.table_ID_silver != null)
            {
                var data = await WebManager.SendData(
                    new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                    WebManager.RequestType.GetTableData, false);
                if (data.notification.isNotify)
                {
                    Notifications.Notify(botClient, user.id, data.notification);
                }
            }
            if (userData.playerData.UserTableList.table_ID_gold != null)
            {
                var data = await WebManager.SendData(
                    new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                    WebManager.RequestType.GetTableData, false);
                if (data.notification.isNotify)
                {
                    Notifications.Notify(botClient, user.id, data.notification);
                }
            }
            if (userData.playerData.UserTableList.table_ID_platinum != null)
            {
                var data = await WebManager.SendData(
                    new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                    WebManager.RequestType.GetTableData, false);
                if (data.notification.isNotify)
                {
                    Notifications.Notify(botClient, user.id, data.notification);
                }
            }
            if (userData.playerData.UserTableList.table_ID_diamond != null)
            {
                var data = await WebManager.SendData(
                    new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                    WebManager.RequestType.GetTableData, false);
                if (data.notification.isNotify)
                {
                    Notifications.Notify(botClient, user.id, data.notification);
                }
            }
            
        }
        Trace.Write("\n---------------------------------------------------"
                          + $"\nTriggered by: [{callbackQuery.From.Id}] " + userData.playerData.username + " at " + DateTime.Now +
                          "\nCallBackQuery: " +
                          callbackQuery.Data
                          + "\n---------------------------------------------------");
        var chatId = callbackQuery.Message.Chat.Id;
        if (callbackQuery.Data.Contains("Captcha"))
        {
            var data = callbackQuery.Data.Split("|");
            if (data[data.Length - 1].Contains("CaptchaTrue"))
            {
                callbackQuery.Data = data[0].Replace("Captcha", "");
                ;
                Trace.Write(callbackQuery.Data);
            }
            else
            {
                callbackQuery.Data = data[0].Replace("Captcha", "");
                await Languages.Captcha(botClient, chatId, callbackQuery);
                return;
            }
        }

        if (callbackQuery.Data.Contains("TryToReg"))
        {
            var refIdString = callbackQuery.Data.Split("|");
            var refId = Int32.Parse(refIdString[0]);
            var NewUser = new UserProfile(callbackQuery.From.Id, refId, callbackQuery.From.Username);
            switch (callbackQuery.From.LanguageCode)
            {
                case "ru":
                    NewUser.AddLang("ru");
                    break;
                case "en":
                    NewUser.AddLang("eng");
                    break;
                case "fr":
                    NewUser.AddLang("fr");
                    break;
                case "de":
                    NewUser.AddLang("de");
                    break;
            }

            if (callbackQuery.From.Username == null)
            {
                Trace.Write("Username is still null");
                Languages.Warning(botClient, chatId, callbackQuery, NewUser, Error.UserWithoutUsername, null);
                return;
            }

            var error = await WebManager.SendData(NewUser, WebManager.RequestType.RegisterWithRef, true);
            if (error.error.errorText != "RefLink invalid")
            {
                Languages.RegLanguageMenu(botClient, chatId);
            }
            else
            {
                Languages.Warning(botClient, chatId, callbackQuery, NewUser, Error.RefLinkInvalid, null);
            }
        }

        if (callbackQuery.Data.Contains("GetBankerData"))
        {
            Trace.Write("\nGetBankerData");
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var tableRole = Table.TableRole.banker;
            if (tableType != null)
            {
                UserData? tableData;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.bankerID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.bankerID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.bronze:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.bankerID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.bankerID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.silver:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.bankerID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.bankerID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.gold:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.bankerID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.bankerID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.platinum:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.bankerID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.bankerID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.diamond:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.bankerID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.bankerID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                }
            }
        }

        if (callbackQuery.Data.Contains("GetManagerAData"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var tableRole = Table.TableRole.manager;
            if (tableType != null)
            {
                UserData? tableData;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.bronze:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.silver:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.gold:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.platinum:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.diamond:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                }
            }
        }

        if (callbackQuery.Data.Contains("GetManagerBData"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var tableRole = Table.TableRole.manager;
            if (tableType != null)
            {
                UserData? tableData;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.bronze:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.silver:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.gold:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.platinum:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.diamond:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.managerB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                }
            }
        }

        if (callbackQuery.Data.Contains("GetGiverAData"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var tableRole = Table.TableRole.giver;
            if (tableType != null)
            {
                UserData? tableData;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.bronze:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.silver:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.gold:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.platinum:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.diamond:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverA_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverA_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                }
            }
        }

        if (callbackQuery.Data.Contains("GetGiverBData"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var tableRole = Table.TableRole.giver;
            if (tableType != null)
            {
                UserData? tableData;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.bronze:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.silver:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.gold:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.platinum:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.diamond:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverB_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                }
            }
        }

        if (callbackQuery.Data.Contains("GetGiverCData"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var tableRole = Table.TableRole.giver;
            if (tableType != null)
            {
                UserData? tableData;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverC_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverC_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.bronze:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverC_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverC_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.silver:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverC_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverC_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.gold:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverC_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverC_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.platinum:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverC_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverC_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.diamond:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverC_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverC_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                }
            }
        }

        if (callbackQuery.Data.Contains("GetGiverDData"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var tableRole = Table.TableRole.giver;
            if (tableType != null)
            {
                UserData? tableData;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.managerB_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverD_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.bronze:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverD_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverD_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.silver:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverD_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverD_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.gold:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverD_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverD_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.platinum:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverD_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverD_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                    case Table.TableType.diamond:
                        tableData = await WebManager.SendData(
                            new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.giverD_ID != null)
                        {
                            var data = await WebManager.SendData(new UserProfile( tableData.tableData.giverD_ID),
                                WebManager.RequestType.GetUserData, true);
                            Languages.GetUserData(botClient, chatId, callbackQuery, userData.playerData,
                                data.playerData, tableType);
                        }
                        else
                        {
                            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserIsNotExist, tableType);
                        }

                        break;
                }
            }
        }

        if (callbackQuery.Data.Contains("ShowListTeam"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.ShowListTeam(botClient, chatId, callbackQuery, userData.playerData.lang,
                userData.playerData, tableType);
        }

        if (callbackQuery.Data.Contains("TableImage"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);

            Languages.ShowTableAsImage(botClient, chatId, callbackQuery, userData.playerData, tableType);
        }

        if (callbackQuery.Data.Contains("LeaveTable"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.LeaveTable);
        }

        if (callbackQuery.Data.Contains("ConfirmLeaveTable"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            await ConfirmLeaveTable(botClient, callbackQuery, userData, tableType);
        }

        if (callbackQuery.Data.Contains("RemoveFromTableGiverA"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.RemoveFromTable);
        }

        if (callbackQuery.Data.Contains("ConfirmRemoveFromTableGiverA"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? dataToRemove = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToRemove = new UserProfile( dataToRemove.tableData.giverA_ID);
            if (dataToRemove.tableData.tableID != null && userToRemove.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToRemove, dataToRemove.tableData),
                    WebManager.RequestType.RemoveFromTable, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                SelectByTableType(botClient, callbackQuery, userData, tableType);
            }
        }

        if (callbackQuery.Data.Contains("RemoveFromTableGiverB"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.RemoveFromTable);
        }

        if (callbackQuery.Data.Contains("ConfirmRemoveFromTableGiverB"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? dataToRemove = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToRemove = new UserProfile( dataToRemove.tableData.giverB_ID);
            if (dataToRemove.tableData.tableID != null && userToRemove.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToRemove, dataToRemove.tableData),
                    WebManager.RequestType.RemoveFromTable, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                SelectByTableType(botClient, callbackQuery, userData, tableType);
            }
        }

        if (callbackQuery.Data.Contains("RemoveFromTableGiverC"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.RemoveFromTable);
        }

        if (callbackQuery.Data.Contains("ConfirmRemoveFromTableGiverC"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);

            UserData? dataToRemove = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToRemove = new UserProfile( dataToRemove.tableData.giverC_ID);
            //userToRemove.id =  dataToRemove.tableData.giverC_ID;
            if (dataToRemove.tableData.tableID != null && userToRemove.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToRemove, dataToRemove.tableData),
                    WebManager.RequestType.RemoveFromTable, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                SelectByTableType(botClient, callbackQuery, userData, tableType);
            }
        }

        if (callbackQuery.Data.Contains("RemoveFromTableGiverD"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.RemoveFromTable);
        }

        if (callbackQuery.Data.Contains("ConfirmRemoveFromTableGiverD"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? dataToRemove = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToRemove = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToRemove = new UserProfile( dataToRemove.tableData.giverD_ID);
            if (dataToRemove.tableData.tableID != null && userToRemove.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToRemove, dataToRemove.tableData),
                    WebManager.RequestType.RemoveFromTable, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                SelectByTableType(botClient, callbackQuery, userData, tableType);
            }
        }

        if (callbackQuery.Data.Contains("VerfGiverA"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.Confirm);
        }

        if (callbackQuery.Data.Contains("ConfirmVerfGiverA"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? dataToConfirm = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToConfirm = new UserProfile( dataToConfirm.tableData.giverA_ID);
            if (dataToConfirm.tableData.tableID != null && userToConfirm.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToConfirm, dataToConfirm.tableData),
                    WebManager.RequestType.Confirm, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                if (!(response.error.isError && response.error.errorText == "TableCompleted"))
                    SelectByTableType(botClient, callbackQuery, userData, tableType);
                else
                    Languages.TableMenu(botClient, chatId, callbackQuery, userData.playerData);
            }
        }

        if (callbackQuery.Data.Contains("VerfGiverB"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.Confirm);
        }

        if (callbackQuery.Data.Contains("ConfirmVerfGiverB"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? dataToConfirm = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToConfirm = new UserProfile( dataToConfirm.tableData.giverB_ID);
            if (dataToConfirm.tableData.tableID != null && userToConfirm.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToConfirm, dataToConfirm.tableData),
                    WebManager.RequestType.Confirm, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                if (!(response.error.isError && response.error.errorText == "TableCompleted"))
                    SelectByTableType(botClient, callbackQuery, userData, tableType);
                else
                    Languages.TableMenu(botClient, chatId, callbackQuery, userData.playerData);
            }
        }

        if (callbackQuery.Data.Contains("VerfGiverC"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.Confirm);
        }

        if (callbackQuery.Data.Contains("ConfirmVerfGiverC"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? dataToConfirm = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToConfirm = new UserProfile( dataToConfirm.tableData.giverC_ID);
            if (dataToConfirm.tableData.tableID != null && userToConfirm.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToConfirm, dataToConfirm.tableData),
                    WebManager.RequestType.Confirm, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                if (!(response.error.isError && response.error.errorText == "TableCompleted"))
                    SelectByTableType(botClient, callbackQuery, userData, tableType);
                else
                    Languages.TableMenu(botClient, chatId, callbackQuery, userData.playerData);
            }
        }

        if (callbackQuery.Data.Contains("VerfGiverD"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            Languages.Warning(botClient, chatId, callbackQuery, userData.playerData, tableType,
                WebManager.RequestType.Confirm);
        }

        if (callbackQuery.Data.Contains("ConfirmVerfGiverD"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? dataToConfirm = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    dataToConfirm = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            UserProfile userToConfirm = new UserProfile( dataToConfirm.tableData.giverD_ID);
            if (dataToConfirm.tableData.tableID != null && userToConfirm.id != null)
            {
                var response = await WebManager.SendData(new UserData(userToConfirm, dataToConfirm.tableData),
                    WebManager.RequestType.Confirm, true);
                if (response.notification.isNotify)
                {
                    Notifications.Notify(botClient, userData.playerData.id, response.notification);
                }
                if (!(response.error.isError && response.error.errorText == "TableCompleted"))
                    SelectByTableType(botClient, callbackQuery, userData, tableType);
                else
                    Languages.TableMenu(botClient, chatId, callbackQuery, userData.playerData);
            }
        }

        if (callbackQuery.Data.Contains("NotifyBanker"))
        {
            var tableTypeData = callbackQuery.Data.Split("|");
            var tableType = TableProfile.GetTableType(tableTypeData[1]);
            var userTableList = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
            UserData? table = null;
            switch (tableType)
            {
                case Table.TableType.copper:
                    table = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.bronze:
                    table = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.silver:
                    table = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.gold:
                    table = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.platinum:
                    table = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                    break;
                case Table.TableType.diamond:
                    table = await WebManager.SendData(
                        new TableProfile(userTableList.playerData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                    break;
            }

            var notification = new Notification();
            notification.notificationText = "NotifyBanker";
            notification.tableID = table.tableData.tableID;
            notification.bankerID = table.tableData.bankerID;
            notification.isNotify = true;
            Notifications.Notify(botClient, user.id, notification);
        }
        switch (callbackQuery.Data)
        {
            //--------REG_LANG--------
            case "Close":
                await botClient.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
                break;
            case "Reg_RUCaptcha":
                await Languages.Captcha(botClient, chatId, callbackQuery);
                break;
            case "Reg_RU":
                user.AddLang("ru");
                await Agreement(botClient, callbackQuery, user);
                break;
            case "Reg_ENGCaptcha":
                await Languages.Captcha(botClient, chatId, callbackQuery);
                break;
            case "Reg_ENG":
                user.AddLang("eng");
                await Agreement(botClient, callbackQuery, user);
                break;
            case "Reg_FRCaptcha":
                await Languages.Captcha(botClient, chatId, callbackQuery);
                break;
            case "Reg_FR":
                user.AddLang("fr");
                await Agreement(botClient, callbackQuery, user);
                break;
            case "Reg_DECaptcha":
                await Languages.Captcha(botClient, chatId, callbackQuery);
                break;
            case "Reg_DE":
                user.AddLang("de");
                await Agreement(botClient, callbackQuery, user);
                break;
            //--------MAIN_MENU--------
            case "MainMenu":
                Languages.MainMenu(botClient, chatId, callbackQuery, userData.playerData.lang);
                break;
            //--------CHOOSE_TABLE--------\\
            case "ChooseTable":
                Trace.Write("ChooseTable");
                Languages.TableMenu(botClient, chatId, callbackQuery, userData.playerData);
                break;
            //--------Table_Selection--------\\
            //-//------CopperTable------\\-\\
            case "CopperTable":
                Trace.Write("TableSelected: Copper");
                if (userData.playerData.UserTableList.table_ID_copper == null)
                {
                    Languages.Warning(botClient, chatId, callbackQuery, userData,
                        Table.TableType.copper);
                }
                else
                {
                    var tableData = await WebManager.SendData(
                        new TableProfile(userData.playerData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                    if (tableData.tableData.tableType == Table.TableType.copper)
                        Languages.Tables.Copper(botClient, chatId, callbackQuery, userData);
                    else
                        Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                            Error.UserAlreadyAtAnotherTable);
                }

                break;
            case "OpenCopperTable":
                Languages.Tables.Copper(botClient, chatId, callbackQuery, userData);
                break;
            //-//------BronzeTable------\\-\\
            case "BronzeTable":
                Trace.Write("TableSelected: Bronze");
                if (userData.playerData.UserTableList.table_ID_bronze == null)
                {
                    Languages.Warning(botClient, chatId, callbackQuery, userData,
                        Table.TableType.bronze);
                }
                else
                {
                    var tableData = await WebManager.SendData(
                        new TableProfile(userData.playerData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                    if (tableData.tableData.tableType == Table.TableType.bronze)
                        Languages.Tables.Bronze(botClient, chatId, callbackQuery, userData);
                    else
                        Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                            Error.UserAlreadyAtAnotherTable);
                }

                break;
            case "OpenBronzeTable":
                Languages.Tables.Bronze(botClient, chatId, callbackQuery, userData);
                break;
            //-//------SilverTable------\\-\\
            case "SilverTable":
                Trace.Write("TableSelected: Silver");
                if (userData.playerData.invited >= 2 ||
                    userData.playerData.level_tableType.CompareTo(Table.TableType.silver) >= 0)
                {
                    if (userData.playerData.UserTableList.table_ID_silver == null)
                    {
                        Languages.Warning(botClient, chatId, callbackQuery, userData,
                            Table.TableType.silver);
                    }
                    else
                    {
                        var tableData =
                            await WebManager.SendData(
                                new TableProfile(userData.playerData.UserTableList.table_ID_silver),
                                WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.tableType == Table.TableType.silver)
                            Languages.Tables.Silver(botClient, chatId, callbackQuery, userData);
                        else
                            Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserAlreadyAtAnotherTable);
                    }
                }
                else
                {
                    Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                        Error.UserDontMeetConnetionRequriments, 2);
                }

                break;
            case "OpenSilverTable":
                Languages.Tables.Silver(botClient, chatId, callbackQuery, userData);
                break;
            //-//------GoldTable------\\-\\
            case "GoldTable":
                Trace.Write("TableSelected: Gold");
                if (userData.playerData.invited >= 4 ||
                    userData.playerData.level_tableType.CompareTo(Table.TableType.gold) >= 0)
                {
                    if (userData.playerData.UserTableList.table_ID_gold == null)
                    {
                        Languages.Warning(botClient, chatId, callbackQuery, userData,
                            Table.TableType.gold);
                    }
                    else
                    {
                        var tableData =
                            await WebManager.SendData(new TableProfile(userData.playerData.UserTableList.table_ID_gold),
                                WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.tableType == Table.TableType.gold)
                            Languages.Tables.Gold(botClient, chatId, callbackQuery, userData);
                        else
                            Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserAlreadyAtAnotherTable);
                    }
                }
                else
                {
                    Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                        Error.UserDontMeetConnetionRequriments, 4);
                }

                break;
            case "OpenGoldTable":
                Languages.Tables.Gold(botClient, chatId, callbackQuery, userData);
                break;
            //-//------PlatinumTable------\\-\\
            case "PlatinumTable":
                Trace.Write("TableSelected: Platinum");
                if (userData.playerData.invited >= 6 ||
                    userData.playerData.level_tableType.CompareTo(Table.TableType.platinum) >= 0)
                {
                    if (userData.playerData.UserTableList.table_ID_platinum == null)
                    {
                        Languages.Warning(botClient, chatId, callbackQuery, userData,
                            Table.TableType.platinum);
                    }
                    else
                    {
                        var tableData =
                            await WebManager.SendData(
                                new TableProfile(userData.playerData.UserTableList.table_ID_platinum),
                                WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.tableType == Table.TableType.platinum)
                            Languages.Tables.Platinum(botClient, chatId, callbackQuery, userData);
                        else
                            Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserAlreadyAtAnotherTable);
                    }
                }
                else
                {
                    Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                        Error.UserDontMeetConnetionRequriments, 6);
                }

                break;
            case "OpenPlatinumTable":
                Languages.Tables.Platinum(botClient, chatId, callbackQuery, userData);
                break;
            //-//-//-----DiamondTable-----\\-\\-\\
            case "DiamondTable":
                Trace.Write("TableSelected: Diamond");
                if (userData.playerData.invited >= 12 ||
                    userData.playerData.level_tableType.CompareTo(Table.TableType.diamond) >= 0)
                {
                    if (userData.playerData.UserTableList.table_ID_diamond == null)
                    {
                        Languages.Warning(botClient, chatId, callbackQuery, userData,
                            Table.TableType.diamond);
                    }
                    else
                    {
                        var tableData =
                            await WebManager.SendData(
                                new TableProfile(userData.playerData.UserTableList.table_ID_diamond),
                                WebManager.RequestType.GetTableData, true);
                        if (tableData.tableData.tableType == Table.TableType.diamond)
                            Languages.Tables.Diamond(botClient, chatId, callbackQuery, userData);
                        else
                            Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                                Error.UserAlreadyAtAnotherTable);
                    }
                }
                else
                {
                    Languages.ConnectingError(botClient, chatId, callbackQuery, userData.playerData,
                        Error.UserDontMeetConnetionRequriments, 12);
                }

                break;
            case "OpenDiamondTable":
                Languages.Tables.Diamond(botClient, chatId, callbackQuery, userData);
                break;
            //--------STATUS--------\\
            case "Status":
                Trace.Write("Status");
                //userData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetUserData, true);
                Languages.Status(botClient, chatId, callbackQuery, userData.playerData);
                break;
            //--------Info--------\\
            case "Info":
                Trace.Write("Info");
                Languages.Info(botClient, chatId, callbackQuery, userData.playerData);
                break;
            //--------REF_LINK--------\\
            case "RefLink":
                Trace.Write("RefLink");
                Languages.RefLink(botClient, chatId, userData.playerData, callbackQuery);
                break;
            //--------TECH_SUPPORT--------\\
            case "TechSupport":
                Trace.Write("TechSupport");
                Languages.TechSupport(botClient, chatId, callbackQuery, userData.playerData);
                break;
            //--------CHANGE_LANG--------\\
            case "ChangeLang":
                Languages.LanguageMenu(botClient, chatId, callbackQuery, userData.playerData);
                Trace.Write("ChangeLang");
                break;
            //--------LANG_SELECTION--------\\
            case "ChangeToRU":
                user.AddLang("ru");
                await ChangeLang(botClient, callbackQuery, user);
                Trace.Write("ru");
                break;
            case "ChangeToENG":
                user.AddLang("eng");
                await ChangeLang(botClient, callbackQuery, user);
                Trace.Write("eng");
                break;
            case "ChangeToFR":
                user.AddLang("fr");
                await ChangeLang(botClient, callbackQuery, user);
                Trace.Write("fr");
                break;
            case "ChangeToDE":
                user.AddLang("de");
                await ChangeLang(botClient, callbackQuery, user);
                Trace.Write("de");
                break;


            default:
                Trace.Write("\nWrong data");
                break;
        }

        /*
        await botClient.AnswerCallbackQueryAsync(
            callbackQueryId: callbackQuery.Id,
            text: $"Received {callbackQuery.Data}");

        await botClient.SendTextMessageAsync(
            chatId: callbackQuery.Message!.Chat.Id,
            text: $"Received {callbackQuery.Data}");
            */
    }

    private static void SelectByTableType(ITelegramBotClient botClient, CallbackQuery callbackQuery, UserData userData,
        Table.TableType tableType)
    {
        switch (tableType)
        {
            case Table.TableType.copper:
            {
                Languages.Tables.Copper(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData);
                break;
            }
            case Table.TableType.bronze:
            {
                Languages.Tables.Bronze(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData);
                break;
            }
            case Table.TableType.silver:
            {
                Languages.Tables.Silver(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData);
                break;
            }
            case Table.TableType.gold:
            {
                Languages.Tables.Gold(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData);
                break;
            }
            case Table.TableType.platinum:
            {
                Languages.Tables.Platinum(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData);
                break;
            }
            case Table.TableType.diamond:
            {
                Languages.Tables.Diamond(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData);
                break;
            }
        }
    }

    private static async Task ConfirmLeaveTable(ITelegramBotClient botClient, CallbackQuery callbackQuery,
        UserData userData, Table.TableType tableType)
    {
        await WebManager.SendData(new UserProfile(userData.playerData.id, tableType),
            WebManager.RequestType.LeaveTable, true);
        Languages.TableMenu(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData.playerData);
    }

    private static async Task Agreement(ITelegramBotClient botClient, CallbackQuery callbackQuery, UserProfile user)
    {
        await WebManager.SendData(user, WebManager.RequestType.ChangeLang, true);
        Languages.Agreement(botClient, callbackQuery.Message.Chat.Id, callbackQuery, user);
    }

    private static async Task ChangeLang(ITelegramBotClient botClient, CallbackQuery callbackQuery, UserProfile user)
    {
        await WebManager.SendData(user, WebManager.RequestType.ChangeLang, true);
        Languages.MainMenu(botClient, callbackQuery.Message.Chat.Id, callbackQuery, user.lang);
    }

    private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
    {
        Trace.Write($"Received inline query from: {inlineQuery.From.Id}");

        InlineQueryResult[] results =
        {
            // displayed result
            new InlineQueryResultArticle(
                "1",
                "TgBots",
                new InputTextMessageContent(
                    "hello"
                )
            )
        };

        await botClient.AnswerInlineQueryAsync(inlineQuery.Id,
            results,
            isPersonal: true,
            cacheTime: 0);
    }

    private static Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient,
        ChosenInlineResult chosenInlineResult)
    {
        Trace.Write($"Received inline result: {chosenInlineResult.ResultId}");
        return Task.CompletedTask;
    }

    private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
    {
        Trace.Write($"Unknown update type: {update.Type}");
        return Task.CompletedTask;
    }
}