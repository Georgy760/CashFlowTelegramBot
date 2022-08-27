using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace CashFlowTelegramBot.Skywards.Telegram;

public class Notifications
{
    public enum TypeOfNotifications
    {
        NewGiver,
        TableCompleted,
        BannedAfterDayUnVerified,
        GiverIsDeleted,
        GiverIsVerified,
        Congrats2Invited,
        Congrats4Invited,
        Congrats6Invited,
        Congrats12Invited,
        NotifyBanker
    }
    public static TypeOfNotifications GetTypeOfNotifications(string data)
    {
        TypeOfNotifications typeOfNotifications = Enum.Parse<TypeOfNotifications>(data, true);
        return typeOfNotifications;
    }
    
    public static void Notify(ITelegramBotClient botClient, int? executorID, Notification notification)
    {
        Console.WriteLine($"\nNotify: {GetTypeOfNotifications(notification.notificationText)}");
        switch (GetTypeOfNotifications(notification.notificationText))
        {
            case TypeOfNotifications.NewGiver:
                
                if (notification.isNotify && notification.tableID != null)
                {
                    if(notification.bankerID != null) NewGiver(botClient, executorID,(int) notification.bankerID, (int) notification.tableID);
                    if(notification.managerA_ID != null) NewGiver(botClient, executorID,(int) notification.managerA_ID, (int) notification.tableID);
                    if(notification.managerB_ID != null) NewGiver(botClient, executorID,(int) notification.managerB_ID, (int) notification.tableID);
                    if(notification.giverA_ID != null) NewGiver(botClient, executorID,(int) notification.giverA_ID, (int) notification.tableID);
                    if(notification.giverB_ID != null) NewGiver(botClient, executorID,(int) notification.giverB_ID, (int) notification.tableID);
                    if(notification.giverC_ID != null) NewGiver(botClient, executorID,(int) notification.giverC_ID, (int) notification.tableID);
                    if(notification.giverD_ID != null) NewGiver(botClient, executorID,(int) notification.giverD_ID, (int) notification.tableID);
                }
                break;
            case TypeOfNotifications.NotifyBanker:
                if (notification.bankerID != null) NotifyBanker(botClient, executorID,(int) notification.bankerID, (int) notification.tableID);
                break;
            case TypeOfNotifications.GiverIsVerified:
                if (notification.isNotify && notification.tableID != null)
                {
                    if(notification.giverA_ID != null) GiverIsConfirmed(botClient, executorID,(int) notification.giverA_ID, (int) notification.tableID);
                    if(notification.giverB_ID != null) GiverIsConfirmed(botClient, executorID,(int) notification.giverB_ID, (int) notification.tableID);
                    if(notification.giverC_ID != null) GiverIsConfirmed(botClient, executorID,(int) notification.giverC_ID, (int) notification.tableID);
                    if(notification.giverD_ID != null) GiverIsConfirmed(botClient, executorID,(int) notification.giverD_ID, (int) notification.tableID);
                }
                break;
            case TypeOfNotifications.GiverIsDeleted:
                if (notification.isNotify && notification.tableID != null)
                {
                    if(notification.giverA_ID != null) GiverIsDeleted(botClient, executorID,(int) notification.giverA_ID, (int) notification.tableID);
                    if(notification.giverB_ID != null) GiverIsDeleted(botClient, executorID,(int) notification.giverB_ID, (int) notification.tableID);
                    if(notification.giverC_ID != null) GiverIsDeleted(botClient, executorID,(int) notification.giverC_ID, (int) notification.tableID);
                    if(notification.giverD_ID != null) GiverIsDeleted(botClient, executorID,(int) notification.giverD_ID, (int) notification.tableID);
                }
                break;
            case TypeOfNotifications.TableCompleted:
                
                break;
        }
    }
    
