using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.ImageEditor;
using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using File = System.IO.File;

namespace CashFlowTelegramBot.Skywards.Telegram;

public partial class Languages
{
    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuRU =
        InlineKeyboardButton.WithCallbackData("🔙 Назад", "MainMenu");

    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuENG =
        InlineKeyboardButton.WithCallbackData("🔙 Back", "MainMenu");

    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuFR =
        InlineKeyboardButton.WithCallbackData("🔙 Retour", "MainMenu");

    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuDE =
        InlineKeyboardButton.WithCallbackData("🔙 Zurück", "MainMenu");


    //LANGUAGE MENU//
    public static async void LanguageMenu(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile user)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/langSelection.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\langSelection.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        string? caption;

        switch (user.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇬🇧 English", "ChangeToENG"),
                            InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "ChangeToFR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "ChangeToRU"),
                            InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "ChangeToDE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuRU
                        },
                    });
                caption = $"<b>🌐 Языковое меню</b>" +
                          $"\n\nНажмите соответствующую кнопку для смены языка:";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇬🇧 English", "ChangeToENG"),
                            InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "ChangeToFR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "ChangeToRU"),
                            InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "ChangeToDE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        },
                    });
                caption = $"<b>🌐 Language menu</b>" +
                          $"\n\nClick the corresponding button to change the language:";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇬🇧 English", "ChangeToENG"),
                            InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "ChangeToFR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "ChangeToRU"),
                            InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "ChangeToDE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuFR
                        },
                    });
                caption = $"<b>🌐 Menu Langue</b>" +
                          $"\n\nCliquez sur le bouton correspondant pour changer la langue:";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇬🇧 English", "ChangeToENG"),
                            InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "ChangeToFR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "ChangeToRU"),
                            InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "ChangeToDE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuDE
                        },
                    });
                caption = $"<b>🌐 Sprachmenü</b>" +
                          $"\n\nKlicken Sie auf die entsprechende Schaltfläche, um die Sprache zu ändern:";
                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇬🇧 English", "ChangeToENG"),
                            InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "ChangeToFR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "ChangeToRU"),
                            InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "ChangeToDE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        },
                    });
                caption = $"<b>🌐 Language menu</b>" +
                          $"\n\nClick the corresponding button to change the language:";
                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void RegLanguageMenu(ITelegramBotClient botClient, long chatId)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/langSelection.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\langSelection.png");
        var inlineKeyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🇬🇧 English", "Reg_ENGCaptcha"),
                    InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "Reg_FRCaptcha")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "Reg_RUCaptcha"),
                    InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "Reg_DECaptcha")
                }
            });
        try
        {
            var sentPhoto = await botClient.SendPhotoAsync(
                chatId,
                File.OpenRead(path)!,
                "<b>🌐 Language menu:</b>" +
                "\n\nClick the corresponding button to change the language:",
                ParseMode.Html,
                replyMarkup: inlineKeyboard);
        }
        catch
        {
        }
    }

    public static async void GetUserData(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData, UserProfile SearchedUser, Table.TableType tableType)
    {
        var lang = userData.lang;
        Table.TableRole? tableRole = Table.TableRole.giver;
        string path = null;
        string? caption = null;
        /*UserData data = await WebManager.SendData(SearchedUser, WebManager.RequestType.GetUserData);
        SearchedUser = data.playerData;*/
        InlineKeyboardMarkup? inlineKeyboard = null;
        UserData invitedBy = null;
        if (SearchedUser.refId != null)
            invitedBy = await WebManager.SendData(new UserProfile(SearchedUser.refId),
                WebManager.RequestType.GetUserData, true);
        UserData tableToBack = new UserData();
        Table.TableRole? userTableRole = Table.TableRole.giver;
        switch (tableType)
        {
            case Table.TableType.copper:
                if (SearchedUser.UserTableList.table_ID_copper == null)
                {
                    tableToBack.tableData.tableID = 0;
                    break;
                }

                tableToBack = await WebManager.SendData(new TableProfile(SearchedUser.UserTableList.table_ID_copper),
                    WebManager.RequestType.GetTableData, true);
                userTableRole = SearchedUser.UserTableList.copperTableRole;
                tableRole = userData.UserTableList.copperTableRole;
                break;
            case Table.TableType.bronze:
                if (SearchedUser.UserTableList.table_ID_bronze == null)
                {
                    tableToBack.tableData.tableID = 0;
                    break;
                }

                tableToBack = await WebManager.SendData(new TableProfile(SearchedUser.UserTableList.table_ID_bronze),
                    WebManager.RequestType.GetTableData, true);
                userTableRole = SearchedUser.UserTableList.bronzeTableRole;
                tableRole = userData.UserTableList.bronzeTableRole;
                break;
            case Table.TableType.silver:
                if (SearchedUser.UserTableList.table_ID_silver == null)
                {
                    tableToBack.tableData.tableID = 0;
                    break;
                }

                tableToBack = await WebManager.SendData(new TableProfile(SearchedUser.UserTableList.table_ID_silver),
                    WebManager.RequestType.GetTableData, true);
                userTableRole = SearchedUser.UserTableList.silverTableRole;
                tableRole = userData.UserTableList.silverTableRole;
                break;
            case Table.TableType.gold:
                if (SearchedUser.UserTableList.table_ID_gold == null)
                {
                    tableToBack.tableData.tableID = 0;
                    break;
                }

                tableToBack = await WebManager.SendData(new TableProfile(SearchedUser.UserTableList.table_ID_gold),
                    WebManager.RequestType.GetTableData, true);
                userTableRole = SearchedUser.UserTableList.goldTableRole;
                tableRole = userData.UserTableList.goldTableRole;
                break;
            case Table.TableType.platinum:
                if (SearchedUser.UserTableList.table_ID_platinum == null)
                {
                    tableToBack.tableData.tableID = 0;
                    break;
                }

                tableToBack = await WebManager.SendData(new TableProfile(SearchedUser.UserTableList.table_ID_platinum),
                    WebManager.RequestType.GetTableData, true);
                userTableRole = SearchedUser.UserTableList.platinumTableRole;
                tableRole = userData.UserTableList.platinumTableRole;
                break;
            case Table.TableType.diamond:
                if (SearchedUser.UserTableList.table_ID_diamond == null)
                {
                    tableToBack.tableData.tableID = 0;
                    break;
                }

                tableToBack = await WebManager.SendData(new TableProfile(SearchedUser.UserTableList.table_ID_diamond),
                    WebManager.RequestType.GetTableData, true);
                userTableRole = SearchedUser.UserTableList.diamondTableRole;
                tableRole = userData.UserTableList.diamondTableRole;
                break;
        }

        //await WebManager.SendData(SearchedUser, WebManager.RequestType.GetTableData);
        if (tableToBack.tableData.tableID != 0)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/MainMenu/status.png");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\MainMenu\status.png");
            var callbackAddress = GetCallbackAddress(tableToBack.tableData.tableType);
            var flag = false;
            var searchedUserRole = "";
            string? firstName = "";
            string? lastName = "";
            try
            {
                firstName = botClient.GetChatAsync(SearchedUser.id).Result.FirstName;
            }
            catch (AggregateException aex)
            {
                Trace.Write("Handle Remaining Exceptions");
                aex.Handle(ex => Exceptions.HandleException(ex));
            }

            try
            {
                lastName = botClient.GetChatAsync(SearchedUser.id).Result.LastName;
            }
            catch (AggregateException aex)
            {
                Trace.Write("Handle Remaining Exceptions");
                aex.Handle(ex => Exceptions.HandleException(ex));
            }


            //firstName = botClient.GetChatAsync(SearchedUser.id).Result.FirstName;
            //lastName = botClient.GetChatAsync(SearchedUser.id).Result.LastName;

            var IsSearchedUserVerf =
                (tableToBack.tableData.giverA_ID == SearchedUser.id && tableToBack.tableData.verf_A) ||
                (tableToBack.tableData.giverB_ID == SearchedUser.id && tableToBack.tableData.verf_B) ||
                (tableToBack.tableData.giverC_ID == SearchedUser.id && tableToBack.tableData.verf_C) ||
                (tableToBack.tableData.giverD_ID == SearchedUser.id && tableToBack.tableData.verf_D);
            if (!(tableToBack.tableData.bankerID == SearchedUser.id || tableToBack.tableData.managerA_ID ==
                                                                    SearchedUser.id
                                                                    || tableToBack.tableData.managerB_ID ==
                                                                    SearchedUser.id))
            {
                if (IsSearchedUserVerf)
                    searchedUserRole = " ✅ ";
                else searchedUserRole = " ❌ ";
            }

            switch (lang)
            {
                case "ru":
                {
                    if (tableRole == Table.TableRole.banker && userTableRole == Table.TableRole.giver)
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_A)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Активировать",
                                                "VerfGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола",
                                                "RemoveFromTableGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_B)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Активировать",
                                                "VerfGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола",
                                                "RemoveFromTableGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_C)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Активировать",
                                                "VerfGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола",
                                                "RemoveFromTableGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_D)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Активировать",
                                                "VerfGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Удалить со стола",
                                                "RemoveFromTableGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Связаться",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }
                    }

                    if (!flag)
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Связаться",
                                        "https://t.me/" + SearchedUser.username),
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                }
                            });

                    if (invitedBy != null)
                        caption = "<b>📋 Информация о пользователе:</b>" +
                                  "\n" +
                                  $"\n<b>Роль:</b> {SearchedUser.GetTableRole(lang, tableType) + searchedUserRole}" +
                                  $"\n<b>Ник:</b> @{SearchedUser.username}" +
                                  $"\n<b>Имя пользователя:</b> {firstName} {lastName}" +
                                  $"\n<b>Лично приглашенных:</b> {SearchedUser.invited}" +
                                  "\n" +
                                  $"\n<b>Пригласил:</b> @{invitedBy.playerData.username}";
                    break;
                }
                case "eng":
                {
                    if (tableRole == Table.TableRole.banker && userTableRole == Table.TableRole.giver)
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_A)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirm",
                                                "VerfGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Delete from the table",
                                                "RemoveFromTableGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_B)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirm",
                                                "VerfGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Delete from the table",
                                                "RemoveFromTableGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_C)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirm",
                                                "VerfGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Delete from the table",
                                                "RemoveFromTableGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_D)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirm",
                                                "VerfGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Delete from the table",
                                                "RemoveFromTableGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }
                    }

                    if (!flag)
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });

                    if (invitedBy != null)
                        caption = "<b>📋 User info:</b>" +
                                  "\n" +
                                  $"\n<b>Role:</b> {SearchedUser.GetTableRole(lang, tableType) + searchedUserRole}" +
                                  $"\n<b>Nickname:</b> @{SearchedUser.username}" +
                                  $"\n<b>Username:</b> {firstName} {lastName}" +
                                  $"\n<b>Personally invited:</b> {SearchedUser.invited}" +
                                  "\n" +
                                  $"\n<b>Invited by:</b> @{invitedBy.playerData.username}";

                    break;
                }
                case "fr":
                {
                    if (tableRole == Table.TableRole.banker && userTableRole == Table.TableRole.giver)
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_A)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirmer",
                                                "VerfGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau",
                                                "RemoveFromTableGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_B)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirmer",
                                                "VerfGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau",
                                                "RemoveFromTableGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_C)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirmer",
                                                "VerfGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau",
                                                "RemoveFromTableGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_D)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Confirmer",
                                                "VerfGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌Supprimer du tableau",
                                                "RemoveFromTableGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Contact",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }
                    }

                    if (!flag)
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                }
                            });

                    if (invitedBy != null)
                        caption = "<b>📋 Informations de l'utilisateur:</b>" +
                                  "\n" +
                                  $"\n<b>Rôle:</b> {SearchedUser.GetTableRole(lang, tableType) + searchedUserRole}" +
                                  $"\n<b>Surnom:</b> @{SearchedUser.username}" +
                                  $"\n<b>Nom d'utilisateur:</b> {firstName} {lastName}" +
                                  $"\n<b>invité personnellement:</b> {SearchedUser.invited}" +
                                  "\n" +
                                  $"\n<b>inviter par:</b> @{invitedBy.playerData.username}";
                    break;
                }
                case "de":
                {
                    if (tableRole == Table.TableRole.banker && userTableRole == Table.TableRole.giver)
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_A)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Bestätigen",
                                                "VerfGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen",
                                                "RemoveFromTableGiverA|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_B)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Bestätigen",
                                                "VerfGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen",
                                                "RemoveFromTableGiverB|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_C)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Bestätigen",
                                                "VerfGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen",
                                                "RemoveFromTableGiverC|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
                        {
                            if (!tableToBack.tableData.verf_D)
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("✅ Bestätigen",
                                                "VerfGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("❌ Aus der Tabelle löschen",
                                                "RemoveFromTableGiverD|" + tableType)
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                            else
                            {
                                inlineKeyboard = new InlineKeyboardMarkup(
                                    new[]
                                    {
                                        new[]
                                        {
                                            InlineKeyboardButton.WithUrl("📨 Kontakt",
                                                "https://t.me/" + SearchedUser.username),
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                            InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                        }
                                    });
                                flag = true;
                            }
                        }
                    }

                    if (!flag)
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                }
                            });
                    if (invitedBy != null)
                        caption = "<b>📋 Benutzerinformation:</b>" +
                                  "\n" +
                                  $"\n<b>Rolle:</b> {SearchedUser.GetTableRole(lang, tableType) + searchedUserRole}" +
                                  $"\n<b>Spitzname:</b> @{SearchedUser.username}" +
                                  $"\n<b>Benutzername:</b> {firstName} {lastName}" +
                                  $"\n<b>Persönlich eingeladen:</b> {SearchedUser.invited}" +
                                  "\n" +
                                  $"\n<b>ingeladen von:</b> @{invitedBy.playerData.username}";

                    break;
                }
            }
        }
        else
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/MainMenu/status.png");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\MainMenu\status.png");
            switch (lang)
            {
                case "ru":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Назад", "ChooseTable")
                            }
                        });
                    caption = "Вы больше не участник стола";
                    break;
                }
                case "eng":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Back", "ChooseTable")
                            }
                        });
                    caption = "You are no longer a member of the table";
                    break;
                }
                case "fe":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Retour", "ChooseTable")
                            }
                        });
                    caption = "Vous n'êtes plus membre de la table";
                    break;
                }
                case "de":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Zurück", "ChooseTable")
                            }
                        });
                    caption = "Sie sind kein Mitglied des Tisches mehr";
                    break;
                }
            }
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    private static string GetCallbackAddress(Table.TableType tableType)
    {
        var callbackAddress = "";
        switch (tableType)
        {
            case Table.TableType.copper:
                callbackAddress = "OpenCopperTable";
                break;
            case Table.TableType.bronze:
                callbackAddress = "OpenBronzeTable";
                break;
            case Table.TableType.silver:
                callbackAddress = "OpenSilverTable";
                break;
            case Table.TableType.gold:
                callbackAddress = "OpenGoldTable";
                break;
            case Table.TableType.platinum:
                callbackAddress = "OpenPlatinumTable";
                break;
            case Table.TableType.diamond:
                callbackAddress = "OpenDiamondTable";
                break;
        }

        return callbackAddress;
    }

    public static async void ShowListTeam(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        string lang, UserProfile user, Table.TableType tableType)
    {
        UserData table = new UserData();

        switch (tableType)
        {
            case Table.TableType.copper:
                if (user.UserTableList.table_ID_copper != null)
                {
                    table = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                }
                else table.tableData.tableID = 0;

                break;
            case Table.TableType.bronze:
                if (user.UserTableList.table_ID_bronze != null)
                {
                    table = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                }
                else table.tableData.tableID = 0;

                break;
            case Table.TableType.silver:
                if (user.UserTableList.table_ID_silver != null)
                {
                    table = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                }
                else table.tableData.tableID = 0;

                break;
            case Table.TableType.gold:
                if (user.UserTableList.table_ID_gold != null)
                {
                    table = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                }
                else table.tableData.tableID = 0;

                break;
            case Table.TableType.platinum:
                if (user.UserTableList.table_ID_platinum != null)
                {
                    table = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                }
                else table.tableData.tableID = 0;

                break;
            case Table.TableType.diamond:
                if (user.UserTableList.table_ID_diamond != null)
                {
                    table = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                }
                else table.tableData.tableID = 0;

                break;
        }

        string path = null;
        InlineKeyboardMarkup? inlineKeyboard = null;
        string? caption = null;

        if (table.tableData.tableID != 0)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/Tables/");
                path += table.tableData.tableType + ".MP4";
            }


            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\Tables\");
                path += table.tableData.tableType + ".MP4";
            }

            var callbackAddress = GetCallbackAddress(table.tableData.tableType);


            caption = "";
            inlineKeyboard = null;
            if (table.tableData.bankerID != null)
            {
                var userData = await WebManager.SendData(new UserProfile(table.tableData.bankerID),
                    WebManager.RequestType.GetUserData, true);
                caption += userData.playerData.UserInfo(botClient, user.lang, table.tableData,
                    table.tableData.bankerID == user.id);
                //if (table.tableData.bankerID == user.id) tableInfo += "🔘";
                //tableInfo += $"🏦Банкир: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.banker);
            }

            if (table.tableData.managerA_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile(table.tableData.managerA_ID),
                    WebManager.RequestType.GetUserData, true);
                caption += userData.playerData.UserInfo(botClient, user.lang, table.tableData,
                    table.tableData.managerA_ID == user.id, 1);
                //if (table.tableData.managerA_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"👤Менеджер-1: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.manager, 1);
            }

            if (table.tableData.giverA_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile(table.tableData.giverA_ID),
                    WebManager.RequestType.GetUserData, true);
                caption += userData.playerData.UserInfo(botClient, user.lang, table.tableData,
                    table.tableData.giverA_ID == user.id, table.tableData.verf_A, 1);
                //if (table.tableData.giverA_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-1: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver, 1);
            }

            if (table.tableData.giverB_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile(table.tableData.giverB_ID),
                    WebManager.RequestType.GetUserData, true);
                caption += userData.playerData.UserInfo(botClient, user.lang, table.tableData,
                    table.tableData.giverB_ID == user.id, table.tableData.verf_B, 2);
                //if (table.tableData.giverB_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-2: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver, 2);
            }

            if (table.tableData.managerB_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile(table.tableData.managerB_ID),
                    WebManager.RequestType.GetUserData, true);
                caption += userData.playerData.UserInfo(botClient, user.lang, table.tableData,
                    table.tableData.managerB_ID == user.id, 2);
                //if (table.tableData.managerB_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"👤Менеджер-2: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.manager, 2);
            }

            if (table.tableData.giverC_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile(table.tableData.giverC_ID),
                    WebManager.RequestType.GetUserData, true);
                caption += userData.playerData.UserInfo(botClient, user.lang, table.tableData,
                    table.tableData.giverC_ID == user.id, table.tableData.verf_C, 3);
                //if (table.tableData.giverC_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-3: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver, 3);
            }

            if (table.tableData.giverD_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile(table.tableData.giverD_ID),
                    WebManager.RequestType.GetUserData, true);
                caption += userData.playerData.UserInfo(botClient, user.lang, table.tableData,
                    table.tableData.giverD_ID == user.id, table.tableData.verf_D, 4);
                //if (table.tableData.giverD_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-4: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver, 4);
            }

            switch (lang)
            {
                case "ru":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                            }
                        });
                    break;
                }
                case "eng":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                            }
                        });
                    break;
                }
                case "fr":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                            }
                        });
                    break;
                }
                case "de":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Zurück", callbackAddress),
                                InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                            }
                        });
                    break;
                }
            }
        }
        else
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/MainMenu/mainMenu.png");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\MainMenu\mainMenu.png");
            switch (lang)
            {
                case "ru":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "Вы больше не участник стола";
                    break;
                }
                case "eng":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "You are no longer a member of the table";
                    break;
                }
                case "fe":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "Vous n'êtes plus membre de la table";
                    break;
                }
                case "de":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "Sie sind kein Mitglied des Tisches mehr";
                    break;
                }
            }
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void ShowTableAsImage(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData, Table.TableType tableType)
    {
        UserData? tableData = null;
        switch (tableType)
        {
            case Table.TableType.copper:
                if (userData.UserTableList.table_ID_copper != null)
                {
                    tableData = await WebManager.SendData(new TableProfile(userData.UserTableList.table_ID_copper),
                        WebManager.RequestType.GetTableData, true);
                }
                else tableData.tableData.tableID = 0;

                break;
            case Table.TableType.bronze:
                if (userData.UserTableList.table_ID_bronze != null)
                {
                    tableData = await WebManager.SendData(new TableProfile(userData.UserTableList.table_ID_bronze),
                        WebManager.RequestType.GetTableData, true);
                }
                else tableData.tableData.tableID = 0;

                break;
            case Table.TableType.silver:
                if (userData.UserTableList.table_ID_silver != null)
                {
                    tableData = await WebManager.SendData(new TableProfile(userData.UserTableList.table_ID_silver),
                        WebManager.RequestType.GetTableData, true);
                }
                else tableData.tableData.tableID = 0;

                break;
            case Table.TableType.gold:
                if (userData.UserTableList.table_ID_gold != null)
                {
                    tableData = await WebManager.SendData(new TableProfile(userData.UserTableList.table_ID_gold),
                        WebManager.RequestType.GetTableData, true);
                }
                else tableData.tableData.tableID = 0;

                break;
            case Table.TableType.platinum:
                if (userData.UserTableList.table_ID_platinum != null)
                {
                    tableData = await WebManager.SendData(new TableProfile(userData.UserTableList.table_ID_platinum),
                        WebManager.RequestType.GetTableData, true);
                }
                else tableData.tableData.tableID = 0;

                break;
            case Table.TableType.diamond:
                if (userData.UserTableList.table_ID_diamond != null)
                {
                    tableData = await WebManager.SendData(new TableProfile(userData.UserTableList.table_ID_diamond),
                        WebManager.RequestType.GetTableData, true);
                }
                else tableData.tableData.tableID = 0;

                break;
        }

        string path = null;

        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string? caption = null;
        Message sentPhoto;
        if (tableData.tableData.tableID != 0)
        {
            var callbackAddress = GetCallbackAddress(tableData.tableData.tableType);
            switch (userData.lang)
            {
                case "ru":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                            }
                        });
                    caption = $"<b>ID стола: {tableData.tableData.tableID}</b>";
                    break;
                case "eng":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                            }
                        });
                    caption = $"<b>Table ID: {tableData.tableData.tableID}</b>";
                    break;
                case "fr":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                            }
                        });
                    caption = $"<b>ID de table: {tableData.tableData.tableID}</b>";
                    break;
                case "de":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                            }
                        });
                    caption = $"<b>Tabellen-ID: {tableData.tableData.tableID}</b>";
                    break;
                default:
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                            }
                        });
                    caption = $"<b>Table ID: {tableData.tableData.tableID}</b>";
                    break;
            }

            Stream stream = TableImage.CreateTableImage(tableData.tableData, userData).Result;
            try
            {
                await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                    callbackData.Message.MessageId,
                    media: new InputMediaPhoto(new InputMedia(stream, "media"))
                ).WaitAsync(TimeSpan.FromSeconds(10));
            }
            catch
            {
                Trace.Write("Handle Remaining Exceptions");
                try
                {
                    await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            }
                        });
                    caption = "<b>An error occurred</b>\n\n" +
                              "Please contact technical support and describe what caused this error";
                    try
                    {
                        await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path),
                            caption,
                            ParseMode.Html,
                            replyMarkup: inlineKeyboard
                        );
                    }
                    catch
                    {
                    }
                }
                catch
                {
                    // ignored
                }
            }

            try
            {
                await botClient.EditMessageCaptionAsync(
                    callbackData.Message.Chat.Id,
                    callbackData.Message.MessageId,
                    caption,
                    ParseMode.Html,
                    null,
                    inlineKeyboard
                );
                stream.Dispose();
            }
            catch
            {
                Trace.Write("Handle Remaining Exceptions");
                try
                {
                    await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            }
                        });
                    caption = "<b>An error occurred</b>\n\n" +
                              "Please contact technical support and describe what caused this error";
                    try
                    {
                        await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path),
                            caption,
                            ParseMode.Html,
                            replyMarkup: inlineKeyboard
                        );
                    }
                    catch
                    {
                        
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }
        else
        {
            string lang = userData.lang;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images/MainMenu/mainMenu.png");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                    @"Images\MainMenu\mainMenu.png");
            switch (lang)
            {
                case "ru":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "Вы больше не участник стола";
                    break;
                }
                case "eng":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "You are no longer a member of the table";
                    break;
                }
                case "fe":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "Vous n'êtes plus membre de la table";
                    break;
                }
                case "de":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable")
                            }
                        });
                    caption = "Sie sind kein Mitglied des Tisches mehr";
                    break;
                }
            }

            try
            {
                using (Stream
                       stream = System.IO.File.OpenRead(path))
                    try
                    {
                        await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId,
                            media: new InputMediaPhoto(new InputMedia(stream, "media"))
                        ).WaitAsync(TimeSpan.FromSeconds(10));
                    }
                    catch
                    {
                    }
            }
            catch
            {
                Trace.Write("Handle Remaining Exceptions");
                try
                {
                    await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            }
                        });
                    caption = "<b>An error occurred</b>\n\n" +
                              "Please contact technical support and describe what caused this error";
                    try
                    {
                        await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path),
                            caption,
                            ParseMode.Html,
                            replyMarkup: inlineKeyboard
                        );
                    }
                    catch
                    {
                    }
                }
                catch
                {
                    // ignored
                }
            }

            try
            {
                await botClient.EditMessageCaptionAsync(
                    callbackData.Message.Chat.Id,
                    callbackData.Message.MessageId,
                    caption,
                    ParseMode.Html,
                    null,
                    inlineKeyboard
                );
            }
            catch
            {
                Trace.Write("Handle Remaining Exceptions");
                try
                {
                    await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            }
                        });
                    caption = "<b>An error occurred</b>\n\n" +
                              "Please contact technical support and describe what caused this error";
                    try
                    {
                        await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path),
                            caption,
                            ParseMode.Html,
                            replyMarkup: inlineKeyboard
                        );
                    }
                    catch
                    {
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }
    }

    public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile user, Error error, Table.TableType? tableType)
    {
        string path = null;
        Message? sentMessage;
        InlineKeyboardMarkup? inlineKeyboard = null;
        string? caption;
        switch (error)
        {
            case Error.UserIsNotExist:
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/status.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\status.png");

                UserData? tableToBack = null;
                switch (tableType)
                {
                    case Table.TableType.copper:
                        tableToBack = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_copper),
                            WebManager.RequestType.GetTableData, true);
                        break;
                    case Table.TableType.bronze:
                        tableToBack = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_bronze),
                            WebManager.RequestType.GetTableData, true);
                        break;
                    case Table.TableType.silver:
                        tableToBack = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_silver),
                            WebManager.RequestType.GetTableData, true);
                        break;
                    case Table.TableType.gold:
                        tableToBack = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_gold),
                            WebManager.RequestType.GetTableData, true);
                        break;
                    case Table.TableType.platinum:
                        tableToBack = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_platinum),
                            WebManager.RequestType.GetTableData, true);
                        break;
                    case Table.TableType.diamond:
                        tableToBack = await WebManager.SendData(new TableProfile(user.UserTableList.table_ID_diamond),
                            WebManager.RequestType.GetTableData, true);
                        break;
                }

                var callbackAddress = GetCallbackAddress(tableToBack.tableData.tableType);
                switch (user.lang)
                {
                    case "ru":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                }
                            });

                        caption = "🤷‍♂️ Этот Даритель ещё не присоединился к игре.";
                        break;
                    }

                    case "eng":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });
                        caption = "🤷‍♂️ This Giver has not joined the game yet.";
                        break;
                    }

                    case "fr":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Retour", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                }
                            });
                        caption = "🤷‍♂️ Ce Donateur n'a pas encore rejoint le jeu.";
                        break;
                    }

                    case "de":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                }
                            });
                        caption = "🤷‍♂️ Dieser Geber ist dem Spiel noch nicht beigetreten.";
                        break;
                    }
                    default:
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });
                        caption = "🤷‍♂️ This Giver has not joined the game yet.";
                        break;
                }

                try
                {
                    using (Stream
                           stream = System.IO.File.OpenRead(path))
                        try
                        {
                            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                                callbackData.Message.MessageId,
                                media: new InputMediaPhoto(new InputMedia(stream, "media"))
                            ).WaitAsync(TimeSpan.FromSeconds(10));
                        }
                        catch
                        {
                        }
                }
                catch
                {
                    Trace.Write("Handle Remaining Exceptions");
                    try
                    {
                        await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId);
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                }
                            });
                        caption = "<b>An error occurred</b>\n\n" +
                                  "Please contact technical support and describe what caused this error";
                        try
                        {
                            await botClient.SendPhotoAsync(
                                chatId,
                                File.OpenRead(path),
                                caption,
                                ParseMode.Html,
                                replyMarkup: inlineKeyboard
                            );
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                try
                {
                    await botClient.EditMessageCaptionAsync(
                        callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        caption,
                        ParseMode.Html,
                        null,
                        inlineKeyboard
                    );
                }
                catch
                {
                    Trace.Write("Handle Remaining Exceptions");
                    try
                    {
                        await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId);
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                }
                            });
                        caption = "<b>An error occurred</b>\n\n" +
                                  "Please contact technical support and describe what caused this error";
                        try
                        {
                            await botClient.SendPhotoAsync(
                                chatId,
                                File.OpenRead(path),
                                caption,
                                ParseMode.Html,
                                replyMarkup: inlineKeyboard
                            );
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                break;
            }
            case Error.RefLinkInvalid:
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/mainMenu.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\mainMenu.png");
                caption = null;
                switch (user.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📣 Общий чат", "https://t.me/cashflow_official_chat"),
                                }
                            });
                        caption = $"<b>👋 Добро пожаловать в игру " +
                                  $"\n«CASH FLOW»!</b>" +
                                  $"\n\nЧтобы продолжить пользоваться ботом, зайдите по реферальной ссылке!";

                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📣 General chat",
                                        "https://t.me/cashflow_official_chat"),
                                }
                            });
                        caption = $"<b>👋 Welcome to the game" +
                                  $"\n\"CASH FLOW\"!</b>" +
                                  $"\n\nTo continue using the bot, go via referral link!";
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📣 Chat général",
                                        "https://t.me/cashflow_official_chat"),
                                }
                            });
                        caption = $"<b>👋 Bienvenue dans le jeu" +
                                  $"\n\"FLUX DE TRÉSORERIE\"!</b>" +
                                  $"\n\nPour continuer à utiliser le bot, passez par le lien de parrainage!";
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithUrl("📣 Allgemeiner Chat",
                                        "https://t.me/cashflow_official_chat"),
                                }
                            });
                        caption = $"<b>👋 Willkommen zum Spiel" +
                                  $"\n\"CASH FLOW\"!</b>" +
                                  $"\n\nUm den Bot weiter zu verwenden, gehen Sie über den Empfehlungslink!";
                        break;
                }

                try
                {
                    sentMessage = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }

                break;
            case Error.UserWithoutUsername:
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/mainMenu.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\mainMenu.png");
                caption = null;
                switch (user.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅ Продолжить", user.refId + "|TryToReg")
                                }
                            });
                        caption = $"<b>👋 Добро пожаловать в игру " +
                                  $"\n«CASH FLOW»!</b>" +
                                  $"\n\nЧтобы продолжить пользоваться ботом, установите \"Имя Пользователя\" в настройках Telegram:" +
                                  $"\n\n<b>Настройки - Изменить - Имя пользователя</b>";

                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅ Continue", user.refId + "|TryToReg")
                                }
                            });
                        caption = $"<b>👋 Welcome to the game" +
                                  $"\n\"CASH FLOW\"!</b>" +
                                  $"\n\nTo continue using the bot, set the \"Username\" in Telegram settings:" +
                                  $"\n\n<b>Settings - Edit - Username</b>";
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅ Continuer", user.refId + "|TryToReg")
                                }
                            });
                        caption = $"<b>👋 Bienvenue dans le jeu" +
                                  $"\n\"FLUX DE TRÉSORERIE\"!</b>" +
                                  $"\n\nPour continuer à utiliser le bot, définissez le \"Nom d'utilisateur\" dans les paramètres de Telegram :" +
                                  $"\n\n<b>Paramètres - Modifier - Nom d'utilisateur</b>";
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅ Fortsetzen", user.refId + "|TryToReg")
                                }
                            });
                        caption = $"<b>👋 Willkommen zum Spiel" +
                                  $"\n\"CASH FLOW\"!</b>" +
                                  $"\n\nUm den Bot weiterhin zu verwenden, legen Sie den \"Benutzernamen\" in den Telegram-Einstellungen fest:" +
                                  $"\n\n<b>Einstellungen - Bearbeiten - Benutzername</b>";
                        break;
                }

                if (callbackData.Data == "null")
                {
                    try
                    {
                        sentMessage = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path),
                            caption,
                            ParseMode.Html,
                            replyMarkup: inlineKeyboard
                        );
                    }
                    catch
                    {
                    }
                }
                else
                {
                    try
                    {
                        using (Stream
                               stream = System.IO.File.OpenRead(path))
                            try
                            {
                                await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                                    callbackData.Message.MessageId,
                                    media: new InputMediaPhoto(new InputMedia(stream, "media"))
                                ).WaitAsync(TimeSpan.FromSeconds(10));
                            }
                            catch
                            {
                            }
                    }
                    catch
                    {
                        Trace.Write("Handle Remaining Exceptions");
                        try
                        {
                            await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                                callbackData.Message.MessageId);
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                    }
                                });
                            caption = "<b>An error occurred</b>\n\n" +
                                      "Please contact technical support and describe what caused this error";
                            try
                            {
                                await botClient.SendPhotoAsync(
                                    chatId,
                                    File.OpenRead(path),
                                    caption,
                                    ParseMode.Html,
                                    replyMarkup: inlineKeyboard
                                );
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                    try
                    {
                        await botClient.EditMessageCaptionAsync(
                            callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId,
                            caption,
                            ParseMode.Html,
                            null,
                            inlineKeyboard
                        );
                    }
                    catch
                    {
                        Trace.Write("Handle Remaining Exceptions");
                        try
                        {
                            await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                                callbackData.Message.MessageId);
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                    }
                                });
                            caption = "<b>An error occurred</b>\n\n" +
                                      "Please contact technical support and describe what caused this error";
                            try
                            {
                                await botClient.SendPhotoAsync(
                                    chatId,
                                    File.OpenRead(path),
                                    caption,
                                    ParseMode.Html,
                                    replyMarkup: inlineKeyboard
                                );
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }

                break;
        }
    }

    public static async void ConnectingError(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile user, Error error)
    {
        string? caption = null;
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        switch (error)
        {
            case Error.UserAlreadyAtAnotherTable:
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/mainMenu.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\mainMenu.png");
                var tableData = await WebManager.SendData(user, WebManager.RequestType.GetTableData, true);
                Trace.Write(tableData.tableData.tableType);
                switch (user.lang)
                {
                    case "ru":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Назад", "ChooseTable")
                                }
                            });

                        caption = $"🤷 Вы уже находитесь на столе: {tableData.tableData.GetTableType(user)}.";

                        break;
                    }

                    case "eng":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Back", "ChooseTable")
                                }
                            });
                        caption = $"🤷 You are already on the table: {tableData.tableData.GetTableType(user)}.";
                        break;
                    }

                    case "fr":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Retour", "ChooseTable")
                                }
                            });
                        caption = $"🤷 Vous êtes déjà sur la table: {tableData.tableData.GetTableType(user)}.";
                        break;
                    }

                    case "de":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Zurück", "ChooseTable")
                                }
                            });
                        caption = $"🤷 Sie sind bereits auf dem Tisch: {tableData.tableData.GetTableType(user)}.";
                        break;
                    }
                }

                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Markdown,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void ConnectingError(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile user, Error error, int toInvite)
    {
        string? caption = null;
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        toInvite = Math.Abs(user.invited - toInvite);
        switch (error)
        {
            case Error.UserDontMeetConnetionRequriments:
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/mainMenu.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\mainMenu.png");
                switch (user.lang)
                {
                    case "ru":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Назад", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                }
                            });
                        if (toInvite < 2)
                        {
                        }

                        caption =
                            $"🤷 Для открытия данного стола Вам необходимо пригласить на Бронзовый стол еще {toInvite} игроков или пройти стол уровнем ниже";
                        break;
                    }

                    case "eng":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Back", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("🗂 Main menu", "MainMenu")
                                }
                            });
                        caption =
                            "🤷 To open this table, you need to fulfill the conditions for invited players OR go through the table below.";
                        break;
                    }

                    case "fr":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Retour", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("🗂 Menu principal", "MainMenu")
                                }
                            });
                        caption =
                            "🤷 Pour ouvrir ce tableau, vous devez remplir les conditions des joueurs invités OU passer par le tableau ci-dessous.";
                        break;
                    }

                    case "de":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Zurück", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                }
                            });
                        caption =
                            "🤷 Um diese Tabelle zu öffnen, müssen Sie die Bedingungen für eingeladene Spieler erfüllen ODER die Tabelle unten durchgehen.";
                        break;
                    }
                }

                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Markdown,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void MainMenu(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        string lang)
    {
        string path = null;
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string? caption;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenuImage.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenuImage.png");
        Message? sentPhoto;
        switch (lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Выбрать стол", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 Мой статус", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Инфо", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Реф. ссылка", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Тех. поддержка", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Сменить язык", "ChangeLang")
                        }
                    });
                caption = $"🗂 <b>Главное меню</b>" +
                          "\n\nВыберите нужный раздел:";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Choose the table", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 My status", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Refferal link", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Change language", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Main Menu</b>" +
                          $"\n\nSelect the desired section:";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Choisissez le tableau", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 Mon statut", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Lien de référence", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Soutien technique", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Changer de langue", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Menu</b>" +
                          $"\n\nSélectionnez la rubrique souhaitée:";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Tisch auswählen", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 Mein status", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Die Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Empfehlungslink", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Technischer Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Sprache ändern", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Das Menu</b>" +
                          $"\n\nWählen Sie den gewünschten Abschnitt aus:";
                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Choose the table", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 My status", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Refferal link", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Change language", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Main Menu</b>" +
                          $"\n\nSelect the desired section:";
                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void MainMenu(ITelegramBotClient botClient, long chatId, string lang)
    {
        string path = null;
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string? caption;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        Message? sentPhoto;
        switch (lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Выбрать стол", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 Мой статус", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Инфо", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Реф. ссылка", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Тех. поддержка", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Сменить язык", "ChangeLang")
                        }
                    });
                caption = $"🗂 <b>Главное меню</b>" +
                          "\n\nВыберите нужный раздел:";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Choose the table", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 My status", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Refferal link", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Change language", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Main Menu</b>" +
                          $"\n\nSelect the desired section:";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Choisissez le tableau", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 Mon statut", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Lien de référence", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Soutien technique", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Changer de langue", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Menu</b>" +
                          $"\n\nSélectionnez la rubrique souhaitée:";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Tisch auswählen", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 Mein status", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Die Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Empfehlungslink", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Technischer Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Sprache ändern", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Das Menu</b>" +
                          $"\n\nWählen Sie den gewünschten Abschnitt aus:";
                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("⚜ Choose the table", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("🔖 My status", "Status")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📄 Info", "Info"),
                            InlineKeyboardButton.WithCallbackData("🔗 Refferal link", "RefLink")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                            InlineKeyboardButton.WithCallbackData("🌐 Change language", "ChangeLang")
                        }
                    });
                caption = $"<b>🗂 Main Menu</b>" +
                          $"\n\nSelect the desired section:";
                break;
        }

        try
        {
            sentMessage = await botClient.SendPhotoAsync(
                chatId,
                File.OpenRead(path),
                caption,
                ParseMode.Html,
                replyMarkup: inlineKeyboard
            );
        }
        catch
        {
        }
    }

    public static async void Status(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData)
    {
        //GetTableData - TableIsNotFound - ERROR
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/status.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\status.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        string? caption = null;
        switch (userData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📣 Общий чат", "https://t.me/cashflow_official_chat"),
                            InlineKeyboardButton.WithUrl("💠 Канал", "https://t.me/original_cashflow")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuRU
                        }
                    });
                caption = "🔖 <b>Мой статус</b>" +
                          "\n\n" +
                          $"<b>Ваш ID:</b> {callbackData.From.Id}" +
                          "\n" +
                          $"<b>Ваш ник:</b> @{userData.username}" +
                          "\n" +
                          $"<b>Лично приглашённых:</b> {userData.invited}" +
                          "\n" +
                          $"<b>Общая команда:</b> {userData.team}" +
                          "\n\n" +
                          $"<b>Получено подарков на сумму:</b> {userData.giftsReceived}$" +
                          "\n" +
                          "<b>Фиксированный курс:</b>" +
                          "\n" +
                          "📈 1$ = 0.98€ = 62₽" +
                          "\n\n" +
                          $"<b>Вас пригласил:</b> @{userData.invitedBy}" +
                          "\n" +
                          "<b>Ваши столы:</b>" +
                          "\n";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📣 General chat", "https://t.me/cashflow_official_chat"),
                            InlineKeyboardButton.WithUrl("💠 Channel", "https://t.me/original_cashflow")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });
                caption = "🔖 <b>My Status</b>" +
                          "\n\n" +
                          $"<b>Your ID:</b> {callbackData.From.Id}" +
                          "\n" +
                          $"<b>Your nickname:</b> @{userData.username}" +
                          "\n" +
                          $"<b>Personally invited:</b> {userData.invited}" +
                          "\n" +
                          $"<b>Common team:</b> {userData.team}" +
                          "\n\n" +
                          $"<b>Gifts worth:</b> {userData.giftsReceived}$" +
                          "\n" +
                          "<b>Fixed exchange rate:</b>" +
                          "\n" +
                          "📈 1$ = 0.98€ = 62₽" +
                          "\n\n" +
                          $"<b>You were invited by:</b> @{userData.invitedBy}" +
                          "\n" +
                          "<b>Your tables:</b>" +
                          "\n";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📣 Chat général", "https://t.me/cashflow_official_chat"),
                            InlineKeyboardButton.WithUrl("💠 Le canal", "https://t.me/original_cashflow")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuFR
                        }
                    });
                caption = "🔖 <b>Mon statut</b>" +
                          "\n\n" +
                          $"<b>Votre identifiant:</b> {callbackData.From.Id}" +
                          "\n" +
                          $"<b>Votre surnom:</b> @{userData.username}" +
                          "\n" +
                          $"<b>Personnellement invité:</b> {userData.invited}" +
                          "\n" +
                          $"<b>Équipe commune:</b> {userData.team}" +
                          "\n\n" +
                          $"<b>Valeur des cadeaux:</b> {userData.giftsReceived}$" +
                          "\n" +
                          "<b>Taux de change fixe:</b>" +
                          "\n" +
                          "📈 1$ = 0,98€ = 62₽" +
                          "\n\n" +
                          $"<b>Vous avez été invité par:</b> @{userData.invitedBy}" +
                          "\n" +
                          "<b>Vos tableaux:</b>" +
                          "\n";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📣 Allgemeiner Chat", "https://t.me/cashflow_official_chat"),
                            InlineKeyboardButton.WithUrl("💠 Kanal)", "https://t.me/original_cashflow")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuDE
                        }
                    });
                caption = "🔖 <b>Mein Status</b>" +
                          "\n\n" +
                          $"<b>Ihre ID:</b> {callbackData.From.Id}" +
                          "\n" +
                          $"<b>Ihr Nickname:</b> @{userData.username}" +
                          "\n" +
                          $"<b>Persönlich eingeladen:</b> {userData.invited}" +
                          "\n" +
                          $"<b>Gemeinsames Team:</b> {userData.team}" +
                          "\n\n" +
                          $"<b>Geschenke im Wert von:</b> {userData.giftsReceived}$" +
                          "\n" +
                          "<b>Fester Wechselkurs:</b>" +
                          "\n" +
                          "📈 1$ = 0,98€ = 62₽" +
                          "\n\n" +
                          $"<b>Sie wurden eingeladen von:</b> @{userData.invitedBy}" +
                          "\n" +
                          "<b>Ihre Tabellen:</b>" +
                          "\n";
                break;
        }

        var copper = false;
        var bronze = false;
        var silver = false;
        var gold = false;
        var platinum = false;
        var diamond = false;

        var IsGiverVerf = false;
        string tableType;
        string tableRole;
        int num = 0;

        if (userData.UserTableList.table_ID_copper != null)
        {
            num = 0;
            var tableDataCopper = await WebManager.SendData(
                new TableProfile(userData.UserTableList.table_ID_copper), WebManager.RequestType.GetTableData, true);

            copper = true;
            var giverCountCopper = 0;
            tableRole = userData.GetTableRole(userData.lang, Table.TableType.copper);
            num = 0;
            if (tableDataCopper.tableData.managerA_ID != null)
            {
                if (tableDataCopper.tableData.managerA_ID == userData.id)
                {
                    num = 1;
                }
            }

            if (tableDataCopper.tableData.managerA_ID != null)
            {
                if (tableDataCopper.tableData.managerB_ID == userData.id)
                {
                    num = 2;
                }
            }

            if (tableDataCopper.tableData.giverA_ID != null)
            {
                if (tableDataCopper.tableData.giverA_ID == userData.id)
                {
                    num = 1;
                    if (tableDataCopper.tableData.verf_A)
                        IsGiverVerf = true;
                }

                giverCountCopper++;
            }

            if (tableDataCopper.tableData.giverB_ID != null)
            {
                if (tableDataCopper.tableData.giverB_ID == userData.id)
                {
                    num = 2;
                    if (tableDataCopper.tableData.verf_B)
                        IsGiverVerf = true;
                }

                giverCountCopper++;
            }

            if (tableDataCopper.tableData.giverC_ID != null)
            {
                if (tableDataCopper.tableData.giverC_ID == userData.id)
                {
                    num = 3;
                    if (tableDataCopper.tableData.verf_C)
                        IsGiverVerf = true;
                }

                giverCountCopper++;
            }

            if (tableDataCopper.tableData.giverD_ID != null)
            {
                if (tableDataCopper.tableData.giverD_ID == userData.id)
                {
                    num = 4;
                    if (tableDataCopper.tableData.verf_D)
                        IsGiverVerf = true;
                }

                giverCountCopper++;
            }

            string numeration = $"-{num}";
            tableType = TableProfile.GetTableType(userData, Table.TableType.copper);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType} стол</b>" +
                               $"\n" +
                               $"Ваша роль: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Всего дарителей на столе: {giverCountCopper} из 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Вы активированы на столе\n";
                        else caption += "\n❌ Вы не активированы на столе\n";
                    }
                    else caption += "\n";

                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Your role: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total givers on the table: {giverCountCopper} of 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ You are activated on the table\n";
                        else caption += "\n❌ You are not activated on the table\n";
                    }
                    else caption += "\n";

                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total des donateurs sur la table: {giverCountCopper} sur 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Vous êtes activé sur la table\n";
                        else caption += "\n❌ Vous n'êtes pas activé sur la table\n";
                    }
                    else caption += "\n";

                    break;
                case "de":
                    caption += "\n" +
                               $"<b>{tableType} tisch</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountCopper} von 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Du bist auf dem Tisch aktiviert\n";
                        else caption += "\n❌ Du bist am Tisch nicht aktiviert\n";
                    }
                    else caption += "\n";

                    break;
            }
        }

        if (userData.UserTableList.table_ID_bronze != null)
        {
            num = 0;
            var tableDataBronze = await WebManager.SendData(
                new TableProfile(userData.UserTableList.table_ID_bronze), WebManager.RequestType.GetTableData, true);

            bronze = true;
            var giverCountBronze = 0;
            tableRole = userData.GetTableRole(userData.lang, Table.TableType.bronze);
            if (tableDataBronze.tableData.managerA_ID != null)
            {
                if (tableDataBronze.tableData.managerA_ID == userData.id)
                {
                    num = 1;
                }
            }

            if (tableDataBronze.tableData.managerA_ID != null)
            {
                if (tableDataBronze.tableData.managerB_ID == userData.id)
                {
                    num = 2;
                }
            }

            if (tableDataBronze.tableData.giverA_ID != null)
            {
                if (tableDataBronze.tableData.giverA_ID == userData.id)
                {
                    num = 1;
                    if (tableDataBronze.tableData.verf_A)
                        IsGiverVerf = true;
                }

                giverCountBronze++;
            }

            if (tableDataBronze.tableData.giverB_ID != null)
            {
                if (tableDataBronze.tableData.giverB_ID == userData.id)
                {
                    num = 2;
                    if (tableDataBronze.tableData.verf_B)
                        IsGiverVerf = true;
                }

                giverCountBronze++;
            }

            if (tableDataBronze.tableData.giverC_ID != null)
            {
                if (tableDataBronze.tableData.giverC_ID == userData.id)
                {
                    num = 3;
                    if (tableDataBronze.tableData.verf_C)
                        IsGiverVerf = true;
                }

                giverCountBronze++;
            }

            if (tableDataBronze.tableData.giverD_ID != null)
            {
                if (tableDataBronze.tableData.giverD_ID == userData.id)
                {
                    num = 4;
                    if (tableDataBronze.tableData.verf_D)
                        IsGiverVerf = true;
                }

                giverCountBronze++;
            }

            string numeration = $"-{num}";
            tableType = TableProfile.GetTableType(userData, Table.TableType.bronze);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType} стол</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Всего дарителей на столе: {giverCountBronze} из 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Вы активированы на столе\n";
                        else caption += "\n❌ Вы не активированы на столе\n";
                    }
                    else caption += "\n";

                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Your role: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total givers on the table: {giverCountBronze} of 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ You are activated on the table\n";
                        else caption += "\n❌ You are not activated on the table\n";
                    }
                    else caption += "\n";

                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole} tisch";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total des donateurs sur la table: {giverCountBronze} sur 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Vous êtes activé sur la table\n";
                        else caption += "\n❌ Vous n'êtes pas activé sur la table\n";
                    }
                    else caption += "\n";

                    break;
                case "de":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole} table";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountBronze} von 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Du bist auf dem Tisch aktiviert\n";
                        else caption += "\n❌ Du bist am Tisch nicht aktiviert\n";
                    }
                    else caption += "\n";

                    break;
            }
        }

        if (userData.UserTableList.table_ID_silver != null)
        {
            num = 0;
            var tableDataSilver = await WebManager.SendData(
                new TableProfile(userData.UserTableList.table_ID_silver), WebManager.RequestType.GetTableData, true);

            silver = true;
            var giverCountSilver = 0;

            tableRole = userData.GetTableRole(userData.lang, Table.TableType.silver);
            if (tableDataSilver.tableData.managerA_ID != null)
            {
                if (tableDataSilver.tableData.managerA_ID == userData.id)
                {
                    num = 1;
                }
            }

            if (tableDataSilver.tableData.managerA_ID != null)
            {
                if (tableDataSilver.tableData.managerB_ID == userData.id)
                {
                    num = 2;
                }
            }

            if (tableDataSilver.tableData.giverA_ID != null)
            {
                if (tableDataSilver.tableData.giverA_ID == userData.id)
                {
                    num = 1;
                    if (tableDataSilver.tableData.verf_A)
                        IsGiverVerf = true;
                }

                giverCountSilver++;
            }

            if (tableDataSilver.tableData.giverB_ID != null)
            {
                if (tableDataSilver.tableData.giverB_ID == userData.id)
                {
                    num = 2;
                    if (tableDataSilver.tableData.verf_B)
                        IsGiverVerf = true;
                }

                giverCountSilver++;
            }

            if (tableDataSilver.tableData.giverC_ID != null)
            {
                if (tableDataSilver.tableData.giverC_ID == userData.id)
                {
                    num = 3;
                    if (tableDataSilver.tableData.verf_C)
                        IsGiverVerf = true;
                }

                giverCountSilver++;
            }

            if (tableDataSilver.tableData.giverD_ID != null)
            {
                if (tableDataSilver.tableData.giverD_ID == userData.id)
                {
                    num = 4;
                    if (tableDataSilver.tableData.verf_D)
                        IsGiverVerf = true;
                }

                giverCountSilver++;
            }

            string numeration = $"-{num}";
            tableType = TableProfile.GetTableType(userData, Table.TableType.silver);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType} стол</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Всего дарителей на столе: {giverCountSilver} из 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Вы активированы на столе\n";
                        else caption += "\n❌ Вы не активированы на столе\n";
                    }
                    else caption += "\n";

                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Your role: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total givers on the table: {giverCountSilver} of 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ You are activated on the table\n";
                        else caption += "\n❌ You are not activated on the table\n";
                    }
                    else caption += "\n";

                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType} tisch</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total des donateurs sur la table: {giverCountSilver} sur 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Vous êtes activé sur la table\n";
                        else caption += "\n❌ Vous n'êtes pas activé sur la table\n";
                    }
                    else caption += "\n";

                    break;
                case "de":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountSilver} von 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Du bist auf dem Tisch aktiviert\n";
                        else caption += "\n❌ Du bist am Tisch nicht aktiviert\n";
                    }
                    else caption += "\n";

                    break;
            }
        }

        if (userData.UserTableList.table_ID_gold != null)
        {
            num = 0;
            var tableDataGold = await WebManager.SendData(new TableProfile(userData.UserTableList.table_ID_gold),
                WebManager.RequestType.GetTableData, true);

            gold = true;
            var giverCountGold = 0;

            tableRole = userData.GetTableRole(userData.lang, Table.TableType.gold);
            if (tableDataGold.tableData.managerA_ID != null)
            {
                if (tableDataGold.tableData.managerA_ID == userData.id)
                {
                    num = 1;
                }
            }

            if (tableDataGold.tableData.managerA_ID != null)
            {
                if (tableDataGold.tableData.managerB_ID == userData.id)
                {
                    num = 2;
                }
            }

            if (tableDataGold.tableData.giverA_ID != null)
            {
                if (tableDataGold.tableData.giverA_ID == userData.id)
                {
                    num = 1;
                    if (tableDataGold.tableData.verf_A)
                        IsGiverVerf = true;
                }

                giverCountGold++;
            }

            if (tableDataGold.tableData.giverB_ID != null)
            {
                if (tableDataGold.tableData.giverB_ID == userData.id)
                {
                    num = 2;
                    if (tableDataGold.tableData.verf_B)
                        IsGiverVerf = true;
                }

                giverCountGold++;
            }

            if (tableDataGold.tableData.giverC_ID != null)
            {
                if (tableDataGold.tableData.giverC_ID == userData.id)
                {
                    num = 3;
                    if (tableDataGold.tableData.verf_C)
                        IsGiverVerf = true;
                }

                giverCountGold++;
            }

            if (tableDataGold.tableData.giverD_ID != null)
            {
                if (tableDataGold.tableData.giverD_ID == userData.id)
                {
                    num = 4;
                    if (tableDataGold.tableData.verf_D)
                        IsGiverVerf = true;
                }

                giverCountGold++;
            }

            string numeration = $"-{num}";
            tableType = TableProfile.GetTableType(userData, Table.TableType.gold);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType} стол</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Всего дарителей на столе: {giverCountGold} из 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Вы активированы на столе\n";
                        else caption += "\n❌ Вы не активированы на столе\n";
                    }
                    else caption += "\n";

                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Your role: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total givers on the table: {giverCountGold} of 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ You are activated on the table\n";
                        else caption += "\n❌ You are not activated on the table\n";
                    }
                    else caption += "\n";

                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType} tisch</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total des donateurs sur la table: {giverCountGold} sur 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Vous êtes activé sur la table\n";
                        else caption += "\n❌ Vous n'êtes pas activé sur la table\n";
                    }
                    else caption += "\n";

                    break;
                case "de":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountGold} von 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Du bist auf dem Tisch aktiviert\n";
                        else caption += "\n❌ Du bist am Tisch nicht aktiviert\n";
                    }
                    else caption += "\n";

                    break;
            }
        }

        if (userData.UserTableList.table_ID_platinum != null)
        {
            num = 0;
            var tableDataPlatinum = await WebManager.SendData(
                new TableProfile(userData.UserTableList.table_ID_platinum), WebManager.RequestType.GetTableData, true);

            platinum = true;
            var giverCountPlatinum = 0;

            tableRole = userData.GetTableRole(userData.lang, Table.TableType.platinum);
            if (tableDataPlatinum.tableData.managerA_ID != null)
            {
                if (tableDataPlatinum.tableData.managerA_ID == userData.id)
                {
                    num = 1;
                }
            }

            if (tableDataPlatinum.tableData.managerA_ID != null)
            {
                if (tableDataPlatinum.tableData.managerB_ID == userData.id)
                {
                    num = 2;
                }
            }

            if (tableDataPlatinum.tableData.giverA_ID != null)
            {
                if (tableDataPlatinum.tableData.giverA_ID == userData.id)
                {
                    num = 1;
                    if (tableDataPlatinum.tableData.verf_A)
                        IsGiverVerf = true;
                }

                giverCountPlatinum++;
            }

            if (tableDataPlatinum.tableData.giverB_ID != null)
            {
                if (tableDataPlatinum.tableData.giverB_ID == userData.id)
                {
                    num = 2;
                    if (tableDataPlatinum.tableData.verf_B)
                        IsGiverVerf = true;
                }

                giverCountPlatinum++;
            }

            if (tableDataPlatinum.tableData.giverC_ID != null)
            {
                if (tableDataPlatinum.tableData.giverC_ID == userData.id)
                {
                    num = 3;
                    if (tableDataPlatinum.tableData.verf_C)
                        IsGiverVerf = true;
                }

                giverCountPlatinum++;
            }

            if (tableDataPlatinum.tableData.giverD_ID != null)
            {
                if (tableDataPlatinum.tableData.giverD_ID == userData.id)
                {
                    num = 4;
                    if (tableDataPlatinum.tableData.verf_D)
                        IsGiverVerf = true;
                }

                giverCountPlatinum++;
            }

            string numeration = $"-{num}";
            tableType = TableProfile.GetTableType(userData, Table.TableType.platinum);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType} стол</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Всего дарителей на столе: {giverCountPlatinum} из 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Вы активированы на столе\n";
                        else caption += "\n❌ Вы не активированы на столе\n";
                    }

                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Your role: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total givers on the table: {giverCountPlatinum} of 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ You are activated on the table\n";
                        else caption += "\n❌ You are not activated on the table\n";
                    }

                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType} tisch</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total des donateurs sur la table: {giverCountPlatinum} sur 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Vous êtes activé sur la table\n";
                        else caption += "\n❌ Vous n'êtes pas activé sur la table\n";
                    }

                    break;
                case "de":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountPlatinum} von 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Du bist auf dem Tisch aktiviert\n";
                        else caption += "\n❌ Du bist am Tisch nicht aktiviert\n";
                    }
                    else caption += "\n";

                    break;
            }
        }

        if (userData.UserTableList.table_ID_diamond != null)
        {
            num = 0;
            var tableDataDiamond = await WebManager.SendData(
                new TableProfile(userData.UserTableList.table_ID_diamond), WebManager.RequestType.GetTableData, true);

            diamond = true;
            var giverCountDiamond = 0;

            tableRole = userData.GetTableRole(userData.lang, Table.TableType.diamond);
            if (tableDataDiamond.tableData.managerA_ID != null)
            {
                if (tableDataDiamond.tableData.managerA_ID == userData.id)
                {
                    num = 1;
                }
            }

            if (tableDataDiamond.tableData.managerA_ID != null)
            {
                if (tableDataDiamond.tableData.managerB_ID == userData.id)
                {
                    num = 2;
                }
            }

            if (tableDataDiamond.tableData.giverA_ID != null)
            {
                if (tableDataDiamond.tableData.giverA_ID == userData.id)
                {
                    num = 1;
                    if (tableDataDiamond.tableData.verf_A)
                        IsGiverVerf = true;
                }

                giverCountDiamond++;
            }

            if (tableDataDiamond.tableData.giverB_ID != null)
            {
                if (tableDataDiamond.tableData.giverB_ID == userData.id)
                {
                    num = 2;
                    if (tableDataDiamond.tableData.verf_B)
                        IsGiverVerf = true;
                }

                giverCountDiamond++;
            }

            if (tableDataDiamond.tableData.giverC_ID != null)
            {
                if (tableDataDiamond.tableData.giverC_ID == userData.id)
                {
                    num = 3;
                    if (tableDataDiamond.tableData.verf_C)
                        IsGiverVerf = true;
                }

                giverCountDiamond++;
            }

            if (tableDataDiamond.tableData.giverD_ID != null)
            {
                if (tableDataDiamond.tableData.giverD_ID == userData.id)
                {
                    num = 4;
                    if (tableDataDiamond.tableData.verf_D)
                        IsGiverVerf = true;
                }

                giverCountDiamond++;
            }

            string numeration = $"-{num}";
            tableType = TableProfile.GetTableType(userData, Table.TableType.diamond);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType} стол</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Всего дарителей на столе: {giverCountDiamond} из 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Вы активированы на столе\n";
                        else caption += "\n❌ Вы не активированы на столе\n";
                    }

                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Your role: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total givers on the table: {giverCountDiamond} of 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ You are activated on the table\n";
                        else caption += "\n❌ You are not activated on the table\n";
                    }

                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType} tisch</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Total des donateurs sur la table: {giverCountDiamond} sur 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Vous êtes activé sur la table\n";
                        else caption += "\n❌ Vous n'êtes pas activé sur la table\n";
                    }

                    break;
                case "de":
                    caption += "\n" +
                               $"<b>{tableType} table</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}";
                    if (num != 0)
                    {
                        caption += $"{numeration}";
                    }

                    caption += "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountDiamond} von 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "\n✅ Du bist auf dem Tisch aktiviert\n";
                        else caption += "\n❌ Du bist am Tisch nicht aktiviert\n";
                    }
                    else caption += "\n";

                    break;
            }
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
            {
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
            }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            ).WaitAsync(TimeSpan.FromSeconds(10));
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void RefLink(ITelegramBotClient botClient, long chatId, UserProfile userData,
        CallbackQuery callbackData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/refLink.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\refLink.png");
        InlineKeyboardMarkup inlineKeyboard = null;
        Message? sentMessage;
        string? caption;
        if (userData.id != 0)
        {
            Trace.Write("https://t.me/originalCashFlowbot?start=R" + userData.id);
            Message sentPhoto;
            switch (userData.lang)
            {
                case "ru":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonMainMenuRU
                            }
                        });
                    caption = $"<b>🔗 Ваша реферальная ссылка:</b>" +
                              $"\n\nhttps://t.me/originalCashFlowbot?start=R{userData.id}";
                    break;
                case "eng":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonMainMenuENG
                            }
                        });
                    caption = $"<b>🔗 Your referral link:</b>" +
                              $"\n\nhttps://t.me/originalCashFlowbot?start=R{userData.id}";
                    break;
                case "fr":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonMainMenuFR
                            }
                        });
                    caption = $"<b>🔗 Votre lien de référence:</b>" +
                              $"\n\nhttps://t.me/originalCashFlowbot?start=R{userData.id}";
                    break;
                case "de":
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonMainMenuDE
                            }
                        });
                    caption = $"<b>🔗 Ihr Empfehlungslink:</b>" +
                              $"\n\nhttps://t.me/originalCashFlowbot?start=R{userData.id}";
                    break;
                default:
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonMainMenuENG
                            }
                        });
                    caption = $"<b>🔗 Your referral link:</b>" +
                              $"\n\nhttps://t.me/originalCashFlowbot?start=R{userData.id}";
                    break;
            }
        }
        else
        {
            inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                    }
                });
            caption = "<b>An error occurred</b>\n\n" +
                      "Please contact technical support and describe what caused this error";
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                    callbackData.Message.MessageId,
                    media: new InputMediaPhoto(new InputMedia(stream, "media"))
                ).WaitAsync(TimeSpan.FromSeconds(10));
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }

            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void Agreement(ITelegramBotClient botClient, long chatId,
        CallbackQuery callbackData, UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/agreement.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\agreement.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        Message sentPhoto;
        string? caption;
        switch (userData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📖 Прочитать", "https://telegra.ph/Opta-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ Я принимаю", "MainMenu")
                        }
                    });
                caption = $"<b>📌 ПОЛЬЗОВАТЕЛЬСКОЕ СОГЛАШЕНИЕ</b>" +
                          $"\n\nПеред началом игры пользователь обязуется ознакомиться с текстом данного соглашения:";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📖 Read", "https://telegra.ph/User-Agreement-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ I accept", "MainMenu")
                        }
                    });
                caption = $"<b>📌 User Agreement</b>" +
                          $"\nBefore starting the game, the user undertakes to read the text of this agreement:";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📖 Lire", "https://telegra.ph/Conditions-dutilisation-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ J'accepte", "MainMenu")
                        }
                    });
                caption = $"<b>📌 Conditions d'utilisation</b>" +
                          $"\nAvant de commencer le jeu, l'utilisateur s'engage à lire le texte de cet accord:";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📖 Lesen", "https://telegra.ph/Benutzervereinbarung-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ Ich akzeptiere", "MainMenu")
                        }
                    });
                caption = $"<b>📌 Benutzervereinbarung</b>" +
                          $"\nVor Spielbeginn verpflichtet sich der Nutzer, den Text dieser Vereinbarung zu lesen:";
                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📖 Read", "https://telegra.ph/User-Agreement-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ I accept", "MainMenu")
                        }
                    });
                caption = $"<b>📌 User Agreement</b>" +
                          $"\nBefore starting the game, the user undertakes to read the text of this agreement:";
                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                    callbackData.Message.MessageId,
                    media: new InputMediaPhoto(new InputMedia(stream, "media"))
                ).WaitAsync(TimeSpan.FromSeconds(10));
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void TechSupport(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/techSupport.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\techSupport.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string? caption;
        Message sentPhoto;
        switch (userData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇬🇧 English", "https://t.me/CF_Support_EN"),
                            InlineKeyboardButton.WithUrl("🇫🇷 Français", "https://t.me/CF_Support_FR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇷🇺 Русский", "https://t.me/CF_Support_RU"),
                            InlineKeyboardButton.WithUrl("🇩🇪 Deutsch", "https://t.me/CF_Support_DE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuRU
                        }
                    });
                caption = $"<b>📲 Техническая поддержка</b>" +
                          $"\n\nВыберите язык тех. поддержки и нажмите, чтобы открыть чат:";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇬🇧 English", "https://t.me/CF_Support_EN"),
                            InlineKeyboardButton.WithUrl("🇫🇷 Français", "https://t.me/CF_Support_FR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇷🇺 Русский", "https://t.me/CF_Support_RU"),
                            InlineKeyboardButton.WithUrl("🇩🇪 Deutsch", "https://t.me/CF_Support_DE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });
                caption = $"<b>📲 Tech support</b>" +
                          $"\n\nSelect the language of those. support and click to open chat:";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇬🇧 English", "https://t.me/CF_Support_EN"),
                            InlineKeyboardButton.WithUrl("🇫🇷 Français", "https://t.me/CF_Support_FR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇷🇺 Русский", "https://t.me/CF_Support_RU"),
                            InlineKeyboardButton.WithUrl("🇩🇪 Deutsch", "https://t.me/CF_Support_DE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuFR
                        }
                    });
                caption = $"<b>📲 Support technique</b>" +
                          $"\n\nSélectionnez la langue de ceux-ci. support et cliquez pour ouvrir le chat:";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("English 🇬🇧", "https://t.me/CF_Support_EN"),
                            InlineKeyboardButton.WithUrl("Français 🇫🇷", "https://t.me/CF_Support_FR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Русский 🇷🇺", "https://t.me/CF_Support_RU"),
                            InlineKeyboardButton.WithUrl("Deutsch 🇩🇪", "https://t.me/CF_Support_DE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuDE
                        }
                    });
                caption = $"<b>📲 Technischer Support</b>" +
                          $"\n\nWählen Sie die Sprache dieser aus. Support und zum Öffnen des Chats klicken:";
                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇬🇧 English", "https://t.me/CF_Support_EN"),
                            InlineKeyboardButton.WithUrl("🇫🇷 Français", "https://t.me/CF_Support_FR")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🇷🇺 Русский", "https://t.me/CF_Support_RU"),
                            InlineKeyboardButton.WithUrl("🇩🇪 Deutsch", "https://t.me/CF_Support_DE")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });
                caption = $"<b>📲 Tech support</b>" +
                          $"\n\nSelect the language of those. support and click to open chat:";
                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }


    //BOT INFO// needed to be updated with couple of staff
    public static async void Info(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/info.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\info.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string? caption;
        Message sentPhoto;
        switch (userData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌐 Идеология",
                                "https://telegra.ph/IDEOLOGIYA-08-09")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 Пользовательское соглашение",
                                "https://telegra.ph/Opta-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 Правила CASH FLOW",
                                "https://telegra.ph/PRAVILA-CASH-FLOW-08-07")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Столы и условия",
                                "https://telegra.ph/USLOVIYA-STOLOV-08-07")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Роли", "https://telegra.ph/Roli-v-CASH-FLOW-08-07")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuRU
                        }
                    });
                caption = $"<b>📄 Инфо</b>" +
                          $"\n\nДанный раздел содержит необходимую информацию об игре CASH FLOW:";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌐 Ideology", "https://telegra.ph/Ideology-08-27")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 User Agreement", "https://telegra.ph/User-Agreement-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 CASH FLOW Rules",
                                "https://telegra.ph/RULES-09-01-9")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Tables and conditions",
                                "https://telegra.ph/TABLES-AND-CONDITIONS-08-27")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Roles", "https://telegra.ph/ROLES-09-01")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });
                caption = $"<b>📄 Info</b>" +
                          $"\nThis section contains the necessary information about the CASH FLOW game:";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌐 Idéologie", "https://telegra.ph/Idéologie-08-27")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 Conditions d'utilisation",
                                "https://telegra.ph/Conditions-dutilisation-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 Règles CASH FLOW",
                                "https://telegra.ph/RÈGLES-09-01")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Tableaux et conditions",
                                "https://telegra.ph/TABLEAUX-ET-CONDITIONS-08-27")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 RÔLE ", "https://telegra.ph/ROLES-09-01-2")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuFR
                        }
                    });
                caption = $"<b>📄 Info</b>" +
                          $"\nCette section contient les informations nécessaires sur le jeu CASH FLOW :";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌐 Ideologie", "https://telegra.ph/Ideologie-08-27")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 Benutzervereinbarung",
                                "https://telegra.ph/Benutzervereinbarung-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 CASH FLOW Regeln",
                                "https://telegra.ph/REGELN-09-01")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Tabellen und Bedingungen",
                                "https://telegra.ph/TABELLEN-UND-KONDITIONEN-09-01")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Rollen", "https://telegra.ph/ROLLEN-09-01")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuDE
                        }
                    });
                caption = $"<b>📄 Info</b>" +
                          $"\nDieser Abschnitt enthält die notwendigen Informationen zum Spiel CASH FLOW:";
                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌐 Ideology", "https://telegra.ph/Ideology-08-27")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 User Agreement", "https://telegra.ph/User-Agreement-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 CASH FLOW Rules",
                                "https://telegra.ph/Cash-Flow-Rules-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Tables and conditions",
                                "https://telegra.ph/TABLES-AND-CONDITIONS-08-27")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Roles", "https://telegra.ph/Roles-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });
                caption = $"<b>📄 Info</b>" +
                          $"\nThis section contains the necessary information about the CASH FLOW game:";
                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    //TABLE MENU//
    public static async void TableMenu(ITelegramBotClient botClient, long chatId,
        CallbackQuery callbackData, UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/tableSelection.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\tableSelection.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        string? caption;
        Message sentPhoto;
        switch (userData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎗 Медный стол", "CopperTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥉 Бронзовый стол", "BronzeTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥈 Серебряный стол", "SilverTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥇 Золотой стол", "GoldTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎖 Платиновый стол", "PlatinumTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("💎 Алмазный стол", "DiamondTable")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuRU
                        }
                    });
                caption = $"<b>⚜ Список столов</b>" +
                          $"\n\nВыберите стол, на который хотите зайти:";
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎗 Copper table", "CopperTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥉 Bronze table", "BronzeTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥈 Silver table", "SilverTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥇 Gold table", "GoldTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎖 Platinum table", "PlatinumTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("💎 Diamond table", "DiamondTable")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });
                caption = $"<b>⚜ List of tables</b>" +
                          $"\n\nSelect the table you want to join:";
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎗 Table en cuivre", "CopperTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥉 Table en bronze", "BronzeTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥈 Table d'argent", "SilverTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥇 Tableau doré", "GoldTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎖 Table de platine", "PlatinumTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("💎 Tableau de diamant", "DiamondTable")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuFR
                        }
                    });
                caption = $"<b>⚜ Liste des tableaux</b>" +
                          $"\n\nSélectionnez la table que vous souhaitez rejoindre :";
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎗 kupfer Tisch", "CopperTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥉 bronze Tisch", "BronzeTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥈 silberner Tisch", "SilverTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥇 goldener Tisch", "GoldTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎖 platin Tisch", "PlatinumTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("💎 diamant Tisch", "DiamondTable")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuDE
                        }
                    });
                caption = $"<b>⚜ Tabellenverzeichnis</b>" +
                          $"\n\nWählen Sie den Tisch aus, dem Sie beitreten möchten:";
                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎗 Copper table", "CopperTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥉 Bronze table", "BronzeTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥈 Silver table", "SilverTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🥇 Gold table", "GoldTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎖 Platinum table", "PlatinumTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("💎 Diamond table", "DiamondTable")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });
                caption = $"<b>⚜ List of tables</b>" +
                          $"\n\nSelect the table you want to join:";
                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserData userData, Table.TableType tableType)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        InlineKeyboardMarkup inlineKeyboard = null;
        string? caption = null;
        switch (userData.playerData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ Да", "Open" + callbackData.Data),
                            InlineKeyboardButton.WithCallbackData("❌ Нет", "ChooseTable")
                        }
                    });
                switch (tableType)
                {
                    case Table.TableType.copper:
                    {
                        caption = "*Вы действительно хотите войти*" +
                                  "\n*на 🎗 Медный стол?*";
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        caption = "*Вы действительно хотите войти*" +
                                  "\n*на 🥉 Бронзовый стол?*";
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        caption = "*Вы действительно хотите войти*" +
                                  "\n*на 🥈 Серебрянный стол?*";
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        caption = "*Вы действительно хотите войти*" +
                                  "\n*на 🥇 Золотой стол?*";
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        caption = "*Вы действительно хотите войти*" +
                                  "\n*на 🎖 Платиновый стол?*";
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        caption = "*Вы действительно хотите войти*" +
                                  "\n*на 💎 Алмазный стол?*";
                        break;
                    }
                }

                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ Yes", "Open" + callbackData.Data),
                            InlineKeyboardButton.WithCallbackData("❌ No", "ChooseTable"),
                        }
                    });

                switch (tableType)
                {
                    case Table.TableType.copper:
                    {
                        caption = "*Are you sure you want to join* " +
                                  "\n*🎗 Copper Table?*";
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🥉 Bronze Table?*";
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🥈 Silver Table?*";
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🥇 Gold Table?*";
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🎖 Platinum Table?*";
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 💎 Diamond Table?*";
                        break;
                    }
                }

                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ Oui", "Open" + callbackData.Data),
                            InlineKeyboardButton.WithCallbackData("❌ no", "ChooseTable"),
                        }
                    });

                switch (tableType)
                {
                    case Table.TableType.copper:
                    {
                        caption = "*Êtes-vous sûr de vouloir rejoindre*" +
                                  "\n* 🎗 Copper Table?*";
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        caption = "*Êtes-vous sûr de vouloir rejoindre*" +
                                  "\n* 🥉 Table Bronze?*";
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        caption = "*Êtes-vous sûr de vouloir rejoindre*" +
                                  "\n* 🥈 Silver Table?*";
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        caption = "*Êtes-vous sûr de vouloir rejoindre*" +
                                  "\n* 🥇 Gold Table?*";
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        caption = "*Êtes-vous sûr de vouloir rejoindre*" +
                                  "\n* 🎖 Platinum Table?*";
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        caption = "*Êtes-vous sûr de vouloir rejoindre*" +
                                  "\n* 💎 Diamond Table?*";
                        break;
                    }
                }

                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ JA", "Open" + callbackData.Data),
                            InlineKeyboardButton.WithCallbackData("❌ NEIN", "ChooseTable"),
                        }
                    });
                switch (tableType)
                {
                    case Table.TableType.copper:
                    {
                        caption = "*Bist du sicher, dass du*" +
                                  "\n* 🎗 Copper Table beitreten möchtest?*";
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        caption = "*Bist du sicher, dass du am*" +
                                  "\n* 🥉 Bronze Table teilnehmen möchtest?*";
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        caption = "*Bist du sicher, dass du am*" +
                                  "\n* 🥈 Silver Table teilnehmen möchtest?*";
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        caption = "*Bist du sicher, dass du am*" +
                                  "\n* 🥇 Gold Table teilnehmen möchtest?*";
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        caption = "*Bist du sicher, dass du am*" +
                                  "\n* 🎖 Platinum Table teilnehmen möchtest?*";
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        caption = "*Bist du sicher, dass du am*" +
                                  "\n* 💎 diamond Table teilnehmen möchtest?*";
                        break;
                    }
                }

                break;
            default:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("✅ Yes", "Open" + callbackData.Data),
                            InlineKeyboardButton.WithCallbackData("❌ No", "ChooseTable"),
                        }
                    });

                switch (tableType)
                {
                    case Table.TableType.copper:
                    {
                        caption = "*Are you sure you want to join* " +
                                  "\n*🎗 Copper Table?*";
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🥉 Bronze Table?*";
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🥈 Silver Table?*";
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🥇 Gold Table?*";
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 🎖 Platinum Table?*";
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        caption = "*Are you sure you want to join*" +
                                  "\n* 💎 Diamond Table?*";
                        break;
                    }
                }

                break;
        }

        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.MarkdownV2,
                null,
                inlineKeyboard
            ).WaitAsync(TimeSpan.FromSeconds(10));
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }

    public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData,
        Table.TableType tableType,
        WebManager.RequestType requestType)
    {
        switch (requestType)
        {
            case WebManager.RequestType.LeaveTable:
            {
                string path = null;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/mainMenu.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\mainMenu.png");
                InlineKeyboardMarkup? inlineKeyboard = null;
                Message sentMessage;
                string? caption;
                switch (userData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Да",
                                        "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌Нет", "ChooseTable"),
                                }
                            });
                        caption = $"<b>🏃‍♂️ Выход со стола</b>" +
                                  $"\n\nПосле выхода со стола, дальнейший вход на данный стол будет заблокирован на 24 часа.";
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Yes",
                                        "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>🏃‍♂️ Exit the table</b>" +
                                  $"\n\nAfter leaving the table, further entry to this table will be blocked for 24 hours.";
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Yes",
                                        "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>🏃‍♂️ Quitter la table</b>" +
                                  $"\n\nAprès avoir quitté la table, toute autre entrée à cette table sera bloquée pendant 24 heures.";
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅JA",
                                        "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌NEIN", "ChooseTable"),
                                }
                            });
                        caption = $"<b>🏃‍♂️ Tisch verlassen</b>" +
                                  $"\n\nNach dem Verlassen des Tisches wird der weitere Zutritt zu diesem Tisch für 24 Stunden gesperrt.";
                        break;
                    default:
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Yes",
                                        "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>🏃‍♂️ Exit the table</b>" +
                                  $"\n\nAfter leaving the table, further entry to this table will be blocked for 24 hours.";
                        break;
                }

                using (Stream
                       stream = System.IO.File.OpenRead(path))
                    try
                    {
                        await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId,
                            media: new InputMediaPhoto(new InputMedia(stream, "media"))
                        ).WaitAsync(TimeSpan.FromSeconds(10));
                    }
                    catch
                    {
                        Trace.Write("Handle Remaining Exceptions");
                        try
                        {
                            await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                                callbackData.Message.MessageId);
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                    }
                                });
                            caption = "<b>An error occurred</b>\n\n" +
                                      "Please contact technical support and describe what caused this error";
                            try
                            {
                                await botClient.SendPhotoAsync(
                                    chatId,
                                    File.OpenRead(path),
                                    caption,
                                    ParseMode.Html,
                                    replyMarkup: inlineKeyboard
                                );
                            }
                            catch
                            {
                            }
                        }
                        catch
                        {
                            // ignored
                        }
                    }

                try
                {
                    await botClient.EditMessageCaptionAsync(
                        callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        caption,
                        ParseMode.Html,
                        null,
                        inlineKeyboard
                    );
                }
                catch
                {
                    Trace.Write("Handle Remaining Exceptions");
                    try
                    {
                        await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId);
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                }
                            });
                        caption = "<b>An error occurred</b>\n\n" +
                                  "Please contact technical support and describe what caused this error";
                        try
                        {
                            await botClient.SendPhotoAsync(
                                chatId,
                                File.OpenRead(path),
                                caption,
                                ParseMode.Html,
                                replyMarkup: inlineKeyboard
                            );
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                break;
            }
            case WebManager.RequestType.RemoveFromTable:
            {
                string path = null;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/mainMenu.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\mainMenu.png");
                InlineKeyboardMarkup inlineKeyboard = null;
                Message sentMessage;
                string? caption;
                switch (userData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Да", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌Нет", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Удалить игрока cо стола?</b>";
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Yes", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Remove a player from the table?</b>";
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Oui", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌no", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Supprimer un joueur de la table?</b>";
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅JA", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌NEIN", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Einen Spieler vom Tisch entfernen?</b>";
                        break;
                    default:
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Yes", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Remove a player from the table?</b>";
                        break;
                }

                try
                {
                    using (Stream
                           stream = System.IO.File.OpenRead(path))
                        try
                        {
                            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                                callbackData.Message.MessageId,
                                media: new InputMediaPhoto(new InputMedia(stream, "media"))
                            ).WaitAsync(TimeSpan.FromSeconds(10));
                        }
                        catch
                        {
                        }
                }
                catch
                {
                    Trace.Write("Handle Remaining Exceptions");
                    try
                    {
                        await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId);
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                }
                            });
                        caption = "<b>An error occurred</b>\n\n" +
                                  "Please contact technical support and describe what caused this error";
                        try
                        {
                            await botClient.SendPhotoAsync(
                                chatId,
                                File.OpenRead(path),
                                caption,
                                ParseMode.Html,
                                replyMarkup: inlineKeyboard
                            );
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                try
                {
                    await botClient.EditMessageCaptionAsync(
                        callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        caption,
                        ParseMode.Html,
                        null,
                        inlineKeyboard
                    );
                }
                catch
                {
                    Trace.Write("Handle Remaining Exceptions");
                    try
                    {
                        await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId);
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                }
                            });
                        caption = "<b>An error occurred</b>\n\n" +
                                  "Please contact technical support and describe what caused this error";
                        try
                        {
                            await botClient.SendPhotoAsync(
                                chatId,
                                File.OpenRead(path),
                                caption,
                                ParseMode.Html,
                                replyMarkup: inlineKeyboard
                            );
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                break;
            }
            case WebManager.RequestType.Confirm:
            {
                string path = null;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images/MainMenu/mainMenu.png");

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @"Images\MainMenu\mainMenu.png");
                InlineKeyboardMarkup inlineKeyboard = null;
                Message? sentMessage;
                string? caption;
                switch (userData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Да", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌Нет", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Активировать игрока?</b>";
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Yes", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Activate player?</b>";
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Oui", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌no", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Activer le joueur?</b>";
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅JA", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌NEIN", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Spieler aktivieren?</b>";
                        break;
                    default:
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("✅Yes", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>Activate player?</b>";
                        break;
                }

                try
                {
                    using (Stream
                           stream = System.IO.File.OpenRead(path))
                        try
                        {
                            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                                callbackData.Message.MessageId,
                                media: new InputMediaPhoto(new InputMedia(stream, "media"))
                            ).WaitAsync(TimeSpan.FromSeconds(10));
                        }
                        catch
                        {
                        }
                }
                catch
                {
                    Trace.Write("Handle Remaining Exceptions");
                    try
                    {
                        await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId);
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                }
                            });
                        caption = "<b>An error occurred</b>\n\n" +
                                  "Please contact technical support and describe what caused this error";
                        try
                        {
                            await botClient.SendPhotoAsync(
                                chatId,
                                File.OpenRead(path),
                                caption,
                                ParseMode.Html,
                                replyMarkup: inlineKeyboard
                            );
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                try
                {
                    await botClient.EditMessageCaptionAsync(
                        callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        caption,
                        ParseMode.Html,
                        null,
                        inlineKeyboard
                    );
                }
                catch
                {
                    Trace.Write("Handle Remaining Exceptions");
                    try
                    {
                        await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id,
                            callbackData.Message.MessageId);
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                                }
                            });
                        caption = "<b>An error occurred</b>\n\n" +
                                  "Please contact technical support and describe what caused this error";
                        try
                        {
                            await botClient.SendPhotoAsync(
                                chatId,
                                File.OpenRead(path),
                                caption,
                                ParseMode.Html,
                                replyMarkup: inlineKeyboard
                            );
                        }
                        catch
                        {
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                break;
            }
        }
    }

    public static async Task Captcha(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/mainMenu.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\mainMenu.png");
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        Message sentPhoto;
        var num1 = Random.Shared.NextInt64(1, 50);
        var num2 = Random.Shared.NextInt64(1, 50);
        long wrongAnswer0 = 0;
        long wrongAnswer1 = 0;
        long wrongAnswer2 = 0;
        if (num1 < num2)
        {
            wrongAnswer0 = Random.Shared.NextInt64(num1, num2);
            wrongAnswer1 = Random.Shared.NextInt64(num1, num2);
            wrongAnswer2 = Random.Shared.NextInt64(num1, num2);
        }
        else
        {
            wrongAnswer0 = Random.Shared.NextInt64(num2, num1);
            wrongAnswer1 = Random.Shared.NextInt64(num2, num1);
            wrongAnswer2 = Random.Shared.NextInt64(num2, num1);
        }

        var answer = num1 + num2;
        switch (Random.Shared.NextInt64(0, 4))
        {
            case 0:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(answer.ToString(),
                                callbackData.Data! + "|CaptchaTrue"),
                            InlineKeyboardButton.WithCallbackData(wrongAnswer0.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(wrongAnswer1.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                            InlineKeyboardButton.WithCallbackData(wrongAnswer2.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                        }
                    });
                break;
            case 1:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(wrongAnswer0.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                            InlineKeyboardButton.WithCallbackData(answer.ToString(),
                                callbackData.Data! + "|CaptchaTrue"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(wrongAnswer1.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                            InlineKeyboardButton.WithCallbackData(wrongAnswer2.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                        }
                    });
                break;
            case 2:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(wrongAnswer0.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                            InlineKeyboardButton.WithCallbackData(wrongAnswer1.ToString(), "CaptchaFalse"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(answer.ToString(),
                                callbackData.Data! + "|CaptchaTrue"),
                            InlineKeyboardButton.WithCallbackData(wrongAnswer2.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                        }
                    });
                break;
            case 3:
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(wrongAnswer0.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                            InlineKeyboardButton.WithCallbackData(wrongAnswer1.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData(wrongAnswer2.ToString(),
                                callbackData.Data! + "|CaptchaFalse"),
                            InlineKeyboardButton.WithCallbackData(answer.ToString(),
                                callbackData.Data! + "|CaptchaTrue"),
                        }
                    });
                break;
        }

        var caption = $"<b>Captcha</b>" +
                      $"\n\n{num1} + {num2} = ?:";
        try
        {
            using (Stream
                   stream = System.IO.File.OpenRead(path))
                try
                {
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id,
                        callbackData.Message.MessageId,
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    ).WaitAsync(TimeSpan.FromSeconds(10));
                }
                catch
                {
                }
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }

        try
        {
            await botClient.EditMessageCaptionAsync(
                callbackData.Message.Chat.Id,
                callbackData.Message.MessageId,
                caption,
                ParseMode.Html,
                null,
                inlineKeyboard
            );
        }
        catch
        {
            Trace.Write("Handle Remaining Exceptions");
            try
            {
                await botClient.DeleteMessageAsync(callbackData.Message.Chat.Id, callbackData.Message.MessageId);
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("📲 Tech Support", "TechSupport"),
                        }
                    });
                caption = "<b>An error occurred</b>\n\n" +
                          "Please contact technical support and describe what caused this error";
                try
                {
                    await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                catch
                {
                }
            }
            catch (ApiRequestException apiRequestException)
            {
                //
            }
        }
    }
}