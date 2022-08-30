using System.Diagnostics;
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
    
    public static void Notify(ITelegramBotClient botClient, long executorID, Notification notification)
    {
        Trace.WriteLine($"\nNotify: {GetTypeOfNotifications(notification.notificationText)}");
        switch (GetTypeOfNotifications(notification.notificationText))
        {
            case TypeOfNotifications.NewGiver:
                if (notification.isNotify && notification.tableID != null)
                {
                    if(notification.bankerID != null)
                        if (executorID != null)
                            NewGiver(botClient,  executorID,  notification.bankerID,
                                 notification.tableID);
                    if(notification.managerA_ID != null)
                        if (executorID != null)
                            NewGiver(botClient,  executorID,  notification.managerA_ID,
                                 notification.tableID);
                    if(notification.managerB_ID != null)
                        if (executorID != null)
                            NewGiver(botClient,  executorID,  notification.managerB_ID,
                                 notification.tableID);
                    if(notification.giverA_ID != null && 
                       notification.giverA_ID != executorID)
                        if (executorID != null)
                            NewGiver(botClient,  executorID,  notification.giverA_ID,
                                 notification.tableID);
                    if(notification.giverB_ID != null && 
                       notification.giverB_ID != executorID)
                        if (executorID != null)
                            NewGiver(botClient,  executorID,  notification.giverB_ID,
                                 notification.tableID);
                    if(notification.giverC_ID != null && 
                       notification.giverC_ID != executorID)
                        if (executorID != null)
                            NewGiver(botClient,  executorID,  notification.giverC_ID,
                                 notification.tableID);
                    if(notification.giverD_ID != null && 
                       notification.giverD_ID != executorID)
                        if (executorID != null)
                            NewGiver(botClient,  executorID,  notification.giverD_ID,
                                 notification.tableID);
                }
                break;
            case TypeOfNotifications.TableCompleted:
                if (notification.isNotify && notification.tableID != null)
                {
                    if(notification.bankerID != null)
                        if (executorID != null)
                            TableCompletedBanker(botClient,  executorID, notification);
                    if(notification.managerA_ID != null)TableCompletedManager(botClient, notification.managerA_ID, notification);
                    if(notification.managerB_ID != null)TableCompletedManager(botClient, notification.managerB_ID, notification);
                    if(notification.giverA_ID != null)TableCompletedGiver(botClient, notification.giverA_ID, notification);
                    if(notification.giverB_ID != null)TableCompletedGiver(botClient, notification.giverB_ID, notification);
                    if(notification.giverC_ID != null)TableCompletedGiver(botClient, notification.giverC_ID, notification);
                    if(notification.giverD_ID != null)TableCompletedGiver(botClient, notification.giverD_ID, notification);
                }
                break;
            case TypeOfNotifications.BannedAfterDayUnVerified:
                if(notification.giverA_ID != null) BannedAfterDayUnVerified(botClient, notification.giverA_ID);
                if(notification.giverB_ID != null) BannedAfterDayUnVerified(botClient, notification.giverB_ID);
                if(notification.giverC_ID != null) BannedAfterDayUnVerified(botClient, notification.giverC_ID);
                if(notification.giverD_ID != null) BannedAfterDayUnVerified(botClient, notification.giverD_ID);
                break;
            case TypeOfNotifications.GiverIsDeleted:
                if (notification.isNotify && notification.tableID != null)
                {
                    if(notification.giverA_ID != null)
                        if (executorID != null)
                            GiverIsDeleted(botClient,  executorID,  notification.giverA_ID,
                                 notification.tableID);
                    if(notification.giverB_ID != null)
                        if (executorID != null)
                            GiverIsDeleted(botClient,  executorID,  notification.giverB_ID,
                                 notification.tableID);
                    if(notification.giverC_ID != null)
                        if (executorID != null)
                            GiverIsDeleted(botClient,  executorID,  notification.giverC_ID,
                                 notification.tableID);
                    if(notification.giverD_ID != null)
                        if (executorID != null)
                            GiverIsDeleted(botClient,  executorID,  notification.giverD_ID,
                                 notification.tableID);
                }
                break;
            case TypeOfNotifications.GiverIsVerified:
                if (notification.isNotify && notification.tableID != null)
                {
                    if(notification.giverA_ID != null)
                        if (executorID != null)
                            GiverIsConfirmed(botClient,  executorID,  notification.giverA_ID,
                                 notification.tableID);
                    if(notification.giverB_ID != null)
                        if (executorID != null)
                            GiverIsConfirmed(botClient,  executorID,  notification.giverB_ID,
                                 notification.tableID);
                    if(notification.giverC_ID != null)
                        if (executorID != null)
                            GiverIsConfirmed(botClient,  executorID,  notification.giverC_ID,
                                 notification.tableID);
                    if(notification.giverD_ID != null)
                        if (executorID != null)
                            GiverIsConfirmed(botClient,  executorID,  notification.giverD_ID,
                                 notification.tableID);
                }
                break;
            case TypeOfNotifications.Congrats2Invited:
                if(notification.bankerID != null)Congrats2Givers(botClient,  notification.bankerID);
                break;
            case TypeOfNotifications.Congrats4Invited:
                if(notification.bankerID != null)Congrats4Givers(botClient,  notification.bankerID);
                break;
            case TypeOfNotifications.Congrats6Invited:
                if(notification.bankerID != null)Congrats6Givers(botClient,  notification.bankerID);
                break;
            case TypeOfNotifications.Congrats12Invited:
                if(notification.bankerID != null)Congrats12Givers(botClient,  notification.bankerID);
                break;
            case TypeOfNotifications.NotifyBanker:
                if (notification.bankerID != null)
                    if (executorID != null)
                        NotifyBanker(botClient,  executorID,  notification.bankerID,
                             notification.tableID);
                break;
        }
    }
    private static async void NewGiver(ITelegramBotClient botClient, long executorID, long? userToNotify, int? tableID)
    {
        //Trace.WriteLine("\nNewGiver Method");
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string caption = "";
        var userDataExecutor = await WebManager.SendData(new UserProfile( executorID), WebManager.RequestType.GetUserData, true);
        var user = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData, true);
        var tableType = tableData.tableData.tableType;
        long? chatId = null;
        Trace.WriteLine($"\nNewGiver Method : User lang: {user.playerData.lang}");
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
            Trace.WriteLine($"Handle Remaining Exceptions: {userToNotify}");
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
    private static async void TableCompletedBanker(ITelegramBotClient botClient, long executorID, Notification notification)
    {
        //var userData = await WebManager.SendData(new UserProfile( notification.giverA_ID), WebManager.RequestType.GetUserData);
        var userDataExecutor = await WebManager.SendData(new UserProfile( executorID), WebManager.RequestType.GetUserData, true);
        UserData? tableData = null;
        var tableType = notification.tableType;
        
        InlineKeyboardMarkup? inlineKeyboardExecutor = null;
        
        string captionExecutor = "";
        long? chatIdExecutor = null;
        if (userDataExecutor.playerData.invited >= 2)
        {
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
                    captionExecutor = $"<b>👏 Поздравляем!</b>\n" +
                                      $"<b>Ваш {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} стол успешно пройден!</b>\n\n" +
                                      $"Теперь Вы можете пойти на вышестоящий стол или пройти повторно этот же стол. 🚀";
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
                    captionExecutor = $"<b>👏 Congratulations!</b>\n" +
                                      $"<b>Your {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} table passed successfully!</b>\n\n" +
                                      $"Now you can go to the higher table or go through the same table again. 🚀";
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
                    captionExecutor = $"<b>👏 Félicitations !</b>\n" +
                                      $"<b>Votre table {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} a été transmise avec succès !</b>\n\n" +
                                      $"Vous pouvez maintenant passer à la table supérieure ou repasser par la même table. 🚀" ;
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
                    captionExecutor = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                                      $"<b>Ihre Tabelle {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} erfolgreich bestanden!</b>\n\n" +
                                      $"Jetzt können Sie zum höheren Tisch gehen oder denselben Tisch noch einmal durchgehen. 🚀";
                    break;
            }
        }
        else
        {
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
                    captionExecutor = $"<b>👏 Поздравляем!</b>\n" +
                                      $"<b>Ваш {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} стол успешно пройден!</b>\n\n" +
                                      $"Теперь Вы можете повторно пройти данный стол.\n" +
                                      $"Для того, чтобы перейти на вышестоящий стол, выполните условия по приглашениям. 👥\n\n" +
                                      $"Лично приглашенных участников: {userDataExecutor.playerData.invited}";
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
                    captionExecutor = $"<b>👏 Congratulations!</b>\n" +
                                      $"<b>Your {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} table passed successfully!</b>\n\n" +
                                      $"You can now replay this table.\n" +
                                      $"To move to a higher table, follow the conditions on the invitations. 👥\n\n" +
                                      $"Personally invited participants: {userDataExecutor.playerData.invited}";
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
                    captionExecutor = $"<b>👏 Félicitations !</b>\n" +
                                      $"<b>Votre table {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} a été transmise avec succès !</b>\n\n" +
                                      $"Vous pouvez maintenant rejouer cette table.\n" +
                                      $"Pour passer à une table supérieure, suivez les conditions sur les invitations. 👥\n\n" +
                                      $"Participants personnellement invités : {userDataExecutor.playerData.invited}" ;
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
                    captionExecutor = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                                      $"<b>Ihre Tabelle {TableProfile.GetTableType(userDataExecutor.playerData, tableType)} erfolgreich bestanden!</b>\n\n" +
                                      $"Sie können diesen Tisch jetzt erneut spielen.\n" +
                                      $"Um an einen höheren Tisch zu wechseln, folgen Sie den Bedingungen auf den Einladungen. 👥\n\n" +
                                      $"Persönlich eingeladene Teilnehmer: {userDataExecutor.playerData.invited}";
                    break;
            }
        }
        
        try
        {
            chatIdExecutor = botClient.GetChatAsync(executorID).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
    }
    private static async void TableCompletedManager(ITelegramBotClient botClient, long? managerID, Notification notification)
    {
        var manager = await WebManager.SendData(new UserProfile( managerID), WebManager.RequestType.GetUserData, true);
        var tableType = notification.tableType;
        
        var giftSum = "";
        switch (tableType)
        {
            case Table.TableType.copper:
                giftSum = "400";
                break;
            case Table.TableType.bronze:
                giftSum = "1 600";
                break;
            case Table.TableType.silver:
                giftSum = "4 000";
                break;
            case Table.TableType.gold:
                giftSum = "10 000";
                break;
            case Table.TableType.platinum:
                giftSum = "20 000";
                break;
            case Table.TableType.diamond:
                giftSum = "40 000";
                break;
        }
        
        InlineKeyboardMarkup? inlineKeyboardManager = null;

        

        string captionManager = "";

        long? chatIdManager = null;

        switch (manager.playerData.lang)
        {
            case "ru":
                inlineKeyboardManager = new InlineKeyboardMarkup(
                    new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionManager = $"<b>👏 Поздравляем!</b>\n" +
                                  $"<b>Вы перешли на следующий уровень! </b>\n\n" +
                                  $"<b>Теперь Ваша роль:</b> 🏦 Банкир\n" +
                                  $"На этом уровне Вы получите 4 подарка на общую сумму {giftSum}$ 💸";
                break;
            case "eng":
                inlineKeyboardManager = new InlineKeyboardMarkup(
                    new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionManager = $"<b>👏 Congratulations!</b>\n" +
                                  $"<b>You have reached the next level! </b>\n\n" +
                                  $"<b>Your role is now:</b> 🏦 Banker\n" +
                                  $"At this level, you will receive 4 gifts for a total of {giftSum}$ 💸";
                break;
            case "fr":
                inlineKeyboardManager = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Cacher", "Close")
                        }
                    });
                captionManager = $"<b>👏 Félicitations !</b>\n" +
                                  $"<b>Vous avez atteint le niveau suivant ! </b>\n\n" +
                                  $"<b>Votre rôle est maintenant:</b> 🏦 Banquier\n" +
                                  $"A ce niveau, vous recevrez 4 cadeaux pour un total de {giftSum}$ 💸" ;
                break;
            case "de":
                inlineKeyboardManager = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Ausblenden", "Close")
                        }
                    });
                captionManager = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                                  $"<b>Sie haben das nächste Level erreicht! </b>\n\n" +
                                  $"<b>Ihre Rolle ist jetzt:</b> 🏦 Bankier\n" +
                                  $"Auf dieser Stufe erhalten Sie 4 Geschenke im Gesamtwert von {giftSum}$ 💸";
                break;
        }

        try
        {
            chatIdManager = botClient.GetChatAsync(manager.playerData.id).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }

        if (chatIdManager != null)
        {
            var sentMessageToManager = await botClient.SendTextMessageAsync(
                chatIdManager,
                captionManager,
                ParseMode.Html,
                replyMarkup: inlineKeyboardManager);
        }
    }
    private static async void TableCompletedGiver(ITelegramBotClient botClient, long? giverID, Notification notification)
    {

        InlineKeyboardMarkup? inlineKeyboardGiver = null;

        var giver = await WebManager.SendData(new UserProfile( giverID), WebManager.RequestType.GetUserData, true);
        var managerNum = 0;
        if (notification.giverA_ID == giverID) managerNum = 1;
        if (notification.giverB_ID == giverID) managerNum = 2;
        if (notification.giverC_ID == giverID) managerNum = 1;
        if (notification.giverD_ID == giverID) managerNum = 2;
        string captionGiver = "";

        long? chatIdGiver = null;

        switch (giver.playerData.lang)
        {
            case "ru":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionGiver = $"<b>👏 Поздравляем!</b>\n" +
                                  $"<b>Вы перешли на следующий уровень! </b>\n\n" +
                                  $"<b>Теперь Ваша роль:</b> 👤 Менеджер-{managerNum}\n" +
                                  $"На этом уровне Ваша задача пригласить 2-ух игроков в игру по своей реферальной ссылке. 👥";
                break;
            case "eng":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Hide", "Close")
                        }
                    });
                captionGiver = $"<b>👏 Congratulations!</b>\n" +
                                  $"<b>You have reached the next level! </b>\n\n" +
                                  $"<b>Your role is now:</b> 👤 Manager-{managerNum}\n" +
                                  $"At this level, your task is to invite 2 players to the game using your referral link. 👥";
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
                captionGiver = $"<b>👏 Félicitations !</b>\n" +
                                  $"<b>Vous avez atteint le niveau suivant ! </b>\n\n" +
                                  $"<b>Votre rôle est maintenant:</b> 👤 Gestionnaire-{managerNum}\n" +
                                  $"A ce niveau, votre tâche est d'inviter 2 joueurs au jeu en utilisant votre lien de parrainage. 👥";
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
                captionGiver = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                                  $"<b>Sie haben das nächste Level erreicht! </b>\n\n" +
                                  $"<b>Ihre Rolle ist jetzt:</b> 👤 Manager-{managerNum}\n" +
                                  $"Auf diesem Level besteht Ihre Aufgabe darin, 2 Spieler mit Ihrem Empfehlungslink zum Spiel einzuladen. 👥";
                break;
        }

        try
        {
            chatIdGiver = botClient.GetChatAsync(giver.playerData.id).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }

        if (chatIdGiver != null)
        {
            var sentMessageToGiver= await botClient.SendTextMessageAsync(
                chatIdGiver,
                captionGiver,
                ParseMode.Html,
                replyMarkup: inlineKeyboardGiver);
        }
    }
    private static async void BannedAfterDayUnVerified(ITelegramBotClient botClient, long? userToNotify)
    {
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string caption = "";
        var user = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        long? chatId = null;
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
                caption = "<b>⌛️ По истечению 24 часов Вы были исключены со стола.</b>\n" +
                          "Теперь Вы не сможете зайти на данный стол в течение 24 часов.\n\n" +
                          "<b>Другие столы доступны для входа без ограничений.</b>\n\n" +
                          "Если Вы сделали финансовый подарок, но Банкир Вас не подтвердил, то сообщите об этом в тех. поддержку:\n\n" +
                          "<b>🗂 Главное меню - 📲 Тех. поддержка - 🌐 Выберите нужный язык</b>";
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
                caption = "<b>⌛️ After 24 hours, you were kicked off the table.</b>\n" +
                          "Now you won't be able to access this table for 24 hours.\n\n" +
                          "<b>Other tables are available for entry without restrictions.</b>\n\n" +
                          "If you made a financial gift, but the Banker did not confirm you, then report it to technical support:\n\n" +
                          "<b>🗂 Main Menu - 📲 Tech Support - 🌐 Choose your language</b>";
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
                caption = "<b>⌛️ Après 24 heures, vous avez été expulsé de la table.</b>\n" +
                          "Maintenant, vous ne pourrez plus accéder à cette table pendant 24 heures.\n\n" +
                          "<b>D'autres tables sont disponibles pour l'entrée sans restrictions.</b>\n\n" +
                          "Si vous avez fait un don financier, mais que le banquier ne vous a pas confirmé, signalez-le au support technique:\n\n" +
                          "<b>🗂 Menu principal - 📲 Support technique - 🌐 Choisissez votre langue</b>";
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
                caption = "<b>⌛️ Nach 24 Stunden wurdest du vom Tisch geworfen.</b>\n" +
                          "Jetzt können Sie 24 Stunden lang nicht auf diese Tabelle zugreifen.\n\n" +
                          "<b>Andere Tische stehen uneingeschränkt zur Verfügung.</b>\n\n" +
                          "Wenn Sie ein finanzielles Geschenk gemacht haben, aber der Bankier Sie nicht bestätigt hat, melden Sie es dem technischen Support:\n\n" +
                          "<b>🗂 Hauptmenü - 📲 Technischer Support - 🌐 Wählen Sie Ihre Sprache</b>";
                break;
        }
        try
        {
            chatId = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine($"Handle Remaining Exceptions: {userToNotify}");
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
    private static async void GiverIsDeleted(ITelegramBotClient botClient, long executorID, long? userToNotify, int? tableID)
    {
        InlineKeyboardMarkup? inlineKeyboardGiver = null;
        InlineKeyboardMarkup? inlineKeyboardExecutor = null;
        
        
        string captionGiver = "";
        string captionExecutor = "";
        
        var userDataExecutor = await WebManager.SendData(new UserProfile( executorID), WebManager.RequestType.GetUserData, true);
        var giver = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData, true);
        
        var tableType = tableData.tableData.tableType;
        long? chatIdGiver = null;
        long? chatIdExecutor = null;
        
        switch (giver.playerData.lang)
        {
            case "ru":
                inlineKeyboardGiver = new InlineKeyboardMarkup(
                    new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionGiver = $"<b>Вы были удалены Банкиром (@{giver.playerData.username}) с {GetTableTypeRemove(giver.playerData, tableType)} стола.</b>" +
                               $"\n\n" +
                               $"Теперь Вы не сможете зайти на данный стол в течение 24 часов." +
                               $"\n\n" +
                               $"Если это произошло по ошибке, то сообщите об этом в тех. поддержку:\n\n" +
                               $"<b>🗂 Главное меню - 📲 Тех. поддержка - 🌐 Выберите нужный язык</b>";
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
                captionGiver = $"<b>You have been removed by the Banker (@{giver.playerData.username}) from the {GetTableTypeRemove(giver.playerData, tableType)} table.</b>" +
                               $"\n\n" +
                               $"You will now be unable to access this table for 24 hours." +
                               $"\n\n" +
                               $"If this happened by mistake, please report it to tech support:\n\n" +
                               $"<b>🗂 Main menu - 📲 Tech Support - 🌐 Choose your preferred language</b>";
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
                captionGiver = $"<b>Vous avez été supprimé par le banquier (@{giver.playerData.username}) de la table {GetTableTypeRemove(giver.playerData, tableType)}.</b>" +
                               $"\n\n" +
                               $"Vous ne pourrez plus accéder à cette table pendant 24 heures." +
                               $"\n\n" +
                               $"Si cela s'est produit par erreur, veuillez le signaler au support technique :\n\n" +
                               $"<b>🗂 Menu principal - 📲 Soutien technique - 🌐 Choisissez votre langue préférée</b>";
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
                captionGiver = $"<b>Sie wurden vom Bankier (@{giver.playerData.username}) aus der Tabelle {GetTableTypeRemove(giver.playerData, tableType)} entfernt.</b>" +
                               $"\n\n" +
                               $"Sie können nun 24 Stunden lang nicht auf diese Tabelle zugreifen." +
                               $"\n\n" +
                               $"Falls dies versehentlich passiert ist, melden Sie es bitte dem technischen Support:\n\n" +
                               $"<b>🗂 Hauptmenü - 📲 Technischer Support - 🌐 Wählen Sie Ihre bevorzugte Sprache</b>";
                break;
        }
        
        switch (userDataExecutor.playerData.lang)
        {
            case "ru":
                inlineKeyboardExecutor = new InlineKeyboardMarkup(
                    new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("❌ Скрыть", "Close")
                        }
                    });
                captionExecutor = $"<b>Вы удалили Дарителя (@{giver.playerData.username}) со стола.</b>" +
                                  $"\n\n" +
                                  $"Если это произошло по ошибке, то сообщите об этом в тех. поддержку:\n\n" +
                                  $"<b>🗂 Главное меню - 📲 Тех. поддержка - 🌐 Выберите нужный язык</b>";
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
                captionExecutor = $"<b>You have removed the Giver (@{giver.playerData.username}) from the table.</b>" +
                                  $"\n\n" +
                                  $"If this happened by mistake, please report it to tech support:\n\n" +
                                  $"<b>🗂 Main menu - 📲 Tech Support - 🌐 Choose your preferred language</b>";
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
                captionExecutor = $"<b>Vous avez supprimé le Donneur (@{giver.playerData.username}) du tableau.</b>" +
                                  $"\n\n" +
                                  $"Si cela s'est produit par erreur, veuillez le signaler au support technique:\n\n" +
                                  $"<b>🗂 Menu principal - 📲 Soutien technique - 🌐 Choisissez votre langue préférée</b>";
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
                captionExecutor = $"<b>Du hast den Geber (@{giver.playerData.username}) vom Tisch entfernt.</b>" +
                                  $"\n\n" +
                                  $"Falls dies versehentlich passiert ist, melden Sie es bitte dem technischen Support:\n\n" +
                                  $"<b>🗂 Hauptmenü - 📲 Technischer Support - 🌐 Wählen Sie Ihre bevorzugte Sprache</b>";
                break;
        }
        
        try
        {
            chatIdExecutor = botClient.GetChatAsync(executorID).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
            Trace.WriteLine("Handle Remaining Exceptions");
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
    private static async void GiverIsConfirmed(ITelegramBotClient botClient, long executorID, long? userToNotify, int? tableID)
    {
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData, true);
        var tableType = tableData.tableData.tableType;

        InlineKeyboardMarkup? inlineKeyboardGiver = null;
        var giver = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        string captionGiver = "";
        long? chatIdGiver = null;
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
        try
        {
            chatIdGiver = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
        
        InlineKeyboardMarkup? inlineKeyboardExecutor = null;
        var userDataExecutor = await WebManager.SendData(new UserProfile( executorID), WebManager.RequestType.GetUserData, true);
        string captionExecutor = "";
        long? chatIdExecutor = null;
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
            Trace.WriteLine("Handle Remaining Exceptions");
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
    }
    private static async void Congrats2Givers(ITelegramBotClient botClient, long? userToNotify)
    {
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string caption = "";
        var user = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        long? chatId = null;
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
                caption = $"<b>👏 Поздравляем!</b>\n" +
                          "У Вас 2 лично приглашенных игроков\n" +
                          $"<b>Теперь Вам доступен 🥈 Серебряный стол!</b>";
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
                caption = $"<b>👏 Congratulations!</b>\n" +
                          "You have 2 personally invited players\n" +
                          $"<b>Now you have access to the 🥈 Silver Table!</b>";
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
                caption = $"<b>👏 Félicitations!</b>\n" +
                          "Vous avez 2 joueurs personnellement invités\n" +
                          $"<b>Vous avez maintenant accès à la 🥈 table d'argent!</b>" ;
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
                caption = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                          "Du hast 2 persönlich eingeladene Spieler\n" +
                          $"<b>Jetzt haben Sie Zugang zum 🥈 Silbertisch!</b>";
                break;
        }
        try
        {
            chatId = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
    private static async void Congrats4Givers(ITelegramBotClient botClient, long? userToNotify)
    {
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string caption = "";
        var user = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        long? chatId = null;
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
                caption = $"<b>👏 Поздравляем!</b>\n" +
                          "У Вас 4 лично приглашенных игроков\n" +
                          $"<b>Теперь Вам доступен 🥇 Золотой стол!</b>";
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
                caption = $"<b>👏 Congratulations!</b>\n" +
                          "You have 4 personally invited players\n" +
                          $"<b>Now you have access to the 🥇 Golden Table!</b>";
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
                caption = $"<b>👏 Félicitations!</b>\n" +
                          "Vous avez 4 joueurs personnellement invités\n" +
                          $"<b>Vous avez maintenant accès à la 🥇 Table Dorée !</b>" ;
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
                caption = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                          "Du hast 4 persönlich eingeladene Spieler\n" +
                          $"<b>Jetzt haben Sie Zugang zum 🥇 Golden Table!</b>";
                break;
        }
        try
        {
            chatId = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
    private static async void Congrats6Givers(ITelegramBotClient botClient, long? userToNotify)
    {
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string caption = "";
        var user = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        long? chatId = null;
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
                caption = $"<b>👏 Поздравляем!</b>\n" +
                          "У Вас 6 лично приглашенных игроков\n" +
                          $"<b>Теперь Вам доступен 🎖 Платиновый стол!</b>";
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
                caption = $"<b>👏 Congratulations!</b>\n" +
                          "You have 6 personally invited players\n" +
                          $"<b>Now you have access to the 🎖 Platinum Table!</b>";
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
                caption = $"<b>👏 Félicitations!</b>\n" +
                          "Vous avez 6 joueurs personnellement invités\n" +
                          $"<b>Vous avez maintenant accès à la 🎖 Table Platine!</b>" ;
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
                caption = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                          "Du hast 6 persönlich eingeladene Spieler\n" +
                          $"<b>Jetzt haben Sie Zugang zum 🎖 Platinum Table!</b>";
                break;
        }
        try
        {
            chatId = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
    private static async void Congrats12Givers(ITelegramBotClient botClient, long? userToNotify)
    {
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string caption = "";
        var user = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        long? chatId = null;
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
                caption = $"<b>👏 Поздравляем!</b>\n" +
                          "У Вас 12 лично приглашенных игроков\n" +
                          $"<b>Теперь Вам доступен 💎 Алмазный стол!</b>";
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
                caption = $"<b>👏 Congratulations!</b>\n" +
                          "You have 6 personally invited players\n" +
                          $"<b>Now you have access to the 🥈 Platinum Table!</b>";
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
                caption = $"<b>👏 Félicitations!</b>\n" +
                          "Vous avez 6 joueurs personnellement invités\n" +
                          $"<b>Vous avez maintenant accès à la 💎 Table Platine!</b>" ;
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
                caption = $"<b>👏 Herzlichen Glückwunsch!</b>\n" +
                          "Du hast 6 persönlich eingeladene Spieler\n" +
                          $"<b>Jetzt haben Sie Zugang zum 💎 Platinum Table!</b>";
                break;
        }
        try
        {
            chatId = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
    private static async void NotifyBanker(ITelegramBotClient botClient, long executorID, long? userToNotify, int? tableID)
    {
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
        
        var userDataExecutor = await WebManager.SendData(new UserProfile( executorID), WebManager.RequestType.GetUserData, true);
        var banker = await WebManager.SendData(new UserProfile( userToNotify), WebManager.RequestType.GetUserData, true);
        
        var tableData = await WebManager.SendData(new TableProfile(tableID), WebManager.RequestType.GetTableData, true);
        
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
            Trace.WriteLine("Handle Remaining Exceptions");
            aex.Handle(ex => Exceptions.HandleException(ex));
        }
        try
        {
            lastName = botClient.GetChatAsync(executorID).Result.LastName;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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
            Trace.WriteLine("Handle Remaining Exceptions");
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
            chatIdBanker = botClient.GetChatAsync(userToNotify).Result.Id;
        }
        catch(AggregateException aex)
        {
            Trace.WriteLine("Handle Remaining Exceptions");
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