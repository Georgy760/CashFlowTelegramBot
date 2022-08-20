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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
            );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
        );
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

        var sentPhoto = await botClient.SendPhotoAsync(
            chatId,
            File.OpenRead(path)!,
            "<b>🌐 Language menu:</b>" +
            "\n\nClick the corresponding button to change the language:",
            ParseMode.Html,
            replyMarkup: inlineKeyboard);
    }
    public static async void GetUserData(ITelegramBotClient botClient, long chatId, string lang,
        UserProfile SearchedUser,
        Table.TableRole tableRole)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/status.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\status.png");
        UserData invitedBy = null;
        if (SearchedUser.refId != null)
            invitedBy = await WebManager.SendData(new UserProfile((int) SearchedUser.refId),
                WebManager.RequestType.GetUserData);

        var tableToBack = await WebManager.SendData(SearchedUser, WebManager.RequestType.GetTableData);
        if (tableToBack.tableData.tableID != 0)
        {
            var callbackAddress = GetCallbackAddress(tableToBack.tableData.tableType);

            InlineKeyboardMarkup? inlineKeyboard = null;
            var flag = false;
            var searchedUserRole = SearchedUser.GetTableRole(lang);
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
                    searchedUserRole += " ✅ ";
                else searchedUserRole += " ❌ ";
            }

            switch (lang)
            {
                case "ru":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverARU,
                                            Tables.InlineKeyboardButtonVerfGiverARU
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBRU,
                                            Tables.InlineKeyboardButtonVerfGiverBRU
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCRU,
                                            Tables.InlineKeyboardButtonVerfGiverCRU
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDRU,
                                            Tables.InlineKeyboardButtonVerfGiverDRU
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
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
                                    InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress)
                                }
                            });

                    if (invitedBy != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "📋 Информация о пользователе: \n\n" +
                            $"Роль: {searchedUserRole}\n" +
                            $"Ник: @{SearchedUser.username}\n" +
                            $"Пригласил: @{invitedBy.playerData.username}\n" +
                            $"Лично приглашенных: {SearchedUser.invited}" +
                            $"\n\nСвязаться c @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "📋 Информация о пользователе:\n\n" +
                            $"Роль: {searchedUserRole}\n" +
                            $"Ник: @{SearchedUser.username}\n" +
                            $"Лично приглашенных: {SearchedUser.invited}" +
                            $"\n\nСвязаться c @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }

                    break;
                }
                case "eng":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverAENG,
                                            Tables.InlineKeyboardButtonVerfGiverAENG
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙  Back", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙  Back", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBENG,
                                            Tables.InlineKeyboardButtonVerfGiverBENG
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCENG,
                                            Tables.InlineKeyboardButtonVerfGiverCENG
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDENG,
                                            Tables.InlineKeyboardButtonVerfGiverDENG
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("🔙 Back", callbackAddress)
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
                                    InlineKeyboardButton.WithCallbackData("🔙  Back", callbackAddress)
                                }
                            });

                    if (invitedBy != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "📋 User info:\n\n" +
                            $"Role: {searchedUserRole}\n" +
                            $"Nickname: @{SearchedUser.username}\n" +
                            $"Invited by: @{invitedBy.playerData.username}\n" +
                            $"Personally invited: {SearchedUser.invited}" +
                            $"\n\nContact with @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "📋 User info:\n\n" +
                            $"Role: {searchedUserRole}\n" +
                            $"Nickname: @{SearchedUser.username}\n" +
                            $"Personally invited: {SearchedUser.invited}" +
                            $"\n\nContact with @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }

                    break;
                }
                case "fr":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverAFR,
                                            Tables.InlineKeyboardButtonVerfGiverAFR
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBFR,
                                            Tables.InlineKeyboardButtonVerfGiverBFR
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCFR,
                                            Tables.InlineKeyboardButtonVerfGiverCFR
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDFR,
                                            Tables.InlineKeyboardButtonVerfGiverDFR
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                    InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
                                }
                            });

                    if (invitedBy != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "📋 Informations de l'utilisateur:\n\n" +
                            $"Rôle: {searchedUserRole}\n" +
                            $"Surnom: @{SearchedUser.username}\n" +
                            $"inviter par: @{invitedBy.playerData.username}\n" +
                            $"invité personnellement: {SearchedUser.invited}" +
                            $"\n\nContacter avec @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "📋 Informations de l'utilisateur:\n\n" +
                            $"Rôle: {searchedUserRole}\n" +
                            $"Surnom: @{SearchedUser.username}\n" +
                            $"invité personnellement: {SearchedUser.invited}" +
                            $"\n\nContacter avec @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }

                    break;
                }
                case "de":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverADE,
                                            Tables.InlineKeyboardButtonVerfGiverADE
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBDE,
                                            Tables.InlineKeyboardButtonVerfGiverBDE
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCDE,
                                            Tables.InlineKeyboardButtonVerfGiverCDE
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDDE,
                                            Tables.InlineKeyboardButtonVerfGiverDDE
                                        },
                                        new[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                            InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
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
                                    InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
                                }
                            });

                    if (invitedBy != null)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "📋 Benutzerinformation: \n\n" +
                            $"Rolle: {searchedUserRole}\n" +
                            $"Spitzname: @{SearchedUser.username}\n" +
                            $"ingeladen von: @{invitedBy.playerData.username}\n" +
                            $"Persönlich eingeladen: {SearchedUser.invited}" +
                            $"\n\nKontakt mit @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Benutzerinformation\n\n" +
                            $"Rolle: {searchedUserRole}\n" +
                            $"Spitzname: @{SearchedUser.username}\n" +
                            $"Persönlich eingeladen: {SearchedUser.invited}" +
                            $"\n\nKontakt mit @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }

                    break;
                }
            }
        }
        else
        {
            InlineKeyboardMarkup? inlineKeyboard;
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Вы больше не участник стола",
                        replyMarkup: inlineKeyboard);
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "You are no longer a member of the table",
                        replyMarkup: inlineKeyboard);
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Vous n'êtes plus membre de la table",
                        replyMarkup: inlineKeyboard);
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Sie sind kein Mitglied des Tisches mehr",
                        replyMarkup: inlineKeyboard);
                    break;
                }
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
    public static async void ShowListTeam(ITelegramBotClient botClient, long chatId, string lang, UserProfile user)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/status.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\status.png");
        var table = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
        if (table.tableData.tableID != 0)
        {
            var callbackAddress = GetCallbackAddress(table.tableData.tableType);


            var tableInfo = "";
            InlineKeyboardMarkup? inlineKeyboard = null;
            if (table.tableData.bankerID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.bankerID),
                    WebManager.RequestType.GetUserData);
                tableInfo += userData.playerData.UserInfo(user.lang);
                //tableInfo += $"🏦Банкир: @{userData.playerData.username}\n";
            }

            if (table.tableData.managerA_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerA_ID),
                    WebManager.RequestType.GetUserData);
                tableInfo += userData.playerData.UserInfo(user.lang);
                //tableInfo += $"👤Менеджер-1: @{userData.playerData.username}\n";
            }

            if (table.tableData.managerB_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerB_ID),
                    WebManager.RequestType.GetUserData);
                tableInfo += userData.playerData.UserInfo(user.lang);
                //tableInfo += $"👤Менеджер-2: @{userData.playerData.username}\n";
            }

            if (table.tableData.giverA_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverA_ID),
                    WebManager.RequestType.GetUserData);
                tableInfo += userData.playerData.UserInfo(user.lang);
                //tableInfo += $"🎁Даритель-1: @{userData.playerData.username}\n";
            }

            if (table.tableData.giverB_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverB_ID),
                    WebManager.RequestType.GetUserData);
                tableInfo += userData.playerData.UserInfo(user.lang);
                //tableInfo += $"🎁Даритель-2: @{userData.playerData.username}\n";
            }

            if (table.tableData.giverC_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverC_ID),
                    WebManager.RequestType.GetUserData);
                tableInfo += userData.playerData.UserInfo(user.lang);
                //tableInfo += $"🎁Даритель-3: @{userData.playerData.username}\n";
            }

            if (table.tableData.giverD_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverD_ID),
                    WebManager.RequestType.GetUserData);
                tableInfo += userData.playerData.UserInfo(user.lang);
                //tableInfo += $"🎁Даритель-4: @{userData.playerData.username}\n";
            }

