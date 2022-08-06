using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using CashFlowTelegramBot.Skywards.Telegram;

var bot = new TelegramBotClient(TelegramToken.BotToken);

var me = await bot.GetMeAsync();
Console.Title = me.Username ?? "CashFlow";

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions() 
{
    AllowedUpdates = Array.Empty<UpdateType>(),
    ThrowPendingUpdates = true,
};



bot.StartReceiving(updateHandler: UpdateHandlers.HandleUpdateAsync,
    pollingErrorHandler: UpdateHandlers.PollingErrorHandler,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token);

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

