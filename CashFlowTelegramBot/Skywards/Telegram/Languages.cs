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
    public static async void LanguageMenu(ITelegramBotClient botClient, long chatId)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/langSelection.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\langSelection.png");
        InlineKeyboardMarkup? inlineKeyboard;
        inlineKeyboard = new InlineKeyboardMarkup(
            // keyboard
            new[]
            {
                // first row
                new[]
                {
                    // first button in row
                    InlineKeyboardButton.WithCallbackData("🇬🇧 English", "ChangeToENG"),
                    // second button in row
                    InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "ChangeToFR")
                },
                // second row
                new[]
                {
                    // first button in row
                    InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "ChangeToRU"),
                    // second button in a row
                    InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "ChangeToDE")
                }
            });

        var sentPhoto = await botClient.SendPhotoAsync(
            chatId,
            File.OpenRead(path)!,
            "🌐 Language menu:" +
            "\n\nClick the corresponding button to change the language:",
            replyMarkup: inlineKeyboard);
    }

    //FIRST LANGUAGE MENU//
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
                    InlineKeyboardButton.WithCallbackData("🇬🇧 English", "Reg_ENG"),
                    InlineKeyboardButton.WithCallbackData("🇫🇷 Français", "Reg_FR")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("🇷🇺 Русский", "Reg_RU"),
                    InlineKeyboardButton.WithCallbackData("🇩🇪 Deutsch", "Reg_DE")
                }
            });

        var sentPhoto = await botClient.SendPhotoAsync(
            chatId,
            File.OpenRead(path)!,
            "🌐 Language menu:" +
            "\n\nClick the corresponding button to change the language:",
            replyMarkup: inlineKeyboard);
    }

    //GET USER DATA//
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

    //Team on tables//
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

    public static async void Warning(ITelegramBotClient botClient, long chatId, UserProfile user, Error error)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/status.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\status.png");
        var tableToBack = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
        var callbackAddress = GetCallbackAddress(tableToBack.tableData.tableType);


        Message? sentMessage;
        switch (error)
        {
            case Error.UserIsNotExist:
            {
                switch (user.lang)
                {
                    case "ru":
                    {
                        var inlineKeyboard = new InlineKeyboardMarkup(
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
                        var inlineKeyboard = new InlineKeyboardMarkup(
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
                        var inlineKeyboard = new InlineKeyboardMarkup(
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
                        var inlineKeyboard = new InlineKeyboardMarkup(
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
                sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"RU: Реферальная ссылка неверна\n\n" +
                    $"ENG: Referral link is invalid\n\n" +
                    $"FR: Le lien de parrainage n'est pas valide\n\n" +
                    $"DE: Empfehlungslink ist ungültig\n\n"
                );
                break;
            case Error.UserWithoutUsername:
                sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"RU: Использование бота без никнейма к сожалению не возможно, пожалуйста введите свой Username в настройках аккаунта Telegram и заново перейдите по реферальной ссылке\n\n" +
                    $"ENG: Using a bot without a nickname is unfortunately not possible, please enter your Username in the Telegram account settings and re-follow the referral link\n\n" +
                    $"FR: L'utilisation d'un bot sans pseudonyme n'est malheureusement pas possible, veuillez entrer votre nom d'utilisateur dans les paramètres du compte Telegram et suivre à nouveau le lien de parrainage\n\n" +
                    $"DE: Die Verwendung eines Bots ohne Nickname ist leider nicht möglich, bitte geben Sie Ihren Benutzernamen in den Telegram-Kontoeinstellungen ein und folgen Sie erneut dem Referral-Link\n\n"
                    );
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

    //MAIN MENU// DONE
    public static async void MainMenu(ITelegramBotClient botClient, long chatId, string lang)
    {
        string path = null;
        InlineKeyboardMarkup? inlineKeyboard;
        Message? sentMessage;
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🗂 <b>Главное меню</b>" +
                    "\n\nВыберите нужный раздел:",
                    ParseMode.Html,
                    replyMarkup: inlineKeyboard);
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🗂 Main Menu" +
                    "\n\nSelect the desired section:",
                    replyMarkup: inlineKeyboard);
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🗂 Menu" +
                    "\n\nSélectionnez la rubrique souhaitée:",
                    replyMarkup: inlineKeyboard);
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🗂 Das Menu" +
                    "\n\nWählen Sie den gewünschten Abschnitt aus:",
                    replyMarkup: inlineKeyboard);
                break;
        }
    }

    //STATUS// 
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
                            InlineKeyboardButton.WithCallbackData("🔙 Retour", "MainMenu")
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
                            InlineKeyboardButton.WithCallbackData("🔙 Zurück", "MainMenu")
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

    //REFFERAL LINK//Done   
    public static async void RefLink(ITelegramBotClient botClient, long chatId, UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/refLink.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\refLink.png");
        InlineKeyboardMarkup? inlineKeyboard;
        Message? sentMessage;
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🔗 Ваша реферальная ссылка:" +
                    "\n\nhttps://t.me/originalCashFlowbot?start=R" + userData.id,
                    replyMarkup: inlineKeyboard);
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
                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🔗 Your referral link:" +
                    "\n\nhttps://t.me/originalCashFlowbot?start=R" + userData.id,
                    replyMarkup: inlineKeyboard);
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
                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🔗 Votre lien de référence:" +
                    "\n\nhttps://t.me/originalCashFlowbot?start=R" + userData.id,
                    replyMarkup: inlineKeyboard);
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
                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b🔗 Ihr Empfehlungslink:" +
                    "\n\nhttps://t.me/originalCashFlowbot?start=R" + userData.id,
                    replyMarkup: inlineKeyboard);
                break;
        }
    }

    //USER AGREEMENT//Done
    public static async void Agreement(ITelegramBotClient botClient, long chatId, UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/agreement.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\agreement.png");
        InlineKeyboardMarkup? inlineKeyboard;
        Message? sentMessage;
        Message sentPhoto;
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📌 ПОЛЬЗОВАТЕЛЬСКОЕ СОГЛАШЕНИЕ" +
                    "\n\nПеред началом игры пользователь обязуется ознакомиться с текстом данного соглашения:",
                    replyMarkup: inlineKeyboard);
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📌 User Agreement" +
                    "\nBefore starting the game, the user undertakes to read the text of this agreement:",
                    replyMarkup: inlineKeyboard);
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📌 Conditions d'utilisation" +
                    "\nAvant de commencer le jeu, l'utilisateur s'engage à lire le texte de cet accord:",
                    replyMarkup: inlineKeyboard);
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📌 Benutzervereinbarung" +
                    "\nVor Spielbeginn verpflichtet sich der Nutzer, den Text dieser Vereinbarung zu lesen:",
                    replyMarkup: inlineKeyboard);
                break;
        }
    }

    //TECH SUPPORT // Done
    public static async void TechSupport(ITelegramBotClient botClient, long chatId, UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/techSupport.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\techSupport.png");
        InlineKeyboardMarkup? inlineKeyboard;
        Message? sentMessage;
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📲 Тех. Поддержка" +
                    "\n\nВыберите язык тех. поддержки и нажмите, чтобы открыть чат:",
                    replyMarkup: inlineKeyboard);
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

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📲 Tech support" +
                    "\n\nSelect the language of those. support and click to open chat:",
                    replyMarkup: inlineKeyboard);
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
                            InlineKeyboardButton.WithCallbackData("🔙 Retour", "MainMenu")
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📲 Support technique" +
                    "\n\nSélectionnez la langue de ceux-ci. support et cliquez pour ouvrir le chat :",
                    replyMarkup: inlineKeyboard);
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
                            InlineKeyboardButton.WithCallbackData("Zurück", "MainMenu")
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📲 Technischer Support" +
                    "\n\nWählen Sie die Sprache dieser aus. Support und zum Öffnen des Chats klicken:",
                    replyMarkup: inlineKeyboard);
                break;
        }
    }

    //BOT INFO// needed to be updated with couple of staff
    public static async void Info(ITelegramBotClient botClient, long chatId, UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/info.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\info.png");
        InlineKeyboardMarkup? inlineKeyboard;
        Message? sentMessage;
        Message sentPhoto;
        switch (userData.lang)
        {
            case "ru":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 Пользовательское соглашение",
                                "https://telegra.ph/Opta-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📍 Правила CASH FLOW",
                                "https://telegra.ph/Pravila-Cash-Flow-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("⚜ Столы", "https://telegra.ph/Stoly-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Роли", "https://telegra.ph/Roli-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 Условия столов",
                                "https://telegra.ph/Usloviya-stolov-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuRU
                        }
                    });
                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b📄 Инфо" +
                    "\n\nДанный раздел содержит необходимую информацию об игре CASH FLOW:",
                    replyMarkup: inlineKeyboard);
                break;
            case "eng":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 User Agreement", "https://telegra.ph/User-Agreement-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📍 Cash Flow Rules",
                                "https://telegra.ph/Cash-Flow-Rules-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("⚜ Cash Flow tables", "https://telegra.ph/Tables-07-21-2")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Roles", "https://telegra.ph/Roles-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 Table conditions",
                                "https://telegra.ph/Cash-Flow-tables-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\bInfo" +
                    "\nThis section contains the necessary information about the CASH FLOW game:",
                    replyMarkup: inlineKeyboard);
                break;
            case "fr":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 Conditions d'utilisation",
                                "https://telegra.ph/Conditions-dutilisation-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📍 Règles de trésorerie",
                                "https://telegra.ph/Cash-Flow-r%C3%A8gles-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("⚜ Tableaux des flux de trésorerie",
                                "https://telegra.ph/Les-tables-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 RÔLE ", "https://telegra.ph/R%C3%94LE-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 Conditions du tableau",
                                "https://telegra.ph/Cash-Flow-tableaux-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Retour", "MainMenu")
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\bInfo" +
                    "\nCette section contient les informations nécessaires sur le jeu CASH FLOW :",
                    replyMarkup: inlineKeyboard);
                break;
            case "de":
                inlineKeyboard = new InlineKeyboardMarkup(
                    new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 Benutzervereinbarung",
                                "https://telegra.ph/Benutzervereinbarung-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📍 Cashflow-Regeln",
                                "https://telegra.ph/Cashflow-Regeln-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("⚜ Cashflow-Tabellen", "https://telegra.ph/Tische-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("👥 Rollen", "https://telegra.ph/3-ROLLEN-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("📌 Tabellenbedingungen",
                                "https://telegra.ph/Cashflow-Tabellen-07-21")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🔙 Zurück", "MainMenu")
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\bInfo" +
                    "\nDieser Abschnitt enthält die notwendigen Informationen zum Spiel CASH FLOW:",
                    replyMarkup: inlineKeyboard);
                break;
        }
    }

    //TABLE MENU//
    public static async void TableMenu(ITelegramBotClient botClient, long chatId,
        UserProfile userData)
    {
        string path = null;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images/MainMenu/tableSelection.png");

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Images\MainMenu\tableSelection.png");
        InlineKeyboardMarkup? inlineKeyboard;
        Message? sentMessage;
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
                    }, /*
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("🎖 Платиновый стол", "PlatinumTable")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("💎 Алмазный стол", "DiamondTable")
                    },*/
                        new[]
                        {
                            InlineKeyboardButtonMainMenuRU
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b⚜ Список столов" +
                    "\n\nВыберите стол на который хотите зайти:",
                    replyMarkup: inlineKeyboard);
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
                    },/*
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("🎖 Platinum table", "PlatinumTable")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("💎 Diamond table", "DiamondTable")
                    },*/
                        new[]
                        {
                            InlineKeyboardButtonMainMenuENG
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b⚜ List of tables" +
                    "\n\nSelect the table you want to join:",
                    replyMarkup: inlineKeyboard);
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
                        },/*
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎖 Table de platine", "PlatinumTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("💎 Tableau de diamant", "DiamondTable")
                        },*/
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Retour", "MainMenu")
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b⚜ Liste des tableaux" +
                    "\n\nSélectionnez la table que vous souhaitez rejoindre :",
                    replyMarkup: inlineKeyboard);
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
                        },/*
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🎖 platin Tisch", "PlatinumTable")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("💎 diamant Tisch", "DiamondTable")
                        },*/
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Zurück", "MainMenu")
                        }
                    });

                sentPhoto = await botClient.SendPhotoAsync(
                    chatId,
                    File.OpenRead(path)!,
                    "\b⚜ Tabellenverzeichnis" +
                    "\n\nWählen Sie den Tisch aus, dem Sie beitreten möchten:",
                    replyMarkup: inlineKeyboard);
                break;
        }
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
                InlineKeyboardMarkup? inlineKeyboard;
                Message sentMessage;
                switch (userData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Нет", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Да", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Вы точно хотите покинуть данный стол?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Yes", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Are you sure you want to leave this table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Yes", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Êtes-vous sûr de vouloir quitter cette table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("NEIN", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("JA", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Möchten Sie diesen Tisch wirklich verlassen?",
                            replyMarkup: inlineKeyboard);
                        break;
                }

                break;
            }
            case WebManager.RequestType.RemoveFromTable:
            {
                Message sentMessage;
                InlineKeyboardMarkup inlineKeyboard;
                switch (userData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Нет", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Да", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Вы точно хотите удалить данного пользователя со стола?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Yes", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Are you sure you want to remove this user from the table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("no", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Oui", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Voulez-vous vraiment supprimer cet utilisateur du tableau?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("NEIN", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("JA", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Möchten Sie diesen Benutzer wirklich aus der Tabelle entfernen?",
                            replyMarkup: inlineKeyboard);
                        break;
                }

                break;
            }
            case WebManager.RequestType.Confirm:
            {
                InlineKeyboardMarkup inlineKeyboard;
                Message? sentMessage;
                switch (userData.lang)
                {
                    case "ru":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Нет", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Да", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Вы точно хотите подтвердить платеж от данного пользователя?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "eng":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Yes", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Are you sure you want to confirm the payment from this user?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "fr":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("no", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("Oui", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Voulez-vous vraiment confirmer le paiement de cet utilisateur?",
                            replyMarkup: inlineKeyboard);
                        break;
                    case "de":
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("NEIN", "ChooseTable"),
                                    InlineKeyboardButton.WithCallbackData("JA", "Confirm" + callbackData.Data)
                                }
                            });
                        sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            "Möchten Sie die Zahlung von diesem Nutzer wirklich bestätigen?",
                            replyMarkup: inlineKeyboard);
                        break;
                }

                break;
            }
        }
    }
}