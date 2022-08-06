using System.Diagnostics;
using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

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

        Console.WriteLine(ErrorMessage);
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
        Console.WriteLine("MessageReceived: " + updateMessage.Text);
        if (updateMessage.Text == "/start")
        {
            var user = new UserProfile(updateMessage.From.Id, updateMessage.From.Username);
            WebManager.SendData(user, WebManager.RequestType.Register);
            await botClient.DeleteMessageAsync(updateMessage.Chat.Id, updateMessage.MessageId);
            Languages.RegLanguageMenu(botClient, updateMessage.Chat.Id);
        }

        if (updateMessage.Text.Contains("/start R"))
        {
            var splitMessage = updateMessage.Text.Split("R");
            var refId = int.Parse(splitMessage[1]);
            var user = new UserProfile(updateMessage.From.Id, refId, updateMessage.From.Username);
            WebManager.SendData(user, WebManager.RequestType.RegisterWithRef);
            await botClient.DeleteMessageAsync(updateMessage.Chat.Id, updateMessage.MessageId);
            Languages.RegLanguageMenu(botClient, updateMessage.Chat.Id);
        }

        if (updateMessage.Text == "/GetUserData")
        {
            var user = new UserProfile(updateMessage.From!.Id, updateMessage.From.Username!);
            //User.PrintUserProfile();
            var getData = await WebManager.SendData(user, WebManager.RequestType.GetUserData);
            getData.playerData.PrintUserProfile();
            if (getData.playerData.refId == null) Console.WriteLine("NULL");
        }

        if (updateMessage.Text == "/reg")
        {
            var user = new UserProfile(updateMessage.From!.Id, updateMessage.From.Username!);
            //User.PrintUserProfile();
            await WebManager.SendData(user, WebManager.RequestType.Register);
        }

        if (updateMessage.Text == "/langMenu")
        {
            var user = new UserProfile(updateMessage.From!.Id, updateMessage.From.Username!);
            Languages.LanguageMenu(botClient, updateMessage.Chat.Id);
        }

        if (updateMessage.Text == "/RefLink")
        {
        }

        if (updateMessage.Text == "/menu") Languages.English.MainMenu(botClient, updateMessage.Chat.Id);
        if (updateMessage.Text == "/status")
        {
            var user = new UserProfile();
            //await WebManager.SendData(user, WebManager.RequestType.Register);
        }
    }

    // Process Inline Keyboard callback data
    private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        Console.WriteLine("\nCallbackQuery.from.Username : " + callbackQuery.From.Username);
        Console.WriteLine("\nCallbackQuery.from.Id : " + callbackQuery.From.Id);
        var user = new UserProfile(callbackQuery.From.Id, callbackQuery.From.Username!);
        var userData = await WebManager.SendData(user, WebManager.RequestType.GetUserData);
        await botClient.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
        Console.WriteLine("\n---------------------------------------------------"
                          + "\nTriggered by: " + userData.playerData.username + " at " + DateTime.Now + "\nCallBackQuery: " +
                          callbackQuery.Data
                          + "\n---------------------------------------------------");
        UserData? dataToRemove;
        switch (callbackQuery.Data)
        {
            //--------MAIN_MENU--------
            case "MainMenu":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "eng":
                        Languages.English.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "fr":
                        Languages.French.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "de":
                        Languages.German.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                }
                break;
            //--------CHOOSE_TABLE--------\\
            case "ChooseTable":
                Console.WriteLine("ChooseTable");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "eng":
                        Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "fr":
                        Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "de":
                        Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                }

                break;
            //--------Table_Selection--------\\
            //-//------CopperTable------\\-\\
            case "CopperTable":
                Console.WriteLine("TableSelected: Copper");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.copper);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.copper)
                                Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "eng":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.copper);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.copper)
                                Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "fr":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.copper);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.copper)
                                Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "de":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.copper);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.copper)
                                Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                }

                break;
            case "OpenCopperTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "eng":
                        Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "fr":
                            Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                            break;
                    
                    case "de":
                        Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                }
                break;
            //-//------BronzeTable------\\-\\
            case "BronzeTable":
                Console.WriteLine("TableSelected: Bronze");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.bronze);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.bronze)
                                Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "eng":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.bronze);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.bronze)
                                Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "fr":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.bronze);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.bronze)
                                Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "de":
                        if (userData.playerData.table_id == null)
                        {
                            Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.bronze);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.bronze)
                                Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                }
                break;
            case "OpenBronzeTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "eng":
                        Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "fr":
                        Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "de":
                        Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                }
                break;
            //-//------SilverTable------\\-\\
            case "SilverTable":
                Console.WriteLine("TableSelected: Silver");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        if ((userData.playerData.invited >= 2 ||  
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.silver) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.silver);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.silver)
                                Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "eng":
                        if ((userData.playerData.invited >= 2 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.silver) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.silver);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.silver)
                                Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "fr":
                        if ((userData.playerData.invited >= 2 ||  
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.silver) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.silver);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.silver)
                                Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "de":
                        if ((userData.playerData.invited >= 2 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.silver) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.silver);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.silver)
                                Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                }

                break;
            case "OpenSilverTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "eng":
                        Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "fr":
                        Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "de":
                        Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                }
                break;
            //-//------GoldTable------\\-\\
            case "GoldTable":
                Console.WriteLine("TableSelected: Gold");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        if ((userData.playerData.invited >= 4 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.gold) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.gold);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.gold)
                                Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "eng":
                        if ((userData.playerData.invited >= 4 ||  
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.gold) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.gold);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.gold)
                                Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "fr":
                        if ((userData.playerData.invited >= 4 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.gold) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.gold);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.gold)
                                Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "de":
                        if ((userData.playerData.invited >= 4 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.gold) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.gold);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.gold)
                                Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                }
                break;
            case "OpenGoldTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    case "eng":
                        Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    case "fr":
                        Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    case "de":
                        Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                }
                break;
            //-//------PlatinumTable------\\-\\
            case "PlatinumTable":
                Console.WriteLine("TableSelected: Platinum");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        if ((userData.playerData.invited >= 6 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.platinum) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.platinum);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.platinum)
                                Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "eng":
                        if ((userData.playerData.invited >= 6 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.platinum) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.platinum);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.platinum)
                                Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "fr":
                        if ((userData.playerData.invited >= 6 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.platinum) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.platinum);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.platinum)
                                Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "de":
                        if ((userData.playerData.invited >= 6 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.platinum) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.platinum);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.platinum)
                                Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                }

                break;
            case "OpenPlatinumTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "eng":
                        Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "fr":
                        Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    
                    case "de":
                        Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                }
                break;
            //-//-//-----DiamondTable-----\\-\\-\\
            case "DiamondTable":
                Console.WriteLine("TableSelected: Diamond");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        if ((userData.playerData.invited >= 12 || 
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.diamond) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.diamond);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.diamond)
                                Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "eng":
                        if ((userData.playerData.invited >= 12 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.diamond) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.diamond);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.diamond)
                                Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "fr":
                        if ((userData.playerData.invited >= 12 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.diamond) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.diamond);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.diamond)
                                Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                    case "de":
                        if ((userData.playerData.invited >= 12 ||
                             Enum.Parse<Table.TableType>(userData.playerData.level_tableType, true).CompareTo(Table.TableType.diamond) >= 0) && userData.playerData.table_id == null)
                        {
                            Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, userData, Table.TableType.diamond);
                        }
                        else
                        {
                            UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                            if(tableData.tableData.tableType == Table.TableType.diamond)
                                Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                            else Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        }
                        break;
                }
                break;
            case "OpenDiamondTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    case "eng":
                        Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    case "fr":
                        Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                    case "de":
                        Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id, userData);
                        break;
                }
                break;
            //-//-//---LeaveTable---\\-\\-\\
            case "LeaveTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.LeaveTable);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.LeaveTable);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.LeaveTable);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.LeaveTable);
                        break;
                }
                break;
            case "ConfirmLeaveTable":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        await WebManager.SendData(userData.playerData, WebManager.RequestType.LeaveTable);
                        Languages.Russian.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "eng":
                        await WebManager.SendData(userData.playerData, WebManager.RequestType.LeaveTable);
                        Languages.English.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "fr":
                        await WebManager.SendData(userData.playerData, WebManager.RequestType.LeaveTable);
                        Languages.French.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "de":
                        await WebManager.SendData(userData.playerData, WebManager.RequestType.LeaveTable);
                        Languages.German.TableMenu(botClient, callbackQuery.Message.Chat.Id);
                        break;
                }
                break;
            //-//-//---GetBankerData---\\-\\-\\
            case "GetBankerData":
            {
                UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if (tableData.tableData.bankerID != null)
                {
                    UserData data = await WebManager.SendData(new UserProfile((int)tableData.tableData.bankerID), WebManager.RequestType.GetUserData);
                    Languages.GetUserData(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, data.playerData, Enum.Parse<Table.TableRole>(userData.playerData.tableRole, true));
                }
                else
                {
                    Languages.Warning(botClient, callbackQuery.Message.Chat.Id, userData.playerData, Error.UserIsNotExist);
                }
                break;
            }
            //-//-//---GetManagerAData---\\-\\-\\
            case "GetManagerAData":
            {
                UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if (tableData.tableData.managerA_ID != null)
                {
                    UserData data = await WebManager.SendData(new UserProfile((int)tableData.tableData.managerA_ID), WebManager.RequestType.GetUserData);
                    Languages.GetUserData(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, data.playerData, Enum.Parse<Table.TableRole>(userData.playerData.tableRole, true));
                }
                else
                {
                    Languages.Warning(botClient, callbackQuery.Message.Chat.Id, userData.playerData, Error.UserIsNotExist);
                }
                break;
            }
            //-//-//---GetManagerBData---\\-\\-\\
            case "GetManagerBData":
            {
                UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if (tableData.tableData.managerB_ID != null)
                {
                    UserData data = await WebManager.SendData(new UserProfile((int)tableData.tableData.managerB_ID), WebManager.RequestType.GetUserData);
                    Languages.GetUserData(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, data.playerData, Enum.Parse<Table.TableRole>(userData.playerData.tableRole, true));
                }
                else
                {
                    Languages.Warning(botClient, callbackQuery.Message.Chat.Id, userData.playerData, Error.UserIsNotExist);
                }
                break;
            }
            //-//-//---GetGiverAData---\\-\\-\\
            case "GetGiverAData":
            {
                UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if (tableData.tableData.giverA_ID != null)
                {
                    UserData data = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverA_ID), WebManager.RequestType.GetUserData);
                    Languages.GetUserData(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, data.playerData, Enum.Parse<Table.TableRole>(userData.playerData.tableRole, true));
                }
                else
                {
                    Languages.Warning(botClient, callbackQuery.Message.Chat.Id, userData.playerData, Error.UserIsNotExist);
                }
                break;
            }
            //-//-//---GetGiverBData---\\-\\-\\
            case "GetGiverBData":
            {
                UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if (tableData.tableData.giverB_ID != null)
                {
                    UserData data = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverB_ID), WebManager.RequestType.GetUserData);
                    Languages.GetUserData(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, data.playerData, Enum.Parse<Table.TableRole>(userData.playerData.tableRole, true));
                }
                else
                {
                    Languages.Warning(botClient, callbackQuery.Message.Chat.Id, userData.playerData, Error.UserIsNotExist);
                }
                break;
            }
            //-//-//---GetGiverCData---\\-\\-\\
            case "GetGiverCData":
            {
                UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if (tableData.tableData.giverC_ID != null)
                {
                    UserData data = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverC_ID), WebManager.RequestType.GetUserData);
                    Languages.GetUserData(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, data.playerData, Enum.Parse<Table.TableRole>(userData.playerData.tableRole, true));
                }
                else
                {
                    Languages.Warning(botClient, callbackQuery.Message.Chat.Id, userData.playerData, Error.UserIsNotExist);
                }
                break;
            }
            //-//-//---GetGiverDData---\\-\\-\\
            case "GetGiverDData":
            {
                UserData tableData = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if (tableData.tableData.giverD_ID != null)
                {
                    UserData data = await WebManager.SendData(new UserProfile((int)tableData.tableData.giverD_ID), WebManager.RequestType.GetUserData);
                    Languages.GetUserData(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, data.playerData, Enum.Parse<Table.TableRole>(userData.playerData.tableRole, true));
                }
                else
                {
                    Languages.Warning(botClient, callbackQuery.Message.Chat.Id, userData.playerData, Error.UserIsNotExist);
                }

                break;
            }
            //-//-//---ShowListTeam---\\-\\-\\
            case "ShowListTeam":
            {
                Languages.ShowListTeam(botClient, callbackQuery.Message.Chat.Id, userData.playerData.lang, userData.playerData);
                break;
            }
            //-//-//---RemoveFromTable---\\-\\-\\
            //-//-//-//RemoveFromTableManagerA\\-\\-\\-\\
            case "RemoveFromTableManagerA":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                }
                break;
            case "ConfirmRemoveFromTableManagerA":
                dataToRemove = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToRemove.tableData.tableID != null && dataToRemove.tableData.managerA_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToRemove.tableData.managerA_ID, dataToRemove.tableData.tableID),
                        WebManager.RequestType.RemoveFromTable);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            //-//-//-//RemoveFromTableManagerB\\-\\-\\-\\
            case "RemoveFromTableManagerB":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                }
                break;
            case "ConfirmRemoveFromTableManagerB":
                dataToRemove = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToRemove.tableData.tableID != null && dataToRemove.tableData.managerB_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToRemove.tableData.managerB_ID, dataToRemove.tableData.tableID),
                        WebManager.RequestType.RemoveFromTable);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            //-//-//-//RemoveFromTableGiverA\\-\\-\\-\\
            case "RemoveFromTableGiverA":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                }
                break;
            case "ConfirmRemoveFromTableGiverA":
                dataToRemove = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToRemove.tableData.tableID != null && dataToRemove.tableData.giverA_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToRemove.tableData.giverA_ID, dataToRemove.tableData.tableID),
                        WebManager.RequestType.RemoveFromTable);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            //-//-//-//RemoveFromTableGiverB\\-\\-\\-\\
            case "RemoveFromTableGiverB":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                }
                break;
            case "ConfirmRemoveFromTableGiverB":
                dataToRemove = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToRemove.tableData.tableID != null && dataToRemove.tableData.giverB_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToRemove.tableData.giverB_ID, dataToRemove.tableData.tableID),
                        WebManager.RequestType.RemoveFromTable);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            //-//-//-//RemoveFromTableGiverC\\-\\-\\-\\
            case "RemoveFromTableGiverC":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                }
                break;
            case "ConfirmRemoveFromTableGiverC":
                dataToRemove = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToRemove.tableData.tableID != null && dataToRemove.tableData.giverC_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToRemove.tableData.giverC_ID, dataToRemove.tableData.tableID),
                        WebManager.RequestType.RemoveFromTable);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            //-//-//-//RemoveFromTableGiverD\\-\\-\\-\\
            case "RemoveFromTableGiverD":
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.RemoveFromTable);
                        break;
                }
                break;
            case "ConfirmRemoveFromTableGiverD":
                dataToRemove = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToRemove.tableData.tableID != null && dataToRemove.tableData.giverD_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToRemove.tableData.giverD_ID, dataToRemove.tableData.tableID),
                        WebManager.RequestType.RemoveFromTable);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            //-//-//---VerfGiver---\\-\\-\\
            //-//-//-//VerfGiverA\\-\\-\\-\\
            case "VerfGiverA":
            {
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                }
                break;
            }
            case "ConfirmVerfGiverA":
            {
                var dataToConfirm = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToConfirm.tableData.tableID != null && dataToConfirm.tableData.giverA_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToConfirm.tableData.giverA_ID, dataToConfirm.tableData.tableID),
                        WebManager.RequestType.Confirm);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            }
            //-//-//-//VerfGiverB\\-\\-\\-\\
            case "VerfGiverB":{
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                }
                break;
            }
            case "ConfirmVerfGiverB":
            {
                var dataToConfirm = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToConfirm.tableData.tableID != null && dataToConfirm.tableData.giverB_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToConfirm.tableData.giverB_ID, dataToConfirm.tableData.tableID),
                        WebManager.RequestType.Confirm);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            }
            case "VerfGiverC":{
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                }
                break;
            }
            case "ConfirmVerfGiverC":
                {
                var dataToConfirm = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToConfirm.tableData.tableID != null && dataToConfirm.tableData.giverC_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToConfirm.tableData.giverC_ID, dataToConfirm.tableData.tableID),
                        WebManager.RequestType.Confirm);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            }
            //-//-//-//VerfGiverD\\-\\-\\-\\
            case "VerfGiverD":{
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "eng":
                        Languages.English.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "fr":
                        Languages.French.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                    case "de":
                        Languages.German.Warning(botClient, callbackQuery.Message.Chat.Id, callbackQuery, WebManager.RequestType.Confirm);
                        break;
                }
                break;
            }
            case "ConfirmVerfGiverD":
                {
                var dataToConfirm = await WebManager.SendData(userData.playerData, WebManager.RequestType.GetTableData);
                if(dataToConfirm.tableData.tableID != null && dataToConfirm.tableData.giverD_ID != null)
                {
                    await WebManager.SendData(new UserProfile((int)dataToConfirm.tableData.giverD_ID, dataToConfirm.tableData.tableID),
                        WebManager.RequestType.Confirm);
                    switch (userData.playerData.lang)
                    {
                        case "ru":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.Russian.TablesRU.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.Russian.TablesRU.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.Russian.TablesRU.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.Russian.TablesRU.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.Russian.TablesRU.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.Russian.TablesRU.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "eng":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.English.TablesENG.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.English.TablesENG.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.English.TablesENG.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.English.TablesENG.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.English.TablesENG.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.English.TablesENG.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "fr":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.French.TablesFR.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.French.TablesFR.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.French.TablesFR.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.French.TablesFR.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.French.TablesFR.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.French.TablesFR.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                        case "de":
                        {
                            switch (userData.playerData.level_tableType)
                            {
                                case "copper":
                                {
                                    Languages.German.TablesDE.Copper(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "bronze":
                                {
                                    Languages.German.TablesDE.Bronze(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "silver":
                                {
                                    Languages.German.TablesDE.Silver(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "gold":
                                {
                                    Languages.German.TablesDE.Gold(botClient, callbackQuery.Message.Chat.Id, userData);
                                    break;
                                }
                                case "platinum":
                                {
                                    Languages.German.TablesDE.Platinum(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                                case "diamond":
                                {
                                    Languages.German.TablesDE.Diamond(botClient, callbackQuery.Message.Chat.Id,
                                        userData);
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                break;
            }
            //--------STATUS--------\\
            case "Status":
                Console.WriteLine("Status");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Status(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                    case "eng":
                        Languages.English.Status(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                    case "fr":
                        Languages.French.Status(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                    case "de":
                        Languages.German.Status(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                }

                break;
            //--------Info--------\\
            case "Info":
                Console.WriteLine("Info");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.Info(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "eng":
                        Languages.English.Info(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "fr":
                        Languages.French.Info(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "de":
                        Languages.German.Info(botClient, callbackQuery.Message.Chat.Id);
                        break;
                }

                break;
            //--------REF_LINK--------\\
            case "RefLink":
                Console.WriteLine("RefLink");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.RefLink(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                    case "eng":
                        Languages.English.RefLink(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                    case "fr":
                        Languages.French.RefLink(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                    case "de":
                        Languages.German.RefLink(botClient, callbackQuery.Message.Chat.Id, userData.playerData);
                        break;
                }

                break;
            //--------TECH_SUPPORT--------\\
            case "TechSupport":
                Console.WriteLine("TechSupport");
                switch (userData.playerData.lang)
                {
                    case "ru":
                        Languages.Russian.TechSupport(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "eng":
                        Languages.English.TechSupport(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "fr":
                        Languages.French.TechSupport(botClient, callbackQuery.Message.Chat.Id);
                        break;
                    case "de":
                        Languages.German.TechSupport(botClient, callbackQuery.Message.Chat.Id);
                        break;
                }

                break;
            //--------CHANGE_LANG--------\\
            case "ChangeLang":
                //await botClient.DeleteMessageAsync(callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId);
                Languages.LanguageMenu(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("ChangeLang");
                break;
            //--------LANG_SELECTION--------\\
            case "ChangeToRU":
                user.AddLang("ru");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.Russian.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("ru");
                break;
            case "ChangeToENG":
                user.AddLang("eng");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.English.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("eng");
                break;
            case "ChangeToFR":
                user.AddLang("fr");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.French.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("fr");
                break;
            case "ChangeToDE":
                user.AddLang("de");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.German.MainMenu(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("de");
                break;
            //--------REG_LANG--------
            case "Reg_RU":
                user.AddLang("ru");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.Russian.Agreement(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("ru");
                break;
            case "Reg_ENG":
                user.AddLang("eng");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.English.Agreement(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("eng");
                break;
            case "Reg_FR":
                user.AddLang("fr");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.French.Agreement(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("fr");
                break;
            case "Reg_DE":
                user.AddLang("de");
                await WebManager.SendData(user, WebManager.RequestType.ChangeLang);
                Languages.German.Agreement(botClient, callbackQuery.Message.Chat.Id);
                Console.WriteLine("de");
                break;


            default:
                Console.WriteLine("\nWrong data");
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

    private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
    {
        Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

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
        Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
        return Task.CompletedTask;
    }

    private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
    {
        Console.WriteLine($"Unknown update type: {update.Type}");
        return Task.CompletedTask;
    }
}