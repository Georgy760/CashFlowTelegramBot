using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using CashFlowTelegramBot.Skywards.ImageEditor;
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
    public static async void GetUserData(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        string lang,
        UserProfile SearchedUser,
        Table.TableRole tableRole, TableProfile tableData)
    { //TODO
        /*
        string path = null;
        string? caption = null;
        
        InlineKeyboardMarkup? inlineKeyboard = null;
        UserData invitedBy = null;
        if (SearchedUser.refId != null)
            invitedBy = await WebManager.SendData(new UserProfile((int) SearchedUser.refId),
                WebManager.RequestType.GetUserData);

        var tableToBack = await WebManager.SendData(SearchedUser, WebManager.RequestType.GetTableData);
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverARU,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverARU  
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverBRU,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBRU  
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverCRU,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCRU  
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverDRU,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDRU  
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
                                            InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),
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
                                    InlineKeyboardButton.WithUrl("📨 Связаться", "https://t.me/" + SearchedUser.username),
                                },
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Назад", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Главное меню", "MainMenu")
                                }
                            });
                    
                    caption = "<b>📋 Информация о пользователе:</b>" + 
                              "\n" +
                              $"\n<b>Роль:</b> {SearchedUser.GetTableRole(lang) + searchedUserRole}" +
                              $"\n<b>Ник:</b> @{SearchedUser.username}" +
                              $"\n<b>Имя пользователя:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                              $"\n<b>Лично приглашенных:</b> {SearchedUser.invited}" +
                              "\n" +
                              $"\n<b>Пригласил:</b> @{invitedBy.playerData.username}";
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverAENG,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverAENG  
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverBENG,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBENG  
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverCENG,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCENG  
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverDENG,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDENG
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                    
                    caption = "<b>📋 User info:</b>" + 
                              "\n" +
                              $"\n<b>Role:</b> {SearchedUser.GetTableRole(lang) + searchedUserRole}" +
                              $"\n<b>Nickname:</b> @{SearchedUser.username}" +
                              $"\n<b>Username:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                              $"\n<b>Personally invited:</b> {SearchedUser.invited}" +
                              "\n" +
                              $"\n<b>Invited by:</b> @{invitedBy.playerData.username}";

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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverAFR,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverAFR  
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverBFR,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBFR  
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverCFR,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCFR  
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverDFR,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDFR  
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
                                            InlineKeyboardButton.WithUrl("📨 Contact", "https://t.me/" + SearchedUser.username),
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
                    
                        caption = "<b>📋 Informations de l'utilisateur:</b>" + 
                                  "\n" +
                                  $"\n<b>Rôle:</b> {SearchedUser.GetTableRole(lang) + searchedUserRole}" +
                                  $"\n<b>Surnom:</b> @{SearchedUser.username}" +
                                  $"\n<b>Nom d'utilisateur:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
                                  $"\n<b>invité personnellement:</b> {SearchedUser.invited}" +
                                  "\n" +
                                  $"\n<b>inviter par:</b> @{invitedBy.playerData.username}";
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverADE,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverADE  
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverBDE,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverBDE  
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverCDE,
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverCDE 
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),  
                                        },
                                        new[]
                                        {
                                            Tables.InlineKeyboardButtonVerfGiverDDE
                                        },
                                        new []
                                        {
                                            Tables.InlineKeyboardButtonRemoveFromTableGiverDDE
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
                                            InlineKeyboardButton.WithUrl("📨 Kontakt", "https://t.me/" + SearchedUser.username),
                                        },
                                        new []
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
                                new []
                                {
                                    InlineKeyboardButton.WithCallbackData("🔙 Der Rücken", callbackAddress),
                                    InlineKeyboardButton.WithCallbackData("🗂 Hauptmenü", "MainMenu")
                                }
                            });
                    var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "📋 Benutzerinformation: \n\n" +
                            $"Rolle: {searchedUserRole}\n" +
                            $"Spitzname: @{SearchedUser.username}\n" +
                            $"ingeladen von: @{invitedBy.playerData.username}\n" +
                            $"Persönlich eingeladen: {SearchedUser.invited}" +
                            $"\n\nKontakt mit @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    caption = "<b>📋 Benutzerinformation:</b>" + 
                              "\n" +
                              $"\n<b>Rolle:</b> {SearchedUser.GetTableRole(lang) + searchedUserRole}" +
                              $"\n<b>Spitzname:</b> @{SearchedUser.username}" +
                              $"\n<b>Benutzername:</b> " + //{callbackData.From.FirstName + callbackData.From.LastName} //TODO Add to db field with first & last names
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
        */
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
        string lang, UserProfile user)
    {
        var table = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
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
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.bankerID),
                    WebManager.RequestType.GetUserData);
                caption += userData.playerData.UserInfo(user.lang, table.tableData, table.tableData.bankerID == user.id);
                //if (table.tableData.bankerID == user.id) tableInfo += "🔘";
                //tableInfo += $"🏦Банкир: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.banker);
            }

            if (table.tableData.managerA_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerA_ID),
                    WebManager.RequestType.GetUserData);
                caption += userData.playerData.UserInfo(user.lang, table.tableData, table.tableData.managerA_ID == user.id);
                //if (table.tableData.managerA_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"👤Менеджер-1: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.manager);
            }

            if (table.tableData.managerB_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerB_ID),
                    WebManager.RequestType.GetUserData);
                caption += userData.playerData.UserInfo(user.lang, table.tableData, table.tableData.managerB_ID == user.id);
                //if (table.tableData.managerB_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"👤Менеджер-2: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.manager);
            }

            if (table.tableData.giverA_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverA_ID),
                    WebManager.RequestType.GetUserData);
                caption += userData.playerData.UserInfo(user.lang, table.tableData, table.tableData.giverA_ID == user.id, table.tableData.verf_A, 1);
                //if (table.tableData.giverA_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-1: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver);
            }

            if (table.tableData.giverB_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverB_ID),
                    WebManager.RequestType.GetUserData);
                caption += userData.playerData.UserInfo(user.lang, table.tableData, table.tableData.giverB_ID == user.id, table.tableData.verf_B, 2);
                //if (table.tableData.giverB_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-2: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver);
            }

            if (table.tableData.giverC_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverC_ID),
                    WebManager.RequestType.GetUserData);
                caption += userData.playerData.UserInfo(user.lang, table.tableData, table.tableData.giverC_ID == user.id, table.tableData.verf_C, 3);
                //if (table.tableData.giverC_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-3: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver);
            }

            if (table.tableData.giverD_ID != null)
            {
                var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverD_ID),
                    WebManager.RequestType.GetUserData);
                caption += userData.playerData.UserInfo(user.lang, table.tableData, table.tableData.giverD_ID == user.id, table.tableData.verf_D, 4);
                //if (table.tableData.giverD_ID == user.id) tableInfo += "🔘";
                //tableInfo += $"🎁Даритель-4: @{userData.playerData.username}\n";
            }
            else
            {
                caption += user.UserInfo(Table.TableRole.giver);
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
    public static async void ShowTableAsImage(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
        UserProfile userData)
    {
        string path = null;
        var tableData = await WebManager.SendData(userData, WebManager.RequestType.GetTableData);
        /*if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/techSupport.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\techSupport.png");*/
        InlineKeyboardMarkup? inlineKeyboard = null;
        Message? sentMessage;
        string? caption;
        Message sentPhoto;
        var callbackAddress = GetCallbackAddress(tableData.tableData.tableType);
        inlineKeyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🔙", callbackAddress),
                }
            });
        caption = $"<b>Table ID: {tableData.tableData.tableID}</b>";
        Stream stream = TableImage.CreateTableImage(tableData.tableData).Result;
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
        stream.Dispose();
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
                var tableData = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
                Console.WriteLine(tableData.tableData.tableType);
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
            ParseMode.Markdown, 
            null, 
            inlineKeyboard
        );
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
                        caption = $"🤷 Для открытия данного стола Вам необходимо пригласить на Бронзовый стол еще {toInvite} игроков";
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
                        caption = "🤷 To open this table, you need to fulfill the conditions for invited players OR go through the table below.";
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
                        caption = "🤷 Pour ouvrir ce tableau, vous devez remplir les conditions des joueurs invités OU passer par le tableau ci-dessous.";
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
                        caption = "🤷 Um diese Tabelle zu öffnen, müssen Sie die Bedingungen für eingeladene Spieler erfüllen ODER die Tabelle unten durchgehen.";
                        break;
                    }
                }

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
            ParseMode.Markdown, 
            null, 
            inlineKeyboard
        );
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
                          $"<b>Ваш ID:</b> {userData.id}" +
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
                          $"<b>Your ID:</b> {userData.id}" +
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
                          $"<b>Votre identifiant:</b> {userData.id}" +
                          "\n" +
                          $"<b>Votre surnom:</b> @{userData.username}" +
                          "\n" +
                          $"<b>Personnellement invité:</b> {userData.invited}" +
                          "\n" +
                          $"<b>Équipe commune:</b> {userData.team}" +
                          "\n\n" +
                          $"<b>Valeur des cadeaux:</b> {userData.giftsReceived}$"+
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
                          $"<b>Ihre ID:</b> {userData.id}" +
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
        
        if (userData.UserTableList.table_ID_copper != null)
        {
            var tableDataCopper = await WebManager.SendData(
                new TableProfile((int) userData.UserTableList.table_ID_copper), WebManager.RequestType.GetTableData);

            copper = true;
            var giverCountCopper = 0;
            tableRole = userData.GetTableRole(userData.lang);
            if (tableDataCopper.tableData.giverA_ID != null)
            {
                if (tableDataCopper.tableData.giverA_ID == userData.id)
                {
                    if (tableDataCopper.tableData.verf_A)
                        IsGiverVerf = true;
                }
                giverCountCopper++;
            }
            if (tableDataCopper.tableData.giverB_ID != null)
            {
                if (tableDataCopper.tableData.giverB_ID == userData.id)
                {
                    if (tableDataCopper.tableData.verf_B)
                        IsGiverVerf = true;
                }
                giverCountCopper++;
            }
            if (tableDataCopper.tableData.giverC_ID != null)
            {
                if (tableDataCopper.tableData.giverC_ID == userData.id)
                {
                    if (tableDataCopper.tableData.verf_C)
                        IsGiverVerf = true;
                }
                giverCountCopper++;
            }
            if (tableDataCopper.tableData.giverD_ID != null)
            {
                if (tableDataCopper.tableData.giverD_ID == userData.id)
                {
                    if (tableDataCopper.tableData.verf_D)
                        IsGiverVerf = true;
                }
                giverCountCopper++;
            }
            tableType = TableProfile.GetTableType(userData, Table.TableType.copper);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}" +
                               "\n" +
                               $"Всего дарителей на столе: {giverCountCopper} из 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Вы активированы на столе";
                        else caption += "❌ Вы не активированы на столе";

                    }
                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Your role: {tableRole}" +
                               "\n" +
                               $"Total givers on the table: {giverCountCopper} of 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ You are activated on the table";
                        else caption += "❌ You are not activated on the table";

                    }
                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}" +
                               "\n" +
                               $"Total des donateurs sur la table: {giverCountCopper} sur 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Vous êtes activé sur la table";
                        else caption += "❌ Vous n'êtes pas activé sur la table";

                    }
                    break;
                case "de": 
                    caption += "\n" + 
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}" +
                               "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountCopper} von 4";
                    if (userData.UserTableList.copperTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Du bist auf dem Tisch aktiviert";
                        else caption += "❌ Du bist am Tisch nicht aktiviert";

                    }
                    break;
            }
        }

        if (userData.UserTableList.table_ID_bronze != null)
        {
            var tableDataBronze = await WebManager.SendData(
                new TableProfile((int) userData.UserTableList.table_ID_bronze), WebManager.RequestType.GetTableData);
            
            bronze = true;
            var giverCountBronze = 0;
            tableRole = userData.GetTableRole(userData.lang);
            if (tableDataBronze.tableData.giverA_ID != null)
            {
                if (tableDataBronze.tableData.giverA_ID == userData.id)
                {
                    if (tableDataBronze.tableData.verf_A)
                        IsGiverVerf = true;
                }
                giverCountBronze++;
            }

            if (tableDataBronze.tableData.giverB_ID != null)
            {
                if (tableDataBronze.tableData.giverB_ID == userData.id)
                {
                    if (tableDataBronze.tableData.verf_B)
                        IsGiverVerf = true;
                }
                giverCountBronze++;
            }
            if (tableDataBronze.tableData.giverC_ID != null)
            {
                if (tableDataBronze.tableData.giverC_ID == userData.id)
                {
                    if (tableDataBronze.tableData.verf_C)
                        IsGiverVerf = true;
                }
                giverCountBronze++;
            }
            if (tableDataBronze.tableData.giverD_ID != null)
            {
                if (tableDataBronze.tableData.giverD_ID == userData.id)
                {
                    if (tableDataBronze.tableData.verf_D)
                        IsGiverVerf = true;
                }
                giverCountBronze++;
            }
            tableType = TableProfile.GetTableType(userData, Table.TableType.bronze);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}" +
                               "\n" +
                               $"Всего дарителей на столе: {giverCountBronze} из 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Вы активированы на столе";
                        else caption += "❌ Вы не активированы на столе";

                    }
                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Your role: {tableRole}" +
                               "\n" +
                               $"Total givers on the table: {giverCountBronze} of 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ You are activated on the table";
                        else caption += "❌ You are not activated on the table";

                    }
                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}" +
                               "\n" +
                               $"Total des donateurs sur la table: {giverCountBronze} sur 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Vous êtes activé sur la table";
                        else caption += "❌ Vous n'êtes pas activé sur la table";

                    }
                    break;
                case "de": 
                    caption += "\n" + 
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}" +
                               "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountBronze} von 4";
                    if (userData.UserTableList.bronzeTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Du bist auf dem Tisch aktiviert";
                        else caption += "❌ Du bist am Tisch nicht aktiviert";

                    }
                    break;
            }
        }

        if (userData.UserTableList.table_ID_silver != null)
        {
            var tableDataSilver = await WebManager.SendData(
                new TableProfile((int) userData.UserTableList.table_ID_silver), WebManager.RequestType.GetTableData);
            
            silver = true;
            var giverCountSilver = 0;
            
            tableRole = userData.GetTableRole(userData.lang);
            if (tableDataSilver.tableData.giverA_ID != null)
            {
                if (tableDataSilver.tableData.giverA_ID == userData.id)
                {
                    if (tableDataSilver.tableData.verf_A)
                        IsGiverVerf = true;
                }
                giverCountSilver++;
            }
            if (tableDataSilver.tableData.giverB_ID != null)
            {
                if (tableDataSilver.tableData.giverB_ID == userData.id)
                {
                    if (tableDataSilver.tableData.verf_B)
                        IsGiverVerf = true;
                }
                giverCountSilver++;
            }
            if (tableDataSilver.tableData.giverC_ID != null)
            {
                if (tableDataSilver.tableData.giverC_ID == userData.id)
                {
                    if (tableDataSilver.tableData.verf_C)
                        IsGiverVerf = true;
                }
                giverCountSilver++;
            }
            if (tableDataSilver.tableData.giverD_ID != null)
            {
                if (tableDataSilver.tableData.giverD_ID == userData.id)
                {
                    if (tableDataSilver.tableData.verf_D)
                        IsGiverVerf = true;
                }
                giverCountSilver++;
            }
            tableType = TableProfile.GetTableType(userData, Table.TableType.silver);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}" +
                               "\n" +
                               $"Всего дарителей на столе: {giverCountSilver} из 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Вы активированы на столе";
                        else caption += "❌ Вы не активированы на столе";

                    }
                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Your role: {tableRole}" +
                               "\n" +
                               $"Total givers on the table: {giverCountSilver} of 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ You are activated on the table";
                        else caption += "❌ You are not activated on the table";

                    }
                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}" +
                               "\n" +
                               $"Total des donateurs sur la table: {giverCountSilver} sur 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Vous êtes activé sur la table";
                        else caption += "❌ Vous n'êtes pas activé sur la table";

                    }
                    break;
                case "de": 
                    caption += "\n" + 
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}" +
                               "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountSilver} von 4";
                    if (userData.UserTableList.silverTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Du bist auf dem Tisch aktiviert";
                        else caption += "❌ Du bist am Tisch nicht aktiviert";

                    }
                    break;
            }
        }

        if (userData.UserTableList.table_ID_gold != null)
        {
            var tableDataGold = await WebManager.SendData(new TableProfile((int) userData.UserTableList.table_ID_gold),
                WebManager.RequestType.GetTableData);
            
            gold = true;
            var giverCountGold = 0;
            
            tableRole = userData.GetTableRole(userData.lang);
            if (tableDataGold.tableData.giverA_ID != null)
            {
                if (tableDataGold.tableData.giverA_ID == userData.id)
                {
                    if (tableDataGold.tableData.verf_A)
                        IsGiverVerf = true;
                }
                giverCountGold++;
            }
            if (tableDataGold.tableData.giverB_ID != null)
            {
                if (tableDataGold.tableData.giverB_ID == userData.id)
                {
                    if (tableDataGold.tableData.verf_B)
                        IsGiverVerf = true;
                }
                giverCountGold++;
            }
            if (tableDataGold.tableData.giverC_ID != null)
            {
                if (tableDataGold.tableData.giverC_ID == userData.id)
                {
                    if (tableDataGold.tableData.verf_C)
                        IsGiverVerf = true;
                }
                giverCountGold++;
            }
            if (tableDataGold.tableData.giverD_ID != null)
            {
                if (tableDataGold.tableData.giverD_ID == userData.id)
                {
                    if (tableDataGold.tableData.verf_D)
                        IsGiverVerf = true;
                }
                giverCountGold++;
            }
            tableType = TableProfile.GetTableType(userData, Table.TableType.gold);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}" +
                               "\n" +
                               $"Всего дарителей на столе: {giverCountGold} из 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Вы активированы на столе";
                        else caption += "❌ Вы не активированы на столе";

                    }
                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Your role: {tableRole}" +
                               "\n" +
                               $"Total givers on the table: {giverCountGold} of 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ You are activated on the table";
                        else caption += "❌ You are not activated on the table";

                    }
                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}" +
                               "\n" +
                               $"Total des donateurs sur la table: {giverCountGold} sur 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Vous êtes activé sur la table";
                        else caption += "❌ Vous n'êtes pas activé sur la table";

                    }
                    break;
                case "de": 
                    caption += "\n" + 
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}" +
                               "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountGold} von 4";
                    if (userData.UserTableList.goldTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Du bist auf dem Tisch aktiviert";
                        else caption += "❌ Du bist am Tisch nicht aktiviert";

                    }
                    break;
            }
        }

        if (userData.UserTableList.table_ID_platinum != null)
        {
            var tableDataPlatinum = await WebManager.SendData(
                new TableProfile((int) userData.UserTableList.table_ID_platinum), WebManager.RequestType.GetTableData);
            
            platinum = true;
            var giverCountPlatinum = 0;
            
            tableRole = userData.GetTableRole(userData.lang);
            if (tableDataPlatinum.tableData.giverA_ID != null)
            {
                if (tableDataPlatinum.tableData.giverA_ID == userData.id)
                {
                    if (tableDataPlatinum.tableData.verf_A)
                        IsGiverVerf = true;
                }
                giverCountPlatinum++;
            }
            if (tableDataPlatinum.tableData.giverB_ID != null)
            {
                if (tableDataPlatinum.tableData.giverB_ID == userData.id)
                {
                    if (tableDataPlatinum.tableData.verf_B)
                        IsGiverVerf = true;
                }
                giverCountPlatinum++;
            }
            if (tableDataPlatinum.tableData.giverC_ID != null)
            {
                if (tableDataPlatinum.tableData.giverC_ID == userData.id)
                {
                    if (tableDataPlatinum.tableData.verf_C)
                        IsGiverVerf = true;
                }
                giverCountPlatinum++;
            }
            if (tableDataPlatinum.tableData.giverD_ID != null)
            {
                if (tableDataPlatinum.tableData.giverD_ID == userData.id)
                {
                    if (tableDataPlatinum.tableData.verf_D)
                        IsGiverVerf = true;
                }
                giverCountPlatinum++;
            }
            tableType = TableProfile.GetTableType(userData, Table.TableType.platinum);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}" +
                               "\n" +
                               $"Всего дарителей на столе: {giverCountPlatinum} из 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Вы активированы на столе";
                        else caption += "❌ Вы не активированы на столе";

                    }
                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Your role: {tableRole}" +
                               "\n" +
                               $"Total givers on the table: {giverCountPlatinum} of 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ You are activated on the table";
                        else caption += "❌ You are not activated on the table";

                    }
                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}" +
                               "\n" +
                               $"Total des donateurs sur la table: {giverCountPlatinum} sur 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Vous êtes activé sur la table";
                        else caption += "❌ Vous n'êtes pas activé sur la table";

                    }
                    break;
                case "de": 
                    caption += "\n" + 
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}" +
                               "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountPlatinum} von 4";
                    if (userData.UserTableList.platinumTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Du bist auf dem Tisch aktiviert";
                        else caption += "❌ Du bist am Tisch nicht aktiviert";

                    }
                    break;
            }
        }

        if (userData.UserTableList.table_ID_diamond != null)
        {
            var tableDataDiamond = await WebManager.SendData(
                new TableProfile((int) userData.UserTableList.table_ID_diamond), WebManager.RequestType.GetTableData);
            
            diamond = true;
            var giverCountDiamond = 0;
            
            tableRole = userData.GetTableRole(userData.lang);
            if (tableDataDiamond.tableData.giverA_ID != null)
            {
                if (tableDataDiamond.tableData.giverA_ID == userData.id)
                {
                    if (tableDataDiamond.tableData.verf_A)
                        IsGiverVerf = true;
                }
                giverCountDiamond++;
            }
            if (tableDataDiamond.tableData.giverB_ID != null)
            {
                if (tableDataDiamond.tableData.giverB_ID == userData.id)
                {
                    if (tableDataDiamond.tableData.verf_B)
                        IsGiverVerf = true;
                }
                giverCountDiamond++;
            }
            if (tableDataDiamond.tableData.giverC_ID != null)
            {
                if (tableDataDiamond.tableData.giverC_ID == userData.id)
                {
                    if (tableDataDiamond.tableData.verf_C)
                        IsGiverVerf = true;
                }
                giverCountDiamond++;
            }
            if (tableDataDiamond.tableData.giverD_ID != null)
            {
                if (tableDataDiamond.tableData.giverD_ID == userData.id)
                {
                    if (tableDataDiamond.tableData.verf_D)
                        IsGiverVerf = true;
                }
                giverCountDiamond++;
            }
            tableType = TableProfile.GetTableType(userData, Table.TableType.diamond);
            switch (userData.lang)
            {
                case "ru":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ваша роль: {tableRole}" +
                               "\n" +
                               $"Всего дарителей на столе: {giverCountDiamond} из 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Вы активированы на столе";
                        else caption += "❌ Вы не активированы на столе";

                    }
                    break;
                case "eng":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Your role: {tableRole}" +
                               "\n" +
                               $"Total givers on the table: {giverCountDiamond} of 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ You are activated on the table";
                        else caption += "❌ You are not activated on the table";

                    }
                    break;
                case "fr":
                    caption += "\n" +
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Votre rôle: {tableRole}" +
                               "\n" +
                               $"Total des donateurs sur la table: {giverCountDiamond} sur 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Vous êtes activé sur la table";
                        else caption += "❌ Vous n'êtes pas activé sur la table";

                    }
                    break;
                case "de": 
                    caption += "\n" + 
                               $"<b>{tableType}</b>" +
                               "\n" +
                               $"Ihre Rolle: {tableRole}" +
                               "\n" +
                               $"Gesamtzahl der Geber auf dem Tisch: {giverCountDiamond} von 4";
                    if (userData.UserTableList.diamondTableRole == Table.TableRole.giver)
                    {
                        if (IsGiverVerf)
                            caption += "✅ Du bist auf dem Tisch aktiviert";
                        else caption += "❌ Du bist am Tisch nicht aktiviert";

                    }
                    break;
            }
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
            ParseMode.MarkdownV2, 
            null, 
            inlineKeyboard
        );
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