//TODO
            switch (lang)
            {
                case "ru":
                {
                    inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Назад", callbackAddress)
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
                                InlineKeyboardButton.WithCallbackData("Back", callbackAddress)
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
                                InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
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
                                InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
                            }
                        });
                    break;
                }
            }

            var sentPhoto = await botClient.SendPhotoAsync(
                chatId,
                File.OpenRead(path)!,
                tableInfo,
                replyMarkup: inlineKeyboard);
        }
        else
        {
            InlineKeyboardMarkup? inlineKeyboard;
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Вы больше не участник стола",
                        replyMarkup: inlineKeyboard);
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "You are no longer a member of the table",
                        replyMarkup: inlineKeyboard);
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Vous n'êtes plus membre de la table",
                        replyMarkup: inlineKeyboard);
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
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Sie sind kein Mitglied des Tisches mehr",
                        replyMarkup: inlineKeyboard);
                    break;
                }
            }
        }
    }
    public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile user, Error error)
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
                var tableToBack = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
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
                                    InlineKeyboardButton.WithCallbackData("Назад", callbackAddress)
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷‍♂️ Этот Даритель ещё не присоединился к игре.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "eng":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Back", callbackAddress)
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷‍♂️ This Giver has not joined the game yet.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "fr":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Retour", callbackAddress)
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷‍♂️ Ce Donateur n'a pas encore rejoint le jeu.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "de":
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress)
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷‍♂️ Dieser Geber ist dem Spiel noch nicht beigetreten.",
                            replyMarkup: inlineKeyboard);
                        break;
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
                                    InlineKeyboardButton.WithUrl("📣 General chat", "https://t.me/cashflow_official_chat"),
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
                                    InlineKeyboardButton.WithUrl("📣 Chat général", "https://t.me/cashflow_official_chat"),
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
                                    InlineKeyboardButton.WithUrl("📣 Allgemeiner Chat", "https://t.me/cashflow_official_chat"),
                                }
                            });
                        caption = $"<b>👋 Willkommen zum Spiel" +
                                  $"\n\"CASH FLOW\"!</b>" +
                                  $"\n\nUm den Bot weiter zu verwenden, gehen Sie über den Empfehlungslink!";
                        break;
                }
                sentMessage = await botClient.SendPhotoAsync(
                        chatId, 
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
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
                    sentMessage = await botClient.SendPhotoAsync(
                        chatId, 
                        File.OpenRead(path),
                        caption,
                        ParseMode.Html,
                        replyMarkup: inlineKeyboard
                    );
                }
                else
                {
                    using (Stream
                           stream = System.IO.File.OpenRead(path)) 
                        await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                            callbackData.Message.MessageId, 
                            media: new InputMediaPhoto(new InputMedia(stream, "media"))
                        );
                    await botClient.EditMessageCaptionAsync(
                        callbackData.Message.Chat.Id, 
                        callbackData.Message.MessageId, 
                        caption, 
                        ParseMode.Html, 
                        null, 
                        inlineKeyboard
                    );
                }

                break;
        }
    }
    public static async void ConnectingError(ITelegramBotClient botClient, long chatId, UserProfile user, Error error)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/tableSelection.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\tableSelection.png");
        switch (error)
        {
            case Error.UserAlreadyAtAnotherTable:

                var tableData = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
                Console.WriteLine(tableData.tableData.tableType);
                switch (user.lang)
                {
                    case "ru":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Назад", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"🤷 Вы уже находитесь на столе: {tableData.tableData.GetTableType(user)}.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "eng":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Back", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"🤷 You are already on the table: {tableData.tableData.GetTableType(user)}.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "fr":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Retour", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"🤷 Vous êtes déjà sur la table: {tableData.tableData.GetTableType(user)}.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "de":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Zurück", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"🤷 Sie sind bereits auf dem Tisch: {tableData.tableData.GetTableType(user)}.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                }

                break;
            case Error.UserDontMeetConnetionRequriments:
                switch (user.lang)
                {
                    case "ru":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Назад", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷 Для открытия данного стола Вам необходимо выполнить условия по приглашённым игрокам ИЛИ пройти стол уровнем ниже.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "eng":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Back", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷 To open this table, you need to fulfill the conditions for invited players OR go through the table below.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "fr":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Retour", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷 Pour ouvrir ce tableau, vous devez remplir les conditions des joueurs invités OU passer par le tableau ci-dessous.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }

                    case "de":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Zurück", "ChooseTable")
                                }
                            });

                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            "🤷 Um diese Tabelle zu öffnen, müssen Sie die Bedingungen für eingeladene Spieler erfüllen ODER die Tabelle unten durchgehen.",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                }

                break;
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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
            );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
        );
    }
    public static async void Status(ITelegramBotClient botClient, long chatId, UserProfile userData)
    {
        string path = null;
        var tableData = await WebManager.SendData(userData, WebManager.RequestType.GetTableData);
        string tableType;
        string tableRole;
        var giverCount = 0;
        if (tableData.tableData.giverA_ID != null) giverCount++;
        if (tableData.tableData.giverB_ID != null) giverCount++;
        if (tableData.tableData.giverC_ID != null) giverCount++;
        if (tableData.tableData.giverD_ID != null) giverCount++;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/status.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\status.png");
        InlineKeyboardMarkup? inlineKeyboard;
        Message? sentMessage;
        tableRole = userData.GetTableRole(userData.lang);
        userData.level_tableType = tableData.tableData.tableType.ToString();
        tableType = userData.GetTableType();
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
                if (userData.table_id != null)
                {
                    if (userData.refId != null)
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"\nВаш ID: {userData.id}\nВаш ник: @{userData.username}\nФиксированный курс:\n📈 1$ = 0.98€ = 62₽" +
                            $"\n\nВас пригласил: @{userData.invitedBy}\nЛично приглашенных: {userData.invited}" +
                            $"\nВаши столы:\n\n{tableType} стол " +
                            $"\nВаша роль: ({tableRole})\nВсего дарителей на столе: {giverCount} из 4",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentPhoto = await botClient.SendPhotoAsync(
                            chatId,
                            File.OpenRead(path)!,
                            $"\nВаш ID: {userData.id}\nВаш ник: @{userData.username}\nФиксированный курс:\n📈 1$ = 0.98€ = 62₽" +
                            $"\n\nВас пригласил: @{userData.invitedBy}\nЛично приглашенных: {userData.invited}" +
                            $"\nВаши столы:\n\n{tableType} стол " +
                            $"\n\nВаша роль: ({tableRole})\nВсего дарителей на столе: {giverCount} из 4",
                            replyMarkup: inlineKeyboard);
                    }
                }
                else
                {
                    var sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"\nВаш ID: {userData.id}\nВаш ник: @{userData.username}\nФиксированный курс:\n📈 1$ = 0.98€ = 62₽" +
                        $"\n\nВас пригласил: @{userData.invitedBy}\nЛично приглашенных: {userData.invited}",
                        replyMarkup: inlineKeyboard);
                }

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

                if (userData.refId != null)
                {
                    var sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"\nYour ID: {userData.id}\nYour nickname: @{userData.username}\nFixed rate:\n📈 1$ = 0.98€ = 62₽" +
                        $"\n\nInvited by: @{userData.invitedBy}\nYour Invites: {userData.invited}" +
                        $"\nYour tables:\n\n{tableType} table: ({tableRole})\nGivers on table: {giverCount} of 4",
                        replyMarkup: inlineKeyboard);
                }
                else
                {
                    var sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"\nYour ID: {userData.id}\nYour nickname: @{userData.username}\nFixed rate:\n📈 1$ = 0.98€ = 62₽" +
                        $"\n\nYour Invites: {userData.invited}" +
                        $"\nYour tables:\n\n{tableType} table: ({tableRole})\nGivers on table: {giverCount} of 4",
                        replyMarkup: inlineKeyboard);
                }

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
                if (userData.refId != null)
                {
                    var sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"\nVotre identifiant: {userData.id}\nTon surnom: @{userData.username}\nTaux fixe :\n📈 1$ = 0.98€ = 62₽" +
                        $"\n\nInviter par: @{userData.invitedBy}\nAuto-invité: {userData.invited}" +
                        $"\nVos tableaux:\n\n{tableType} table: ({tableRole})\nDonneurs sur table: {giverCount} sur 4",
                        replyMarkup: inlineKeyboard);
                }
                else
                {
                    var sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        $"\nVotre identifiant: {userData.id}\nTon surnom: @{userData.username}\nTaux fixe :\n📈 1$ = 0.98€ = 62₽" +
                        $"\n\nAuto-invité: {userData.invited}" +
                        $"\nVos tableaux:\n\n{tableType} table: ({tableRole})\nDonneurs sur table: {giverCount} sur 4",
                        replyMarkup: inlineKeyboard);
                }

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
                if (userData.refId != null)
                {
                    var sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"\nIhre ID: {userData.id}\nDein Spitzname: @{userData.username}\nFester Zinssatz:\n1$ = 62₽ = 0.98€" +
                        $"\n\nDich eingeladen: @{userData.invitedBy}\nPersönlich eingeladen: {userData.invited}" +
                        $"\nIhre Tische:\n\n{tableType} Tisch: ({tableRole})\nGesamtspender auf dem Tisch: {giverCount} von 4",
                        replyMarkup: inlineKeyboard);
                }
                else
                {
                    var sentPhoto = await botClient.SendPhotoAsync(
                        chatId,
                        File.OpenRead(path)!,
                        $"\nIhre ID: {userData.id}\nDein Spitzname: @{userData.username}\nFester Zinssatz:\n1$ = 62₽ = 0.98€" +
                        $"\n\nPersönlich eingeladen: {userData.invited}" +
                        $"\nIhre Tische:\n\n{tableType} Tisch: ({tableRole})\nGesamtspender auf dem Tisch: {giverCount} von 4",
                        replyMarkup: inlineKeyboard);
                }

                break;
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
        Console.WriteLine("https://t.me/originalCashFlowbot?start=R" + userData.id);
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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
            );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
        );
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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
            );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
        );
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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
            );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
        );
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
                                "https://telegra.ph/Opta-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 Пользовательское соглашение",
                                "https://telegra.ph/Opta-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 Правила CASH FLOW",
                                "https://telegra.ph/Pravila-Cash-Flow-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Столы и условия", "https://telegra.ph/Stoly-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Роли", "https://telegra.ph/Roli-07-21")
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
                        new []
                        {
                            InlineKeyboardButton.WithUrl("🌐 Ideology", "https://telegra.ph/User-Agreement-07-21")
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
                                "https://telegra.ph/Cash-Flow-tables-07-21")
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
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithUrl("🌐 Idéologie", "https://telegra.ph/User-Agreement-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 Conditions d'utilisation",
                                "https://telegra.ph/Conditions-dutilisation-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 Règles CASH FLOW",
                                "https://telegra.ph/Cash-Flow-r%C3%A8gles-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Tableaux et conditions",
                                "https://telegra.ph/Cash-Flow-tables-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 RÔLE ", "https://telegra.ph/R%C3%94LE-07-21")
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
                        new []
                        {
                            InlineKeyboardButton.WithUrl("🌐 Ideologie", "https://telegra.ph/User-Agreement-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("🌀 Benutzervereinbarung",
                                "https://telegra.ph/Benutzervereinbarung-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("💠 CASH FLOW Regeln",
                                "https://telegra.ph/Cashflow-Regeln-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📘 Tabellen und Bedingungen",
                                "https://telegra.ph/Cash-Flow-tables-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Rollen", "https://telegra.ph/3-ROLLEN-07-21")
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
                        new []
                        {
                            InlineKeyboardButton.WithUrl("🌐 Ideology", "https://telegra.ph/User-Agreement-07-21")
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
                                "https://telegra.ph/Cash-Flow-tables-07-21")
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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
            );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
        );
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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
            );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
        );
    }

    public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserData userData, Table.TableType tableType)
    {
        InlineKeyboardMarkup inlineKeyboard;
        switch (userData.playerData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Нет", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("Да", "Open" + callbackData.Data)
                        }
                    });
                if (userData.playerData.tableRole == "giver")
                {
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "На этом столе Вам нужно сделать подарок 100$\nВы точно хотите зайти на данный стол?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.bronze:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "На этом столе Вам нужно сделать подарок 400$\nВы точно хотите зайти на данный стол?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.silver:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "На этом столе Вам нужно сделать подарок 1000$\nВы точно хотите зайти на данный стол?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.gold:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "На этом столе Вам нужно сделать подарок 2500$\nВы точно хотите зайти на данный стол?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.platinum:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "На этом столе Вам нужно сделать подарок 5000$\nВы точно хотите зайти на данный стол?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.diamond:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "На этом столе Вам нужно сделать подарок 10000$\nВы точно хотите зайти на данный стол?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                    }
                }
                else
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Вы точно хотите зайти на данный стол?",
                        replyMarkup: inlineKeyboard);
                }

                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("Yes", "Open" + callbackData.Data)
                        }
                    });
                if (userData.playerData.tableRole == "giver")
                {
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "On this table you need to make a gift of $100\nAre you sure you want to go to this table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.bronze:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "On this table you need to make a gift of $400\nAre you sure you want to go to this table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.silver:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "On this table you need to make a gift of $1000\nAre you sure you want to go to this table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.gold:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "On this table you need to make a gift of $2500\nAre you sure you want to go to this table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.platinum:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "On this table you need to make a gift of $5000\nAre you sure you want to go to this table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.diamond:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "On this table you need to make a gift of $10000\nAre you sure you want to go to this table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                    }
                }
                else
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Are you sure you want to join this table?",
                        replyMarkup: inlineKeyboard);
                }

                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("no", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("Oui", "Open" + callbackData.Data)
                        }
                    });

                if (userData.playerData.tableRole == "giver")
                {
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "Sur cette table, vous devez faire un don de 100$\nÊtes-vous sûr de vouloir aller à cette table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.bronze:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "Sur cette table, vous devez faire un don de 400$\nÊtes-vous sûr de vouloir aller à cette table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.silver:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "Sur cette table, vous devez faire un don de 1000$\nÊtes-vous sûr de vouloir aller à cette table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.gold:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "Sur cette table, vous devez faire un don de 2500$\nÊtes-vous sûr de vouloir aller à cette table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.platinum:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "Sur cette table, vous devez faire un don de 5000$\nÊtes-vous sûr de vouloir aller à cette table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.diamond:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "Sur cette table, vous devez faire un don de 10000$\nÊtes-vous sûr de vouloir aller à cette table?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                    }
                }
                else
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Êtes-vous sûr de vouloir rejoindre cette table?",
                        replyMarkup: inlineKeyboard);
                }

                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("NEIN", "ChooseTable"),
                            InlineKeyboardButton.WithCallbackData("JA", "Open" + callbackData.Data)
                        }
                    });
                if (userData.playerData.tableRole == "giver")
                {
                    switch (tableType)
                    {
                        case Table.TableType.copper:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "An diesem Tisch müssen Sie 100$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.bronze:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "An diesem Tisch müssen Sie 400$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.silver:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "An diesem Tisch müssen Sie 1000$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.gold:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "An diesem Tisch müssen Sie 2500$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.platinum:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "An diesem Tisch müssen Sie 5000$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                        case Table.TableType.diamond:
                        {
                            var sentMessage = await botClient.SendTextMessageAsync(
                                chatId,
                                "An diesem Tisch müssen Sie 10000$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                                replyMarkup: inlineKeyboard);
                            break;
                        }
                    }
                }
                else
                {
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        "Sind Sie sicher, dass Sie an diesem Tisch teilnehmen möchten?",
                        replyMarkup: inlineKeyboard);
                }

                break;
        }
    }

    public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData,
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
                                    InlineKeyboardButton.WithCallbackData("✅Да", "Confirm" + callbackData.Data),
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
                                    InlineKeyboardButton.WithCallbackData("✅Yes", "Confirm" + callbackData.Data),
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
                                    InlineKeyboardButton.WithCallbackData("✅Yes", "Confirm" + callbackData.Data),
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
                                    InlineKeyboardButton.WithCallbackData("✅JA", "Confirm" + callbackData.Data),
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
                                    InlineKeyboardButton.WithCallbackData("✅Yes", "Confirm" + callbackData.Data),
                                    InlineKeyboardButton.WithCallbackData("❌No", "ChooseTable"),
                                }
                            });
                        caption = $"<b>🏃‍♂️ Exit the table</b>" +
                                  $"\n\nAfter leaving the table, further entry to this table will be blocked for 24 hours.";
                        break;
                }
                using (Stream
                       stream = System.IO.File.OpenRead(path)) 
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                        callbackData.Message.MessageId, 
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    );
                await botClient.EditMessageCaptionAsync(
                    callbackData.Message.Chat.Id, 
                    callbackData.Message.MessageId, 
                    caption, 
                    ParseMode.Html, 
                    null, 
                    inlineKeyboard
                );
                
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
                using (Stream
                       stream = System.IO.File.OpenRead(path)) 
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                        callbackData.Message.MessageId, 
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    );
                await botClient.EditMessageCaptionAsync(
                    callbackData.Message.Chat.Id, 
                    callbackData.Message.MessageId, 
                    caption, 
                    ParseMode.Html, 
                    null, 
                    inlineKeyboard
                );

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
                using (Stream
                       stream = System.IO.File.OpenRead(path)) 
                    await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                        callbackData.Message.MessageId, 
                        media: new InputMediaPhoto(new InputMedia(stream, "media"))
                    );
                await botClient.EditMessageCaptionAsync(
                    callbackData.Message.Chat.Id, 
                    callbackData.Message.MessageId, 
                    caption, 
                    ParseMode.Html, 
                    null, 
                    inlineKeyboard
                );
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
        if(num1 < num2)
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
        using (Stream
               stream = System.IO.File.OpenRead(path)) 
            await botClient.EditMessageMediaAsync(callbackData.Message.Chat.Id, 
                callbackData.Message.MessageId, 
                media: new InputMediaPhoto(new InputMedia(stream, "media"))
                );
        await botClient.EditMessageCaptionAsync(
            callbackData.Message.Chat.Id, 
            callbackData.Message.MessageId, 
            caption, 
            ParseMode.Html, 
            null, 
            inlineKeyboard
            );
    }
}