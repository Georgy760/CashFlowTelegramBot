using CashFlowTelegramBot.Skywards.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CashFlowTelegramBot.Skywards.Telegram;

public partial class Languages
{
    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuRU =
        InlineKeyboardButton.WithCallbackData("Назад", "MainMenu");

    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuENG =
        InlineKeyboardButton.WithCallbackData("Back", "MainMenu");

    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuFR =
        InlineKeyboardButton.WithCallbackData("Retour", "MainMenu");

    public static readonly InlineKeyboardButton InlineKeyboardButtonMainMenuDE =
        InlineKeyboardButton.WithCallbackData("Zurück", "MainMenu");

    public static async void LanguageMenu(ITelegramBotClient botClient, long chatId)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(
            // keyboard
            new[]
            {
                // first row
                new[]
                {
                    // first button in row
                    InlineKeyboardButton.WithCallbackData("English 🇬🇧", "ChangeToENG"),
                    // second button in row
                    InlineKeyboardButton.WithCallbackData("Français 🇫🇷", "ChangeToFR")
                },
                // second row
                new[]
                {
                    // first button in row
                    InlineKeyboardButton.WithCallbackData("Русский 🇷🇺", "ChangeToRU"),
                    // second button in a row
                    InlineKeyboardButton.WithCallbackData("Deutsch 🇩🇪", "ChangeToDE")
                }
            });