    private static async void NewGiver(ITelegramBotClient botClient, int? executorID, int userToNotify, int tableID)
    {
        //Console.WriteLine("\nNewGiver Method");
        string path = null;
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        string caption = "";
        var userDataExecutor = await WebManager.SendData(new UserProfile((int) executorID), WebManager.RequestType.GetUserData);
        var user = await WebManager.SendData(new UserProfile((int) userToNotify), WebManager.RequestType.GetUserData);
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData);
        var tableType = tableData.tableData.tableType;
        long? chatId = null;
        Console.WriteLine($"\nNewGiver Method : User lang: {user.playerData.lang}");
        switch (user.playerData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                caption = $"<b>На Ваш {TableProfile.GetTableType(user.playerData, tableType)}</b>\n" +
                          "<b>стол зашёл новый Даритель:</b>\n" +
                          $"@{userDataExecutor.playerData.username}";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                caption = $"<b>A new Giver has entered your {TableProfile.GetTableType(user.playerData, tableType)}</b>\n" +
                          "<b>table:</b>\n" +
                          $"@{userDataExecutor.playerData.username}";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                caption = $"<b>Un nouveau Donateur est entré dans votre</b>\n" +
                          $"<b>table {TableProfile.GetTableType(user.playerData, tableType)}:</b>\n" +
                          $"@{userDataExecutor.playerData.username}";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                caption = $"<b>Ein neuer Geber hat Ihren</b>\n" +
                          $"{TableProfile.GetTableType(user.playerData, tableType)}-Tisch:</b>\n" +
                          $"@{userDataExecutor.playerData.username}";
                break;
        }
        try
        {
            chatId = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }

        if (chatId != null)
        {
            sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                caption,
                ParseMode.Html,
                replyMarkup: inlineKeyboard);
        }
    }
    private static async void NotifyBanker(ITelegramBotClient botClient, int? executorID, int userToNotify, int tableID)
    {
        //Console.WriteLine("\nNewGiver Method");
        string path = null;
        
        InlineKeyboardMarkup? inlineKeyboardBanker = null;
        InlineKeyboardMarkup? inlineKeyboardExecutor = null;
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        
        string captionBanker = "";
        string captionExecutor = "";
        
        var userDataExecutor = await WebManager.SendData(new UserProfile((int) executorID), WebManager.RequestType.GetUserData);
        var banker = await WebManager.SendData(new UserProfile((int) userToNotify), WebManager.RequestType.GetUserData);
        
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData);
        
        var tableType = tableData.tableData.tableType;
        var giverNum = 0;
        var giver = "";
        if (tableData.tableData.giverA_ID == userDataExecutor.playerData.id)
        {
            giverNum = 1;
            giver = $"GetGiverAData|{tableType}";
        }
        if (tableData.tableData.giverB_ID == userDataExecutor.playerData.id)
        {
            giverNum = 2;
            giver = $"GetGiverBData|{tableType}";
        }
        if (tableData.tableData.giverC_ID == userDataExecutor.playerData.id)
        {
            giverNum = 3;
            giver = $"GetGiverCData|{tableType}";
        }
        if (tableData.tableData.giverD_ID == userDataExecutor.playerData.id)
        {
            giverNum = 4;
            giver = $"GetGiverDData|{tableType}";
        }
        
        long? chatIdBanker = null;
        long? chatIdExecutor = null;

        var giftSum = "";
        switch (tableType)
        {
            case Table.TableType.copper:
                giftSum = "100";
                break;
            case Table.TableType.bronze:
                giftSum = "400";
                break;
            case Table.TableType.silver:
                giftSum = "1 000";
                break;
            case Table.TableType.gold:
                giftSum = "2 500";
                break;
            case Table.TableType.platinum:
                giftSum = "5 000";
                break;
            case Table.TableType.diamond:
                giftSum = "10 000";
                break;
        }
        string? firstName = "";
        string? lastName = "";
        try
        {
            firstName = botClient.GetChatAsync(executorID).Result.FirstName;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        try
        {
            lastName = botClient.GetChatAsync(executorID).Result.LastName;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        
        switch (banker.playerData.lang)
        {
            case "ru":
                inlineKeyboardBanker = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📨 Связаться с Дарителем", "https://t.me/" + userDataExecutor.playerData.username),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("📄 О Дарителе", giver)
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionBanker = $"<b>Даритель-{giverNum}(@{userDataExecutor.playerData.username})</b>❌\n" +
                                $"{firstName} {lastName}" +
                                $"\nХочет подарить подарок 🎁\n" +
                                $"в размере - <b>{giftSum} $</b>\n" +
                                $"и ждёт реквизиты!" +
                                $"\n\n" +
                                $"После получения подарка, нажмите кнопку \"О Дарителе\" и активируйте участника! ✅";
                break;
            case "eng":
                inlineKeyboardBanker = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📨 Contact the Giver", "https://t.me/" + userDataExecutor.playerData.username),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("📄 About the Giver", giver)
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionBanker = $"<b>Giver-{giverNum}(@{userDataExecutor.playerData.username})</b>❌\n" +
                                $"{firstName} {lastName}" +
                                $"\nWant to give a gift 🎁\n" +
                                $"in the amount of - <b>{giftSum} $</b>\n" +
                                $"and is waiting for account details!" +
                                $"\n\n" +
                                $"After receiving the gift, click the \"About the Giver\" button and activate the participant! ✅";
                break;
            case "fr":
                inlineKeyboardBanker = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📨 Contacter le donneur", "https://t.me/" + userDataExecutor.playerData.username),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("📄 À propos du donneur", giver)
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                captionBanker = $"<b>Donateur-{giverNum}(@{userDataExecutor.playerData.username})</b>❌\n" +
                                $"{firstName} {lastName}" +
                                $"\nVeut offrir un cadeau 🎁\n" +
                                $"d'un montant de - <b>{giftSum} $</b>\n" +
                                $"et attend les détails du compte!" +
                                $"\n\n" +
                                $"Après avoir reçu le cadeau, cliquez sur le bouton \"À propos du donateur\" et activez le participant! ✅";
                break;
            case "de":
                inlineKeyboardBanker = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📨 Kontaktiere den Geber", "https://t.me/" + userDataExecutor.playerData.username),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Über den Geber", giver)
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                captionBanker = $"<b>Geber-{giverNum}(@{userDataExecutor.playerData.username})</b>❌\n" +
                                $"{firstName} {lastName}" +
                                $"\nWill ein Geschenk 🎁\n" +
                                $"von - <b>{giftSum} $ machen</b>\n" +
                                $"und wartet auf Kontodaten!" +
                                $"\n\n" +
                                $"Klicken Sie nach Erhalt des Geschenks auf die Schaltfläche \"Über den Geber\" und aktivieren Sie den Teilnehmer! ✅";
                break;
        }
        switch (userDataExecutor.playerData.lang)
        {
            case "ru":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionExecutor = $"Вы успешно оповестили Банкира! ✅";
                break;
            case "eng":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionExecutor = $"You have successfully notified the Banker! ✅";
                break;
            case "fr":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                captionExecutor = $"Vous avez notifié avec succès le banquier! ✅";
                break;
            case "de":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                captionExecutor = $"Sie haben den Banker erfolgreich benachrichtigt! ✅";
                break;
        }
        
        try
        {
            chatIdExecutor = botClient.GetChatAsync(executorID).Result.Id;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        
        if (chatIdExecutor != null)
        {
            var sentMessageToExecutor = await botClient.SendTextMessageAsync(
                chatIdExecutor,
                captionExecutor,
                ParseMode.Html,
                replyMarkup: inlineKeyboardBanker);
        }
        
        try
        {
            chatIdBanker = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }

        if (chatIdBanker != null)
        {
            var sentMessageToBanker = await botClient.SendPhotoAsync(
                chatIdBanker,
                File.OpenRead(path),
                captionBanker,
                ParseMode.Html,
                replyMarkup: inlineKeyboardBanker);
        }
    }
    private static async void GiverIsConfirmed(ITelegramBotClient botClient, int? executorID, int userToNotify, int tableID)
    {
        string path = null;
        
        InlineKeyboardMarkup? inlineKeyboardGiver = null;
        InlineKeyboardMarkup? inlineKeyboardExecutor = null;
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        
        string captionGiver = "";
        string captionExecutor = "";
        
        var userDataExecutor = await WebManager.SendData(new UserProfile((int) executorID), WebManager.RequestType.GetUserData);
        var giver = await WebManager.SendData(new UserProfile((int) userToNotify), WebManager.RequestType.GetUserData);
        
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData);
        
        var tableType = tableData.tableData.tableType;
        long? chatIdGiver = null;
        long? chatIdExecutor = null;
        
        switch (giver.playerData.lang)
        {
            case "ru":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionGiver = $"<b>✅ Вы были успешно активированы на\n {GetTableTypeConfirm(giver.playerData, tableType)}</b>";
                break;
            case "eng":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionGiver = $"<b>✅ You have been successfully activated on the\n {GetTableTypeConfirm(giver.playerData, tableType)}</b>";
                break;
            case "fr":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                captionGiver = $"<b>Vous avez été activé avec succès sur le\n {GetTableTypeConfirm(giver.playerData, tableType)}</b>";
                break;
            case "de":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                captionGiver = $"<b>Sie wurden erfolgreich auf der aktiviert\n {GetTableTypeConfirm(giver.playerData, tableType)}</b>";
                break;
        }
        
        switch (userDataExecutor.playerData.lang)
        {
            case "ru":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionExecutor = $"✅ Даритель @{giver.playerData.username} был успешно активирован.";
                break;
            case "eng":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionExecutor = $"✅ The @{giver.playerData.username} giver has been successfully activated.";
                break;
            case "fr":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                captionExecutor = $"✅ Le donneur @{giver.playerData.username} a été activé avec succès.";
                break;
            case "de":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                captionExecutor = $"✅ Der Spender @{giver.playerData.username} wurde erfolgreich aktiviert.";
                break;
        }
        
        try
        {
            chatIdExecutor = botClient.GetChatAsync(executorID).Result.Id;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        
        if (chatIdExecutor != null)
        {
            var sentMessageToExecutor = await botClient.SendTextMessageAsync(
                chatIdExecutor,
                captionExecutor,
                ParseMode.Html,
                replyMarkup: inlineKeyboardExecutor);
        }
        
        try
        {
            chatIdGiver = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }

        if (chatIdGiver != null)
        {
            var sentMessageToGiver = await botClient.SendTextMessageAsync(
                chatIdGiver,
                captionGiver,
                ParseMode.Html,
                replyMarkup: inlineKeyboardGiver);
        }
    }
    private static async void GiverIsDeleted(ITelegramBotClient botClient, int? executorID, int userToNotify, int tableID)
    {
        string path = null;
        
        InlineKeyboardMarkup? inlineKeyboardGiver = null;
        InlineKeyboardMarkup? inlineKeyboardExecutor = null;
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        
        string captionGiver = "";
        string captionExecutor = "";
        
        var userDataExecutor = await WebManager.SendData(new UserProfile((int) executorID), WebManager.RequestType.GetUserData);
        var giver = await WebManager.SendData(new UserProfile((int) userToNotify), WebManager.RequestType.GetUserData);
        
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData);
        
        var tableType = tableData.tableData.tableType;
        long? chatIdGiver = null;
        long? chatIdExecutor = null;
        
        switch (giver.playerData.lang)
        {
            case "ru":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Тех. поддержка", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Выберите нужный язык", "ChangeLang")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionGiver = $"<b>Вы были удалены Банкиром (@{giver.playerData.username}) с {GetTableTypeRemove(giver.playerData, tableType)} стола.</b>" +
                               $"\n\n" +
                               $"Теперь Вы не сможете зайти на данный стол в течение 24 часов." +
                               $"\n\n" +
                               $"Если это произошло по ошибке, то сообщите об этом в тех. поддержку:";
                break;
            case "eng":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Choose your preferred language", "ChangeLang")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionGiver = $"<b>You have been removed by the Banker (@{giver.playerData.username}) from the {GetTableTypeRemove(giver.playerData, tableType)} table.</b>" +
                               $"\n\n" +
                               $"You will now be unable to access this table for 24 hours." +
                               $"\n\n" +
                               $"If this happened by mistake, please report it to tech support:";
                break;
            case "fr":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Soutien technique", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Choisissez votre langue préférée", "ChangeLang")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                captionGiver = $"<b>Vous avez été supprimé par le banquier (@{giver.playerData.username}) de la table {GetTableTypeRemove(giver.playerData, tableType)}.</b>" +
                               $"\n\n" +
                               $"Vous ne pourrez plus accéder à cette table pendant 24 heures." +
                               $"\n\n" +
                               $"Si cela s'est produit par erreur, veuillez le signaler au support technique :";
                break;
            case "de":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Technischer Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Wählen Sie Ihre bevorzugte Sprache", "ChangeLang")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                captionGiver = $"<b>Sie wurden vom Bankier (@{giver.playerData.username}) aus der Tabelle {GetTableTypeRemove(giver.playerData, tableType)} entfernt.</b>" +
                               $"\n\n" +
                               $"Sie können nun 24 Stunden lang nicht auf diese Tabelle zugreifen." +
                               $"\n\n" +
                               $"Falls dies versehentlich passiert ist, melden Sie es bitte dem technischen Support:";
                break;
        }
        
        switch (userDataExecutor.playerData.lang)
        {
            case "ru":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Тех. поддержка", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Выберите нужный язык", "ChangeLang")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionExecutor = $"<b>Вы удалили Дарителя (@{giver.playerData.username}) со стола.</b>" +
                                  $"\n\n" +
                                  $"Если это произошло по ошибке, то сообщите об этом в тех. поддержку:";
                break;
            case "eng":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Choose your preferred language", "ChangeLang")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionExecutor = $"<b>You have removed the Giver (@{giver.playerData.username}) from the table.</b>" +
                                  $"\n\n" +
                                  $"If this happened by mistake, please report it to tech support:";
                break;
            case "fr":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Soutien technique", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Choisissez votre langue préférée", "ChangeLang")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                captionExecutor = $"<b>Vous avez supprimé le Donneur (@{giver.playerData.username}) du tableau.</b>" +
                                  $"\n\n" +
                                  $"Si cela s'est produit par erreur, veuillez le signaler au support technique:" ;
                break;
            case "de":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu"),
                            InlineKeyboardButton.WithCallbackData("📲 Technischer Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Wählen Sie Ihre bevorzugte Sprache", "ChangeLang")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                captionExecutor = $"<b>Du hast den Geber (@{giver.playerData.username}) vom Tisch entfernt.</b>" +
                                  $"\n\n" +
                                  $"Falls dies versehentlich passiert ist, melden Sie es bitte dem technischen Support:";
                break;
        }
        
        try
        {
            chatIdExecutor = botClient.GetChatAsync(executorID).Result.Id;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        
        if (chatIdExecutor != null)
        {
            var sentMessageToExecutor = await botClient.SendPhotoAsync(
                chatIdExecutor,
                File.OpenRead(path),
                captionExecutor,
                ParseMode.Html,
                replyMarkup: inlineKeyboardExecutor);
        }
        
        try
        {
            chatIdGiver = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Console.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }

        if (chatIdGiver != null)
        {
            var sentMessageToGiver = await botClient.SendPhotoAsync(
                chatIdGiver,
                File.OpenRead(path),
                captionGiver,
                ParseMode.Html,
                replyMarkup: inlineKeyboardGiver);
        }
    }
    private static string GetTableTypeConfirm(UserProfile user, Table.TableType tableType)
    {
        var result = "";
        switch (user.lang)
        {
            case "ru":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Медном";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Бронзовом";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Серебрянном";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Золотом";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Платиновом";
                        break;
                    case Table.TableType.diamond:
                        result = "💎 Алмазном";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "eng":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Copper";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Silver";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Gold";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platinum";
                        break;
                    case Table.TableType.diamond:
                        result = "💎 Diamond";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "fr":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Cuivre";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Argent";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Doré";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platine";
                        break;
                    case Table.TableType.diamond:
                        result = "💎 Diamant";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "de":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Kupfer";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Silberner";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Goldener";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platin";
                        break;
                    case Table.TableType.diamond:
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
    private static string GetTableTypeRemove(UserProfile user, Table.TableType tableType)
    {
        var result = "";
        switch (user.lang)
        {
            case "ru":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Медного";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Бронзового";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Серебряного";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Золотого";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Платинового";
                        break;
                    case Table.TableType.diamond:
                        result = "💎 Алмазного";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "eng":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Copper";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Silver";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Gold";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platinum";
                        break;
                    case Table.TableType.diamond:
                        result = "💎 Diamond";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "fr":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Cuivre";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Argent";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Doré";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platine";
                        break;
                    case Table.TableType.diamond:
                        result = "💎 Diamant";
                        break;
                    default:
                        result = "empty";
                        break;
                }

                break;
            case "de":
                switch (tableType)
                {
                    case Table.TableType.copper:
                        result = "🎗 Kupfer";
                        break;
                    case Table.TableType.bronze:
                        result = "🥉 Bronze";
                        break;
                    case Table.TableType.silver:
                        result = "🥈 Silberner";
                        break;
                    case Table.TableType.gold:
                        result = "🥇 Goldener";
                        break;
                    case Table.TableType.platinum:
                        result = "🎖 Platin";
                        break;
                    case Table.TableType.diamond:
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
}