        var sentMessage = await botClient.SendTextMessageAsync(
            chatId,
            $"Language menu",
            replyMarkup: inlineKeyboard);
    }

    public static async void RegLanguageMenu(ITelegramBotClient botClient, long chatId)
    {
        var inlineKeyboard = new InlineKeyboardMarkup(
            new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("English 🇬🇧", "Reg_ENG"),
                    InlineKeyboardButton.WithCallbackData("Français 🇫🇷", "Reg_FR")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Русский 🇷🇺", "Reg_RU"),
                    InlineKeyboardButton.WithCallbackData("Deutsch 🇩🇪", "Reg_DE")
                }
            });

        var sentMessage = await botClient.SendTextMessageAsync(
            chatId,
            $"Language menu",
            replyMarkup: inlineKeyboard);
    }

    public static async void GetUserData(ITelegramBotClient botClient, long chatId, string lang,
        UserProfile SearchedUser,
        Table.TableRole tableRole)
    {
        UserData invitedBy = null;
        if (SearchedUser.refId != null)
        {
            invitedBy = await WebManager.SendData(new UserProfile((int) SearchedUser.refId),
                WebManager.RequestType.GetUserData);
        }

        var tableToBack = await WebManager.SendData(SearchedUser, WebManager.RequestType.GetTableData);
        if (tableToBack.tableData.tableID != 0)
        {
            var callbackAddress = "";
            switch (tableToBack.tableData.tableType)
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

            InlineKeyboardMarkup? inlineKeyboard = null;
            bool flag = false;
            switch (lang)
            {
                case "ru":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "manager")
                    {
                        if (tableToBack.tableData.managerA_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerARU
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.managerB_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerBRU
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (!flag)
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                }
                            });
                    }

                    if (invitedBy != null)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Информация о пользователе\n\n" +
                            $"Роль: {SearchedUser.tableRole}\n" +
                            $"Ник: @{SearchedUser.username}\n" +
                            $"Пригласил: @{invitedBy.playerData.username}\n" +
                            $"Лично приглашенных: {SearchedUser.invited}" +
                            $"\n\nСвязаться c @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Информация о пользователе\n\n" +
                            $"Роль: {SearchedUser.tableRole}\n" +
                            $"Ник: @{SearchedUser.username}\n" +
                            $"Лично приглашенных: {SearchedUser.invited}" +
                            $"\n\nСвязаться c @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }

                    break;
                }
                case "eng":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "manager")
                    {
                        if (tableToBack.tableData.managerA_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerAENG
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.managerB_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerBENG
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (!flag)
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                }
                            });
                    }

                    if (invitedBy != null)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"User info\n\n" +
                            $"Role: {SearchedUser.tableRole}\n" +
                            $"Nickname: @{SearchedUser.username}\n" +
                            $"Invited by: @{invitedBy.playerData.username}\n" +
                            $"Personally invited: {SearchedUser.invited}" +
                            $"\n\nContact with @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"User info\n\n" +
                            $"Role: {SearchedUser.tableRole}\n" +
                            $"Nickname: @{SearchedUser.username}\n" +
                            $"Personally invited: {SearchedUser.invited}" +
                            $"\n\nContact with @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }

                    break;
                }
                case "fr":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "manager")
                    {
                        if (tableToBack.tableData.managerA_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerAFR
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.managerB_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerBFR
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (!flag)
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                }
                            });
                    }

                    if (invitedBy != null)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Informations de l'utilisateur\n\n" +
                            $"Rôle: {SearchedUser.tableRole}\n" +
                            $"Surnom: @{SearchedUser.username}\n" +
                            $"inviter par: @{invitedBy.playerData.username}\n" +
                            $"invité personnellement: {SearchedUser.invited}" +
                            $"\n\nContacter avec @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }
                    else
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Informations de l'utilisateur\n\n" +
                            $"Rôle: {SearchedUser.tableRole}\n" +
                            $"Surnom: @{SearchedUser.username}\n" +
                            $"invité personnellement: {SearchedUser.invited}" +
                            $"\n\nContacter avec @{SearchedUser.username}",
                            replyMarkup: inlineKeyboard);
                    }

                    break;
                }
                case "de":
                {
                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "manager")
                    {
                        if (tableToBack.tableData.managerA_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerADE
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.managerB_ID == SearchedUser.id && !flag)
                        {
                            inlineKeyboard = new InlineKeyboardMarkup(
                                new[]
                                {
                                    new[]
                                    {
                                        Tables.InlineKeyboardButtonRemoveFromTableManagerBDE
                                    },
                                    new[]
                                    {
                                        InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (tableRole == Table.TableRole.banker && SearchedUser.tableRole == "giver")
                    {
                        if (tableToBack.tableData.giverA_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverB_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverC_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                    }
                                });
                            flag = true;
                        }

                        if (tableToBack.tableData.giverD_ID == SearchedUser.id && !flag)
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
                                        InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                    }
                                });
                            flag = true;
                        }
                    }

                    if (!flag)
                    {
                        inlineKeyboard = new InlineKeyboardMarkup(
                            new[]
                            {
                                new[]
                                {
                                    InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                }
                            });
                    }

                    if (invitedBy != null)
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Benutzerinformation\n\n" +
                            $"Rolle: {SearchedUser.tableRole}\n" +
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
                            $"Benutzerinformation\n\n" +
                            $"Rolle: {SearchedUser.tableRole}\n" +
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Вы больше не участник стола",
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"You are no longer a member of the table",
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Vous n'êtes plus membre de la table",
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Sie sind kein Mitglied des Tisches mehr",
                        replyMarkup: inlineKeyboard);
                    break;
                }
            }
        }
    }

    public static async void ShowListTeam(ITelegramBotClient botClient, long chatId, string lang, UserProfile user)
    {
        var table = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
        if (table.tableData.tableID != 0)
        {
            var callbackAddress = "";
            switch (table.tableData.tableType)
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

            var tableInfo = "";
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
                                InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                            }
                        });
                    if (table.tableData.bankerID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.bankerID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Банкир: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Менеджер-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Менеджер-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Даритель-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Даритель-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverC_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverC_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Даритель-3: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverD_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverD_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Даритель-4: @{userData.playerData.username}\n";
                    }

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        tableInfo,
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
                                InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                            }
                        });
                    if (table.tableData.bankerID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.bankerID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Banker: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Manager-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Manager-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Giver-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Giver-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverC_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverC_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Giver-3: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverD_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverD_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Giver-4: @{userData.playerData.username}\n";
                    }

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        tableInfo,
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
                                InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                            }
                        });
                    if (table.tableData.bankerID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.bankerID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Banquier: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Gestionnaire-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Gestionnaire-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Donateur-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Donateur-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverC_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverC_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Donateur-3: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverD_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverD_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Donateur-4: @{userData.playerData.username}\n";
                    }

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        tableInfo,
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
                                InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                            }
                        });
                    if (table.tableData.bankerID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.bankerID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Banker: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Manager-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.managerB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.managerB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Manager-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverA_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverA_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Geber-1: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverB_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverB_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Geber-2: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverC_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverC_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Geber-3: @{userData.playerData.username}\n";
                    }

                    if (table.tableData.giverD_ID != null)
                    {
                        var userData = await WebManager.SendData(new UserProfile((int) table.tableData.giverD_ID),
                            WebManager.RequestType.GetUserData);
                        tableInfo += $"Geber-4: @{userData.playerData.username}\n";
                    }

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        tableInfo,
                        replyMarkup: inlineKeyboard);
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Вы больше не участник стола",
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"You are no longer a member of the table",
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Vous n'êtes plus membre de la table",
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
                                InlineKeyboardButton.WithCallbackData("Ok", "ChooseTable"),
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Sie sind kein Mitglied des Tisches mehr",
                        replyMarkup: inlineKeyboard);
                    break;
                }
            }
        }
    }

    public static async void Warning(ITelegramBotClient botClient, long chatId, UserProfile user, Error error)
    {
        var tableToBack = await WebManager.SendData(user, WebManager.RequestType.GetTableData);
        var callbackAddress = "";
        switch (tableToBack.tableData.tableType)
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
                                    InlineKeyboardButton.WithCallbackData("Назад", callbackAddress),
                                }
                            });

                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Пользователь ещё не зашёл на данную позицию",
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
                                    InlineKeyboardButton.WithCallbackData("Back", callbackAddress),
                                }
                            });

                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"The user has not entered this position yet",
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
                                    InlineKeyboardButton.WithCallbackData("Retour", callbackAddress),
                                }
                            });

                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"L'utilisateur n'a pas encore entré cette position",
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
                                    InlineKeyboardButton.WithCallbackData("Zurück", callbackAddress),
                                }
                            });

                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Der Benutzer hat diese Position noch nicht eingegeben",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                }

                break;
            }
        }
    }

    public static async void Warning(ITelegramBotClient botClient, long chatId, UserProfile user,
        WebManager.RequestType error)
    {
    }

    public class Russian
    {
        public static async void MainMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Выбрать стол", "ChooseTable"),
                        InlineKeyboardButton.WithCallbackData("Статус", "Status")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Инфо", "Info"),
                        InlineKeyboardButton.WithCallbackData("Реф. ссылка", "RefLink")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Тех. поддержка", "TechSupport"),
                        InlineKeyboardButton.WithCallbackData("Сменить язык", "ChangeLang")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Меню",
                replyMarkup: inlineKeyboard);
        }

        public static async void Status(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Общий чат", "https://t.me/cashflow_official_chat"),
                        InlineKeyboardButton.WithUrl("Канал", "https://t.me/original_cashflow")
                    },
                    new[]
                    {
                        InlineKeyboardButtonMainMenuRU
                    }
                });
            if (userData.refId != null)
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nВаш ID: " + userData.id + "\nВаш ник: @" + userData.username +
                    "\n\nВас пригласил: @" + userData.invitedBy + "\nЛично приглашенных: " + userData.invited,
                    replyMarkup: inlineKeyboard);
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nВаш ID: " + userData.id + "\nВаш ник: @" + userData.username +
                    "\n\nЛично приглашенных: " + userData.invited,
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void RefLink(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButtonMainMenuRU
                    }
                });
            Console.WriteLine("https://t.me/originalCashFlowbot?start=R" + userData.id);
            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Ваша реферальная ссылка: https://t.me/originalCashFlowbot?start=R" + userData.id,
                replyMarkup: inlineKeyboard);
        }

        public static async void Agreement(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Соглашение", "https://telegra.ph/Opta-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Согласие", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"📌 Пользовательское соглашение",
                replyMarkup: inlineKeyboard);
        }

        public static async void TechSupport(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                        InlineKeyboardButtonMainMenuRU
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Тех. Поддержка",
                replyMarkup: inlineKeyboard);
        }

        public static async void Info(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 Пользовательское соглашение", "https://telegra.ph/Opta-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Правила Cash Flow",
                            "https://telegra.ph/Pravila-Cash-Flow-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 Столы", "https://telegra.ph/Stoly-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Роли", "https://telegra.ph/Roli-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Условия столов", "https://telegra.ph/Usloviya-stolov-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButtonMainMenuRU
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Инфо",
                replyMarkup: inlineKeyboard);
        }

        public static async void TableMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                    }, /*
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
                    },*/
                    new[]
                    {
                        InlineKeyboardButtonMainMenuRU
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                "Выберите стол",
                replyMarkup: inlineKeyboard);
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData, Table.TableType tableType)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                            $"На этом столе Вам нужно сделать подарок 100$\nВы точно хотите зайти на данный стол?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"На этом столе Вам нужно сделать подарок 400$\nВы точно хотите зайти на данный стол?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"На этом столе Вам нужно сделать подарок 1000$\nВы точно хотите зайти на данный стол?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"На этом столе Вам нужно сделать подарок 2500$\nВы точно хотите зайти на данный стол?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"На этом столе Вам нужно сделать подарок 5000$\nВы точно хотите зайти на данный стол?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"На этом столе Вам нужно сделать подарок 10000$\nВы точно хотите зайти на данный стол?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                }
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"Вы точно хотите зайти на данный стол?",
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            WebManager.RequestType requestType)
        {
            switch (requestType)
            {
                case WebManager.RequestType.LeaveTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Нет", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Да", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Вы точно хотите покинуть данный стол?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.RemoveFromTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Нет", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Да", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Вы точно хотите удалить данного пользователя со стола?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.Confirm:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Нет", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Да", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Вы точно хотите подтвердить платеж от данного пользователя?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
            }
        }

        public class TablesRU : Tables
        {
            public static async void Copper(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "copper";
                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.copper, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"К сожалению таких столов пока что нет, попробуйте позже",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Bronze(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "bronze";
                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"К сожалению таких столов пока что нет, попробуйте позже",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Silver(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "silver";
                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.silver, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"К сожалению таких столов пока что нет, попробуйте позже",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Gold(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "gold";
                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.gold, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"К сожалению таких столов пока что нет, попробуйте позже",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Platinum(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "platinum";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"К сожалению таких столов пока что нет, попробуйте позже",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Diamond(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "diamond";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableRU
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"К сожалению таких столов пока что нет, попробуйте позже",
                        replyMarkup: inlineKeyboard);
                }
            }
        }
    }

    public class English
    {
        public static async void MainMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Choose table", "ChooseTable"),
                        InlineKeyboardButton.WithCallbackData("Status", "Status")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Info", "Info"),
                        InlineKeyboardButton.WithCallbackData("Referral link", "RefLink")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Tech support", "TechSupport"),
                        InlineKeyboardButton.WithCallbackData("Change language", "ChangeLang")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                "Menu",
                replyMarkup: inlineKeyboard);
        }

        public static async void Status(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("General chat", "https://t.me/cashflow_official_chat"),
                        InlineKeyboardButton.WithUrl("Channel", "https://t.me/original_cashflow")
                    },
                    new[]
                    {
                        InlineKeyboardButtonMainMenuENG
                    }
                });
            if (userData.refId != null)
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nYour ID: " + userData.id + "\nYour ID: @" + userData.username +
                    "\n\nYou invited by: @" + userData.invitedBy + "\nInvited by you: " + userData.invited,
                    replyMarkup: inlineKeyboard);
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nYour ID: " + userData.id + "\nYour ID: @" + userData.username +
                    "\n\nInvited by you: " + userData.invited,
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void RefLink(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButtonMainMenuENG
                    }
                });
            Console.WriteLine("https://t.me/originalCashFlowbot?start=R" + userData.id);
            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Your referral link: https://t.me/originalCashFlowbot?start=R" + userData.id,
                replyMarkup: inlineKeyboard);
        }

        public static async void Agreement(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Agreement", "https://telegra.ph/User-Agreement-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Agree", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"📌 User Agreement",
                replyMarkup: inlineKeyboard);
        }

        public static async void TechSupport(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                        InlineKeyboardButtonMainMenuENG
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Tech support",
                replyMarkup: inlineKeyboard);
        }

        public static async void Info(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 User Agreement", "https://telegra.ph/User-Agreement-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Cash Flow Rules", "https://telegra.ph/Cash-Flow-Rules-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 Cash Flow tables", "https://telegra.ph/Tables-07-21-2"),
                        InlineKeyboardButton.WithUrl("📌 Roles", "https://telegra.ph/Roles-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Table conditions", "https://telegra.ph/Cash-Flow-tables-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButtonMainMenuENG
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Info",
                replyMarkup: inlineKeyboard);
        }

        public static async void TableMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                    }, /*
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
                    },*/
                    new[]
                    {
                        InlineKeyboardButtonMainMenuENG
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                "Choose a table",
                replyMarkup: inlineKeyboard);
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData, Table.TableType tableType)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                            $"On this table you need to make a gift of $100\nAre you sure you want to go to this table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"On this table you need to make a gift of $400\nAre you sure you want to go to this table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"On this table you need to make a gift of $1000\nAre you sure you want to go to this table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"On this table you need to make a gift of $2500\nAre you sure you want to go to this table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"On this table you need to make a gift of $5000\nAre you sure you want to go to this table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"On this table you need to make a gift of $10000\nAre you sure you want to go to this table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                }
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"Are you sure you want to join this table?",
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            WebManager.RequestType requestType)
        {
            switch (requestType)
            {
                case WebManager.RequestType.LeaveTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Yes", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Are you sure you want to leave this table?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.RemoveFromTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Yes", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Are you sure you want to remove this user from the table?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.Confirm:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("No", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Yes", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Are you sure you want to confirm the payment from this user?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
            }
        }

        public class TablesENG : Tables
        {
            public static async void Copper(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "copper";
                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.copper, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableENG
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Unfortunately, there are no such tables yet, please try again later",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Bronze(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "bronze";
                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableENG
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Unfortunately, there are no such tables yet, please try again later",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Silver(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "silver";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.silver, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                TablesENG.InlineKeyboardButtonChooseTableENG
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Unfortunately, there are no such tables yet, please try again later",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Gold(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "gold";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            TablesENG.Giver(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "manager":
                            TablesENG.Manager(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "banker":
                            TablesENG.Banker(botClient, chatId, Table.TableType.gold, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                TablesENG.InlineKeyboardButtonChooseTableENG
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Unfortunately, there are no such tables yet, please try again later",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Platinum(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "platinum";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableENG
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Unfortunately, there are no such tables yet, please try again later",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Diamond(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "diamond";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            TablesENG.Giver(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "manager":
                            TablesENG.Manager(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "banker":
                            TablesENG.Banker(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                TablesENG.InlineKeyboardButtonChooseTableENG
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Unfortunately, there are no such tables yet, please try again later",
                        replyMarkup: inlineKeyboard);
                }
            }
        }
    }

    public class French
    {
        public static async void MainMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Choisissez le tableau", "ChooseTable"),
                        InlineKeyboardButton.WithCallbackData("Statut", "Status")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Info", "Info"),
                        InlineKeyboardButton.WithCallbackData("Lien de référence", "RefLink")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Support technique", "TechSupport"),
                        InlineKeyboardButton.WithCallbackData("Changer de langue", "ChangeLang")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Menu",
                replyMarkup: inlineKeyboard);
        }

        public static async void Status(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Chat général", "https://t.me/cashflow_official_chat"),
                        InlineKeyboardButton.WithUrl("Le canal", "https://t.me/original_cashflow")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Retour", "MainMenu")
                    }
                });
            if (userData.refId != null)
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nVotre identifiant): " + userData.id + "\nTon surnom: @" +
                    userData.username + "\n\nVous êtes invité par: @" + userData.invitedBy + "\nInvité par vous: " +
                    userData.invited,
                    replyMarkup: inlineKeyboard);
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nVotre identifiant: " + userData.id + "\nTon surnom:" + userData.username +
                    "\n\nVous êtes invité par: " + userData.invited,
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void RefLink(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Retour", "MainMenu")
                    }
                });
            Console.WriteLine("https://t.me/originalCashFlowbot?start=R" + userData.id);
            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"FR(Your referral link): https://t.me/originalCashFlowbot?start=R" + userData.id,
                replyMarkup: inlineKeyboard);
        }

        public static async void Agreement(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Conditions d'utilisation",
                            "https://telegra.ph/Conditions-dutilisation-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Accepter", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"📌 Conditions d'utilisation",
                replyMarkup: inlineKeyboard);
        }

        public static async void TechSupport(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                        InlineKeyboardButton.WithCallbackData("Retour", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Support technique",
                replyMarkup: inlineKeyboard);
        }

        public static async void Info(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 Conditions d'utilisation",
                            "https://telegra.ph/Conditions-dutilisation-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Règles de trésorerie",
                            "https://telegra.ph/Cash-Flow-r%C3%A8gles-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 Tableaux des flux de trésorerie",
                            "https://telegra.ph/Les-tables-07-21"),
                        InlineKeyboardButton.WithUrl("📌 RÔLE ", "https://telegra.ph/R%C3%94LE-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Conditions du tableau",
                            "https://telegra.ph/Cash-Flow-tableaux-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Retour", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Info",
                replyMarkup: inlineKeyboard);
        }

        public static async void TableMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                    /*new[]
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
                    },*/
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Retour", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                "Choisissez un tableau",
                replyMarkup: inlineKeyboard);
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData, Table.TableType tableType)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                            $"Sur cette table, vous devez faire un don de 100$\nÊtes-vous sûr de vouloir aller à cette table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Sur cette table, vous devez faire un don de 400$\nÊtes-vous sûr de vouloir aller à cette table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Sur cette table, vous devez faire un don de 1000$\nÊtes-vous sûr de vouloir aller à cette table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Sur cette table, vous devez faire un don de 2500$\nÊtes-vous sûr de vouloir aller à cette table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Sur cette table, vous devez faire un don de 5000$\nÊtes-vous sûr de vouloir aller à cette table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"Sur cette table, vous devez faire un don de 10000$\nÊtes-vous sûr de vouloir aller à cette table?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                }
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"Êtes-vous sûr de vouloir rejoindre cette table?",
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            WebManager.RequestType requestType)
        {
            switch (requestType)
            {
                case WebManager.RequestType.LeaveTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("no", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Oui", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Êtes-vous sûr de vouloir quitter cette table?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.RemoveFromTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("no", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Oui", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Voulez-vous vraiment supprimer cet utilisateur du tableau?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.Confirm:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("no", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("Oui", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Voulez-vous vraiment confirmer le paiement de cet utilisateur?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
            }
        }

        public class TablesFR : Tables
        {
            public static async void Copper(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "copper";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.copper, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Bronze(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "bronze";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Silver(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "silver";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.silver, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Gold(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "gold";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.gold, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Platinum(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "platinum";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Diamond(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "diamond";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableFR
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Malheureusement, il n'y a pas encore de telles tables, veuillez réessayer plus tard",
                        replyMarkup: inlineKeyboard);
                }
            }
        }
    }

    public class German
    {
        public static async void MainMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Tisch auswählen", "ChooseTable"),
                        InlineKeyboardButton.WithCallbackData("Status", "Status")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Die Info", "Info"),
                        InlineKeyboardButton.WithCallbackData("Empfehlungslink", "RefLink")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Technischer Support", "TechSupport"),
                        InlineKeyboardButton.WithCallbackData("Sprache ändern", "ChangeLang")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                "Speisekarte",
                replyMarkup: inlineKeyboard);
        }

        public static async void Status(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Allgemeiner Chat", "https://t.me/cashflow_official_chat"),
                        InlineKeyboardButton.WithUrl("Kanal)", "https://t.me/original_cashflow")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Zurück", "MainMenu")
                    }
                });
            //float exchangeRate = CurrencyConverter.GetExchangeRate("usd", "rub", 1f);
            //Console.WriteLine("\nexchangeRate " + exchangeRate);
            if (userData.refId != null)
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nIhre ID: " + userData.id + "\nDein Spitzname: @" + userData.username +
                    "\n\nDu hast eingeladen: @" + userData.invitedBy + "\nVon Ihnen eingeladen: " + userData.invited,
                    replyMarkup: inlineKeyboard);
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"1$ - 58₽ - 0.98€" + "\nIhre ID: " + userData.id + "\nDein Spitzname: @" + userData.username +
                    "\n\nVon Ihnen eingeladen: " + userData.invited,
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void RefLink(ITelegramBotClient botClient, long chatId, UserProfile userData)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Zurück", "MainMenu")
                    }
                });
            Console.WriteLine("https://t.me/originalCashFlowbot?start=R" + userData.id);
            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"DE(Your referral link): https://t.me/originalCashFlowbot?start=R" + userData.id,
                replyMarkup: inlineKeyboard);
        }

        public static async void Agreement(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("Benutzervereinbarung",
                            "https://telegra.ph/Benutzervereinbarung-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Einverstanden", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"📌 Benutzervereinbarung",
                replyMarkup: inlineKeyboard);
        }

        public static async void TechSupport(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Technischer Support",
                replyMarkup: inlineKeyboard);
        }

        public static async void Info(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
                new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 Benutzervereinbarung",
                            "https://telegra.ph/Benutzervereinbarung-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Cashflow-Regeln", "https://telegra.ph/Cashflow-Regeln-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithUrl("📌 Cashflow-Tabellen", "https://telegra.ph/Tische-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Rollen", "https://telegra.ph/3-ROLLEN-07-21"),
                        InlineKeyboardButton.WithUrl("📌 Tabellenbedingungen",
                            "https://telegra.ph/Cashflow-Tabellen-07-21")
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Zurück", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                $"Info",
                replyMarkup: inlineKeyboard);
        }

        public static async void TableMenu(ITelegramBotClient botClient, long chatId)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                    /*new[]
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
                    },*/
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Zurück", "MainMenu")
                    }
                });

            var sentMessage = await botClient.SendTextMessageAsync(
                chatId,
                "Wähle einen Tisch",
                replyMarkup: inlineKeyboard);
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            UserData userData, Table.TableType tableType)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(
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
                            $"An diesem Tisch müssen Sie 100$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.bronze:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"An diesem Tisch müssen Sie 400$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.silver:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"An diesem Tisch müssen Sie 1000$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.gold:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"An diesem Tisch müssen Sie 2500$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.platinum:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"An diesem Tisch müssen Sie 5000$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                    case Table.TableType.diamond:
                    {
                        var sentMessage = await botClient.SendTextMessageAsync(
                            chatId,
                            $"An diesem Tisch müssen Sie 10000$ spenden\nSind Sie sicher, dass Sie an diesen Tisch gehen möchten?",
                            replyMarkup: inlineKeyboard);
                        break;
                    }
                }
            }
            else
            {
                var sentMessage = await botClient.SendTextMessageAsync(
                    chatId,
                    $"Sind Sie sicher, dass Sie an diesem Tisch teilnehmen möchten?",
                    replyMarkup: inlineKeyboard);
            }
        }

        public static async void Warning(ITelegramBotClient botClient, long chatId, CallbackQuery callbackData,
            WebManager.RequestType requestType)
        {
            switch (requestType)
            {
                case WebManager.RequestType.LeaveTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("NEIN", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("JA", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Möchten Sie diesen Tisch wirklich verlassen?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.RemoveFromTable:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("NEIN", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("JA", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Möchten Sie diesen Benutzer wirklich aus der Tabelle entfernen?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
                case WebManager.RequestType.Confirm:
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("NEIN", "ChooseTable"),
                                InlineKeyboardButton.WithCallbackData("JA", "Confirm" + callbackData.Data)
                            }
                        });
                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Möchten Sie die Zahlung von diesem Nutzer wirklich bestätigen?",
                        replyMarkup: inlineKeyboard);
                    break;
                }
            }
        }

        public class TablesDE : Tables
        {
            public static async void Copper(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "copper";
                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.copper, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.copper, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Bronze(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "bronze";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.bronze, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Silver(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "silver";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.silver, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.silver, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Gold(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "gold";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.gold, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.gold, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Platinum(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "platinum";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.platinum, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                        replyMarkup: inlineKeyboard);
                }
            }

            public static async void Diamond(ITelegramBotClient botClient, long chatId, UserData userData)
            {
                userData.playerData.level_tableType = "diamond";

                var data = await WebManager.SendData(userData.playerData, WebManager.RequestType.RegisterIntoTable);
                if (!data.error.isError)
                {
                    switch (userData.playerData.tableRole)
                    {
                        case "giver":
                            Giver(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "manager":
                            Manager(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                        case "banker":
                            Banker(botClient, chatId, Table.TableType.diamond, userData);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR");
                    var inlineKeyboard = new InlineKeyboardMarkup(
                        new[]
                        {
                            new[]
                            {
                                InlineKeyboardButtonChooseTableDE
                            }
                        });

                    var sentMessage = await botClient.SendTextMessageAsync(
                        chatId,
                        $"Leider gibt es noch keine solchen Tabellen, bitte versuchen Sie es später erneut",
                        replyMarkup: inlineKeyboard);
                }
            }
        }
    }
}